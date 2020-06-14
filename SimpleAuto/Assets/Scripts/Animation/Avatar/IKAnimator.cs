using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SimpleCar.Animation.Avatar
{
    [Serializable]
    public class IKAnimator
    {
        #region Editor Settings

        [SerializeField] private IKAnimatorInfo _info;

        #endregion
        #region Public Properties

        public IKAnimatorInfo Info => _info;

        #endregion
        #region Private Fields

        private Animator animator;

        #endregion

        public void Init(Animator animator)
        {
            this.animator = animator;
        }

        public void Update()
        {
            Info.CurrentPosition = animator.GetIKPosition(Info.Type);
            Info.CurrentRotation = animator.GetIKRotation(Info.Type);

            if (Info.Goal != null)
            {
                animator.SetIKPositionWeight(Info.Type, 1);
                animator.SetIKRotationWeight(Info.Type, 1);

                var (position, rotation) = Info.NextValues();
                animator.SetIKPosition(Info.Type, position);
                animator.SetIKRotation(Info.Type, rotation);
            }
            else
            {
                animator.SetIKPositionWeight(Info.Type, 0);
                animator.SetIKRotationWeight(Info.Type, 0);
            }
        }
    }
}
