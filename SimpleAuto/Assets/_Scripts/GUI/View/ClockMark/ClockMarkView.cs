using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleCar.GUI.View.ClockMark
{
    public class ClockMarkView
    {
        #region Public Properties

        public ClockMarkViewStyle Style
        {
            get => _clockStyle;
            set
            {
                if (_clockStyle != value)
                {
                    _clockStyle = value;
                    FullRedraw();
                }
            }
        }
        public (float eulerZ, float display) Value 
        { 
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    RedrawValue();
                }
            }
        }
        public bool UseExtremeColor 
        {
            get => _useExtremeColor;
            set
            {
                if (_useExtremeColor != value)
                {
                    _useExtremeColor = value;
                    RedrawColor();
                }
            }
        }
        public bool ShowValue 
        { 
            get => _showValue;
            set
            {
                if (_showValue != value)
                {
                    _showValue = value;
                    textTMP.enabled = value;
                }
            } 
        }
        public bool Long 
        {
            get => _long;
            set
            {
                if (_long != value)
                {
                    _long = value;
                    RedrawSprite();
                }
            }
        }
        public float TextSize
        {
            get => textTMP.fontSize; set { textTMP.fontSize = value; }
        }

        #endregion
        #region Backing Fields

        private ClockMarkViewStyle _clockStyle;
        private (float eulerZ, float display) _value;
        private bool _showValue = true;
        private bool _useExtremeColor = false;
        private bool _long = true;

        #endregion
        #region Private Fields

        private Image markImage;
        private TextMeshProUGUI textTMP;

        #endregion

        #region Public Methods

        public void Init(Transform transform, ClockMarkViewStyle style)
        {
            markImage = transform.GetComponent<Image>();
            textTMP = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            Style = style;
        }

        public void Destroy()
        {
            GameObject.Destroy(markImage.gameObject);
        }

        #endregion
        #region Private Methods

        private void FullRedraw()
        {
            RedrawValue();
        }
        private void RedrawValue()
        {            
            markImage.rectTransform.localEulerAngles = Vector3.forward * Value.eulerZ;

            RedrawTextContent();
            RedrawTextRotation();
            RedrawTextPlacement();
            RedrawColor();
            RedrawSprite();
        }
        private void RedrawTextRotation()
        {
            switch (Style.rotation)
            {
                case ClockMarkRotation.Relative:
                    textTMP.transform.localEulerAngles = Vector3.forward * Style.rotationOffset;
                    break;

                case ClockMarkRotation.Fixed:
                    textTMP.transform.localEulerAngles = -markImage.rectTransform.localEulerAngles 
                        + Vector3.forward * Style.rotationOffset;
                    break;                    
            }
        }
        private void RedrawTextContent()
        {
            var number = Style.roundValue ? Mathf.RoundToInt(Value.display) : Value.display;
            textTMP.text = number.ToString();
        }
        private void RedrawTextPlacement()
        {
            textTMP.rectTransform.anchorMin = Style.textRelativePosition;
            textTMP.rectTransform.anchorMax = Style.textRelativePosition;
        }
        private void RedrawColor()
        {
            var color = _useExtremeColor ? Style.extremeColor : Style.normalColor;

            markImage.color = color;
            textTMP.color = color;
        }
        private void RedrawSprite()
        {
            markImage.sprite = Long ? Style.longSprite : Style.shortSprite;
        }

        #endregion
    }
}
