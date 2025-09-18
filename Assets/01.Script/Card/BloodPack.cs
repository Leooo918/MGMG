using MGMG.Entities;
using MGMG.Entities.Component;
using MGMG.StatSystem;
using UnityEngine;

public class BloodPack : Card
{
    [SerializeField] private float _healthIncrease = 30f;
    [SerializeField] private float _healTick = 1f;
    [SerializeField] private float _healAmount = 0.001f;
    private EntityHealth _health;
    private float _prevHeal;


    public override void Initialize(Entity owner, int index)
    {
        base.Initialize(owner, index);
        _health = _owner.GetCompo<EntityHealth>();
        _health.MaxHealthElement.AddModify("BloodPack", _healthIncrease, EModifyMode.Percent, EModifyLayer.Default);
    }

    public override void OnUpdate()
    {
        if (_prevHeal + _healTick < Time.time)
        {
            _prevHeal = Time.time;
            _health.ApplyDamage(null, Mathf.RoundToInt(_health.MaxHealth * _healAmount), false, false);
        }
    }

    public override void Release()
    {
        _health.MaxHealthElement.RemoveModify("BloodPack", EModifyLayer.Default);
    }

    public override Card GetInstance()
    {
        BloodPack bloodPack = new BloodPack();
        bloodPack._healthIncrease = _healthIncrease;
        bloodPack._healTick = _healTick;
        bloodPack._healAmount = _healAmount;
        return bloodPack;
    }
}
