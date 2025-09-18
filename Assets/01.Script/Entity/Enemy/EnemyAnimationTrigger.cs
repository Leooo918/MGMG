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

        public bool IsVelocityChange { get; set; }
        public override void Initialize(Entity entity)
        {
            base.Initialize(entity);
            _enemy = entity as Enemy;
            _mover = _enemy.GetCompo<EntityMover>();
            _renderer = entity.GetCompo<EntityRenderer>();
        }

        private void Update()
        {
            if(!IsVelocityChange) return;
            Vector2 playerdir = _enemy.PlayerDirection().normalized;
            _renderer.SetParam(_xAnimParam, playerdir.x);
            _renderer.SetParam(_yAnimParam, playerdir.y);
        }
    }
}
