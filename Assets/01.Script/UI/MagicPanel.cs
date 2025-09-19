using MGMG.Core;
using MGMG.Entities;
using MGMG.Magic;
using System.Collections.Generic;
using UnityEngine;

public class MagicPanel : MonoBehaviour
{
    [SerializeField] private IconUI _iconPf;

    private PlayerMagicController _playerMagicController;
    private List<IconUI> _iconList;

    private void Start()
    {
        _playerMagicController = PlayerManager.Instance.Player.GetCompo<PlayerMagicController>();
        _playerMagicController.OnGetMagic += OnGetMagic;
        _playerMagicController.OnUpgradeMagic += OnLevelUpMagic;
        _iconList = new List<IconUI>();
    }

    private void OnDisable()
    {
        if( _playerMagicController != null )
        {
            _playerMagicController.OnGetMagic -= OnGetMagic;
            _playerMagicController.OnUpgradeMagic -= OnLevelUpMagic;
        }
    }

    private void OnGetMagic(MagicSO magic)
    {
        IconUI icon = Instantiate(_iconPf, transform);
        icon.SetTextIcon(magic.magicData.icon, RomeNumberConverter.GetRomeNumber(1));
        _iconList.Add(icon);
    }

    private void OnLevelUpMagic(int index, int level)
    {
        _iconList[index].SetText(RomeNumberConverter.GetRomeNumber(level));
    }
}
