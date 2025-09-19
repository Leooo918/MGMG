using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace MGMG.Core
{
    public class UIManager : MonoSingleton<UIManager>
    {
        [SerializeField] private CardSelectPanel _cardSelectPanel;
        [SerializeField] private MagicSelectPanel _magicSelectPanel;
        [SerializeField] private Timer _timer;

        public CardSelectPanel CardSelectPanel => _cardSelectPanel;
        public MagicSelectPanel MagicSelectPanel => _magicSelectPanel;
        public Timer Timer => _timer;


    } 
}