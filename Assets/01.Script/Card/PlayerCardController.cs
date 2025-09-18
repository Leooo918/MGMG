using MGMG.Entities;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCardController : MonoBehaviour, IEntityComponent
{
    private List<Card> _cardList;
    private Entity _owner;

    public void Initialize(Entity entity)
    {
        _cardList = new List<Card>();
        _owner = entity;
    }

    public void AddCard(Card card)
    {
        card.Initialize(_owner, _cardList.Count);
        _cardList.Add(card);
    }

    public void RemoveCarad(int index)
    {
        _cardList[index].Release();
        _cardList.RemoveAt(index);
    }
}
