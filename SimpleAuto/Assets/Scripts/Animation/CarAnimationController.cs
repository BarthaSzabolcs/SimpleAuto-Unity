using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SimpleCar.Animation
{
    public class CarAnimationController : MonoBehaviour
    {
        #region Editor Settings

        [SerializeField] private CarWheelAnimator[] wheels;

        #endregion


        #region Unity Callbacks

        public void Update()
        {
            foreach (var wheel in wheels)
            {
                wheel.Update();
            }
        }

        #endregion
    }
}
