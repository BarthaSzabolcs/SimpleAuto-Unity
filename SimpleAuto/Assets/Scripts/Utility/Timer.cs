using System;
using UnityEngine;

namespace Utility
{
    [Serializable]
    public class Timer
    {
        #region Editor Settings

        [SerializeField] private float _interval;
        [SerializeField] private bool _autoReset;
        [SerializeField] private bool _clamp;

        #endregion
        #region Public Properties

        public float Interval
        {
            get => _interval;
            set { _interval = value; }
        }
        public bool AutoReset
        {
            get => _autoReset;
            set { _autoReset = value; }
        }
        public bool Clamp
        {
            get => _clamp;
            set { _clamp = value; }
        }

        public Action ActionWhenElapsed { get; set; }

        private float _elapsedTime;
        public float ElapsedTime
        {
            get => _elapsedTime;
            private set
            {
                // ToDo - optimize
                if (ActionWhenElapsed != null && _elapsedTime < Interval && value > Interval)
                {
                    ActionWhenElapsed.Invoke();
                }
                if (AutoReset && value >= Interval)
                {
                    _elapsedTime = value - Interval;
                }
                else if (Clamp && value >= Interval)
                {
                    _elapsedTime = Interval;
                }
                else if (Clamp && value < 0)
                {
                    _elapsedTime = 0;
                }
                else
                {
                    _elapsedTime = value;
                }
            }
        }
        public bool Elapsed { get => ElapsedTime >= Interval; }
        public float ElapsedPercentage { get => ElapsedTime / Interval; }

        #endregion

        public Timer() { }
        public Timer(float time, bool autoReset, bool startElapsed)
        {
            Interval = time;
            AutoReset = autoReset;

            if (startElapsed)
            {
                ElapsedTime = time;
            }
        }

        public void Tick(float deltaTime)
        {
            ElapsedTime += deltaTime;
        }

        public void Reset(float resetTime = 0)
        {
            ElapsedTime = resetTime;
        }
    }
}