using DG.Tweening;
using MGMG.Core.ObjectPooling;
using MGMG.Enemies;
using MGMG.Entities;
using MGMG.Entities.Component;
using System;
using TMPro;
using UnityEngine;

public class Meteor : MonoBehaviour, IPoolable
{
    [SerializeField] private LayerMask _whatIsEnemy;
    [SerializeField] private Transform _meteorTrm;
    [SerializeField] private float _detectRange = 2.0f;

    private Collider2D[] _collider = new Collider2D[5];
    private Vector2 _offset = Vector2.one * 2;
    private Tween _fallTween;

    public GameObject GameObject => gameObject;
    public Enum PoolEnum => SkillPoolingType.Meteor;


    public void Fire(Entity owner, Vector2 position, float fallingDuration, int damage)
    {
        transform.position = position;
        _meteorTrm.position = position + _offset;

        _fallTween = _meteorTrm.DOMove(position, fallingDuration).SetEase(Ease.InQuad).OnComplete(() =>
        {
            //ÀÌÆåÆ® Àç»ý
            ContactFilter2D filter = new ContactFilter2D();
            filter.useLayerMask = true;
            filter.useTriggers = true;
            filter.SetLayerMask(_whatIsEnemy);

            int count = Physics2D.OverlapCircle(transform.position, _detectRange, filter, _collider);
            for(int i = 0; i < count; i++)
            {
                if (_collider[i].TryGetComponent(out Enemy enemy))
                {
                    enemy.GetCompo<EntityHealth>()
                    .ApplyDamage(owner.GetCompo<EntityStat>(), damage);
                }
            }
            
            PoolManager.Instance.Push(this);
        });
    }



    public void OnPop()
    {

    }
    public void OnPush()
    {

    }
}
