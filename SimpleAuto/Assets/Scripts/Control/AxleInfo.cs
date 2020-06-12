using System;
using UnityEngine;

namespace SimpleCar.Control 
{ 
    [Serializable]
    public class AxleInfo
    {
        #region Editor Settings

        [SerializeField] private WheelCollider _leftWheel;
        [SerializeField] private WheelCollider _rightWheel;
        [SerializeField] private bool _motor;
        [SerializeField] private bool _steering;

        #endregion
        #region Public Properties

        public WheelCollider LeftWheel => _leftWheel;
        public WheelCollider RightWheel => _rightWheel;
        public bool Motor => _motor;
        public bool Steering => _steering;
        
        #endregion
    }
}