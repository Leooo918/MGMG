using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

namespace MGMG.Core
{
    public class UIManager : MonoSingleton<UIManager>
    {
        [SerializeField] private CardSelectPanel _cardSelectPanel;
        [SerializeField] private MagicSelectPanel _magicSelectPanel;

        public CardSelectPanel CardSelectPanel => _cardSelectPanel;
        public MagicSelectPanel MagicSelectPanel => _magicSelectPanel;

        public void SceneLoad(string sceneName)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(sceneName);
        }
    } 
}