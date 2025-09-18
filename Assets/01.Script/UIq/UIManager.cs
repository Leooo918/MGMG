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
            #region get magic panel child
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
            #endregion

            #region get card panel child
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
            #endregion

            #region get select panel child
            Transform selectPanel = _canvas.transform.Find("SelectPanel");

            if(selectPanel != null)
            {
                List<Image> selectableImageList = new List<Image>();

                foreach(Transform child in selectPanel)
                {
                    if(child.name.Contains("SelectCardImage"))
                    {
                        Image img = child.GetComponent<Image>();
                        if(img != null)
                            selectableImageList.Add(img);
                    }
                }


            }
            #endregion
        }

        private void Update()
        {
            if(UnityEngine.Input.GetKeyDown(KeyCode.K))
            {
                ShowSelectPanel();
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

        public void HpApply(int damage)
        {
            //_hpSlider.value += damage;
        }

        public void XpApply(int value)
        {
            float prevValue = _xpSlider.value;
            float maxValue = _xpSlider.maxValue;

            float targetValue = prevValue + value;

            if (targetValue >= maxValue)
            {
                float remainingXp = targetValue - maxValue;

                _xpSlider.maxValue *= 1.1f;

                DOTween.To(() => _xpSlider.value, x => _xpSlider.value = x, maxValue, 0.3f)
                    .SetEase(Ease.OutCirc)
                    .OnComplete(() =>
                    {
                        DOTween.To(() => _xpSlider.value, x => _xpSlider.value = x, remainingXp, 0.3f)
                        .SetEase(Ease.OutCirc);
                    });
            }
            else
            {
                DOTween.To(() => _xpSlider.value, x => _xpSlider.value = x, targetValue, 0.3f)
                    .SetEase(Ease.OutCirc);
            }
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