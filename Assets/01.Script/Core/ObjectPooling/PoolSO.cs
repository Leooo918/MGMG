using System;
using System.Diagnostics.Tracing;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Pooling/PoolObject")]
public class PoolSO : ScriptableObject
{
    public string poolName;

    public GameObject poolPrefab;

    public void OnValidate()
    {
        if (poolPrefab == null)
            return;

        if(poolPrefab.TryGetComponent(out IPoolable pool) == false)
        {
            Debug.LogError($"The prefab {poolPrefab.name} does not implement the IPoolable interface. Please ensure it implements IPoolable<{poolPrefab.GetType().Name}>.");
            poolPrefab = null;
        }
    }
}
