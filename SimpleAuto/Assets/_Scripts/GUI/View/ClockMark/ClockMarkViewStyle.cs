using UnityEngine;
using UnityEngine.UI;

namespace SimpleCar.GUI.View.ClockMark
{
    public enum ClockMarkRotation { Relative, Fixed }

    [CreateAssetMenu(fileName = "ClockMarkStyle_", menuName = "View/Style/ClockMark")]
    public class ClockMarkViewStyle : ScriptableObject
    {
        // Image
        public Sprite shortSprite;
        public Sprite longSprite;

        // Colors
        public Color extremeColor;
        public Color normalColor;

        // Placement
        public Vector2 labelOffset;
        public bool roundValue;
    }
}