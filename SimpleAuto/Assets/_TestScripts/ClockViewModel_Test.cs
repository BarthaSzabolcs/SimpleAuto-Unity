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
        [SerializeField] private ClockViewModel[] speedClock;
        [SerializeField] private ClockViewModel[] rpmClock;
        [SerializeField] private CarController carController;

        [Header("View:")]
        [SerializeField] private ClockViewStyle[] styles;
        [SerializeField] private string styleCycleKey;

        #endregion
        #region Private Properties

        private int StyleIndex
        {
            get => _styleIndex;
            set
            {
                if (value >= styles.Length)
                {
                    _styleIndex = 0;
                }
                else
                {
                    _styleIndex = value;
                }
            }
        }

        #endregion
        #region Backing Fields

        private int _styleIndex;

        #endregion
        #region Private Fields

        ClockModel speedClockModel;
        ClockModel rpmClockModel;
        Rigidbody carRigidbody;

        #endregion

        #region Unity Callbacks

        private void Start()
        {
            // Speed
            speedClockModel = new ClockModel()
            {
                Min = 0,
                Max = 1,
                ExtremeValueLimit = 0.75f
            };
            foreach (var clock in speedClock)
            {
                clock.Init(speedClockModel, styles[0]);
                clock.MaxDisplayValue = maxSpeed;
            }

            // RPM
            rpmClockModel = new ClockModel()
            {
                Min = 0,
                Max = 1,
                ExtremeValueLimit = 0.85f
            };
            foreach (var clock in rpmClock)
            {
                clock.Init(rpmClockModel, styles[0]);
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
            speedClockModel.Value = CalculateSpeedoMeterValue();
            rpmClockModel.Value = carController.Gas;

            if (Input.GetKeyDown(styleCycleKey))
            {
                CycleStyles();
            }
        }

        #endregion
        #region Private Methods
        private void CycleStyles()
        {
            StyleIndex++;
            foreach (var clock in rpmClock)
            {
                clock.Style = styles[StyleIndex];
            }
            foreach (var clock in speedClock)
            {
                clock.Style = styles[StyleIndex];
            }
        }

        private float CalculateSpeedoMeterValue()
        {
            return carRigidbody.velocity.magnitude / maxSpeed;
        }

        #endregion
    }
}
