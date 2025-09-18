using MGMG.Core.ObjectPooling;
using MGMG.Entities;
using MGMG.StatSystem;
using UnityEngine;

public class AreaDepolyment : Card
{
    [SerializeField] private StatElementSO _speedStat;
    [SerializeField] private float _speedDownAmount = 30f;
    [SerializeField] private float _range = 3;
    private StatChangeArea _area;

    public override void Initialize(Entity owner, int index)
    {
        base.Initialize(owner, index);
        //���⼭ ���� ȿ���� �������ϸ� Ǯ������ ��������
        _area = (StatChangeArea)PoolManager.Instance.Pop(ObjectPoolingType.AreaDepolyment);
        _area.Initialize(_speedStat, _speedDownAmount, _range);
        _area.transform.SetParent(_owner.transform);
        _area.transform.transform.localPosition = Vector3.zero;
    }

    public override void Release()
    {
        //�׸��� ���⼭ ��������
        PoolManager.Instance.Push(_area,true);
    }

    public override Card GetInstance()
    {
        AreaDepolyment areaDepolyment = new AreaDepolyment();
        return areaDepolyment;
    }
}
