using grpc.location;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWMS_LocationCommon.common.net
{
    /***********************************
    帧协议格式：
    ------------------------------------
    |  3B   |   4B   |       nB        |
    ------------------------------------
    |  stx  |   len  |       msg       |
    ------------------------------------
    stx: "TCB"
    len: len = msg length
    msg: protobuf message
    ************************************/
    public class Decoder
    {
        static private readonly string STX = "TCB";
        private List<byte> _buffer;

        public Decoder()
        {
            _buffer = new List<byte>();
        }

        public bool findSTX(List<byte> bytes, out int location)
        {
            location = -1;
            int offset = 0;
            while (offset < bytes.Count - 3)
            {
                if (bytes.ElementAt(offset) == 'T' && bytes.ElementAt(offset + 1) == 'C' && bytes.ElementAt(offset + 2) == 'B')
                {
                    location = offset;
                    return true;
                }
                offset++;
            }
            location = offset - 1;
            return false;
        }
        public List<Msg> DecodeEx(byte[] bytes)
        {
            _buffer.AddRange(bytes);
            List<Msg> msgs = new List<Msg>();
            int location = 0;
            while (findSTX(_buffer, out location))
            {
                _buffer.RemoveRange(0, location);
                MemoryStream ms = new MemoryStream(_buffer.ToArray());
                byte[] stx = new byte[3];
                ms.Read(stx, 0, stx.Length);
                if (ms.Length >= 4)
                {
                    byte[] len = new byte[4];
                    ms.Read(len, 0, len.Length);
                    Array.Reverse(len);
                    UInt32 ulen = BitConverter.ToUInt32(len, 0);
                    if (ulen <= ms.Length - ms.Position)
                    {
                        byte[] payload = new byte[ulen];
                        ms.Read(payload, 0, payload.Length);
                        msgs.Add(Msg.Parser.ParseFrom(payload));
                        _buffer.RemoveRange(0, 3 + 4 + (int)ulen);
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            return msgs;
        }
        public List<Msg> Decode(byte[] bytes)
        {
            List<Msg> msgs = new List<Msg>();
            MemoryStream ms = new MemoryStream(bytes);
            while (ms.Length - ms.Position >= 4 + 3)
            {
                byte[] stx = new byte[3];
                ms.Read(stx, 0, stx.Length);

                if (STX.CompareTo(Encoding.GetEncoding("gb2312").GetString(stx)) == 0)
                {
                    byte[] len = new byte[4];
                    ms.Read(len, 0, len.Length);
                    Array.Reverse(len);
                    UInt32 ulen = BitConverter.ToUInt32(len, 0);
                    if (ulen <= ms.Length - ms.Position)
                    {
                        byte[] payload = new byte[ulen];
                        ms.Read(payload, 0, payload.Length);
                        msgs.Add(Msg.Parser.ParseFrom(payload));
                    }
                }
            }
            return msgs;
        }
    }
}
