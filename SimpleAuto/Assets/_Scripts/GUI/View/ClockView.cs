using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleCar.GUI.Clock;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace SimpleCar.GUI.View
{
    [Serializable]
    public class ClockView
    {
        #region Public Properties
 
        public ClockViewStyle Style 
        { 
            get => _style;
            set 
            {
                if (_style != value)
                {
                    _style = value;
                    FullRedraw();
                }
            }
        }

        // Face
        public float FaceAngleOffset
        {
            get => _faceAngleOffset;
            set
            {
                if (_faceAngleOffset != value)
                {
                    _faceAngleOffset = value;
                    RedrawFace();
                }
            }
        }
        public float FaceAngleLength
        {
            get => _faceAngleLength;
            set
            {
                if (_faceAngleLength != value)
                {
                    _faceAngleLength = value;
                    RedrawFace();
                }
            }
        }

        // Marks
        public float MinValue
        {
            get => _minValue;
            set
            {
                if (value != _minValue)
                {
                    _minValue = value;
                    RedrawMarks();
                }
            }
        }
        public float MaxValue
        {
            get => _maxValue;
            set
            {
                if (value != _maxValue)
                {
                    _maxValue = value;
                    RedrawMarks();
                }
            }
        }
        public float CurrentValue
        {
            get => _currentValue;
            set
            {
                _currentValue = value;
                RotatePointer(value);
            }
        }

        // Colors
        public Color PointerColor
        {
            get => _pointerColor;
            set
            {
                if (value != _pointerColor)
                {
                    components.Pointer.color = value;
                }
                _pointerColor = value;
            }
        }
        public Color FaceColor
        {
            get => _faceColor;
            set
            {
                if (value != _faceColor)
                {
                    components.Face.color = value;
                }
                _faceColor = value;
            }
        }
        public Color MarkColor
        {
            get => _markColor;
            set
            {
                if (value != _markColor)
                {
                    // ToDo - Refersh mark colo
                    // components.Pointer.color = value;
                }
                _markColor = value;
            }
        }
        public Color ExtremMarkColor
        {
            get => _extremMarkColor;
            set
            {
                if (value != _extremMarkColor)
                {
                    // ToDo - Refresh extreme mark color
                    // components.Pointer.color = value;
                }
                _extremMarkColor = value;
            }
        }

        public bool Flip
        {
            get => components.Canvas.transform.localScale.x < 0;
            set
            {
                if (_flip != value)
                {
                    var localScale = components.Canvas.transform.localScale;

                    components.Canvas.transform.localScale = new Vector3(
                        Mathf.Abs(localScale.x) * (value ? -1 : 1),
                        localScale.y,
                        localScale.z);
                }
                _flip = value;
            }
        }

        #endregion
        #region Backing Fields

        // Face
        private float _faceAngleOffset;
        private float _faceAngleLength = 360;
        private float _currentValue;
        private float _minValue;
        private float _maxValue;

        // Colors
        private Color _pointerColor;
        private Color _faceColor;
        private Color _markColor;
        private Color _extremMarkColor;

        // All
        private bool _flip;
        
        private ClockViewStyle _style;

        #endregion
        #region Private Fields

        private ClockViewComponents components;
        private List<ClockMarkView> marks = new List<ClockMarkView>();

        #endregion


        #region Public Methods

        public ClockView(ClockViewComponents components, ClockViewStyle clockStyle)
        {
            this.components = components;
            Style = clockStyle;
        }

        #endregion
        #region Private Methods

        private void FullRedraw()
        {
            components.Face.color = Style.FaceColor;
            components.Pointer.color = Style.PointerColor;
            RedrawFace();
            RedrawMarks();
        }
        private void RedrawFace()
        {
            var faceAngle = Style.FaceAngleLength / 360;

            components.FaceMask.fillAmount = faceAngle;
            components.FaceMask.rectTransform.localEulerAngles = Style.FaceAngleOffset * Vector3.forward;
            components.Face.rectTransform.localEulerAngles = Style.FaceAngleOffset * Vector3.forward;

            RotatePointer(CurrentValue);
        }
        private void RotatePointer(float percent)
        {
            components.Pointer.rectTransform.localEulerAngles = Vector3.forward * percent * Style.FaceAngleLength;
        }
        private void RedrawMarks()
        {
            // ToDo - Refactor for less garbage.
            // Clear previous Marks. 
            foreach (var mark in marks)
            {
                mark.Destroy();
            }
            marks.Clear();

            // The start and the end of the face.
            var rotationStart = Quaternion.Euler(0, 0, Style.FaceAngleOffset);
            var rotationEnd = Quaternion.Euler(0, 0, Style.FaceAngleOffset + Style.FaceAngleLength);

            var displayValue = Style.MinValue;
            while (displayValue <= Style.MaxValue)
            {
                // Place and Init mark.
                var markInstance = GameObject.Instantiate(components.MarkPrefab, components.Canvas.transform);
                var markView = new ClockMarkView();
                markView.Init(markInstance.transform, Style);

                // Calculate rotation.
                var rotation = Quaternion.Lerp(rotationStart, rotationEnd, displayValue / Style.MaxValue);
                // Set the rotation and display value of the recently placed mark.
                markView.Value = (rotation, displayValue);

                marks.Add(markView);

                // Increase the displayValue, determining where the next mark will be placed.
                displayValue += Style.MarkDistance;
            }
        }

        #endregion
    }
}