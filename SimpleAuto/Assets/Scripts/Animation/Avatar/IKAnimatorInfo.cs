using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SimpleCar.Animation.Avatar
{
    [Serializable]
    public class IKAnimatorInfo
    {
        #region Editor Settings

        [SerializeField] private AvatarIKGoal _type;
        [SerializeField] private float goalLerp;

        #endregion
        #region Public Properties

        public AvatarIKGoal Type => _type;
        public Transform Goal 
        {
            get => _goal; 
            set
            {
                if (_goal != value && _goal != null)
                {
                    previousGoal = (_goal.position, _goal.rotation);
                    currentlerpValue = 0;
                    GoalReached = false;
                }
                _goal = value;
            }
        }
        public bool GoalReached { get; private set; }
        public Vector3 CurrentPosition { get; set; }
        public Quaternion CurrentRotation { get; set; }

        #endregion
        #region Backing Fields

        private Transform _goal;
        private bool _goalReached;

        #endregion
        #region Private Fields

        private (Vector3 position, Quaternion rotation)? previousGoal = new (Vector3, Quaternion)?();
        private float currentlerpValue;

        #endregion

        #region Public Methods

        // ToDo - rename
        public (Vector3 position, Quaternion rotation) NextValues()
        {
            if (previousGoal.HasValue)
            {
                currentlerpValue += goalLerp * Time.deltaTime;

                if (currentlerpValue >= 1)
                {
                    previousGoal = (Goal.position, Goal.rotation);
                    GoalReached = true;
                    return (Goal.position, Goal.rotation);
                }
                else
                {
                    return (Vector3.Lerp(previousGoal.Value.position, Goal.position, currentlerpValue),
                        Quaternion.Lerp(previousGoal.Value.rotation, Goal.rotation, currentlerpValue));
                }
            }
            else
            {
                return (Goal.position, Goal.rotation);
            }
        }

        #endregion
    }
}
