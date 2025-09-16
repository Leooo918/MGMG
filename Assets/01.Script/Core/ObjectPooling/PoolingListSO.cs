using MGMG.Util;
using System.Collections.Generic;
using UnityEngine;

namespace MGMG.Core.ObjectPooling
{
    [CreateAssetMenu(menuName = "SO/Pool/PoolList")]
    public class PoolListSO : ScriptableObject
    {
        [VisibleInspectorSO]
        [SerializeField] private List<PoolingItemSO> _list;

        public List<PoolingItemSO> GetList() => _list;
    }
}
