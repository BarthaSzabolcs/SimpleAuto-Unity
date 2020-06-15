using System.ComponentModel;
using SimpleCar.GUI.Model;
using SimpleCar.GUI.View;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleCar.GUI.Clock
{
    public class ClockMarkGui : MonoBehaviour
    {
        #region Public Properties

        public ClockMarkModel Model 
        { 
            get => _model;
            set 
            {
                if (_model != value)
                {
                    if (_model != null)
                    {
                        _model.PropertyChanged -= HandleModelPropertyChange;
                    }
                    if (value != null)
                    {
                        value.PropertyChanged += HandleModelPropertyChange;
                    }
                }
                _model = value;
            } 
        }
        public Color Color 
        {
            get => view.Color;
            set
            {
                view.Color = value;
            }
        }
        public string Text 
        {
            get => view.Text;
            set
            {
                view.Text = value;
            }
        }
        public float Value { get; set; }

        #endregion
        #region Backing Fields

        private ClockMarkModel _model;

        #endregion
        #region Private Fields

        private ClockGui clockGui;
        private ClockMarkModel model;
        private ClockMarkView view;

        #endregion


        #region Public Methods

        public void Init(ClockGui clockGui, ClockMarkModel model)
        {
            this.clockGui = clockGui;

            view = new ClockMarkView();
            view.Init(transform);
            Model = model;

        }

        #endregion

        //public ClockMarkGui(Transform parent, GameObject prefab, ClockMarkModel model)
        //{
        //    instance = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
            
        //    markImage = instance.GetComponent<Image>();
        //    textGui = instance.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        //    Color = model.Color;
        //    Text = model.Value.ToString();
        //}


        private void HandleModelPropertyChange(object sender, PropertyChangedEventArgs e)
        {

        }

    }
}
