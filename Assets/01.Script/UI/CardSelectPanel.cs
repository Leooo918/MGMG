using DG.Tweening;
using MGMG.Core;
using System.Collections.Generic;
using UnityEngine;

public class CardSelectPanel : MonoBehaviour
{
    [SerializeField] private List<CardSO> _exsistingCard;
    [SerializeField] private List<IconSelection> _cardSelection;

    private PlayerCardController _cardController;
    private CardSO[] _card = new CardSO[3];
    private Tween _openCloseTween;

    private RectTransform _rectTransform => transform as RectTransform;


    private void Start()
    {
        _cardController = PlayerManager.Instance.Player.GetCompo<PlayerCardController>();
        _cardSelection.ForEach(card => card.OnSelectIcon += OnSelectIcon);
        _cardController.OnRemoveCard += OnRemoveCard;
    }

    private void OnDisable()
    {
        _cardSelection.ForEach(card => card.OnSelectIcon -= OnSelectIcon);
        if (_cardController) _cardController.OnRemoveCard -= OnRemoveCard;
    }

    public void Open()
    {
        Time.timeScale = 0;
        int cardToDisplay = Mathf.Min(_exsistingCard.Count, 3);
        var random = _exsistingCard.GetRandomsInListNotDuplicated(cardToDisplay);

        for (int i = 0; i < cardToDisplay; i++)
        {
            _card[i] = random[i];
            _cardSelection[i].Initialize(_card[i].icon, _card[i].displayName, _card[i].description, i);
        }

        for (int i = cardToDisplay; i < 3; i++)
            _cardSelection[i].gameObject.SetActive(false);

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
        _exsistingCard.Remove(_card[index]);
        _cardController.AddCard(_card[index]);
        Close();
    }

    private void OnRemoveCard(CardSO card)
    {
        _exsistingCard.Add(card);
    }
}
