using MGMG.Core.ObjectPooling;
using MGMG.Enemies;
using MGMG.Entities.Component;
using MGMG.StatSystem;
using System;
using UnityEngine;

public class StatChangeArea : MonoBehaviour, IPoolable
{
    [SerializeField] private ObjectPoolingType poolType;
    private StatElementSO _modifyStat;
    private float _modifyValue;

    public GameObject GameObject => gameObject;
    public Enum PoolEnum => poolType;

    public void Initialize(StatElementSO speedStat, float speedDownAmount, float range)
    {
        _modifyStat = speedStat;
        _modifyValue = speedDownAmount;
        transform.localScale = Vector3.one * range;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            EntityStat stat = enemy.GetCompo<EntityStat>();
            stat.StatDictionary[_modifyStat].AddModify("AreaDepolyment", -_modifyValue, EModifyMode.Percent, EModifyLayer.Default);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            EntityStat stat = enemy.GetCompo<EntityStat>();
            stat.StatDictionary[_modifyStat].RemoveModify("AreaDepolyment", EModifyLayer.Default);
        }
    }


    public void OnPop() { }

    public void OnPush() { }
}
