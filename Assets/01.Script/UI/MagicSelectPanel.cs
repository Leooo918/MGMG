using DG.Tweening;
using MGMG.Core;
using MGMG.Entities;
using MGMG.Magic;
using System.Collections.Generic;
using UnityEngine;

public class MagicSelectPanel : MonoBehaviour
{
    [SerializeField] private List<MagicSO> _exsistingMagic;
    [SerializeField] private List<MagicSelectCard> _magicSelection;

    private PlayerMagicController _magicController;
    private MagicSO[] _magic = new MagicSO[3];
    private Tween _openCloseTween;

    private RectTransform _rectTransform => transform as RectTransform;


    private void Start()
    {
        _magicController = PlayerManager.Instance.Player.GetCompo<PlayerMagicController>();
        for(int i = 0; i < _magicSelection.Count; i++)
        {
            if (_magicSelection[i] == null) continue;
            _magicSelection[i].OnSelectIcon += OnSelectIcon;
        }
    }

    private void OnDisable()
    {
        _magicSelection.ForEach(card => card.OnSelectIcon -= OnSelectIcon);
    }

    [ContextMenu("Open")]
    public void Open()
    {
        Time.timeScale = 0;
        int cardToDisplay = Mathf.Min(_exsistingMagic.Count, 3);
        var random = _exsistingMagic.GetRandomsInListNotDuplicated(cardToDisplay);

        for (int i = 0; i < cardToDisplay; i++)
        {
            _magic[i] = random[i];
            _magicSelection[i].Initialize(_magic[i].magicData.icon, _magic[i].magicData.displayName, _magic[i].magicData.description, i);
            Debug.Log(_magic[i]);
            _magicSelection[i].SetLevel(_magicController.GetMagicLevel(_magic[i]));
        }

        for (int i = cardToDisplay; i < 3; i++)
            _magicSelection[i].gameObject.SetActive(false);

        if (_openCloseTween != null && _openCloseTween.active)
            _openCloseTween.Kill();

        _openCloseTween = _rectTransform.DOAnchorPosY(0, 0.3f).SetUpdate(true);
    }

    public void Close()
    {
        Time.timeScale = 1;
        if (_openCloseTween != null && _openCloseTween.active)
            _openCloseTween.Kill();

        _openCloseTween = _rectTransform.DOAnchorPosY(1080, 0.3f).SetUpdate(true);
    }

    private void OnSelectIcon(int index)
    {
        _magicController.GetMagic(_magic[index]);

        //최대 업그레이드 했으면 빼기
        if(_magicController.GetMagicLevel(_magic[index]) == 5)
            _exsistingMagic.Remove(_magic[index]);
        
        Close();
    }
}
