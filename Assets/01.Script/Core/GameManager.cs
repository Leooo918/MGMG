using DG.Tweening;
using MGMG.Core;
using TMPro;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private CanvasGroup _gameOver;
    private float _erapseTime;
    private bool _running = false;
    public int Minute => _minute;
    public int Second => _second;
    private int _minute = 0, _second = 0;
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

        _timerText.text = display;
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
        _gameOver.DOFade(1, 0.2f);
    }
}
