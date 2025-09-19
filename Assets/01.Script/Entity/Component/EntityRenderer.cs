using MGMG.Anim;
using System.Collections.Generic;
using UnityEngine;

namespace MGMG.Entities.Component
{
    public class EntityRenderer : MonoBehaviour, IEntityComponent
    {

        private Entity _entity;
        public Animator Animator { get; private set; }

        
        public void Initialize(Entity entity)
        {
            _entity = entity;
            Animator = GetComponent<Animator>();
        }


                                                                                                                                                                                                                                                                                                                                                                                                                                   
        public void SetParam(AnimParamSO param, bool value) => Animator.SetBool(param.hashValue, value);
        public void SetParam(AnimParamSO param, float value) => Animator.SetFloat(param.hashValue, value);
        public void SetParam(AnimParamSO param, int value) => Animator.SetInteger(param.hashValue, value);
        public void SetParam(AnimParamSO param) => Animator.SetTrigger(param.hashValue);

    }
}
