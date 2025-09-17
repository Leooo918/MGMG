using MGMG.Core.ObjectPooling;
using UnityEngine;

namespace MGMG.Enemies
{
    public class RangedEnemy : Enemy
    {
        [Header("RangedEnemy")]
        [SerializeField] private float _bulletSpeed;

        protected override void Awake()
        {
            base.Awake();
        }

        public override void Attack()
        {
            lastAttackTime = Time.time;
        }
    }
}

