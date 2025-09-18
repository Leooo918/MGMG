using MGMG.Core.ObjectPooling;
using System;
using UnityEngine;

namespace MGMG.Magic
{
    public class Satellite : MonoBehaviour, IPoolable
    {
        [SerializeField] private float _satelliteDistance = 5.0f;
        [SerializeField] private Transform _satelliteTrm;
        [SerializeField] private CrashDamageCaster _crashCheck;
        [SerializeField] private Collider2D _collider;
        private int _damage;

        public GameObject GameObject => gameObject;
        public Enum PoolEnum => _poolingType;
        [SerializeField] private SkillPoolingType _poolingType;

        public void SetDistance(float distance)
        {
            _satelliteDistance = distance;
            _collider.offset = new Vector2(0, distance);
            _satelliteTrm.transform.localPosition = Vector3.up * distance;
        }
        public void SetRotation(float rotation)
        {
            rotation = rotation % 360;
            transform.rotation = Quaternion.Euler(0, 0, rotation);
        }

        public void SetDamage(int damage)
        {
            _damage = damage;
            _crashCheck.SetDamage(_damage);
        }

        public void OnSpawned()
        {
            gameObject.SetActive(true);
        }

        public void OnDespawned()
        {
            gameObject.SetActive(false);
        }

        public void OnPop()
        {

        }

        public void OnPush()
        {

        }
    }
}
