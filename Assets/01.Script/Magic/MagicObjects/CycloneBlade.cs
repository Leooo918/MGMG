using DG.Tweening;
using MGMG.Core.ObjectPooling;
using MGMG.Entities;
using System;
using UnityEngine;

public class CycloneBlade : MonoBehaviour, IPoolable
{
    [SerializeField] private CrashDamageCaster[] _blade;
    public GameObject GameObject => gameObject;
    public Enum PoolEnum => SkillPoolingType.CycloneBlade;

    private Sequence _seq;

    public void Initialize(Entity entity, int damage, int bladeCount)
    {
        foreach (var blade in _blade) blade.gameObject.SetActive(false);
        _seq = DOTween.Sequence();
        _seq.OnComplete(() => PoolManager.Instance.Push(this));

        int offset = 180 / bladeCount;
        for (int i = 0; i < bladeCount; i++)
        {
            _blade[i].SetOwner(entity);
            _blade[i].SetDamage(damage);
            _blade[i].gameObject.SetActive(true);
            _blade[i].transform.rotation = Quaternion.Euler(0, 0, offset * i);
            _seq.Join(_blade[i].transform.DORotate(new Vector3(0, 0, (360 + (offset * i))), 0.5f, RotateMode.FastBeyond360));
        }
    }
    public void OnPop()
    {

    }

    public void OnPush()
    {

    }
}
