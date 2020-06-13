using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Animation.Avatar
{
    public class AvatarAnimationController : MonoBehaviour
    {
        #region Editor Settings


        #endregion
        #region Public Properties

        public float Grab { get; set; }
        public Transform IKGoalLeftHand { get; set; }
        public Transform IKGoalRightHand { get; set; }

        #endregion
        #region Private Fields

        private Animator animator;

        #endregion


        #region Unity Callbacks

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void OnAnimatorIK(int layerIndex)
        {
            RefreshIk(AvatarIKGoal.LeftHand, IKGoalLeftHand);
            RefreshIk(AvatarIKGoal.RightHand, IKGoalRightHand);
        }

        #endregion

        private void RefreshIk(AvatarIKGoal ikBone, Transform goal)
        {
            if (goal != null)
            {
                animator.SetIKPositionWeight(ikBone, 1);
                animator.SetIKRotationWeight(ikBone, 1);
                animator.SetIKPosition(ikBone, goal.position);
                animator.SetIKRotation(ikBone, goal.rotation);
            }
            else
            {
                animator.SetIKPositionWeight(ikBone, 0);
                animator.SetIKRotationWeight(ikBone, 0);
            }
        }

    }
}
