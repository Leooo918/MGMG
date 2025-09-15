using MGMG.Anim;
using System;
using UnityEngine;

namespace MGMG.Entities.Component
{
    public class PlayerAnimationTrigger : EntityAnimationTrigger, IAfterInitable
    {
        private Player _player;
        private EntityRenderer _renderer;
        [SerializeField] private AnimParamSO _xAnimParam, _yAnimParam;

        public override void Initialize(Entity entity)
        {
            base.Initialize(entity);
            _player = entity as Player;
            _renderer = entity.GetCompo<EntityRenderer>();
        }

        public void AfterInit()
        {
            _player.PlayerInput.MoveEvent += HandleMove;
        }

        private void HandleMove(Vector2 vector)
        {
            _renderer.SetParam(_xAnimParam, vector.x);
            _renderer.SetParam(_yAnimParam, vector.y);
        }

        public void Dispose()
        {
            _player.PlayerInput.MoveEvent += HandleMove;

        }

    }
}