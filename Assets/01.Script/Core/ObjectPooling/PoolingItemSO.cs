using UnityEngine;

namespace MGMG.Core.ObjectPooling
{
    [CreateAssetMenu(menuName = "SO/Pool/PoolItem")]
    public class PoolingItemSO : ScriptableObject
    {
        public int poolCount;
        public MonoBehaviour prefab;
        public IPoolable PoolObj => prefab as IPoolable;
        public PoolingKey poolingKey;

        private void OnEnable()
        {
            if (poolingKey == null)
            {
                if (prefab != null)
                {
                    poolingKey = new PoolingKey(PoolObj.PoolEnum);
                }
            }
        }
    }
}