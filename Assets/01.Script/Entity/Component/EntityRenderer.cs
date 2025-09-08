using MGMG.Anim;
using System.Collections.Generic;
using UnityEngine;

namespace MGMG.Entities.Component
{
    public class EntityRenderer : MonoBehaviour, IEntityComponent
    {
        public float FacingDirection { get; private set; } = 1;
        public float Direction { get; private set; }

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

        #region FlipControl

        public void SetRotation(Vector3 dir, bool isImmadient = false)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

            if (isImmadient)
                transform.parent.rotation = targetRotation;
            else
                transform.parent.rotation = Quaternion.Slerp(transform.parent.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
            Direction = angle;
        }
        #endregion
    }
}
