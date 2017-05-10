using grpc.location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWMS_LocationCommon.common.net
{
    public interface IMsgProcessor
    {
        void Process(Msg msg);
    }
}
