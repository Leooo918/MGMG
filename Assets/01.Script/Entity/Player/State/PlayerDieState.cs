using MGMG.Anim;
using MGMG.Entities;
using MGMG.Entities.Component;
using UnityEngine;

namespace MGMG.FSM
{
    public class PlayerDieState : EntityState
    {
        private Player _player;
        private EntityMover _mover;

        public PlayerDieState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
            _player = entity as Player;
            _mover = entity.GetCompo<EntityMover>();
        }

        public override void Enter()
        {
            base.Enter();

            _mover.StopImmediately();
        }

        public override void Update()
        {
            base.Update();
            GameManager.Instance.GameOver();
        }
    }

}
