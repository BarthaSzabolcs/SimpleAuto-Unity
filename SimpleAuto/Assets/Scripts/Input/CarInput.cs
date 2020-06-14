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

        [Header("Mouse")]
        [SerializeField] private string mouseYAxisName;
        [SerializeField] private string mouseXAxisName;
        [SerializeField] private float mouseSenitivity;

        [Header("Buttons")]
        [SerializeField] private string shiftUpName;
        [SerializeField] private string shiftDownName;
        [SerializeField] private string clutchName;
        [SerializeField] private string cameraButtonName;
        [SerializeField] private string canShiftName;

        #endregion
        #region Public Properties

        // Car
        public float Gas { get; private set; }
        public float Steering { get; private set; }
        public int Shift { get; private set; }
        public bool CanShift { get; set; }
        public float Brake { get; private set; }
        public bool Clutch { get; private set; }
        public bool ChangeCamera { get; private set; }

        // Camera
        public Vector2 MouseMovement { get; set; }

        #endregion

        #region Unity Callbacks

        private void Update()
        {
            Gas = Mathf.Clamp(UnityEngine.Input.GetAxis(motorAxisName), 0, 1);
            Steering = UnityEngine.Input.GetAxis(steeringAxisName);
            Brake = UnityEngine.Input.GetAxis(brakeAxisName);
            Clutch = UnityEngine.Input.GetButton(clutchName);
            
            RefreshShift();

            MouseMovement = new Vector2()
            {
                x = UnityEngine.Input.GetAxis(mouseXAxisName) * mouseSenitivity,
                y = UnityEngine.Input.GetAxis(mouseYAxisName) * mouseSenitivity,
            };
            ChangeCamera = UnityEngine.Input.GetButtonDown(cameraButtonName);
        }
        private void RefreshShift()
        {
            if (UnityEngine.Input.GetButtonDown(canShiftName))
            {
                CanShift = !CanShift;
            }

            if (CanShift && UnityEngine.Input.GetButtonDown(shiftUpName))
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
