using MGMG.Core.ObjectPooling;
using System;
using UnityEngine;

public class SkillEffect : MonoBehaviour, IPoolable
{
    [SerializeField] private EffectPoolingType _poolingType;
    [SerializeField] private ParticleSystem _particleSystem;

    public GameObject GameObject => gameObject;
    public Enum PoolEnum => _poolingType;

    public void PlayEffect()
    {
        _particleSystem.Play();
    }

    private void OnParticleSystemStopped()
    {
        PoolManager.Instance.Push(this);
    }

    public void OnPop() { }
    public void OnPush() { }
}
