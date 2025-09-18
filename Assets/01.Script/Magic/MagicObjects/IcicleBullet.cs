using MGMG.Core.ObjectPooling;
using MGMG.Enemies;
using System;
using UnityEngine;

public class IcicleBullet : MonoBehaviour, IPoolable
{
    [SerializeField] private LayerMask _whatIsEnemy;
    private Collider2D[] _collider = new Collider2D[5];

    private float _damage;
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

    public void Initialize(float speed, float damage, float explosionRange)
    {
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
            filter.SetLayerMask(_whatIsEnemy);
            int count = Physics2D.OverlapCircle(transform.position, _explosionRange, filter, _collider);

            for (int i = 0; i < count; i++)
            {
                if (_collider[i].TryGetComponent(out Enemy exEnemy))
                {
                    //데미지 넣기

                }
            }
            PoolManager.Instance.Push(this);
        }
    }

    public void OnPop() { }
    public void OnPush() { }
}
