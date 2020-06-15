using System;
using UnityEngine;

namespace Animation.Avatar.IKGoals
{
    [Serializable]
    public struct LeftFootIKGoals
    {
        #region Editor Settings

        [SerializeField] private Transform _clutch;
        [SerializeField] private Transform _rest;

        #endregion
        #region Public Properties

        public Transform Clutch => _clutch;
        public Transform Rest => _rest;
        
        #endregion
    }
}
