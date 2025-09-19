using MGMG.Combat.Caster;
using MGMG.Core.ObjectPooling;
using MGMG.Entities;
using MGMG.Entities.Component;
using System;
using UnityEngine;

namespace MGMG.Combat.Projectile
{
    public class Projectile : MonoBehaviour, IPoolable
    {
        private Transform _visualTrm;
        private Caster2D _caster;
        private LayerMask _whatIsTarget;
        private float _speed;
        private int _damage;
        private bool _isEnable;


        [SerializeField] private float _rotate = 10f;
        [SerializeField] private float _lifetime = 3f;
        private float _popTime;

        [SerializeField] private ObjectPoolingType _poolingType;
        public GameObject GameObject => gameObject;

        public Enum PoolEnum => _poolingType;

        private Entity _entity;

        private Vector3 _defaultScale, _casterScale;

        private void Awake()
        {
            _caster = GetComponent<Caster2D>();
            _visualTrm = transform.GetChild(0);
            _defaultScale = transform.localScale;
            _casterScale = _caster.offset;
        }

        public void Setting(Entity entity, LayerMask whatIsTarget, float speed, int damage)
        {
            _entity = entity;
            _whatIsTarget = whatIsTarget;
            _speed = speed;
            _damage = damage;
        }

        public void SetScale(float multiple)
        {
            transform.localScale *= multiple;
            _caster.offset *= multiple;
        }

        private void FixedUpdate()
        {
            if (_lifetime + _popTime < Time.time)
            {
                _isEnable = false;
                Die();
            }

            if (_isEnable == false) return;
            Vector3 movement = transform.up * (Time.fixedDeltaTime * _speed);
            if (_caster.CheckCollision(out RaycastHit2D[] hits, _whatIsTarget, movement))
            {
                if (hits[0].transform.TryGetComponent(out Entity entity))
                {
                    entity.GetCompo<EntityHealth>().ApplyDamage(_entity.GetCompo<EntityStat>(), _damage,true,false);
                    Die();
                }
                _isEnable = false;
                transform.position += transform.up * hits[0].distance;
            }
            else
            {
                transform.position += movement;
            }
        }

        private void Die()
        {
            PoolManager.Instance.Push(this);
        }

        public void OnPop()
        {
            _isEnable = true;
            _popTime = Time.time;
            transform.localScale = _defaultScale;
            _caster.offset = _casterScale;
        }

        public void OnPush()
        {

        }
    }

}
