using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace ArshiaPhotoEditorWebsite.Models
{
    public class EditorParameters
    {
        public float Brightness { get; set; }
        public float Contrast { get; set; }
        public float Rcolor { get; set; }
        public float Gcolor { get; set; }
        public float Bcolor { get; set; }
        public string WatermarkText { get; set; }
        public int Size { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
 

    }
}