using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Utility;

namespace Control.Camera
{
    [Serializable]
    public struct CameraSetting
    {
        #region Editor Settings

        [SerializeField] private ValueRange<float> _pitchBounds;
        [SerializeField] private ValueRange<float> _yawBounds;
        [SerializeField] private Vector3 _position;

        #endregion
        #region Public Properties

        public ValueRange<float> PitchBounds => _pitchBounds;
        public ValueRange<float> YawBounds => _yawBounds;
        public Vector3 Position => _position;

        #endregion
    }
}
