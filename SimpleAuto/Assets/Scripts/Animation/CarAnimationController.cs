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

        [Header("IK Target Pairs")]
        [SerializeField] private IKTargetPair rightHandTargets;
        [SerializeField] private IKTargetPair leftFootTargets;
        [SerializeField] private IKTargetPair rightFootTargets;

        [Header("RightFoot Target")]
        [SerializeField] private Vector3 gasLocalPosition;
        [SerializeField] private Vector3 brakeLocalPosition;
        [SerializeField] private Vector3 singlePedalRotation;
        [SerializeField] private Vector3 dualPedalRotation;

        #endregion
        #region Private Fields

        private CarController controller;

        #endregion

        #region Unity Callbacks

        private void Start()
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
                rightHandTargets.Active : rightHandTargets.Rest;

            avatar.LeftFoot.Info.Goal = controller.Clutch > 0 ?
                leftFootTargets.Active : leftFootTargets.Rest;

            if (controller.Gas > 0.0125f && controller.Brake > 0.0125f)
            {
                // If both of the pedals are down.
                avatar.RightFoot.Info.Goal = rightFootTargets.Active;
                avatar.RightFoot.Info.Goal.localEulerAngles = dualPedalRotation;
                avatar.RightFoot.Info.Goal.localPosition = (brakeLocalPosition + gasLocalPosition) / 2;
            }
            else if(controller.Gas < 0.0125f && controller.Brake < 0.0125f)
            {
                // If none of them are down.
                avatar.RightFoot.Info.Goal = rightFootTargets.Rest;
            }
            else
            {
                // If only one of them are down.
                avatar.RightFoot.Info.Goal = rightFootTargets.Active;
                avatar.RightFoot.Info.Goal.localEulerAngles = singlePedalRotation;

                avatar.RightFoot.Info.Deattach();
                if (controller.Gas > controller.Brake)
                {
                    avatar.RightFoot.Info.Goal.localPosition = gasLocalPosition;
                }
                else
                {
                    avatar.RightFoot.Info.Goal.localPosition = brakeLocalPosition;
                }
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