using DG.Tweening;
using MGMG.Anim;
using MGMG.Enemies;
using MGMG.Entities;
using MGMG.Entities.Component;
using UnityEngine;

namespace MGMG.FSM
{
    public class CombatEnemyAttackState : EntityState
    {
        private CombatEnemy _enemy;

        private EntityMover _mover;
        private EnemyAnimationTrigger _animTrigger;

        public CombatEnemyAttackState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
            _enemy = entity as CombatEnemy;
            _animTrigger = _enemy.GetCompo<EnemyAnimationTrigger>();
            _mover = _enemy.GetCompo<EntityMover>();
        }

        public override void Enter()
        {
            base.Enter();
            _animTrigger.IsVelocityChange = false;
            _enemy.lastAttackTime = Time.time;
            _enemy.Attack();
            _mover.StopImmediately();
            _animTrigger.OnAnimationEndTrigger += ChangeState;
        }

        public override void Exit()
        {
            base.Exit();
            _animTrigger.OnAnimationEndTrigger -= ChangeState;
        }
        private void ChangeState()
        {
            if (_enemy.AttackRangeInPlayer())
            {
                _enemy.ChangeState(_enemy.enemyFSM[FSMState.Idle]);
            }
            else
            {
                _enemy.ChangeState(_enemy.enemyFSM[FSMState.Chase]);
            }
        }
    }
}
