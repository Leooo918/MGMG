using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    private bool _isUsingTimer = false;

    private float _timer = 0;
    private int _nextTimerUpdate = 1;

    private void Update()
    {
        if (_isUsingTimer == false) return;

        _timer += Time.deltaTime;
        Debug.Log(Time.timeScale);

        if (_timer >= _nextTimerUpdate)
        {
            _text.SetText(string.Format("{0:00}:{1:00}", _nextTimerUpdate / 60, _nextTimerUpdate % 60));
            _nextTimerUpdate++;
        }

    }


    [ContextMenu("Start")]
    public void StartTimer()
    {
        _isUsingTimer = true;
    }

    public float EndTimer()
    {
        _isUsingTimer = false;
        return _timer;
    }
}
