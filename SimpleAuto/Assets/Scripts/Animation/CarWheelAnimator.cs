using System;
using UnityEngine;

namespace SimpleCar.Animation
{
    [Serializable]
    public class CarWheelAnimator
    {
        #region Editor Setings

        [SerializeField] private WheelCollider collider;
        [SerializeField] private Transform transform;

        #endregion

        #region Public Methods

        public void Update()
        {
            collider.GetWorldPose(out var position, out var rotation);
            
            transform.position = position;
            transform.rotation = rotation;
        }
    
        #endregion
    }
}
