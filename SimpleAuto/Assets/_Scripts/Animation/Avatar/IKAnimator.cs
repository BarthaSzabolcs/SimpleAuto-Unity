﻿using System;
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
            Info.Init(animator.transform);
        }

        public void Update()
        {
            if (Info.Goal != null)
            {
                animator.SetIKPositionWeight(Info.Type, 1);
                animator.SetIKRotationWeight(Info.Type, 1);

                var (position, rotation) = Info.Transition();
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
