﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace SimpleCar.GUI.Model
{
    public class ClockModel : ModelBase
    {
        #region Public Properties

        public float Min
        {
            get => _min;
            set
            {
                _min = Evaluate(_min, value);
            }
        }
        public float Max
        {
            get => _max;
            set
            {
                _max = Evaluate(_max, value);
            }
        }
        public float ExtremeValueLimit
        {
            get => _extremeValueLimit;
            set
            {
                _extremeValueLimit = Evaluate(_extremeValueLimit, value);
            }
        }
        public float Value 
        { 
            get => _value;
            set
            {
                _value = Evaluate(_value, value);
            } 
        }
        public float Percent => _value / (_max - _min);

        #endregion
        #region Backing Fields

        private float _min;
        private float _max;
        private float _extremeValueLimit;
        private float _value;

        #endregion
    }
}
