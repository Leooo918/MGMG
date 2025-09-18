using MGMG.Core.ObjectPooling;
using MGMG.Enemies;
using MGMG.Entities.Component;
using System;
using UnityEngine;

public class ElectricZone : MonoBehaviour, IPoolable
{
    [SerializeField] private float _originRange;
    private float _range;
    [SerializeField] private LayerMask _layerMask;
    private Collider2D[] _collider;
    private float _tickDelay = 0.1f;
    private float _prevTick;

    private float _lifeTime;

    public GameObject GameObject => gameObject;
    public Enum PoolEnum => SkillPoolingType.ElectricZone;


    public void Initialize(float range, float lifeTime)
    {
        _range = _originRange * range;
        transform.localScale = Vector3.one * range;
        _lifeTime = Time.time + lifeTime;
    }

    private void Update()
    {
        if(_lifeTime > Time.time)
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
                if(_collider[i].TryGetComponent(out Enemy enemy))
                {
                    enemy.GetCompo<EntityHealth>().ApplyDamage(enemy.GetCompo<EntityStat>(),200); 
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
