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
        [SerializeField] private Transform boneTransform;

        [Header("Interpolation")]
        [SerializeField] private float lerpSpeed;
        [SerializeField] private float attachDistance;
        [SerializeField] private float deattachDistance;

        #endregion
        #region Public Properties

        public AvatarIKGoal Type => _type;
        public Transform Goal 
        {
            get => _goal; 
            set
            {
                if (_goal == null)
                {
                    previousGoal = CalculatePreviousGoal(boneTransform);
                    
                    Attached = false;
                }
                else if (_goal != value && value != null)
                {
                    if (Attached == false)
                    {
                        previousGoal = CalculatePreviousGoal(boneTransform);
                    }
                    else
                    {
                        previousGoal = CalculatePreviousGoal(boneTransform);
                    }

                    Attached = false;
                }

                if (_goal != value && value != null && Attached == false )
                {
                    goalDistanceAtLerpBegin = CalculateLerpParameters(transform.TransformPoint(previousGoal.Value.position), value.position);
                }

                _goal = value;
            }
        }
        public bool Attached { get; set; }

        #endregion
        #region Backing Fields

        private Transform _goal;

        #endregion
        #region Private Fields

        private Transform transform;
        private (Vector3 position, Quaternion rotation)? previousGoal = new (Vector3, Quaternion)?();
        
        // Lerp
        private float currentlerpValue;
        private float goalDistanceAtLerpBegin;

        #endregion


        #region Public Methods

        public void Init(Transform transform)
        {
            this.transform = transform;
        }

        public (Vector3 position, Quaternion rotation) Transition()
        {
            Attached = IsAttached(); 

            if (Attached)
            {
                return (Goal.position, Goal.rotation);
            }
            else
            {
                currentlerpValue += (lerpSpeed / goalDistanceAtLerpBegin) * Time.deltaTime;

                var translation = CurrentTranslation();

                if (currentlerpValue >= 1)
                {
                    return (Goal.position, Goal.rotation);
                }
                else
                {
                    return translation;
                }
            }
        }
        
        private bool IsAttached()
        {
            var distanceFromGoal = Mathf.Abs(Vector3.Distance(boneTransform.position, Goal.position));
            if (Attached && distanceFromGoal > deattachDistance)
            {
                return false;
            }

            if (Attached == false && distanceFromGoal < attachDistance)
            {
                return true;
            }

            return Attached;
        }

        #endregion
        #region Private Methods

        private (Vector3 position, Quaternion rotation) CurrentTranslation()
        {
            var previousPosition = transform.TransformPoint(previousGoal.Value.position);
            var nextPosition = Vector3.Slerp(previousPosition, Goal.position, currentlerpValue);

            var previousRotation = previousGoal.Value.rotation;
            // KnownBug - Next rotation is incorrect, does not follow the shortest path.
            var nextRotation = Quaternion.Slerp(previousRotation, Goal.rotation, currentlerpValue);

            return (nextPosition, Goal.rotation/*nextRotation*/);
        }

        private float CalculateLerpParameters(Vector3 from, Vector3 to)
        {
            currentlerpValue = 0;
            return Mathf.Abs(Vector3.Distance(from, to));
        }
        
        private (Vector3 position, Quaternion rotation) CalculatePreviousGoal(Transform goal)
        {
            var position = transform.InverseTransformPoint(goal.position);
            return (position, goal.rotation);
        }

        #endregion
    }
}