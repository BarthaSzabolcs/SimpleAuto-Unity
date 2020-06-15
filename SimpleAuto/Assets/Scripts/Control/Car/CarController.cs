using UnityEngine;
using System.Collections.Generic;
using SimpleCar.Input;

namespace SimpleCar.Control.Car
{
    public class CarController : MonoBehaviour
    {
        #region Editor Settings

        [Header("Wheels")]
        [SerializeField] private List<AxleInfo> axleInfos;

        [Header("Gears")]
        [SerializeField] private int minGear;
        [SerializeField] private int maxGear;
        [SerializeField] private float motorTourgePerGear;

        [SerializeField] private float maxSteeringAngle;
        [SerializeField] private float maxBreakTourge;

        #endregion
        #region Public Properties

        public float Steering { get; private set; }
        public float Clutch { get; private set; }
        public float Brake { get; private set; }
        public float Gas { get; private set; }
        public bool CanShift { get; private set; }
        public int Gear 
        {
            get => _gear;
            private set
            {
                if (value < minGear)
                {
                    _gear = minGear;
                }
                else if(value > maxGear)
                {
                    _gear = maxGear;
                }
                else
                {
                    _gear = value;
                }
            }
        }

        #endregion
        #region BackingFields

        private int _gear;

        #endregion
        #region Private Fields

        private CarInput input;

        #endregion


        #region Unity Callbacks

        private void Awake()
        {
            input = GetComponent<CarInput>();
        }

        private void Update()
        {
            Brake = input.Brake;
            Gas = input.Gas;
            Steering = input.Steering;
            Clutch = input.Clutch ? 1f : 0;
            CanShift = input.CanShift;

            if (CanShift && input.Clutch)
            {
                Gear += input.Shift;
            }
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
                    axleInfo.LeftWheel.motorTorque = Gas * Gear * motorTourgePerGear;
                    axleInfo.RightWheel.motorTorque = Gas * Gear * motorTourgePerGear;
                }

                axleInfo.LeftWheel.brakeTorque = input.Brake * maxBreakTourge;
                axleInfo.RightWheel.brakeTorque = input.Brake * maxBreakTourge;
            }
        }

        #endregion
    }
}