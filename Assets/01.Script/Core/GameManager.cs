using DG.Tweening;
using MGMG.Core;
using TMPro;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private TextMeshProUGUI _timerTxt;
    [SerializeField] private CanvasGroup _gameOver;
    [SerializeField] private TextMeshProUGUI _killCountTxt, _gameOverTimeTxt;
    private float _erapseTime;
    private bool _running = false;
    public int Minute => _minute;
    public int Second => _second;
    private int _minute = 0, _second = 0;

    private string _displayTime;
    protected override void Awake()
    {
        base.Awake();

        StartStopwatch();
        UpdateDisplay(); 
    }

    void Update()
    {
        if (!_running) return;

        _erapseTime += Time.deltaTime;
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        int totalSeconds = Mathf.FloorToInt(_erapseTime);
        _minute = totalSeconds / 60;
        _second = totalSeconds % 60;

        string minutesStr = _minute.ToString("00");
        string secondsStr = _second.ToString("00");

        string display = $"{minutesStr}:{secondsStr}";

        _displayTime = display;
        _timerTxt.text = display;
    }

    public void StartStopwatch()
    {
        _running = true;
    }

    public void PauseStopwatch()
    {
        _running = false;
    }

    public void GameOver()
    {
        _killCountTxt.text = $"죽인 적 : {PlayerManager.Instance.EnemyKillCount}";
        _gameOverTimeTxt.text = $"버틴 시간 : {_displayTime}";
        _gameOver.DOFade(1f, 0.2f)
         .SetEase(Ease.OutSine)
         .SetUpdate(true)
         .OnComplete(() =>
         {
             // 필요시 게임오버 상태 전환 처리
             Debug.Log("GameOver Fade Complete");
         });
    }
}
