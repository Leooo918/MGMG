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

        //������ ĳ��Ʈ
        //���� ����Ʈ ���
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
