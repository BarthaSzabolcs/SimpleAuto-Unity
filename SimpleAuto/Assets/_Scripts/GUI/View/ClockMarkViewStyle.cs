using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleCar.GUI.View
{
    public enum ClockMarkPosition { Inside, Outside }
    public class ClockMarkViewStyle
    {
        public Image Image { get; set; }
        public ClockMarkPosition Position { get; set; }
    }
}
