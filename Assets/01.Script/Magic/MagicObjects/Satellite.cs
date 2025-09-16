using MGMG.Core.ObjectPooling;
using System;
using UnityEngine;

namespace MGMG.Magic
{
    public class Satellite : MonoBehaviour, IPoolable
    {
        private int _damage;
        [SerializeField] private float _satelliteDistance = 5.0f;
        [SerializeField] private Transform _satelliteTrm;

        public GameObject GameObject => gameObject;
        public Enum PoolEnum => SkillPoolingType.Satellite;

        public void SetDistance(float distance)
        {
            _satelliteDistance = distance;
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
            //데미지 넣는 부분 막 뭐 해
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
            throw new NotImplementedException();
        }

        public void OnPush()
        {
            throw new NotImplementedException();
        }
    }
}
