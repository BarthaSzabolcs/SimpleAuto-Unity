using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCar.GUI.Model
{
    public class ModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected T Evaluate<T>(T currentValue, T newValue, [CallerMemberName] string propertyName = "") 
            where T: IEquatable<T>
        {
            if (currentValue.Equals(newValue) == false)
            {
                NotifyPropertyChanged(propertyName);
            }

            return newValue;
        }
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
