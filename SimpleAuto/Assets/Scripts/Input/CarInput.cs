using UnityEngine;

namespace SimpleCar.Input
{
    public class CarInput : MonoBehaviour
    {
        #region Editor Settings

        [Header("Axes")]
        [SerializeField] private string motorAxisName;
        [SerializeField] private string steeringAxisName;
        [SerializeField] private string brakeAxisName;

        [Header("Buttons")]
        [SerializeField] private string shiftUpName;
        [SerializeField] private string shiftDownName;
        [SerializeField] private string clutchName;

        #endregion
        #region Public Properties

        public float Gas { get; private set; }
        public float Steering { get; private set; }
        public int Shift { get; private set; }
        public float Brake { get; private set; }
        public bool Clutch { get; private set; }

        #endregion

        #region Unity Callbacks

        private void Update()
        {
            Gas = Mathf.Clamp(UnityEngine.Input.GetAxis(motorAxisName), 0, 1);
            Steering = UnityEngine.Input.GetAxis(steeringAxisName);
            Brake = UnityEngine.Input.GetAxis(brakeAxisName);

            Clutch = UnityEngine.Input.GetButton(clutchName);
            RefreshShift();
        }
        private void RefreshShift()
        {
            if (UnityEngine.Input.GetButtonDown(shiftUpName))
            {
                Shift = 1;
            }
            else if (UnityEngine.Input.GetButtonDown(shiftDownName))
            {
                Shift = -1;
            }
            else
            {
                Shift = 0;
            }
        }

        #endregion
    }
}
