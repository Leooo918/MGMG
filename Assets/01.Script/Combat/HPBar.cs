using DG.Tweening;
using MGMG.Entities.Component;
using UnityEngine;

namespace MGMG.Combat
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private EntityHealth _targetHealth;
        [SerializeField] private Transform _healthBar, _changerBar;
        [SerializeField] private float _changerDelay = 0.5f;

        private Sequence _hpDownSeq;

        private void Awake()
        {
            _targetHealth.OnHealthChangedEvent += HandleHealthChangedEvent;
        }


        private void HandleHealthChangedEvent(int prev, int current, bool isChangeVisible)
        {
            if (_hpDownSeq != null && _hpDownSeq.active) 
                _hpDownSeq.Kill();

            float value = (10000 * current / _targetHealth.MaxHealth) * 0.0001f;

            _hpDownSeq = DOTween.Sequence();

            _hpDownSeq.Append(_healthBar.DOScaleX(value, 0.2f))
                .AppendInterval(_changerDelay)
                .Append(_changerBar.DOScaleX(value, 0.1f));
        }

        private void OnDestroy()
        {
            _targetHealth.OnHealthChangedEvent -= HandleHealthChangedEvent;
        }
    }
}
