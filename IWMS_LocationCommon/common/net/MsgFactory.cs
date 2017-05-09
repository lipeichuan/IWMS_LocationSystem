using grpc.location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWMS_LocationCommon.common.net
{
    public class MsgFactory
    {
        private static Msg createMsg(MsgID msgId, Data data = null)
        {
            Msg msg = new Msg
            {
                TerminalId = "TerminalID",// Properties.Settings.Default.TerminalID,
                MsgId = msgId,
                Data = data
            };
            return msg;
        }
        public static Msg CreateTagLocation(TagLocation tagLocation)
        {
            Data data = new Data
            {
                SubjectData = new SubjectData
                {
                    DataType = DataType.Location,
                    TagLocation = tagLocation
                }
            };
            return createMsg(MsgID.Notify, data);
        }
    }
}
