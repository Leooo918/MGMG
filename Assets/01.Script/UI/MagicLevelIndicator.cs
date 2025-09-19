using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicLevelIndicator : MonoBehaviour
{
    [SerializeField] private List<Image> _indicator;
    [SerializeField] private Color _originColor, _changedColor;

    private Tween _blinkTween;

    public void SetCurrentLevel(int level)
    {
        for(int i = 0; i < level; i++)
            _indicator[i].color = _changedColor;

        for(int i = level; i < _indicator.Count; i++)
            _indicator[i].color = _originColor;

        if (_blinkTween != null && _blinkTween.active) 
            _blinkTween.Kill();

        _blinkTween = _indicator[level].DOColor(_changedColor, 0.5f).SetUpdate(true).SetLoops(-1, LoopType.Yoyo);
    }
}
