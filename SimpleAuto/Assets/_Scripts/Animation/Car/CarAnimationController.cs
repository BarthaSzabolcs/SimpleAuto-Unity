using Animation.Avatar.IKGoals;
using SimpleCar.Animation.Avatar;
using SimpleCar.Control.Car;
using UnityEngine;

namespace SimpleCar.Animation.Car
{
    public class CarAnimationController : MonoBehaviour
    {
        #region Editor Settings

        [SerializeField] private CarWheelAnimator[] wheels;
        [SerializeField] private AvatarAnimationController avatar;

        [Header("Steeringwheel")]
        [SerializeField] private Transform steeringWheel;
        [SerializeField] private float steeringWheelMaxRotation;

        [Header("Gearbox")]
        [SerializeField] private Transform gearShift;
        [SerializeField] private float gearShiftAngleOffset;
        [SerializeField] private float gearShiftBaseAngle;

        [Header("Pedals")]
        [SerializeField] private Transform clutchPedal;
        [SerializeField] private Transform brakePedal;
        [SerializeField] private Transform gasPedal;
        [SerializeField] private float pedalMaxRotation;
        [SerializeField] private float pedalRotationOffset;

        [Header("IK Goals")]
        [SerializeField] private LeftHandIKGoals leftHandGoals;
        [SerializeField] private RightHandIKGoals rightHandGoals;
        [SerializeField] private LeftFootIKGoals leftFootGoals;
        [SerializeField] private RightFootIKGoals rightFootGoals;

        #endregion
        #region Private Fields

        private CarController controller;

        #endregion

        #region Unity Callbacks

        private void Start()
        {
            controller = transform.GetComponent<CarController>();
            avatar.LeftHand.Info.Goal = leftHandGoals.SteeringWheel;
            avatar.RightHand.Info.Goal = rightHandGoals.SteeringWheel;
        }

        private void Update()
        {
            UpdateWheels();
            UpdateSteeringWheel();
            UpdateGearBox();
            UpdatePedals();
            UpdateIKs();
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
        #region Cockpit
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

        private void UpdatePedals()
        {
            SetPedalYaw(clutchPedal, controller.Clutch);
            SetPedalYaw(brakePedal, controller.Brake);
            SetPedalYaw(gasPedal, controller.Gas);
        }        
        // ToDo - Refactor
        private void UpdateIKs()
        {
            avatar.RightHand.Info.Goal = controller.CanShift ?
                rightHandGoals.GearShift : rightHandGoals.SteeringWheel;

            avatar.LeftFoot.Info.Goal = controller.Clutch > 0 ?
                leftFootGoals.Clutch : leftFootGoals.Rest;

            var gasPressed = controller.Gas > 0.0125f;
            var brakePressed = controller.Brake > 0.0125f;

            if (gasPressed && brakePressed == false)
            {
                avatar.RightFoot.Info.Goal = rightFootGoals.Gas;
            }
            else if(gasPressed && brakePressed)
            {
                avatar.RightFoot.Info.Goal = rightFootGoals.Both;
                
                rightFootGoals.Both.position = (rightFootGoals.Gas.position + rightFootGoals.Brake.position) / 2;
                
                var distance = rightFootGoals.Gas.position - rightFootGoals.Brake.position;
                rightFootGoals.Both.rotation = Quaternion.LookRotation(distance, (rightFootGoals.Brake.up + rightFootGoals.Gas.up) / 2);

            }
            else if(gasPressed == false && brakePressed)
            {
                avatar.RightFoot.Info.Goal = rightFootGoals.Brake;
            }
            else
            {
                avatar.RightFoot.Info.Goal = rightFootGoals.Rest;
            }
        }
        private void SetPedalYaw(Transform pedal, float press)
        {
            var eulerAngles = pedal.localEulerAngles;
            eulerAngles.x = press * pedalMaxRotation + pedalRotationOffset;

            pedal.localEulerAngles = eulerAngles;
        }

        #endregion
    }
}