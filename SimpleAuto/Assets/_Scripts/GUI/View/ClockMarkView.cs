using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleCar.GUI.View
{
    public class ClockMarkView
    {
        #region Public Properties

        public ClockViewStyle ClockStyle
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
        public (Quaternion rotation, float display) Value 
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

        #endregion
        #region Backing Fields

        private ClockViewStyle _clockStyle;
        private (Quaternion rotation, float display) _value;

        #endregion
        #region Private Fields

        private Image markImage;
        private TextMeshProUGUI textTMP;

        #endregion

        #region Public Methods

        public void Init(Transform transform, ClockViewStyle style)
        {
            markImage = transform.GetComponent<Image>();
            textTMP = transform.GetChild(0).GetComponent<TextMeshProUGUI>();

            ClockStyle = style;
        }

        public void Destroy()
        {
            GameObject.Destroy(markImage.gameObject);
        }

        #endregion
        #region Private Methods

        private void FullRedraw()
        {
            // ToDo - change mark image etc.
            RedrawValue();
        }
        private void RedrawValue()
        {            
            markImage.rectTransform.localRotation = Value.rotation;

            RedrawText();
            RedrawColor();
        }
        private void RedrawText()
        {
            var number = ClockStyle.RoundMarkValue ? Mathf.RoundToInt(Value.display) : Value.display;
            textTMP.text = number.ToString();
        }
        private void RedrawColor()
        {
            var color = Value.display > ClockStyle.ExtremeValueMargin ? 
                ClockStyle.ExtremMarkColor : 
                ClockStyle.MarkColor;

            markImage.color = color;
            textTMP.color = color;
        }

        #endregion
    }
}
