using UnityEngine;

namespace MGMG.Anim
{
    [CreateAssetMenu(fileName = "AnimParamSO", menuName = "SO/Animation/AnimParam")]
    public class AnimParamSO : ScriptableObject
    {
        public enum ParamType
        {
            Boolean, Float, Integer, Trigger
        }

        public string paramName;
        public ParamType paramType;
        public int hashValue;

        private void OnValidate()
        {
            hashValue = Animator.StringToHash(paramName);
        }
    }
}

