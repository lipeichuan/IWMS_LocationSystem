using Google.Protobuf;
using grpc.location;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWMS_LocationCommon.common.net
{
    public class Encoder
    {
        static private readonly string STX = "TCB";
        static public byte[] Encode(Msg msg)
        {
            byte[] bytes = msg.ToByteArray();
            MemoryStream ms = new MemoryStream();
            //stx
            byte[] stx = Encoding.GetEncoding("gb2312").GetBytes(STX);
            ms.Write(stx, 0, stx.Length);

            //len
            byte[] len = BitConverter.GetBytes((UInt32)bytes.Length);
            Array.Reverse(len);
            ms.Write(len, 0, len.Length);

            //msg
            ms.Write(bytes, 0, bytes.Length);

            return ms.ToArray();
        }
    }
}
