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
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Image _selectPanel;
        //private Slider _hpSlider;
        private Slider _xpSlider;

        private Image[] _magicImages;
        private Image[] _cardImages;

        private int _magicCurrentIndex = 0;
        private int _cardCurrentIndex = 0;

        private int _selectPanelCloseYPos = 1080;
        private int _selectPanelShowYPos = 0;

        protected override void Awake()
        {
            base.Awake();
            _canvas = GetComponent<Canvas>();

            _xpSlider = _canvas.transform.Find("XPSlider").GetComponent<Slider>();
        }

        private void Start()
        {
            Transform magicPanel = _canvas.transform.Find("MagicPanel");

            if (magicPanel != null)
            {
                List<Image> magicList = new List<Image>();

                foreach (Transform child in magicPanel)
                {
                    if (child.name.Contains("MagicImage"))
                    {
                        Image img = child.GetComponent<Image>();
                        if (img != null)
                            magicList.Add(img);
                    }
                }

                _magicImages = magicList.ToArray();
            }

            Transform cardPanel = _canvas.transform.Find("CardPanel");

            if (cardPanel != null)
            {
                List<Image> cardList = new List<Image>();

                foreach (Transform child in cardPanel)
                {
                    if (child.name.Contains("CardImage"))
                    {
                        Image img = child.GetComponent<Image>();
                        if (img != null)
                            cardList.Add(img);
                    }
                }

                _cardImages = cardList.ToArray();
            }
        }

        public void GetMagic(Sprite sprite)
        {
            if (_magicCurrentIndex < _magicImages.Length)
            {
                _magicImages[_magicCurrentIndex].sprite = sprite;
                _magicCurrentIndex++;
            }
        }

        public void GetCard(Sprite sprite)
        {
            if (_cardCurrentIndex < _cardImages.Length)
            {
                _cardImages[_cardCurrentIndex].sprite = sprite;
                _cardCurrentIndex++;
            }
        }

        public void ImvisibleImage()
        {

        }

        public void HpApply(int damage)
        {
            //_hpSlider.value += damage;
        }

        public void XpApply(int value)
        {
            _xpSlider.value += value;

            //if (_xpSlider.value >= _xpSlider.maxValue)
            //{
            //    _xpSlider.value = 0;
            //}
        }

        public void ShowSelectPanel()
        {
            _selectPanel.rectTransform
           .DOAnchorPosY(_selectPanelShowYPos, 0.5f)
           .SetEase(Ease.OutBounce);
            //Time.timeScale = 0;
        }

        public void CloseSelectPanel()
        {
            //Time.timeScale = 1;
            _selectPanel.rectTransform
           .DOAnchorPosY(_selectPanelCloseYPos, 0.3f)
           .SetEase(Ease.OutCirc);
        }
    }
}