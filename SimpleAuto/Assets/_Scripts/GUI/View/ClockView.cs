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
        public float PointerValue
        {
            get => _pointerValue;
            set
            {
                _pointerValue = value;
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

        private float _faceAngleOffset;
        private float _faceAngleLength = 360;
        private float _pointerValue;
        
        // Colors
        private Color _pointerColor;
        private Color _faceColor;
        private Color _markColor;
        private Color _extremMarkColor;

        private bool _flip;

        #endregion
        #region Private Fields

        private ClockViewComponents components;
        private List<ClockMarkGui> marks = new List<ClockMarkGui>();

        // Pointer
        private float pointerOffset;

        #endregion


        #region Public Methods

        public ClockView(ClockViewComponents components)
        {
            this.components = components;

            FullRedraw();
        }

        #endregion
        #region Private Methods

        public void FullRedraw()
        {
            components.Face.color = FaceColor;
            components.Pointer.color = PointerColor;
            RedrawFace();
            RedrawMarks();
        }

        private void RedrawFace()
        {
            var faceAngle = FaceAngleLength / 360;

            components.FaceMask.fillAmount = faceAngle;
            components.FaceMask.rectTransform.localEulerAngles = FaceAngleOffset * Vector3.forward;
            components.Face.rectTransform.localEulerAngles = FaceAngleOffset * Vector3.forward;

            RotatePointer(PointerValue);
        }
        private void RotatePointer(float percent)
        {
            components.Pointer.rectTransform.localEulerAngles = Vector3.forward * percent * FaceAngleLength;
        }
        private void RedrawMarks()
        {

        }

        #endregion

        //private void CreateMarks(ValueRange<float> range, float valueStep, bool round)
        //{
        //    var baseRotation = Quaternion.Euler(0, 0, _faceAngles.Min);
        //    var currentValue = range.Min;

        //    while (currentValue <= range.Max)
        //    {
        //        var markModel = new ClockMarkModel()
        //        {
        //            Color = currentValue > model.ExtremeValueLimit ?
        //            markExtremeColor : markColorNormal,
        //            Value = currentValue
        //        };
        //        var markControl = new ClockMarkGui(transform, mark, markModel);
        //        clockMarks.Add(markControl);

        //        currentValue += valueStep;
        //    }
        //}
    }
}
