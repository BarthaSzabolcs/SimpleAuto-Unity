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

        [Header("Lerp")]
        [SerializeField] private float lerpSpeed;
        [SerializeField] private float attachDistance;

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
                    previousGoal = (transform.TransformPoint(boneTransform.position), boneTransform.rotation);
                    
                    Attached = false;
                }
                else if (_goal != value && value != null)
                {
                    if (Attached == false)
                    {
                        previousGoal = CalculatePreviousGoalMidTransition();
                    }
                    else
                    {
                        previousGoal = CalculatePreviousGoalPostTransition(_goal);
                    }

                    Attached = false;
                }

                if (_goal != value && value != null && Attached == false )
                {
                    lerpMultiplier = CalculateLerpParameters(transform.TransformPoint(previousGoal.Value.position), value.position);
                }

                _goal = value;
            }
        }
        public bool Attached { get; set; }
        public Vector3 CurrentPosition { get; set; }
        public Quaternion CurrentRotation { get; set; }

        #endregion
        #region Backing Fields

        private Transform _goal;

        #endregion
        #region Private Fields

        private Transform transform;
        private (Vector3 position, Quaternion rotation)? previousGoal = new (Vector3, Quaternion)?();
        
        // Lerp
        private float currentlerpValue;
        private float lerpMultiplier;

        #endregion


        #region Public Methods

        public void Init(Transform centerTransform)
        {
            this.transform = centerTransform;
        }

        public (Vector3 position, Quaternion rotation) Transition()
        {
            if (Goal.name == "IK_SteeringWheel_Right")
            {
                int asd = 0;
            }

            if (Attached)
            {
                return (Goal.position, Goal.rotation);
            }
            else
            {
                currentlerpValue += (lerpSpeed / lerpMultiplier) * Time.deltaTime/* (1 / Time.deltaTime)*/;

                var translation = CurrentTranslation();
                var distanceFromGoal = Mathf.Abs(Vector3.Distance(translation.position, Goal.position));

                if (currentlerpValue >= 1 || distanceFromGoal < attachDistance)
                {
                    Attached = true;
                    return (Goal.position, Goal.rotation);
                }
                else
                {
                    return translation;
                }
            }
        }

        public void Deattach()
        {
            Attached = false;
        }

        #endregion
        #region Private Methods

        private (Vector3 position, Quaternion rotation) CurrentTranslation()
        {
            var previousPosition = transform.TransformPoint(previousGoal.Value.position);
            // var previousRotation = Quaternion.Inverse(centerTransform.rotation) * previousGoal.Value.rotation;
            // var previousRotation = centerTransform.rotation * previousGoal.Value.rotation;

            var nextPosition = Vector3.Lerp(previousPosition, Goal.position, currentlerpValue);
            //var nextRotation = Quaternion.Lerp(previousRotation, Goal.rotation, currentlerpValue);

            return (nextPosition, Goal.rotation/*nextRotation*/);
        }

        private float CalculateLerpParameters(Vector3 from, Vector3 to)
        {
            currentlerpValue = 0;
            return Mathf.Abs(Vector3.Distance(from, to));
        }
        

        private (Vector3 position, Quaternion rotation) CalculatePreviousGoalMidTransition()
        {
            var (position, rotation) = CurrentTranslation();
            position = transform.InverseTransformPoint(position);

            return (position, rotation);
        }
        private (Vector3 position, Quaternion rotation) CalculatePreviousGoalPostTransition(Transform goal)
        {
            var position = transform.InverseTransformPoint(goal.position);
            return (position, goal.rotation);
        }

        #endregion
    }
}
