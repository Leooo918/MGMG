using MGMG.Anim;
using MGMG.Core.ObjectPooling;
using MGMG.Enemies;
using MGMG.Entities;
using MGMG.Entities.Component;
using UnityEngine;

namespace MGMG.FSM
{
    public class EnemyDieState : EntityState
    {
        private float _dieTime;
        private float _dissolveTime = 1.5f;

        private Enemy _enemy;

        public EnemyDieState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
            _enemy = entity as Enemy;
        }

        public override void Enter()
        {
            base.Enter();
            _dieTime = Time.time;
            //_entity.GetCompo<EntityRenderer>().Dissolve(_dissolveTime);
            _enemy.GetCompo<EntityMover>()?.StopImmediately();
            PoolManager.Instance.Push(_enemy);
        }
    }

}
