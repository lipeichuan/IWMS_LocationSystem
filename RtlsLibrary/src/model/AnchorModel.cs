using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtlsLibrary.src.model
{
    public class AnchorModel : BaseModel
    {
        public string Sn { get; set; }
        public int SceneId { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }
}
