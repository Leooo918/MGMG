using MGMG.Anim;
using MGMG.Enemies;
using MGMG.Input;
using UnityEngine;

namespace MGMG.Entities.Component
{
    public class EnemyAnimationTrigger : EntityAnimationTrigger
    {
        private Enemy _enemy;
        private EntityMover _mover;
        private EntityRenderer _renderer;
        [SerializeField] private AnimParamSO _xAnimParam, _yAnimParam;

        public override void Initialize(Entity entity)
        {
            base.Initialize(entity);
            _enemy = entity as Enemy;
            _mover = _enemy.GetCompo<EntityMover>();
            _renderer = entity.GetCompo<EntityRenderer>();
        }

        private void Update()
        {
            Vector2 enemyVelocity = _mover.Velocity.sqrMagnitude > 0.001f ? _mover.Velocity.normalized : _mover.LastVelocity.normalized;

            _renderer.SetParam(_xAnimParam, enemyVelocity.x);
            _renderer.SetParam(_yAnimParam, enemyVelocity.y);
        }
    }
}
