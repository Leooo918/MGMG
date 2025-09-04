using MGMG.StatSystem;
using System.Collections.Generic;
using UnityEngine;

namespace MGMG.Entity.Component
{
    public class EntityStat : MonoBehaviour,IEntityComponent
    {
        public List<StatElement> _overrideStatElementList;
        public StatBaseSO _baseStatSO;
        public StatDictionary StatDictionary { get; private set; }

        public void Initialize(Entity entity)
        {
            StatDictionary = new StatDictionary(_overrideStatElementList, _baseStatSO);
        }
    }
}

