using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWMS_LocationServer.src.bll
{
    public class MessageReceivedEventArgs : EventArgs
    {
        // 接收到的消息
        public string Message { get; private set; }

        // 架构 ReceivedEventArgs 
        public MessageReceivedEventArgs(string message)
        {
            Message = message;
        }
    }
}
