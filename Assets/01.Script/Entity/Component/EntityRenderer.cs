using MGMG.Anim;
using System.Collections.Generic;
using UnityEngine;

namespace MGMG.Entities.Component
{
    public class EntityRenderer : MonoBehaviour, IEntityComponent
    {

        private Entity _entity;
        public Animator Animator { get; private set; }
        public List<SpriteRenderer> SpriteRendererList { get; private set; } = new List<SpriteRenderer>();
        private Material _material;

        
        [SerializeField] private float _rotationSpeed = 5;

        public void Initialize(Entity entity)
        {
            _entity = entity;
            Animator = GetComponent<Animator>();
            GetComponentsInChildren(SpriteRendererList);
        }

        private void Start()
        {
            _material = SpriteRendererList[0].material;
        }

                                                                                                                                                                                                                                                                                                                                                                                                                                   
        public void SetParam(AnimParamSO param, bool value) => Animator.SetBool(param.hashValue, value);
        public void SetParam(AnimParamSO param, float value) => Animator.SetFloat(param.hashValue, value);
        public void SetParam(AnimParamSO param, int value) => Animator.SetInteger(param.hashValue, value);
        public void SetParam(AnimParamSO param) => Animator.SetTrigger(param.hashValue);

    }
}
