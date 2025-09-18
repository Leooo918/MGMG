using MGMG.Core.ObjectPooling;
using MGMG.Enemies;
using MGMG.Entities.Component;
using System;
using UnityEngine;

public class IceLink : MonoBehaviour, IPoolable
{
    private bool _isCheckLifeTime = false;
    private float _decreaseValue = 0;
    private float _lifeTime;
    public GameObject GameObject => gameObject;
    public Enum PoolEnum => SkillPoolingType.IceLink;

    public void Initialize(float range, float decreaseValue, float lifeTime)
    {
        _isCheckLifeTime = true;
        _decreaseValue = decreaseValue;
        _lifeTime = Time.time + lifeTime;
        transform.localScale = Vector3.one * range;
    }

    private void Update()
    {
        if (_isCheckLifeTime && Time.time > _lifeTime)
        {
            _isCheckLifeTime = false;
            PoolManager.Instance.Push(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //적 들어오면 이동 속도 감소
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            EntityStat stat = enemy.GetCompo<EntityStat>();
            //stat._overrideStatElementList
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //적 나가면 이동 속도 다시 증가
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            EntityStat stat = enemy.GetCompo<EntityStat>();
            //stat._overrideStatElementList
        }
    }

    public void OnPop()
    {

    }

    public void OnPush()
    {

    }
}
