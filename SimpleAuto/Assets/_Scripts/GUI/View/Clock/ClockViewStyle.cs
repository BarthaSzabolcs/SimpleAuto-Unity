using System;
using SimpleCar.GUI.View.ClockMark;
using UnityEngine;

namespace SimpleCar.GUI.View.Clock
{
    [Serializable]
    [CreateAssetMenu(fileName ="ClockStyle_", menuName = "View/Style/Clock")]
    public class ClockViewStyle : ScriptableObject
    {
        [Header("Overall")]
        public bool leftToRight;

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
        public float faceAngleLength;
        public float faceAngleOffset;

        [Header("Marks")]
        public ClockMarkViewStyle markStyle;
        public float textSizeRatio;
        public int marksBetweenTheExtremes;
        public int longMarkFrequency;
        public bool showShortMarksValue;
    }
}
