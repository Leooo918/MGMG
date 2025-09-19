using MGMG.Core.ObjectPooling;
using MGMG.Enemies;
using MGMG.Entities;
using MGMG.Entities.Component;
using System;
using UnityEngine;

public class IcicleBullet : MonoBehaviour, IPoolable
{
    [SerializeField] private LayerMask _whatIsEnemy;
    private Collider2D[] _collider = new Collider2D[5];

    private Entity _owner;
    private int _damage;
    private float _speed;
    private float _explosionRange;
    private float _lifeTime = 5f;
    private float _deadTime;

    public GameObject GameObject => gameObject;
    public Enum PoolEnum => SkillPoolingType.IcicleBullet;
    
    private void Update()
    {
        if(_deadTime < Time.time)
        {
            PoolManager.Instance.Push(this);
            return;
        }
        transform.position += transform.up * _speed * Time.deltaTime;
    }

    public void Initialize(Entity entity, float speed, int damage, float explosionRange)
    {
        _owner = entity;
        _speed = speed;
        _damage = damage;
        _explosionRange = explosionRange;
        _deadTime = Time.time + _lifeTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Enemy enemy))
        {
            //데미지 넣고
            ContactFilter2D filter = new ContactFilter2D();
            filter.useLayerMask = true;
            filter.useTriggers = true;
            filter.SetLayerMask(_whatIsEnemy);
            int count = Physics2D.OverlapCircle(transform.position, _explosionRange, filter, _collider);
            enemy.GetCompo<EntityHealth>().ApplyDamage(_owner.GetCompo<EntityStat>(), _damage);

            for (int i = 0; i < count; i++)
            {
                if (_collider[i].TryGetComponent(out Enemy exEnemy))
                {
                    //데미지 넣기
                    exEnemy.GetCompo<EntityHealth>().ApplyDamage(_owner.GetCompo<EntityStat>(), _damage);
                }
            }
            PoolManager.Instance.Push(this);
        }
    }

    public void OnPop() { }
    public void OnPush() { }
}
