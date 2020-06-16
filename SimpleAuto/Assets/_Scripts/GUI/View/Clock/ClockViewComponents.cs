using System;
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
        [SerializeField] private Image _faceImage;
        [SerializeField] private Image _faceMaskImage;
        [SerializeField] private GameObject _markPrefab;

        #endregion
        #region Public Properties

        public GameObject MarkPrefab => _markPrefab;  
        public Canvas Canvas => _canvas;
        public Image Pointer => _pointerImage;
        public Image Face => _faceImage;
        public Image FaceMask => _faceMaskImage;

        #endregion
    }
}
