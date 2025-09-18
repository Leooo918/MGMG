using DG.Tweening;
using MGMG.Core.ObjectPooling;
using System;
using TMPro;
using UnityEngine;

public class Meteor : MonoBehaviour, IPoolable
{
    [SerializeField] private Transform _meteorTrm;

    private Vector2 _offset = Vector2.one * 2;
    private Tween _fallTween;

    public GameObject GameObject => gameObject;
    public Enum PoolEnum => SkillPoolingType.Meteor;


    public void Fire(Vector2 position, float fallingDuration)
    {
        transform.position = position;
        _meteorTrm.position = position + _offset;

        _fallTween = _meteorTrm.DOMove(position, fallingDuration).SetEase(Ease.InQuad).OnComplete(() =>
        {
            //일단 바로 푸쉬하고
            //이펙트 재생
            //데미지 캐스트
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
