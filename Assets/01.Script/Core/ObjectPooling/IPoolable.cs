using System;
using UnityEngine;

namespace MGMG.Core.ObjectPooling
{
    public interface IPoolable
    {
        public GameObject GameObject { get; }
        public Enum PoolEnum { get; }
        public void OnPop();
        public void OnPush();
    }

}
