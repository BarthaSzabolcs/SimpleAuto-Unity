using System;
using UnityEngine;

namespace Animation.Avatar.IKGoals
{
    [Serializable]
    public struct LeftHandIKGoals
    {
        #region Editor Settings

        [SerializeField] private Transform _steeringWheel;

        #endregion
        #region Public Properties

        public Transform SteeringWheel => _steeringWheel;

        #endregion
    }
}
