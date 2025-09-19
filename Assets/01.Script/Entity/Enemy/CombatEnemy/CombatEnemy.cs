using MGMG.Combat.Caster;
using MGMG.Entities;
using MGMG.Entities.Component;
using MGMG.StatSystem;
using UnityEngine;

namespace MGMG.Enemies
{
    public class CombatEnemy : Enemy
    {
        [SerializeField] private CircleCaster2D _defaultAttackCaster;

        public StatElement delayTime { get; private set; }

        public override void Attack()
        {
            if (_defaultAttackCaster.CheckCollision(out RaycastHit2D[] hits, whatIsTarget))
            {
                if (hits[0].transform.TryGetComponent(out Player player))
                {
                    player.GetCompo<EntityHealth>().ApplyDamage(_statCompo, Mathf.RoundToInt(damageStat.Value),true,false);
                }
            }
        }
    }
}

