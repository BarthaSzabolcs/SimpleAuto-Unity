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

        [Header("Grab")]
        [SerializeField] private AnimationCurve grabCurve;
        [SerializeField] private Timer grabTimer;
        [SerializeField] private float interpolationGain;

        #endregion
        #region Public Properties

        public float Grab { get; set; }
        public IKAnimator LeftHand => _leftHandIk;
        public IKAnimator RightHand => _rightHandIK;

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
        }

        private void OnAnimatorIK(int layerIndex)
        {
            LeftHand.Update();
            RightHand.Update();
        }

        #endregion

        
        private void RefreshGrab()
        {
            animator.SetFloat(nameof(Grab), grabCurve.Evaluate(grabTimer.ElapsedPercentage));
        }
    }
}
