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
                    SyncView(value);
                }
                _model = value;
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

        private List<ClockMarkGui> marks = new List<ClockMarkGui>();
        private ClockView view;

        #endregion


        #region Public Methods

        public void Init(ClockModel model)
        {
            view = new ClockView(requiredViewComponents);
            Model = model;
        }
        private void SyncView(ClockModel model)
        {
            view.PointerValue = model.Percent;
        }
        //public void  SyncView(ClockModel model)
        //{
        //    var fillAngle = (model.MaxValue - model.MinValue) / 360;

        //    var currentValue = model.MinValue;
        //    while (currentValue <= model.MaxValue)
        //    {
        //        var markModel = new ClockMarkModel()
        //        {
        //            Color = currentValue >  model.ExtremeValueLimit ? 
        //            markExtremeColor : markColorNormal,
        //            Value = currentValue
        //        };
        //        var markControl = new ClockMarkGui(transform, mark, markModel);
        //        clockMarks.Add(markControl);

        //        currentValue += markDistance;
        //    }
        //}

        #endregion

        private void HandleModelPropertyChange(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ClockModel.Value):
                    view.PointerValue = Model.Percent;
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
