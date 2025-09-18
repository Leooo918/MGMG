using MGMG.Core.ObjectPooling;
using MGMG.Enemies;
using MGMG.Entities;
using MGMG.Entities.Component;
using System;
using UnityEngine;

public class ElectricZone : MonoBehaviour, IPoolable
{
    [SerializeField] private float _originRange;
    [SerializeField] private LayerMask _layerMask;

    private int _damage;
    private float _range;
    private Entity _owner;
    private float _lifeTime;

    private float _tickDelay = 0.1f;
    private float _prevTick;

    private Collider2D[] _collider;

    public GameObject GameObject => gameObject;
    public Enum PoolEnum => SkillPoolingType.ElectricZone;


    public void Initialize(Entity owner, int damage, float range, float lifeTime)
    {
        _owner = owner;
        _damage = damage;
        _range = _originRange * range;
        transform.localScale = Vector3.one * range;
        _lifeTime = Time.time + lifeTime;
    }

    private void Update()
    {
        if (_lifeTime > Time.time)
        {
            PoolManager.Instance.Push(this);
            return;
        }

        if (_prevTick + _tickDelay > Time.time)
        {
            _prevTick = Time.time;

            ContactFilter2D contactFilter = new ContactFilter2D();
            contactFilter.SetLayerMask(_layerMask);

            int count = Physics2D.OverlapCircle(transform.position, _range, contactFilter, _collider);
            for (int i = 0; i < count; i++)
            {
                if (_collider[i].TryGetComponent(out Enemy enemy))
                {
                    enemy.GetCompo<EntityHealth>().ApplyDamage(_owner.GetCompo<EntityStat>(), _damage);
                }
            }
        }
    }

    public void OnPop()
    {

    }
    public void OnPush()
    {
        
    }
}
