using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace RtlsLibrary.src.model
{
    public class SceneModel : BaseModel
    {
        public string Name { get; set; }
        public Point OriginalPoint { get; set; }

        public BitmapImage MapImage { get; set; }
        public double MapOffsetX { get; set; }
        public double MapOffsetY { get; set; }
        public double MapScaleH { get; set; }
        public double MapScaleV { get; set; }
        public bool MapFlipH { get; set; }
        public bool MapFlipV { get; set; }

    }
}
