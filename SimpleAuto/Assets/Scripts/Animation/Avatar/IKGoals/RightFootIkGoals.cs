using System;
using UnityEngine;

namespace Animation.Avatar.IKGoals
{
    [Serializable]
    public struct RightFootIKGoals
    {
        #region Editor Settings

        [SerializeField] private Transform _brake;
        [SerializeField] private Transform _gas;
        [SerializeField] private Transform _both;
        [SerializeField] private Transform _rest;

        #endregion
        #region Public Properties

        public Transform Brake => _brake;
        public Transform Gas => _gas;
        public Transform Both => _both;
        public Transform Rest => _rest;

        #endregion
    }
}
