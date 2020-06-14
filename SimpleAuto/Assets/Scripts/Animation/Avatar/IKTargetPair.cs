using System;
using UnityEngine;

namespace SimpleCar.Animation.Avatar
{
    [Serializable]
    public class IKTargetPair
    {
        #region Editor Settings

        [SerializeField] private Transform _rest;
        [SerializeField] private Transform _active;

        #endregion
        #region Public Properties

        public Transform Rest => _rest;
        public Transform Active => _active;
        
        #endregion
    }
}
