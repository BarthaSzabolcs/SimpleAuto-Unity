using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SimpleCar.GUI.Model
{
    public class ClockMarkModel : INotifyPropertyChanged
    {
        public float Value { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
