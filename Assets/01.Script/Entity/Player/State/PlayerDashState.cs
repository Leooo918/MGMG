
using DG.Tweening;
using MGMG.Anim;
using MGMG.Entities;
using MGMG.Entities.Component;
using UnityEngine;

namespace MGMG.FSM
{
    public class PlayerDashState : EntityState
    {
        private Player _player;
        private EntityMover _mover;
        private EntityHealth _health;
        private readonly float _dashDistance = 4.5f, _dashTime = 0.25f;
        public PlayerDashState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
            _player = entity as Player;
            _mover = entity.GetCompo<EntityMover>();
            _health = entity.GetCompo<EntityHealth>();
        }

        public override void Enter()
        {
            base.Enter();
            Vector2 inputDir = _player.PlayerInput.InputDirection;

            _mover.CanManualMove = false;
            _mover.StopImmediately();
            Vector3 destination = _player.transform.position + (Vector3)inputDir * (_dashDistance - 0.5f);

            _player.transform.DOMove(destination, _dashTime).SetEase(Ease.OutQuad).OnComplete(EndDash);
            //CameraManager.Instance.ShakeCamera(5, 12, 0.2f);

            _health.SetInvincible(true);
            //AudioManager.Instance.PlaySound(EAudioName.Dash);
        }

        public override void Exit()
        {
            _mover.StopImmediately();
            _mover.CanManualMove = true; //�̵� ��װ� �߷� ����
            _health.SetInvincible(false);
            base.Exit();
        }


        private void EndDash()
        {
            _player.ChangeState(_player.playerFSM[FSMState.Idle]);
        }
    }

}
