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

        public float CorrectedFaceAngleOffset
        {
            get
            {
                return ClockWise ? faceAngleOffset : -faceAngleOffset;
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
        public Sprite mask;
        public Color rimColor;
        public Color rimExtremeColor;
        public Color maskColor;
        public bool showMask;
        [Range(0, 360)] 
        public float faceAngleLength;
        [Range(-360, 360)] 
        public float faceAngleOffset;

        [Header("Current Value")]
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
        [Range(0, 50)] 
        public int marksBetweenTheExtremes;
        public int longMarkFrequency;
        public bool showShortMarksValue;
    }
}
