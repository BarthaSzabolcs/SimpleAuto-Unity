using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SimpleCar.Input
{
    public class CarInput : MonoBehaviour
    {
        #region Editor Settings

        [SerializeField] private string motorAxisName;
        [SerializeField] private string steeringAxisName;

        #endregion
        #region Public Properties

        public float Motor { get; set; }
        public float Steering { get; set; }

        #endregion

        #region Unity Callbacks

        private void Update()
        {
            Motor = UnityEngine.Input.GetAxis(motorAxisName);
            Steering = UnityEngine.Input.GetAxis(steeringAxisName);
        }

        #endregion
    }
}
