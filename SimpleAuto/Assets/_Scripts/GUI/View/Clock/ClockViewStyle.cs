using System;
using SimpleCar.GUI.View.ClockMark;
using UnityEngine;

namespace SimpleCar.GUI.View.Clock
{
    [Serializable]
    [CreateAssetMenu(fileName ="ClockStyle_", menuName = "View/Style/Clock")]
    public class ClockViewStyle : ScriptableObject
    {
        #region Public Properties

        public float CorrectedRimAngularOffset
        {
            get
            {
                return ClockWise ? rimAngularOffset : -rimAngularOffset;
            }
        }

        #endregion

        [Header("Overall")]
        public bool ClockWise;

        [Header("Pointer")]
        public Sprite pointer;
        public Sprite pointerBase;
        public bool pointerMatchValueColor;
        public Color pointerColor;
        public Color pointerBaseColor;

        [Header("Face")]
        public Sprite rim;
        public Sprite rimExtreme;
        public Sprite face;
        public Color rimColor;
        public Color rimExtremeColor;
        public Color faceColor;
        public bool showFace;
        [Range(0, 360)] 
        public float rimAngulareLength;
        [Range(-360, 360)] 
        public float rimAngularOffset;

        [Header("Current Value")]
        public bool currentValueDrawBeforePointer;
        public bool currentValueShow;
        public float currentValueSizeRatio;
        [Range(0, 15)] 
        public int currentValueDecimalsRounded;
        public string currentValueFormat;
        public Vector2 currentValueOffset;
        public Color currentValueColor;
        public bool currentValueMatchValueColor;

        [Header("Marks")]
        public ClockMarkViewStyle markStyle;
        public float markTextSizeRatio;
        public bool showExtremeValues;
        public bool extremeValuesAreLong;
        [Range(0, 50)] 
        public int marksBetweenTheExtremes;
        public int longMarkFrequency;
        public bool showShortMarksValue;
    }
}
