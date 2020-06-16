using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SimpleCar.GUI.View
{
    [Serializable]
    public class ClockViewStyle : ScriptableObject
    {
        // Colors
        public Color PointerColor { get; set; }
        public Color FaceColor { get; set; }
        public Color MarkColor { get; set; }
        public Color ExtremMarkColor { get; set; }

        // Marks
        public bool RoundMarkValue { get; set; }
        public float MinValue { get; set; }
        public float MaxValue { get; set; }
        public float ExtremeValueMargin { get; set; }
        public float MarkDistance { get; set; }
        
        // Face
        public float FaceAngleOffset { get; set; }
        public float FaceAngleLength { get; set; }
        
        public bool Flip { get; set; }

        //public Color PointerColor { get => view.PointerColor; set { view.PointerColor = value; } }
        //public Color FaceColor { get => view.FaceColor; set { view.FaceColor = value; } }
        //public Color MarkColor { get => view.MarkColor; set { view.MarkColor = value; } }
        //public Color ExtremMarkColor { get => view.ExtremMarkColor; set { view.ExtremMarkColor = value; } }
        //public float FaceAngleOffset { get => view.FaceAngleOffset; set { view.FaceAngleOffset = value; } }
        //public float FaceAngleLength { get => view.FaceAngleLength; set { view.FaceAngleLength = value; } }
        //public bool Flip { get => view.Flip; set { view.Flip = value; } }
    }
}
