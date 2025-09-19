using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IconUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Image _icon;

    public void SetTextIcon(Sprite icon, string text)
    {
        if (_icon != null) _icon.sprite = icon;
        if (_text != null) _text.SetText(text);
    }

    public void SetIcon(Sprite icon)
    {
        if (_icon != null) 
            _icon.sprite = icon;
    }

    public void SetText(string text)
    {
        if (_text != null)
            _text.SetText(text);
    }
}
