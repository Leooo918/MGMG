using MGMG.Core;
using MGMG.Entities;
using MGMG.Magic;
using System.Collections.Generic;
using UnityEngine;

public class CardPanel : MonoBehaviour
{
    [SerializeField] private IconUI _iconPf;

    private PlayerCardController _playerCardController;
    private List<IconUI> _iconList;

    private void Start()
    {
        _iconList = new List<IconUI>();
        _playerCardController = PlayerManager.Instance.Player.GetCompo<PlayerCardController>();
        _playerCardController.OnGetCard += OnGetCard;
        _playerCardController.OnRemoveCardIndex += OnRemoveCard;
    }

    private void OnDisable()
    {
        if (_playerCardController != null)
        {
            _playerCardController.OnGetCard -= OnGetCard;
            _playerCardController.OnRemoveCardIndex -= OnRemoveCard;
        }
    }

    private void OnGetCard(CardSO card)
    {
        IconUI icon = Instantiate(_iconPf, transform);
        icon.SetIcon(card.icon);
        _iconList.Add(icon);
    }

    private void OnRemoveCard(int index)
    {
        Destroy(_iconList[index].gameObject);
        _iconList.RemoveAt(index);
    }
}
