﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleCar.GUI.View.Clock
{
    [Serializable]
    public class ClockViewComponents
    {
        #region Editor Settings

        [SerializeField] private Canvas _canvas;
        [SerializeField] private Image _pointerImage;
        [SerializeField] private Image _pointerBaseImage;
        [SerializeField] private Image _rimImage;
        [SerializeField] private Image _rimExtremeImage;
        [SerializeField] private Image _maskImage;
        [SerializeField] private GameObject _markPrefab;
        [SerializeField] private Transform _markTransform;

        #endregion
        #region Public Properties

        public GameObject MarkPrefab => _markPrefab;  
        public Canvas Canvas => _canvas;
        public Image Pointer => _pointerImage;
        public Image PointerBase => _pointerBaseImage;
        public Image Rim => _rimImage;
        public Image RimExtreme => _rimExtremeImage;
        public Image Mask => _maskImage;
        public Transform MarkTransform => _markTransform;
        
        #endregion
    }
}
