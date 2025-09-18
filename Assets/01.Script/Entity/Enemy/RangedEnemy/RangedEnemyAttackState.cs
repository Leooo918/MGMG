using DG.Tweening;
using MGMG.Anim;
using MGMG.Enemies;
using MGMG.Entities;
using MGMG.Entities.Component;
using System;
using UnityEngine;

namespace MGMG.FSM
{
    public class RangedEnemyAttackState : EntityState
    {
        private RangedEnemy _enemy;
        private EntityMover _mover;
        private EnemyAnimationTrigger _animTrigger;
        public RangedEnemyAttackState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
            _enemy = entity as RangedEnemy;
            _mover = entity.GetCompo<EntityMover>();
            _animTrigger = _enemy.GetCompo<EnemyAnimationTrigger>();
        }


        public override void Enter()
        {
            base.Enter();
            _animTrigger.IsVelocityChange = false;
            _mover.StopImmediately();
            _animTrigger.OnAnimationEndTrigger += ChangeState;

            DOVirtual.DelayedCall(0.6f, () =>
            {
                _enemy.Attack();
            });
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

