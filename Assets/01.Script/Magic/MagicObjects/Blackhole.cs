using MGMG.Core.ObjectPooling;
using MGMG.Enemies;
using System;
using UnityEngine;

public class Blackhole : MonoBehaviour, IPoolable
{
    [SerializeField] private LayerMask _whatIsEnemy;
    private float _lifeTime = 0;
    private Collider2D[] _collider = new Collider2D[20];
    private float _gravity;
    public GameObject GameObject => gameObject;
    public Enum PoolEnum => SkillPoolingType.Blackhole;

    public void Initialize(float gravity, float scale, float lifeTime)
    {
        transform.localScale = Vector3.one * scale;
        _lifeTime = Time.time + lifeTime;  
        _gravity = gravity;
    }

    protected void Update()
    {
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(_whatIsEnemy);
        int count = Physics2D.OverlapCircle(transform.position, _gravity, contactFilter, _collider);

        for(int i = 0; i < count; i++)
        {
            if(_collider[i].TryGetComponent(out Enemy enemy))
            {
                // 뭔가 거리에 따라서 땡기기

            }
        }
        if(_lifeTime < Time.time)
        {
            PoolManager.Instance.Push(this);
        }
    }

    public void OnPop()
    {

    }

    public void OnPush()
    {

    }
}
