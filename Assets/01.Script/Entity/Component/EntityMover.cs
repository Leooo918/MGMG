using UnityEngine;
using MGMG.StatSystem;
using System;
using DG.Tweening;

namespace MGMG.Entities.Component
{
    public class EntityMover : MonoBehaviour, IEntityComponent, IAfterInitable
    {
        private Vector2 _movement;
        [Header("AnimParams")]

        public Vector2 Velocity => _rbCompo.linearVelocity;
        private Vector2 _lastVelocity;
        public Vector2 LastVelocity => _lastVelocity;
        public bool CanManualMove { get; set; } = true; 
        
        public float SpeedMultiplier { get; set; } = 1f;

        [SerializeField] private float _speed;
        private Rigidbody2D _rbCompo;
        private Entity _entity;
        private EntityRenderer _renderer;
        private EntityStat _statCompo;
        private StatElement _speedStat;

        public bool IsStopped { get; set; } = false;
        private Collider2D _collider;

        public void Initialize(Entity entity)
        {
            _entity = entity;
            _rbCompo = entity.GetComponent<Rigidbody2D>();
            _collider = entity.GetComponent<Collider2D>();
            _renderer = entity.GetCompo<EntityRenderer>();
            _statCompo = entity.GetCompo<EntityStat>();
        }

        public void AfterInit()
        {
        }

        public void AddForceToEntity(Vector2 force, ForceMode2D mode = ForceMode2D.Impulse)
        {
            _rbCompo.AddForce(force, mode);
        }

        public void StopImmediately()
        {
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
            if (CanManualMove)
            {
                _rbCompo.linearVelocity = _movement * _speed;
                if (_rbCompo.linearVelocity.sqrMagnitude > 0.001f)
                {
                    _lastVelocity = _rbCompo.linearVelocity;
                }
            }
        }

        public void Dispose()
        {

        }
    }

}
