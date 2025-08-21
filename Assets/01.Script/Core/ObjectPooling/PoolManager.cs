using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoSingleton<PoolManager>
{
    [SerializeField] private PoolGroupSO _poolGroup;
    [SerializeField] private Transform _poolingObjectParent;

    private Dictionary<string, Stack<IPoolable>> _poolingDict;
    private Dictionary<string, PoolSO> _poolSODict;


    protected override void Awake()
    {
        base.Awake();
        InitializePool();
    }

    private void InitializePool()
    {
        _poolSODict = new Dictionary<string, PoolSO>();
        _poolingDict = new Dictionary<string, Stack<IPoolable>>();
        _poolGroup.poolList.ForEach(poolStruct =>
        {
            Stack<IPoolable> poolStack = new Stack<IPoolable>();
            for (int i = 0; i < poolStruct.poolCount; i++)
            {
                GameObject poolObject = Instantiate(poolStruct.poolSO.poolPrefab, _poolingObjectParent);
                IPoolable poolable = poolObject.GetComponent<IPoolable>();
                poolable.OnDespawned();

                poolStack.Push(poolable);
            }
            _poolingDict.Add(poolStruct.poolSO.poolName, poolStack);
            _poolSODict.Add(poolStruct.poolSO.poolName, poolStruct.poolSO);
        });
    }

    public T Pop<T>(string poolName) where T : MonoBehaviour, IPoolable
    {
        if (_poolingDict.TryGetValue(poolName, out Stack<IPoolable> poolStack))
        {
            IPoolable poolable;
            if (poolStack.TryPop(out poolable) == false)
            {
                GameObject poolObject = Instantiate(_poolSODict[poolName].poolPrefab, _poolingObjectParent);
                poolable = poolObject.GetComponent<IPoolable>();
            }

            poolable.OnSpawned();
            return poolable as T;
        }
        else
        {
            Debug.LogError($"PoolObject named {poolName} is not exsist");
            return null;
        }
    }

    public T Pop<T>(string poolName, Vector2 position) where T : MonoBehaviour, IPoolable
    {
        T poolable = Pop<T>(poolName);
        poolable.transform.position = position;
        return poolable;
    }

    public T Pop<T>(string poolName, Vector2 position, Quaternion rotation) where T : MonoBehaviour, IPoolable
    {
        T poolable = Pop<T>(poolName);
        poolable.transform.position = position;
        poolable.transform.rotation = rotation;
        return poolable;
    }

    public void Push(string poolName, IPoolable pool)
    {
        if (_poolingDict.TryGetValue(poolName, out Stack<IPoolable> poolStack))
        {
            pool.OnDespawned();
            poolStack.Push(pool);
        }
        else
        {
            Debug.LogError($"PoolObject named {poolName} is not exsist");
        }
    }

    public void Push<T>(string poolName, T pool) where T : MonoBehaviour, IPoolable
    {
        if (_poolingDict.TryGetValue(poolName, out Stack<IPoolable> poolStack))
        {
            pool.OnDespawned();
            poolStack.Push(pool);
        }
        else
        {
            Debug.LogError($"PoolObject named {poolName} is not exsist");
        }
    }
}
