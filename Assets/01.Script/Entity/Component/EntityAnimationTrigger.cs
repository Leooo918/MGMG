using System;
using UnityEngine;
namespace MGMG.Entities.Component
{
    public class EntityAnimationTrigger : MonoBehaviour, IEntityComponent
    {
        public event Action OnAnimationEndTrigger;
        public event Action<bool> OnAttackTrigger;

        protected Entity _entity;

        public virtual void Initialize(Entity entity)
        {
            _entity = entity;
        }
        protected virtual void AnimationEnd() => OnAnimationEndTrigger?.Invoke();

    }
}
