
using MGMG.Anim;
using MGMG.Entities;
using MGMG.Entities.Component;
using UnityEngine;

namespace MGMG.FSM
{
    public class PlayerIdleState : EntityState
    {
        private Player _player;
        private EntityMover _mover;
        public PlayerIdleState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
            _player = entity as Player;
            _mover = _player.GetCompo<EntityMover>();
        }

        public override void Enter()
        {
            base.Enter();
            _mover.StopImmediately();
        }

        public override void Update()
        {
            base.Update();
            float input = _player.PlayerInput.InputDirection.magnitude;
            if (Mathf.Abs(input) > 0.05f)
            {
                _player.ChangeState(_player.playerFSM[FSMState.Move]);
            }

        }
    }
}
