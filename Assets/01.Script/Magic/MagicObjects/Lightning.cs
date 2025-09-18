using MGMG.Core.ObjectPooling;
using System;
using UnityEngine;

public class Lightning : MonoBehaviour, IPoolable
{
    public GameObject GameObject => gameObject;
    public Enum PoolEnum => SkillPoolingType.Lightning;

    public void Fire(Vector2 position)
    {
        transform.position = position;

        //데미지 캐스트
        //뭔가 이펙트 재생
        //PoolManager.Instance.Pop(EffectPoolingType.LightingEffect);
        PoolManager.Instance.Push(this);
    }

    public void OnPop()
    {

    }

    public void OnPush()
    {

    }
}
