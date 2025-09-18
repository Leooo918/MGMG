using MGMG.Core.ObjectPooling;
using MGMG.Enemies;
using MGMG.Magic;
using System;
using UnityEngine;

public class Lava : MonoBehaviour, IPoolable
{
    [SerializeField] private LayerMask _whatIsEnemy;
    private Collider2D[] _collider = new Collider2D[5];

    private float _range;
    private float _damage;
    private float _lifeTime;
    private float _tickDelay = 0.1f;
    private float _prevTick;

    public GameObject GameObject => gameObject;
    public Enum PoolEnum => SkillPoolingType.Lava;

    public void Initialize(float range, float lifeTime, float tickDamage)
    {
        _range = range;
        transform.localScale = Vector3.one * range;
        _lifeTime = Time.time + lifeTime;
        _damage = tickDamage;
    }

    private void Update()
    {
        if(_lifeTime < Time.time)
        {
            PoolManager.Instance.Push(this);
            return;
        }
        if(_prevTick + _tickDelay > Time.time)
        {
            _prevTick = Time.time;

            ContactFilter2D filter = new ContactFilter2D();
            filter.SetLayerMask(_whatIsEnemy);
            int count = Physics2D.OverlapCircle(transform.position, _range, filter, _collider);

            for (int i = 0; i < count; i++)
            {
                if (_collider[i].TryGetComponent(out Enemy enemy))
                {
                    //데미지 넣기
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
