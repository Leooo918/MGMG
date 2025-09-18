using MGMG.Combat.Projectile;
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
            Debug.Log("Ranged Enemy Attack");
            lastAttackTime = Time.time;
            Projectile projectile = PoolManager.Instance.Pop(ObjectPoolingType.RangedEnemyProjectile) as Projectile;
            projectile.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
            projectile.transform.up = PlayerDirection();    
            projectile.Setting(this, whatIsTarget, _bulletSpeed, Mathf.CeilToInt(damageStat.Value));
        }
    }
}

