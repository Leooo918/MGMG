using UnityEngine;
using MGMG.StatSystem;
using System;
using DG.Tweening;

namespace MGMG.Entities.Component
{
    public class EntityMover : MonoBehaviour, IEntityComponent, IAfterInitable
    {
        private Vector2 _movement;
        private Vector2 _lastVelocity;
        private bool _lockVisualVelocity = false;

        public Vector2 Velocity => _rbCompo.linearVelocity;
        public Vector2 VisualVelocity => _lockVisualVelocity && _lastVelocity.sqrMagnitude > 0.0001f ? _lastVelocity : _rbCompo.linearVelocity;

        [SerializeField] private float _speed;
        private Rigidbody2D _rbCompo;
        private Entity _entity;
        private EntityRenderer _renderer;
        private EntityStat _statCompo;

        public void Initialize(Entity entity)
        {
            _entity = entity;
            _rbCompo = entity.GetComponent<Rigidbody2D>();
            _renderer = entity.GetCompo<EntityRenderer>();
            _statCompo = entity.GetCompo<EntityStat>();
        }

        public void AfterInit() { }

        public void AddForceToEntity(Vector2 force, ForceMode2D mode = ForceMode2D.Impulse)
        {
            _rbCompo.AddForce(force, mode);
        }

        public void StopImmediately(bool visualPreserve = true)
        {
            if (visualPreserve)
                _lastVelocity = _rbCompo.linearVelocity;

            _rbCompo.linearVelocity = Vector2.zero;
            _movement = Vector2.zero;
        }

        public void SetMovement(Vector2 movement) => _movement = movement;

        private void FixedUpdate()
        {
            MoveCharacter();
        }

        private void MoveCharacter()
        {
            if (_movement.sqrMagnitude > 0.0001f)
                _lastVelocity = _movement * _speed;

            _rbCompo.linearVelocity = _movement * _speed;
        }

        public void SetLockVisualVelocity(bool value) => _lockVisualVelocity = value;

        public void Dispose() { }
    }

}
