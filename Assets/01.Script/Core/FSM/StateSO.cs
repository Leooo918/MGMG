using MGMG.Anim;
using UnityEngine;

namespace MGMG.FSM
{
    [CreateAssetMenu(menuName = "SO/FSM/StateSO")]
    public class StateSO : ScriptableObject
    {
        public FSMState stateName;
        public string className;
        public AnimParamSO animParam;

    }
}
