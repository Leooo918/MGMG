using MGMG.Entities;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCardController : MonoBehaviour, IEntityComponent
{
    [SerializeField] private CardSO _debugCard;
    [SerializeField] private int _removeIndex;

    private List<Card> _cardList;
    private Entity _owner;

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
        Card card = cardSO.card.GetInstance();
        card.Initialize(_owner, _cardList.Count);
        _cardList.Add(card);
    }

    public void RemoveCard(int index)
    {
        _cardList[index].Release();
        _cardList.RemoveAt(index);
    }
}
