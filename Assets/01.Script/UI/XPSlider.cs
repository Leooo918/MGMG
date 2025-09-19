using DG.Tweening;
using MGMG.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class XPSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _levelText;

    private void Start()
    {
        float maxValue = PlayerManager.Instance.MaxExp;
        _slider.maxValue = maxValue;

        PlayerManager.Instance.OnAddExp += XpApply;
    }

    public void XpApply(int value)
    {
        float prevValue = _slider.value;
        float maxValue = PlayerManager.Instance.MaxExp;
        _slider.maxValue = maxValue;

        float targetValue = PlayerManager.Instance.CurrentExp;

        if (targetValue >= maxValue)
        {
            float remainingXp = targetValue - maxValue;
            _slider.maxValue *= 1.1f;
            _levelText.SetText($"Lv. {PlayerManager.Instance.CurrentPlayerLevel}");

            DOTween.To(() => _slider.value, x => _slider.value = x, maxValue, 0.3f)
                .SetEase(Ease.OutCirc)
                .OnComplete(() =>
                {
                    DOTween.To(() => _slider.value, x => _slider.value = x, remainingXp, 0.3f)
                    .SetEase(Ease.OutCirc);
                });
        }
        else
        {
            DOTween.To(() => _slider.value, x => _slider.value = x, targetValue, 0.3f)
                .SetEase(Ease.OutCirc);
        }
    }
}
