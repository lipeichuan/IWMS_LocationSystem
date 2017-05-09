using Google.Protobuf;
using grpc.location;
using Grpc.Core;
using IWMS_LocationServer.src.common;
using NLog;
using RtlsLibrary.src.model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utils;

namespace IWMS_LocationServer.src.bll
{
    public class ServiceImpl : Location.LocationBase
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();

        public ServiceImpl()
        {
        }

        public override Task<SessionInfo> DoRegist(TerminalInfo request, ServerCallContext context)
        {
            return Task.FromResult(doRegist(request));
        }

        public override Task<Response> DoHeartBeat(HeartBeat request, ServerCallContext context)
        {
            return Task.FromResult(doHeartBeat(request));
        }

        public override Task<Response> DoSubscribe(Subject request, ServerCallContext context)
        {
            return Task.FromResult(doSubscribe(request, context));
        }

        public override Task<Response> DoUnsubscribe(Subject request, ServerCallContext context)
        {
            return Task.FromResult(doUnsubscribe(request, context));
        }

        public override Task<DataResponse> GetScenes(DataRequest request, ServerCallContext context)
        {
            return Task.FromResult(getScenes(request));
        }

        public override Task<DataResponse> GetAnchors(DataRequest request, ServerCallContext context)
        {
            return Task.FromResult(getAnchors(request));
        }

        public override Task<DataResponse> GetTags(DataRequest request, ServerCallContext context)
        {
            return Task.FromResult(getTags(request));
        }

        private SessionInfo doRegist(TerminalInfo request)
        {
            string sessionId = "";
            if (true)//用于终端机软件版本校验
            {
                sessionId = generateSessionId(request);
                SessionManager.Instance.AddSession(sessionId);
            }

            SessionInfo sessionInfo = new SessionInfo
            {
                SessionId = sessionId
            };
            return sessionInfo;
        }

        private string generateSessionId(TerminalInfo info)
        {
            string sessionId = "";

            if (info.TerminalType == TerminalInfo.Types.TerminalType.Server)
            {
                sessionId += "S";
            }
            else if (info.TerminalType == TerminalInfo.Types.TerminalType.Winpc)
            {
                sessionId += "C";
            }
            else if (info.TerminalType == TerminalInfo.Types.TerminalType.Winpc)
            {
                sessionId += "M";
            }
            else
            {
                sessionId += "U";
            }

            sessionId += sessionId + DateTime.Now.ToFileTimeUtc();

            return sessionId;
        }

        private Response doHeartBeat(HeartBeat request)
        {
            bool ret = false;
            Session session = SessionManager.Instance.GetSession(request.SessionId);
            if (session != null)
            {
                ret = session.HeartBeat(request);
            }
            Response response = new Response
            {
                Code = ret ? (uint)0 : (uint)1,
                Info = "",
                Error = ""
            };
            return response;
        }

        private Response doSubscribe(Subject request, ServerCallContext context)
        {
            bool ret = false;
            Session session = SessionManager.Instance.GetSession(request.SessionId);
            string[] peer = context.Peer.Split(new char[] { ':' });
            if (session != null && request.DataType == DataType.Location && peer.Length == 3)
            {
                ret = session.SubscribeLocation(peer[1], (int)request.Port);
            }
            Response response = new Response
            {
                Code = ret ? (uint)0 : (uint)1,
                Info = "",
                Error = ""
            };
            return response;
        }

        private Response doUnsubscribe(Subject request, ServerCallContext context)
        {
            bool ret = false;
            Session session = SessionManager.Instance.GetSession(request.SessionId);
            string[] peer = context.Peer.Split(new char[] { ':' });
            if (session != null && request.DataType == DataType.Location)
            {
                ret = session.UnsubscribeLocation(peer[1], (int)request.Port);
            }
            Response response = new Response
            {
                Code = ret ? (uint)0 : (uint)1,
                Info = "",
                Error = ""
            };
            return response;
        }

        private DataResponse getScenes(DataRequest request)
        {
            Scenes scenes = new Scenes();
            List<SceneModel> sceneModelList = new List<SceneModel>();
            if (request.SceneIds == null)
            {
                sceneModelList.AddRange(SceneManager.Instance.Scenes);
            }
            foreach (SceneModel sceneModel in sceneModelList)
            {
                Scene scene = new Scene();
                scene.Id = sceneModel.Id;
                scene.Name = sceneModel.Name;
                scene.OriginalX = sceneModel.OriginalPoint.X;
                scene.OriginalY = sceneModel.OriginalPoint.Y;
                scene.MapOffsetX = sceneModel.MapOffsetX;
                scene.MapOffsetY = sceneModel.MapOffsetY;
                scene.MapScaleH = sceneModel.MapScaleH;
                scene.MapScaleV = sceneModel.MapScaleV;
                scene.MapFlipH = sceneModel.MapFlipH;
                scene.MapFlipV = sceneModel.MapFlipV;
                byte[] bytes = BitmapImageHelper.BitmapImageToByteArray(sceneModel.MapImage);
                scene.MapImage = ByteString.CopyFrom(bytes);
                scenes.Scene.Add(scene);
            }
            DataResponse dataResponse = new DataResponse
            {
                DataType = DataType.Scenes,
                Scenes = scenes
            };
            return dataResponse;
        }
        private DataResponse getAnchors(DataRequest request)
        {
            Anchors anchors = new Anchors();
            List<AnchorModel> anchorModelList = new List<AnchorModel>();
            if (request.AnchorIds == null)
            {
                anchorModelList.AddRange(AnchorManager.Instance.Anchors);
            }
            foreach (AnchorModel anchorModel in anchorModelList)
            {
                Anchor anchor = new Anchor
                {
                    Id = anchorModel.Id,
                    Sn = anchorModel.Sn,
                    SceneId = anchorModel.SceneId,
                    X = anchorModel.X,
                    Y = anchorModel.Y,
                    Z = anchorModel.Z
                };
                anchors.Anchor.Add(anchor);
            }
            DataResponse dataResponse = new DataResponse
            {
                DataType = DataType.Anchors,
                Anchors = anchors
            };
            return dataResponse;
        }
        private DataResponse getTags(DataRequest request)
        {
            Tags tags = new Tags();
            List<TagModel> tagModelList = new List<TagModel>();
            if (request.TagIds == null)
            {
                tagModelList.AddRange(TagManager.Instance.Tags);
            }
            foreach (TagModel tagModel in tagModelList)
            {
                Tag tag = new Tag
                {
                    Id = tagModel.Id,
                    Sn = tagModel.Sn
                };
                tags.Tag.Add(tag);
            }
            DataResponse dataResponse = new DataResponse
            {
                DataType = DataType.Tags,
                Tags = tags
            };
            return dataResponse;
        }
    }
}
