using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

namespace MGMG.Core
{
    public class UIManager : MonoSingleton<UIManager>
    {
        [SerializeField] private CardSelectPanel _cardSelectPanel;
        [SerializeField] private MagicSelectPanel _magicSelectPanel;

        public CardSelectPanel CardSelectPanel => _cardSelectPanel;
        public MagicSelectPanel MagicSelectPanel => _magicSelectPanel;
    } 
}