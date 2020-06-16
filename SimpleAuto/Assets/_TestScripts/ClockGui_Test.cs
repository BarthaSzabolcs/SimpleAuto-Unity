using SimpleCar.GUI.Model.Clock;
using SimpleCar.GUI.View.Clock;
using SimpleCar.GUI.ViewModel.Clock;
using UnityEngine;

namespace Test.Gui
{
    public class ClockGui_Test : MonoBehaviour
    {
        #region Editor Settings

        [Header("Required Components")]
        [SerializeField] private ClockViewModel ClockGui;
        
        [Header("Model:")]
        [SerializeField] private float value;
        [SerializeField] private float minValue;
        [Range(0.1f, 25000)][SerializeField] private float maxValue;
        [SerializeField] private float extremeValueLimit;

        [Header("View:")] 
        [SerializeField] private ClockViewStyle style;
        [SerializeField] private float minDisplayValue;
        [SerializeField] private float maxDisplayValue;
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
            style);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void Update()
        {
            // Model
            ClockGui.Model.Value = value;
            ClockGui.Model.Min = minValue;
            ClockGui.Model.Max = maxValue;
            ClockGui.Model.ExtremeValueLimit = extremeValueLimit;

            ClockGui.MinDisplayValue = minDisplayValue;
            ClockGui.MaxDisplayValue = maxDisplayValue;
            ClockGui.Flip = flip;
            ClockGui.Style = style;
        }         

        #endregion
    }
}
