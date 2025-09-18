using MGMG.Anim;
using MGMG.Enemies;
using MGMG.Entities;
using MGMG.Entities.Component;
using UnityEngine;

namespace MGMG.FSM
{
    public class EnemyIdleState : EntityState
    {
        private Enemy _enemy;
        private EntityMover _mover;
        private EnemyAnimationTrigger _animTrigger;

        public EnemyIdleState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
            _enemy = entity as Enemy;
            _animTrigger = _enemy.GetCompo<EnemyAnimationTrigger>();
            _mover = _enemy.GetCompo<EntityMover>();
        }

        public override void Enter()
        {
            base.Enter();
            _animTrigger.IsVelocityChange = false;
            _mover = _enemy.GetCompo<EntityMover>();
        }

        public override void Update()
        {
            base.Update();

            if (_enemy.AttackRangeInPlayer())
            {
                if (_enemy.lastAttackTime + _enemy.attackCooldownStat.Value < Time.time)
                    _enemy.ChangeState(_enemy.enemyFSM[FSMState.Attack]);
            }
            else
            {
                _enemy.ChangeState(_enemy.enemyFSM[FSMState.Chase]);
            }
        }
    }
}

