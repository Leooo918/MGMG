using MGMG.Core.ObjectPooling;
using MGMG.Enemies;
using MGMG.Entities;
using MGMG.Entities.Component;
using System;
using UnityEngine;

public class Blackhole : MonoBehaviour, IPoolable
{
    [SerializeField] private LayerMask _whatIsEnemy;
    private float _lifeTime = 0;
    private Collider2D[] _collider = new Collider2D[20];
    private float _gravity;
    private int _damage;
    private Entity _entity;

    private float _tickDelay = 0.5f;
    private float _prevTick = 0f;

    public GameObject GameObject => gameObject;
    public Enum PoolEnum => SkillPoolingType.Blackhole;

    public void Initialize(Entity entity, int damage, float gravity, float scale, float lifeTime)
    {
        _entity = entity;
        transform.localScale = Vector3.one * scale;
        _lifeTime = Time.time + lifeTime;  
        _gravity = gravity;
        _damage = damage;
    }

    protected void Update()
    {
        if(_prevTick + _tickDelay < Time.time)
        {
            _prevTick = Time.time;
            ContactFilter2D contactFilter = new ContactFilter2D();
            contactFilter.SetLayerMask(_whatIsEnemy);
            int count = Physics2D.OverlapCircle(transform.position, _gravity, contactFilter, _collider);

            for (int i = 0; i < count; i++)
            {
                if (_collider[i].TryGetComponent(out Enemy enemy))
                {
                    // 뭔가 거리에 따라서 땡기기
                    Vector2 direction = (enemy.transform.position - transform.position).normalized;
                    enemy.GetCompo<EntityMover>().AddForceToEntity(direction);
                    enemy.GetCompo<EntityHealth>().ApplyDamage(_entity.GetCompo<EntityStat>(), _damage);
                }
            }
            if (_lifeTime < Time.time)
            {
                PoolManager.Instance.Push(this);
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
