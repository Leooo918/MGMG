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

        string display = string.Format("{0:00}:{1:00}", _minute, _second); 

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
        _killCountTxt.text = $"���� �� : {PlayerManager.Instance.EnemyKillCount}";
        _gameOverTimeTxt.text = $"��ƾ �ð� : {_displayTime}";
        _gameOver.DOFade(1f, 0.2f)
         .SetEase(Ease.OutSine)
         .SetUpdate(true)
         .OnComplete(() =>
         {
             // �ʿ�� ���ӿ��� ���� ��ȯ ó��
             Debug.Log("GameOver Fade Complete");
         });
    }
}
