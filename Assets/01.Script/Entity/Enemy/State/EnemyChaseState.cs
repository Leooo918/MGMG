using MGMG.Anim;
using MGMG.Enemies;
using MGMG.Entities;
using MGMG.Entities.Component;
using UnityEngine;

namespace MGMG.FSM
{
    public class EnemyChaseState : EntityState
    {
        private Enemy _enemy;
        private EntityMover _mover;
        private EntityStat _statCompo;
        private EnemyAnimationTrigger _animTrigger;

        public EnemyChaseState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
            _enemy = entity as Enemy;
            _mover = _enemy.GetCompo<EntityMover>();
            _statCompo = _enemy.GetCompo<EntityStat>();
            _animTrigger = _enemy.GetCompo<EnemyAnimationTrigger>();
        }

        public override void Enter()
        {
            base.Enter();
            _animTrigger.IsVelocityChange = true;
            _mover.SetLockVisualVelocity(false);
        }

        public override void Update()
        {
            base.Update();

            if (_enemy.AttackRangeInPlayer())
            {
                if (_enemy.lastAttackTime + _enemy.attackCooldownStat.Value < Time.time)
                {
                    _enemy.ChangeState(_enemy.enemyFSM[FSMState.Attack]);
                }
                else
                {
                    _enemy.ChangeState(_enemy.enemyFSM[FSMState.Idle]);
                }
            }
            else
            {
                Vector2 movement = _enemy.PlayerDirection();
                _mover.SetMovement(movement);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
