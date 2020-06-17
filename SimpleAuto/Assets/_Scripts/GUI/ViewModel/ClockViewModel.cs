using System.ComponentModel;
using SimpleCar.GUI.Model.Clock;
using SimpleCar.GUI.View.Clock;
using UnityEngine;

namespace SimpleCar.GUI.ViewModel.Clock
{
    public class ClockViewModel : MonoBehaviour
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
        
        // View wrappers
        public ClockViewStyle Style { get => view.Style; set { view.Style = value; } }
        public float MinDisplayValue { get => view.MinValue; set { view.MinValue = value; } }
        public float MaxDisplayValue { get => view.MaxValue; set { view.MaxValue = value; } }
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

            view = new ClockView(
                components: requiredViewComponents,
                clockStyle: style,
                minDisplayedValue: model.Min,
                maxDisplayedValue: model.Max,
                extremePercentage: model.ExtremeValueLimit);
        }
        private void SyncView(ClockModel model)
        {
            view.CurrentValue = model.Percent;
        }

        #endregion
        #region Private Methods

        private void HandleModelPropertyChange(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ClockModel.Value):
                    view.CurrentValue = Model.Percent;
                    break;
  
                //case nameof(ClockModel.Min):
                //    view.MinValue = Model.Min;
                //    break;

                //case nameof(ClockModel.Max):
                //    view.MaxValue = Model.Percent;
                //    break;

                case nameof(ClockModel.ExtremeValueLimit):
                    view.ExtremeValuePercentage = CalculateDisplayExtremeValue();
                    break;
            }
        }

        private float CalculateDisplayExtremeValue()
        {
            var displayRange = MaxDisplayValue - MinDisplayValue;
            var valueRange = Model.Max - Model.Min;
            var ratio = displayRange / valueRange;

            return Model.ExtremeValueLimit * ratio;
        }

        #endregion
    }
}
