using MGMG.Entities;
using MGMG.Entities.Component;
using MGMG.StatSystem;
using UnityEngine;

public class Hunter : Card
{
    [SerializeField] private StatElementSO _attackStat;
    [SerializeField] private int _attackUpStackCount = 10;
    [SerializeField] private int _maxStatUpCount = 10;
    [SerializeField] private int _statUpAmount = 3;

    private EntityHealth _health;
    private EntityStat _stat;
    private int _stack = 0;

    public override void Initialize(Entity owner, int index)
    {
        base.Initialize(owner, index);
        _health = _owner.GetCompo<EntityHealth>();
        _stat = _owner.GetCompo<EntityStat>();
    }

    public void AddStack()
    {
        _stack++;
        if (_stack % _attackUpStackCount == 0)
        {
            //_health.
            int statUpValue = (_stack / _attackUpStackCount) * _statUpAmount;
            _stat.StatDictionary[_attackStat].AddModify("Hunter", statUpValue, EModifyMode.Percent, EModifyLayer.Default, false);
        }
    }

    public override Card GetInstance()
    {
        Hunter hunter = new Hunter();
        return hunter;
    }

    public override void Release()
    {

    }
}
