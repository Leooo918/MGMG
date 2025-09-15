using MGMG.Anim;
using MGMG.Input;
using System;
using UnityEngine;

namespace MGMG.Entities.Component
{
    public class PlayerAnimationTrigger : EntityAnimationTrigger, IAfterInitable
    {
        private Player _player;
        private EntityRenderer _renderer;
        private PlayerInputSO _playerInput;
        [SerializeField] private AnimParamSO _xAnimParam, _yAnimParam;

        public override void Initialize(Entity entity)
        {
            base.Initialize(entity);
            _player = entity as Player;
            _renderer = entity.GetCompo<EntityRenderer>();
            _playerInput = _player.PlayerInput;
        }

        public void AfterInit()
        {
            _playerInput.MoveEvent += HandleMove;
        }

        private void HandleMove()
        {
            _renderer.SetParam(_xAnimParam, _playerInput.InputDirection.normalized.x);
            _renderer.SetParam(_yAnimParam, _playerInput.InputDirection.normalized.y);
        }

        public void Dispose()
        {
            _player.PlayerInput.MoveEvent += HandleMove;

        }

    }
}