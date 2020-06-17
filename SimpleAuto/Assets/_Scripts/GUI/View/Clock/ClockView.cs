﻿using System;
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
        public float ExtremeValuePercentage
        {
            get => _extremeValuePercentage;
            set
            {
                _extremeValuePercentage = value;

                RedrawRimExtreme();
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
        #region Private Properties

        private float ValueRange => _maxValue - _minValue;

        #endregion
        #region Backing Fields

        private ClockViewStyle _style;
        
        // Face
        private float _currentValue;
        private float _minValue;
        private float _maxValue;
        private float _extremeValuePercentage;

        private bool _flip;
        
        #endregion
        #region Private Fields

        private ClockViewComponents components;
        private List<ClockMarkView> marks = new List<ClockMarkView>();

        #endregion


        #region Public Methods

        public ClockView(ClockViewComponents components, ClockViewStyle clockStyle, 
            float minDisplayedValue, float maxDisplayedValue, float extremePercentage)
        {
            // Init
            this.components = components;

            _minValue = minDisplayedValue;
            _maxValue = maxDisplayedValue;
            _extremeValuePercentage = extremePercentage;
            
            // Set style, and redraw.
            Style = clockStyle;
        }

        #endregion
        #region Private Methods

        private void FullRedraw()
        {
            // Colors
            components.Mask.color = Style.maskColor;
            components.Rim.color = Style.rimColor;
            components.RimExtreme.color = Style.rimExtremeColor;
            components.PointerBase.color = Style.pointerBaseColor;
            components.Pointer.color = Style.pointerColor;

            // Sprites
            components.Mask.sprite = Style.mask;
            components.Rim.sprite = Style.rim;
            components.RimExtreme.sprite = Style.rimExtreme;
            components.PointerBase.sprite = Style.pointerBase;
            components.Pointer.sprite = Style.pointer;

            RedrawFace();
            RedrawMarks();
        }

        private void RedrawFace()
        {
            var fillAmount = Style.faceAngleLength / 360;
            components.Mask.fillAmount = fillAmount;

            RedrawRimExtreme();

            components.Mask.rectTransform.localEulerAngles = Style.faceAngleOffset * Vector3.forward;
            // components.RimExtreme.rectTransform.localEulerAngles = Style.faceAngleLength * Vector3.forward;

            RedrawPointer(CurrentValue);
        }
        private void RedrawPointer(float percent)
        {
            var rotationPercent = CalculateRotationPercent(percent);

            components.Pointer.rectTransform.localEulerAngles = 
                Vector3.forward * (Style.faceAngleLength * rotationPercent + Style.faceAngleOffset);

            if (Style.pointerMatchValueColor)
            {
                components.Pointer.color = percent > ExtremeValuePercentage ?
                    Style.rimExtremeColor : Style.rimColor;
            }
        }
        
        private void RedrawRimExtreme()
        {
            var rimExtremeFillAmount = (1 - ExtremeValuePercentage) * (Style.faceAngleLength / 360);
            components.RimExtreme.fillAmount = rimExtremeFillAmount;
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
                    var markInstance = GameObject.Instantiate(components.MarkPrefab, components.MarkTransform);
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
                RefreshMarkTextSize(i);
            }
        }
        private void RefreshMarkValue(int index)
        {
            float percent = (index) / (marks.Count - 1f);
            var displayValue = percent * (MaxValue - MinValue);
            var eulerZ = Style.faceAngleLength * CalculateRotationPercent(percent) + Style.faceAngleOffset;
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
            marks[index].UseExtremeColor = marks[index].Value.display > ValueRange * ExtremeValuePercentage;
        }
        private void RefreshMarkTextSize(int index)
        {
            // ToDo - Test only
            var canvasHeight = components.Canvas.GetComponent<RectTransform>().rect.height;
            marks[index].TextSize = canvasHeight * Style.textSizeRatio;
        }

        #endregion

        private float CalculateRotationPercent(float percent)
        {
            return Style.leftToRight ? 1 - percent: percent;
        }

        #endregion
    }
}