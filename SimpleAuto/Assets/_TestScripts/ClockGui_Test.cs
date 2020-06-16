using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleCar.GUI.Clock;
using SimpleCar.GUI.Model;
using SimpleCar.GUI.View;
using UnityEngine;
using UnityEngine.UI;

namespace Test.Gui
{
    public class ClockGui_Test : MonoBehaviour
    {
        #region Editor Settings

        [Header("Required Components")]
        [SerializeField] private ClockGui ClockGui;
        [SerializeField] private Button refreshStyle;

        [Header("Model:")]
        [SerializeField] private float value;
        [SerializeField] private float minValue;
        [SerializeField] private float maxValue;
        [SerializeField] private float extremeValueLimit;

        [Header("View Style")]
        [Range(-180, 180)][SerializeField] private float faceAngleOffset;
        [Range(0, 360f)][SerializeField] private float faceAngleLength;

        [SerializeField] private Color pointerColor;
        [SerializeField] private Color faceColor;
        [SerializeField] private Color markColor;
        [SerializeField] private Color extremMarkColor;
        [SerializeField] private float markDistance;
        [SerializeField] private float minMarkValue;
        [SerializeField] private float maxMarkValue;
        [SerializeField] private bool roundMarkValue;

        [SerializeField] private bool flip;

        #endregion

        #region Unity Callbacks

        private void Start()
        {

            ClockGui.Init(new ClockModel()
            {
                Min = minValue,
                Max = maxValue,
                Value = value,
                ExtremeValueLimit = extremeValueLimit
            },
            CurrentStyle());

            refreshStyle.onClick.AddListener(() => ClockGui.Style = CurrentStyle());

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void Update()
        {
            ClockGui.Model.Value = value;
            ClockGui.Model.Min = minValue;
            ClockGui.Model.Min = maxValue;
            ClockGui.Model.ExtremeValueLimit = extremeValueLimit;
        }         

        private ClockViewStyle CurrentStyle()
        {
            return new ClockViewStyle()
            {
                FaceColor = faceColor,
                PointerColor = pointerColor,
                FaceAngleOffset = faceAngleOffset,
                FaceAngleLength = faceAngleLength,

                MarkColor = markColor,
                ExtremMarkColor = extremMarkColor,
                MarkDistance = markDistance,
                MinValue = minMarkValue,
                MaxValue = maxMarkValue,
                ExtremeValueMargin = (minMarkValue + maxMarkValue) / 2,
                RoundMarkValue = roundMarkValue,

                Flip = flip
            };
        }

        #endregion
    }
}
