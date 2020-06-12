using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleCar.Input;

namespace SimpleCar.Control
{
    public class CarController : MonoBehaviour
    {
        #region Editor Settings

        [SerializeField] private List<AxleInfo> axleInfos;
        [SerializeField] private float maxMotorTorque;
        [SerializeField] private float maxSteeringAngle;

        #endregion
        #region Private Fields

        private CarInput input;

        #endregion


        #region Unity Callbacks

        private void Awake()
        {
            input = GetComponent<CarInput>();
        }

        private void FixedUpdate()
        {
            foreach (AxleInfo axleInfo in axleInfos)
            {
                if (axleInfo.Steering)
                {
                    axleInfo.LeftWheel.steerAngle = input.Steering * maxSteeringAngle;
                    axleInfo.RightWheel.steerAngle = input.Steering * maxSteeringAngle;
                }
                if (axleInfo.Motor)
                {
                    axleInfo.LeftWheel.motorTorque = input.Motor * maxMotorTorque;
                    axleInfo.RightWheel.motorTorque = input.Motor * maxMotorTorque;
                }
            }
        }

        #endregion
    }
}