using MGMG.Entities;
using MGMG.Magic;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCardController : MonoBehaviour, IEntityComponent
{
    [SerializeField] private CardSO _debugCard;
    [SerializeField] private int _removeIndex;

    private List<Card> _cardList;
    private Entity _owner;

    public event Action<CardSO> OnGetCard;
    public event Action<CardSO> OnRemoveCard;
    public event Action<int> OnRemoveCardIndex;

    private void Update()
    {
        if(Keyboard.current.mKey.wasPressedThisFrame)
        {
            AddCard(_debugCard);
        }
        if(Keyboard.current.nKey.wasPressedThisFrame)
        {
            if (_cardList.Count > _removeIndex)
                RemoveCard(_removeIndex);
        }
    }

    public void Initialize(Entity entity)
    {
        _cardList = new List<Card>();
        _owner = entity;
    }

    public void AddCard(CardSO cardSO)
    {
        OnGetCard?.Invoke(cardSO);
        Card card = cardSO.card.GetInstance();
        card.Initialize(_owner, _cardList.Count);
        _cardList.Add(card);
    }

    public void RemoveCard(int index)
    {
        OnRemoveCardIndex?.Invoke(index);
        _cardList[index].Release();
        _cardList.RemoveAt(index);
    }
}
