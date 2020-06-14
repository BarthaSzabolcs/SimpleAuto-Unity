using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Utility;

namespace SimpleCar.Animation.Avatar
{
    public class AvatarAnimationController : MonoBehaviour
    {
        #region Editor Settings

        [Header("IKAnimators")]
        [SerializeField] private IKAnimator _leftHandIk;
        [SerializeField] private IKAnimator _rightHandIK;
        [SerializeField] private IKAnimator _rightFootIk;
        [SerializeField] private IKAnimator _leftFootIk;

        #endregion
        #region Public Properties

        public float Grab { get; set; }
        public IKAnimator LeftHand => _leftHandIk;
        public IKAnimator RightHand => _rightHandIK;
        public IKAnimator LeftFoot => _leftFootIk;
        public IKAnimator RightFoot => _rightFootIk;

        #endregion
        #region Private Fields

        private Animator animator;

        #endregion


        #region Unity Callbacks

        private void Awake()
        {
            animator = GetComponent<Animator>();
            
            LeftHand.Init(animator);
            RightHand.Init(animator);
            LeftFoot.Init(animator);
            RightFoot.Init(animator);
        }

        private void OnAnimatorIK(int layerIndex)
        {
            LeftHand.Update();
            RightHand.Update();
            LeftFoot.Update();
            RightFoot.Update();
        }

        #endregion

        
        //private void RefreshGrab()
        //{
        //    animator.SetFloat(nameof(Grab), grabCurve.Evaluate(grabTimer.ElapsedPercentage));
        //}
    }
}
