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

        private EntityMover _movement;
        private EntityStat _statCompo;


        public EnemyChaseState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
            _enemy = entity as Enemy;
        }

        public override void Enter()
        {
            base.Enter();
            _movement = _enemy.GetCompo<EntityMover>();
            _statCompo = _enemy.GetCompo<EntityStat>();
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
                    if (_enemy.enemyFSM[FSMState.Idle] == null)
                    {
                        _renderer.SetRotation(_enemy.PlayerDirection());
                        _movement.StopImmediately();
                    }
                    else
                    {
                        _enemy.ChangeState(_enemy.enemyFSM[FSMState.Idle]);
                    }
                }
            }
            else
            {
                Vector2 movement = _enemy.PlayerDirection();
                _movement.SetMovement(movement);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
