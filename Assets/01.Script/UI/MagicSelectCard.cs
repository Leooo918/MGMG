using UnityEngine;

public class MagicSelectCard : IconSelection
{
    [SerializeField] private GameObject _newText;
    [SerializeField] private MagicLevelIndicator _levelIndicator;

    public void SetLevel(int level)
    {
        Debug.Log(level);
        _newText.SetActive(level == 0);
        _levelIndicator.SetCurrentLevel(level);
    }
}
