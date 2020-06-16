using System;
using System.Collections.Generic;
using SimpleCar.GUI.View.ClockMark;
using UnityEngine;

namespace SimpleCar.GUI.View.Clock
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

        // Marks
        public float MinValue
        {
            get => _minValue;
            set
            {
                if (value != _minValue)
                {
                    _minValue = value;

                    for (int i = 0; i < marks.Count; i++)
                    {
                        RefreshMarkValue(i);
                    }
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

                    for (int i = 0; i < marks.Count; i++)
                    {
                        RefreshMarkValue(i);
                    }
                }
            }
        }
        public float CurrentValue
        {
            get => _currentValue;
            set
            {
                _currentValue = value;
                RedrawPointer(value);
            }
        }
        public float ExtremeValueLimit
        {
            get => _extremeValueLimit;
            set
            {
                _extremeValueLimit = value;

                for (int i = 0; i < marks.Count; i++)
                {
                    RefreshMarkColor(i);
                }
            }
        }
        public bool Flip 
        {
            get => _flip;
            set
            {
                if (_flip != value)
                {
                    _flip = value;

                    var scale = components.Canvas.transform.localScale;
                    scale.x = Mathf.Abs(scale.x) *(_flip ? -1 : 1);

                    components.Canvas.transform.localScale = scale;
                }
            }
        }

        #endregion
        #region Backing Fields

        private ClockViewStyle _style;
        
        // Face
        private float _currentValue;
        private float _minValue;
        private float _maxValue;
        private float _extremeValueLimit;

        private bool _flip;
        
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
            components.Face.color = Style.faceColor;
            components.Pointer.color = Style.pointerColor;
            RedrawFace();
            RedrawMarks();
        }

        private void RedrawFace()
        {
            var fillAmount = Style.faceAngleLength / 360;

            components.FaceMask.fillAmount = fillAmount;
            components.FaceMask.rectTransform.localEulerAngles = Style.faceAngleOffset * Vector3.forward;
            components.Face.rectTransform.localEulerAngles = Style.faceAngleOffset * Vector3.forward;

            RedrawPointer(CurrentValue);
        }
        private void RedrawPointer(float percent)
        {
            components.Pointer.rectTransform.localEulerAngles = 
                Vector3.forward * (Style.faceAngleLength * percent + Style.faceAngleOffset);
        }
        
        #region Mark Redraw

        private void RedrawMarks()
        {
            RefreshMarkCount();
            RefreshMarkValues();
        }
        private void RefreshMarkCount()
        {
            var marksNeeded = Style.marksBetweenTheExtremes + 2;
            if (marks.Count > marksNeeded)
            {
                while (marks.Count > marksNeeded)
                {
                    marks[0].Destroy();
                    marks.RemoveAt(0);
                }
            }
            else if(marks.Count < marksNeeded)
            {
                while(marks.Count < marksNeeded)
                { 
                    var markInstance = GameObject.Instantiate(components.MarkPrefab, components.Canvas.transform);
                    var markView = new ClockMarkView();
                    markView.Init(markInstance.transform, Style.markStyle);
                    marks.Add(markView);
                }
            }
        }
        private void RefreshMarkValues()
        {
            for (int i = 0; i < marks.Count; i++)
            {
                RefreshMarkValue(i);
                RefreshMarkLength(i);
                RefreshMarkVisibility(i);
                RefreshMarkColor(i);
            }
        }
        private void RefreshMarkValue(int index)
        {
            float percent = (index) / (marks.Count - 1f);
            var displayValue = percent * (MaxValue - MinValue);
            var eulerZ = Style.faceAngleLength * percent + Style.faceAngleOffset;
            marks[index].Value = (eulerZ, displayValue);
        }
        private void RefreshMarkLength(int index)
        {
            marks[index].Long = index % Style.longMarkFrequency == 0;
        }
        private void RefreshMarkVisibility(int index)
        {
            marks[index].ShowValue = marks[index].Long || Style.showShortMarksValue;
        }
        private void RefreshMarkColor(int index)
        {
            marks[index].UseExtremeColor = marks[index].Value.display > ExtremeValueLimit;
        }

        #endregion

        #endregion
    }
}