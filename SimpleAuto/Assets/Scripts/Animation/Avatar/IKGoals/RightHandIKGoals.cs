using System;
using UnityEngine;

namespace Animation.Avatar.IKGoals
{
    [Serializable]
    public struct RightHandIKGoals
    {
        #region Editor Settings

        [SerializeField] private Transform _steeringWheel;
        [SerializeField] private Transform _gearShift;
        [SerializeField] private Transform _handBrake;

        #endregion
        #region Public Properties

        public Transform SteeringWheel => _steeringWheel;
        public Transform GearShift => _gearShift;
        public Transform HandBrake => _handBrake;
        
        #endregion
    }
}
