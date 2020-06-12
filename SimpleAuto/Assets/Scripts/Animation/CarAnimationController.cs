using SimpleCar.Control;
using UnityEngine;

namespace SimpleCar.Animation
{
    public class CarAnimationController : MonoBehaviour
    {
        #region Editor Settings

        [SerializeField] private CarWheelAnimator[] wheels;

        [Header("Steeringwheel")]
        [SerializeField] private Transform steeringWheel;
        [SerializeField] private float steeringWheelMaxRotation;

        [Header("Gearbox")]
        [SerializeField] private Transform gearKnob;
        [SerializeField] private Vector3 gearShiftOffset;
        [SerializeField] private Vector3 gearShiftCenter;

        [Header("Pedals")]
        [SerializeField] private Transform clutchPedal;
        [SerializeField] private Transform brakePedal;
        [SerializeField] private Transform gasPedal;
        [SerializeField] private float pedalMaxRotation;
        [SerializeField] private float pedalRotationOffset;

        #endregion
        #region Private Fields

        private CarController controller;

        #endregion

        #region Unity Callbacks

        private void Awake()
        {
            controller = transform.GetComponent<CarController>();
        }

        private void Update()
        {
            UpdateWheels();
            UpdateSteeringWheel();
            UpdateGearBox();
            UpdatePedals();
        }

        #endregion

        #region Wheels

        private void UpdateWheels()
        {
            foreach (var wheel in wheels)
            {
                wheel.Update();
            }
        }

        #endregion
        #region Dashboard
        private void UpdateSteeringWheel()
        {
            var eulerAngles = steeringWheel.localEulerAngles;
            eulerAngles.z = controller.Steering * steeringWheelMaxRotation;

            steeringWheel.localEulerAngles = eulerAngles;  
        }
        private void UpdateGearBox()
        {
            gearKnob.localPosition = gearShiftCenter + gearShiftOffset * controller.Gear;
        }

        #endregion
        #region Pedals

        private void UpdatePedals()
        {
            RotatePedal(clutchPedal, controller.Clutch);
            RotatePedal(brakePedal, controller.Brake);
            RotatePedal(gasPedal, controller.Gas);
        }
        private void RotatePedal(Transform pedal, float press)
        {
            var eulerAngles = pedal.localEulerAngles;
            eulerAngles.x = press * pedalMaxRotation + pedalRotationOffset;

            pedal.localEulerAngles = eulerAngles;
        }

        #endregion
    }
}