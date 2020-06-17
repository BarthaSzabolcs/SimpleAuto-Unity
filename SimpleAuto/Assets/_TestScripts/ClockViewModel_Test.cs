using SimpleCar.Control.Car;
using SimpleCar.GUI.Model.Clock;
using SimpleCar.GUI.View.Clock;
using SimpleCar.GUI.ViewModel.Clock;
using UnityEngine;

namespace Test.Gui
{
    public class ClockViewModel_Test : MonoBehaviour
    {
        #region Editor Settings

        [Header("Random data:")]
        [SerializeField] private float maxSpeed;

        [Header("Required Components")]
        [SerializeField] private ClockViewModel[] speedGauges;
        [SerializeField] private ClockViewModel[] rpmGauges;
        [SerializeField] private CarController carController;

        [Header("Model:")]
        [Range(0, 1)] [SerializeField] private float percentage;
        [SerializeField] private float minValue;
        [Range(0.1f, 25000)] [SerializeField] private float maxValue;
        [Range(0, 1)] [SerializeField] private float extremeValuePercentage;

        [Header("View:")] 
        [SerializeField] private bool overwriteViewSettings;
        [SerializeField] private ClockViewStyle speedGaugeStyle;
        [SerializeField] private ClockViewStyle rpmGaugeStyle;

        [SerializeField] private float minDisplayValue;
        [SerializeField] private float maxDisplayValue;
        [SerializeField] private bool flip;

        #endregion
        #region Private Fields

        ClockModel speedGaugeModel;
        ClockModel rpmGaugeModel;
        Rigidbody carRigidbody;

        #endregion

        #region Unity Callbacks

        private void Start()
        {
            // Speed
            speedGaugeModel = new ClockModel()
            {
                Min = 0,
                Max = 1,
                ExtremeValueLimit = 0.75f
            };
            foreach (var clock in speedGauges)
            {
                clock.Init(speedGaugeModel, speedGaugeStyle);
                clock.MaxDisplayValue = maxSpeed;
            }

            // RPM
            rpmGaugeModel = new ClockModel()
            {
                Min = 0,
                Max = 1,
                ExtremeValueLimit = 0.85f
            };
            foreach (var clock in rpmGauges)
            {
                clock.Init(rpmGaugeModel, rpmGaugeStyle);
                clock.MaxDisplayValue = 5;
            }

            // Cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            
            carRigidbody = carController.GetComponent<Rigidbody>();
        }

        private void Update()
        {
            // Model
            speedGaugeModel.Value = CalculateSpeedoMeterValue();
            rpmGaugeModel.Value = carController.Gas;
            //model.Min = minValue;
            //model.Max = maxValue;
            //model.ExtremeValueLimit = extremeValuePercentage * model.Range;

            foreach (var clock in speedGauges)
            {
                clock.MinDisplayValue = minDisplayValue;
                clock.MaxDisplayValue = maxDisplayValue;
                clock.Flip = flip;
                clock.Style = speedGaugeStyle;
            }

            if (overwriteViewSettings)
            {
                foreach (var clock in rpmGauges)
                {
                    clock.MinDisplayValue = minDisplayValue;
                    clock.MaxDisplayValue = maxDisplayValue;
                    clock.Flip = flip;
                    clock.Style = speedGaugeStyle;
                }
            }
        }

        #endregion
        #region Private Methods

        private float CalculateSpeedoMeterValue()
        {
            return carRigidbody.velocity.magnitude / maxSpeed;
        }

        #endregion
    }
}
