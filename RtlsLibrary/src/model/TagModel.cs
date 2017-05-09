using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtlsLibrary.src.model
{
    public class TagModel : BaseModel
    {
        public string Sn { get; set; }
        public bool IsPositioned { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }
}
