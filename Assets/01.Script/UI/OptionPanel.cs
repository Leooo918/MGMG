using DG.Tweening;
using MGMG.Core;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OptionPanel : MonoBehaviour
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _retryButton;
    [SerializeField] private Button _goToTileButton;
    [SerializeField] private Slider _volumeSlider;
    private bool _isOpen = false;

    private Tween _openCloseTween;
    private float _openPosition = 0, _closePosition = -1080;
    private float _openCloseDuration = 0.3f;

    private RectTransform RectTrm => transform as RectTransform;

    private void Awake()
    {
        _resumeButton.onClick.AddListener(ResumeGame);
        _retryButton.onClick.AddListener(RetryGame);
        _goToTileButton.onClick.AddListener(ExitGame);
        _volumeSlider.onValueChanged.AddListener(OnVolumeChnaged);
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (_isOpen) Close();
            else Open();
        }
    }

    private void Open()
    {
        if (_openCloseTween != null && _openCloseTween.active)
            _openCloseTween.Kill();

        _isOpen = true;
        Time.timeScale = 0;
        _openCloseTween = RectTrm.DOAnchorPosY(_openPosition, _openCloseDuration).SetUpdate(true);
    }
    private void Close()
    {
        if (_openCloseTween != null && _openCloseTween.active)
            _openCloseTween.Kill();

        _isOpen = false;
        _openCloseTween = RectTrm.DOAnchorPosY(_closePosition, _openCloseDuration).SetUpdate(true);
        Time.timeScale = 1;
    }

    private void OnVolumeChnaged(float value)
    {

    }

    private void RetryGame()
    {
        UIManager.Instance.SceneLoad("InGameScene");
    }

    private void ExitGame()
    {
        UIManager.Instance.SceneLoad("TitleScene");
    }

    private void ResumeGame()
    {
        Close();
    }
}
