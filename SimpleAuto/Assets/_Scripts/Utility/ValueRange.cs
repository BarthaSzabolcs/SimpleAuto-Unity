using System;
using System.Diagnostics;
using UnityEngine;

namespace Utility
{
    [Serializable]
    public struct ValueRange<T> where T : IComparable
    {
        [SerializeField] private T min;
        [SerializeField] private T max;
        [SerializeField] private bool inclusive;

        #region Properties

        public T Min => min;
        public T Max => max;
        public bool Inclusive => inclusive;

        #endregion

        [DebuggerStepThrough]
        public bool IsInRange(T value)
        {
            if (inclusive)
            {
                return min.CompareTo(value) <= 0 && max.CompareTo(value) >= 0;
            }
            else
            {
                return min.CompareTo(value) < 0 && max.CompareTo(value) > 0;
            }
        }

        /// <summary>
        /// Clamps the value to the closer bound.
        /// </summary>
        [DebuggerStepThrough]
        public T Clamp(T value)
        {
            if (min.CompareTo(value) >= 0)
            {
                return min;
            }
            else if (max.CompareTo(value) <= 0)
            {
                return max;
            }
            else
            {
                return value;
            }
        }
    }
}
