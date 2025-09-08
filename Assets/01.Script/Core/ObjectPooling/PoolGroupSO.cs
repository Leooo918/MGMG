using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Pooling/PoolingGroup")]
public class PoolGroupSO : ScriptableObject
{
    public List<PoolingStruct> poolList;
}

[Serializable]
public struct PoolingStruct
{
    public PoolSO poolSO;
    public int poolCount;
}