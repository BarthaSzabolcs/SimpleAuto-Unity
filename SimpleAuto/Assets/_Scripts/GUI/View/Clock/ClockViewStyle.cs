using System;
using SimpleCar.GUI.View.ClockMark;
using UnityEngine;

namespace SimpleCar.GUI.View.Clock
{
    [Serializable]
    [CreateAssetMenu(fileName ="ClockStyle_", menuName = "View/Style/Clock")]
    public class ClockViewStyle : ScriptableObject
    {
        // Face
        public float faceAngleOffset;
        public float faceAngleLength;

        // Colors
        public Color pointerColor;
        public Color faceColor;

        // Marks
        public ClockMarkViewStyle markStyle;
        public int marksBetweenTheExtremes;
        public int longMarkFrequency;
        public bool showShortMarksValue;
    }
}
