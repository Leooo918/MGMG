using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IconSelection : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _displayText;
    private int _index;

    public event Action<int> OnSelectIcon;

    public void Initialize(Sprite icon, string name, string display, int index)
    {
        _image.sprite = icon;
        _nameText.SetText(name);
        _displayText.SetText(display);
        _index = index;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnSelectIcon?.Invoke(_index);
    }
}
