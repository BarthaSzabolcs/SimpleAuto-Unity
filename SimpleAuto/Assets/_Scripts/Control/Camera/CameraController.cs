using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleCar.Input;
using UnityEngine;
using Utility;

namespace SimpleCar.Control.Camera
{
    public class CameraController : MonoBehaviour
    {
        #region Editor Settings

        [SerializeField] private Transform cameraTransform;
        [SerializeField] CameraSetting[] cameraSettings;

        #endregion
        #region Private Fields

        private CarInput input;
        
        #endregion
        #region Private Properties

        private float _pitch;
        private float Pitch
        {
            get => _pitch;
            set
            {
                _pitch = Setting.PitchBounds.Clamp(value);
            }
        }

        private float _yaw;
        private float Yaw
        {
            get => _yaw;
            set
            {
                _yaw = Setting.YawBounds.Clamp(value % 360);
            }
        }

        private int _cameraPositionIndex;
        private int CameraSettingsIndex 
        { 
            get => _cameraPositionIndex; 
            set
            {
                if (value == cameraSettings.Length)
                {
                    _cameraPositionIndex = 0;
                }
                else
                {
                    _cameraPositionIndex = value;
                }
            }
        }

        private CameraSetting Setting => cameraSettings[CameraSettingsIndex];

        #endregion


        #region UnityCallBacks

        private void Awake()
        {
            input = GetComponent<CarInput>();

            cameraTransform.localPosition = Setting.Position;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            UpdatePosition();
            RotateCamera();
        }

        #endregion

        private void UpdatePosition()
        {
            if (input.ChangeCamera)
            {
                CameraSettingsIndex++;
                cameraTransform.localPosition = Setting.Position;
            }
        }

        private void RotateCamera()
        {
            Pitch -= input.MouseMovement.y;
            Yaw = Yaw + input.MouseMovement.x;

            cameraTransform.localEulerAngles = new Vector3(Pitch, Yaw, 0);
        }
    }
}
