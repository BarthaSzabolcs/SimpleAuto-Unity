using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleCar.GUI.Model;
using SimpleCar.GUI.View;
using UnityEngine;

namespace SimpleCar.GUI.Clock
{
    public class ClockGui : MonoBehaviour
    {
        #region Editor Settings

        [Header("View")]
        [SerializeField] private ClockViewComponents requiredViewComponents;

        #endregion
        #region Public Properties

        public ClockModel Model 
        { 
            get => _model; 
            set
            {
                if (_model != null)
                {
                    _model.PropertyChanged -= HandleModelPropertyChange;
                }
                if (value != null && _model != value)
                {
                    value.PropertyChanged += HandleModelPropertyChange;
                    
                    if (view != null)
                    {
                        SyncView(value);
                    }
                }
                _model = value;
            }
        }
        public ClockViewStyle Style 
        {
            get => view.Style;
            set
            {
                if (value != view.Style)
                {
                    view.Style = value;
                }
            }
        }

        // View wrappers
        public Color PointerColor { get => view.PointerColor; set { view.PointerColor = value; } }
        public Color FaceColor { get => view.FaceColor; set { view.FaceColor = value; } }
        public Color MarkColor { get => view.MarkColor; set { view.MarkColor = value; } }
        public Color ExtremMarkColor { get => view.ExtremMarkColor; set { view.ExtremMarkColor = value; } }
        public float FaceAngleOffset { get => view.FaceAngleOffset; set { view.FaceAngleOffset = value; } }
        public float FaceAngleLength { get => view.FaceAngleLength; set { view.FaceAngleLength = value; } }
        public bool Flip { get => view.Flip; set { view.Flip = value; } }

        #endregion
        #region Backing Fields

        private ClockModel _model;

        #endregion
        #region Private Fields

        private ClockView view;

        #endregion


        #region Public Methods

        public void Init(ClockModel model, ClockViewStyle style)
        {
            Model = model;
            view = new ClockView(requiredViewComponents, style);
        }
        private void SyncView(ClockModel model)
        {
            view.CurrentValue = model.Percent;
        }

        #endregion

        private void HandleModelPropertyChange(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ClockModel.Value):
                    view.CurrentValue = Model.Percent;
                    break;
  
                case nameof(ClockModel.Min):
                    
                    break;

                case nameof(ClockModel.Max):
                    break;

                case nameof(ClockModel.ExtremeValueLimit):

                    break;
            }
        }

    }
}
