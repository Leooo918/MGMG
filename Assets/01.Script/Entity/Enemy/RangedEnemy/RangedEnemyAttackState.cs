using MGMG.Anim;
using MGMG.Enemies;
using MGMG.Entities;
using MGMG.Entities.Component;
using UnityEngine;

namespace MGMG.FSM
{
    public class RangedEnemyAttackState : EntityState
    {
        private RangedEnemy _enemy;
        private EntityMover _mover;
        public RangedEnemyAttackState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
            _enemy = entity as RangedEnemy;
            _mover = entity.GetCompo<EntityMover>();
        }


        public override void Enter()
        {
            base.Enter();
            _mover.StopImmediately();
        }

        public override void Update()
        {
            base.Update();

            if (_enemy.AttackRangeInPlayer())
            {
                if (_enemy.lastAttackTime + _enemy.attackCooldownStat.Value < Time.time)
                {
                    _enemy.Attack();
                }
                else
                {
                    _enemy.ChangeState(_enemy.enemyFSM[FSMState.Idle]);
                }
            }
            else
            {
                _enemy.ChangeState(_enemy.enemyFSM[FSMState.Chase]);
            }
        }
    }
}

