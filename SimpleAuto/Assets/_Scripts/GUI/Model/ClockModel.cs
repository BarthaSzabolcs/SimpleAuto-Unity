using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace SimpleCar.GUI.Model.Clock
{
    public class ClockModel : ModelBase
    {
        #region Public Properties

        public float Min
        {
            get => _min;
            set
            {
                Evaluate(ref _min, value);
            }
        }
        public float Max
        {
            get => _max;
            set
            {
                Evaluate(ref _max, value);
            }
        }
        public float ExtremeValueLimit
        {
            get => _extremeValueLimit;
            set
            {
                Evaluate(ref _extremeValueLimit, value);
            }
        }
        public float Value 
        { 
            get => _value;
            set
            {
                Evaluate(ref _value, value);
            } 
        }
        public float Percent => _value / (_max - _min);
        public float Range => _max - _min;

        #endregion
        #region Backing Fields

        private float _min;
        private float _max;
        private float _extremeValueLimit;
        private float _value;

        #endregion
    }
}
