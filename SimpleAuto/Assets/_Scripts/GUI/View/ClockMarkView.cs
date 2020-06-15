using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleCar.GUI.View
{
    public class ClockMarkView
    {
        #region Public Properties

        public Color Color
        {
            get => _color;
            set
            {
                if (_color != value)
                {
                    markImage.color = value;
                    textTMP.color = value;
                }
                _color = value;
            }
        }
        public string Text
        {
            get => textTMP.text;
            set
            {
                textTMP.text = value;
            }
        }
        public float Value { get; set; }

        #endregion
        #region Backing Fields

        private Color _color;

        #endregion
        #region Private Fields

        private Image markImage;
        private TextMeshProUGUI textTMP;

        #endregion

        #region Public Methods

        public void Init(Transform transform)
        {
            markImage = transform.GetComponent<Image>();
            textTMP = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }

        #endregion
    }
}
