using SimpleCar.Animation.Avatar;
using SimpleCar.Control;
using UnityEngine;

namespace SimpleCar.Animation
{
    public class CarAnimationController : MonoBehaviour
    {
        #region Editor Settings

        [SerializeField] private CarWheelAnimator[] wheels;
        [SerializeField] private AvatarAnimationController avatar;

        [Header("Steeringwheel")]
        [SerializeField] private Transform steeringWheel;
        [SerializeField] private float steeringWheelMaxRotation;
        [SerializeField] private Transform steeringWheeIKLeft;
        [SerializeField] private Transform steeringWheeIKRight;

        [Header("Gearbox")]
        [SerializeField] private Transform gearShift;
        [SerializeField] private Transform gearShiftIK;
        [SerializeField] private float gearShiftAngleOffset;
        [SerializeField] private float gearShiftBaseAngle;

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
            avatar.LeftHand.Info.Goal = steeringWheeIKLeft;
            avatar.RightHand.Info.Goal = steeringWheeIKRight;
        }

        private void Update()
        {
            UpdateWheels();
            UpdateSteeringWheel();
            UpdateGearBox();
            UpdatePedals();

            avatar.RightHand.Info.Goal = controller.CanShift ?
                gearShiftIK : steeringWheeIKRight;

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
            eulerAngles.z = -controller.Steering * steeringWheelMaxRotation;

            steeringWheel.localEulerAngles = eulerAngles;
        }

        private void UpdateGearBox()
        {
            var eulerAngles = gearShift.localEulerAngles;
            eulerAngles.x = gearShiftBaseAngle + controller.Gear * gearShiftAngleOffset;

            gearShift.localEulerAngles = eulerAngles;
        }

        #endregion
        #region Pedals

        private void UpdatePedals()
        {
            SetPedalYaw(clutchPedal, controller.Clutch);
            SetPedalYaw(brakePedal, controller.Brake);
            SetPedalYaw(gasPedal, controller.Gas);
        }

        #endregion
        #region Helper

        private void SetPedalYaw(Transform pedal, float press)
        {
            var eulerAngles = pedal.localEulerAngles;
            eulerAngles.x = press * pedalMaxRotation + pedalRotationOffset;

            pedal.localEulerAngles = eulerAngles;
        }
        
        #endregion
    }
}