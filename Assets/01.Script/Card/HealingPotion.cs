using MGMG.Entities;
using MGMG.Entities.Component;
using UnityEngine;

public class HealingPotion : Card
{
    [SerializeField] private float _healCondition = 0.5f;
    [SerializeField] private float _healRate = 0.33f;
    private EntityHealth _healthCompo;

    public override void Initialize(Entity owner, int index)
    {
        base.Initialize(owner, index);
        _healthCompo = _owner.GetCompo<EntityHealth>();
        _healthCompo.OnHealthChangedEvent += CheckHealthChange;
    }

    public override void Release()
    {
        _healthCompo.OnHealthChangedEvent -= CheckHealthChange;
    }

    private void CheckHealthChange(int prevHealth, int currentHealth, bool visible)
    {
        if((_healthCompo.MaxHealth * _healCondition) > currentHealth)
        {
            int healValue = Mathf.RoundToInt(_healthCompo.MaxHealth * _healRate);
            _healthCompo.ApplyDamage(null, -healValue, false, false);
            
            //사라지기
            _owner.GetCompo<PlayerCardController>().RemoveCarad(_index);
        }
    }

    public override Card GetInstance()
    {
        HealingPotion potion = new HealingPotion();
        potion._healCondition = _healCondition;
        potion._healRate = _healRate;

        return potion;
    }
}
