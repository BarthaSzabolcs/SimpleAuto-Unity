using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleCar.GUI.Clock;
using SimpleCar.GUI.Model;
using UnityEngine;

namespace Test.Gui
{
    public class ClockGui_Test : MonoBehaviour
    {
        #region Editor Settings

        [SerializeField] private ClockGui ClockGui;

        [Header("Test settings")]
        [SerializeField] private float value;
        [SerializeField] private Color pointerColor;
        [SerializeField] private Color faceColor;
        [SerializeField] private Color markColor;
        [SerializeField] private Color extremMarkColor;
        
        [Range(-180, 180)]
        [SerializeField] private float faceAngleOffset;
        [Range(0, 360f)]
        [SerializeField] private float faceAngleLength;

        [SerializeField] private bool flip;

        #endregion

        #region Unity Callbacks

        private void Start()
        {
            ClockGui.Init(new ClockModel()
            {
                Min = 0,
                Max = 10
            });
        }

        private void Update()
        {
            // Colors
            ClockGui.FaceColor = faceColor;
            ClockGui.PointerColor = pointerColor;
            ClockGui.MarkColor = markColor;
            ClockGui.ExtremMarkColor = extremMarkColor;

            // Face angles
            ClockGui.FaceAngleOffset = faceAngleOffset;
            ClockGui.FaceAngleLength = faceAngleLength;

            ClockGui.Model.Value = value;

            ClockGui.Flip = flip;
        }                           

        #endregion
    }
}
