using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using grpc.location;
using IWMS_LocationCommon.common.net;
using IWMS_LocationClient.src.common;
using RtlsLibrary.src.model;

namespace IWMS_LocationClient.src.bll.MsgProcessor
{
    public class NotifyProcessor : IMsgProcessor
    {
        public void Process(Msg msg)
        {
            try
            {
                if (msg.Data.SubjectData != null)
                {
                    if (msg.Data.SubjectData.DataType == DataType.Location)
                    {
                        GetTagLocation(msg.Data.SubjectData.TagLocation);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void GetTagLocation(TagLocation tagLocation)
        {
            if (tagLocation != null)
            {
                TagModel tagModel = Common.Instance.GetTag(tagLocation.TagSn);
                if (tagModel != null)
                {
                    tagModel.IsPositioned = tagLocation.IsLocation;
                    tagModel.X = tagLocation.X;
                    tagModel.Y = tagLocation.Y;
                    tagModel.Z = tagLocation.Z;
                }

            }
        }
    }
}
