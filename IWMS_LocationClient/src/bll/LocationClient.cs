using grpc.location;
using NLog;
using RtlsLibrary.src.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Utils;

namespace IWMS_LocationClient.src.bll
{
    public class LocationClient
    {
        Logger _logger = LogManager.GetCurrentClassLogger();
        readonly Location.LocationClient _client;
        private string _sessionId;
        public LocationClient(Location.LocationClient client)
        {
            _client = client;
        }
        public bool Regist()
        {
            bool ret = false;
            _sessionId = "";
            try
            {
                TerminalInfo terminalInfo = new TerminalInfo
                {
                    TerminalId = "",
                    TerminalType = TerminalInfo.Types.TerminalType.Winpc,
                    SoftwareVersion = "0.1.0.0"
                };
                _logger.Info("===> Regist {0}", terminalInfo.ToString());
                SessionInfo sessionInfo = _client.DoRegist(terminalInfo);
                _logger.Info("<=== Regist response {0}", sessionInfo.ToString());
                if (sessionInfo.SessionId.Length != 0)
                {
                    _sessionId = sessionInfo.SessionId;
                    ret = true;
                }
                else
                {
                    _logger.Error("Regist fail! sessionId is null!");
                }
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
            return ret;
        }

        public bool Heartbeat()
        {
            bool ret = false;
            try
            {
                HeartBeat heartbeat = new HeartBeat
                {
                    SessionId = _sessionId
                };
                Response response = _client.DoHeartBeat(heartbeat);
                if (response.Code == 0)
                {
                    ret = true;
                    //
                    //
                    //
                }
                else
                {
                    _logger.Error("Heartbeat fail!");
                }
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
            return ret;
        }

        public void Subscribe()
        {
            try
            {
                Subject subject = new Subject
                {
                    SessionId = _sessionId,
                    DataType = DataType.Location,
                    Port = Properties.Settings.Default.UdpClientPort
                };
                _logger.Info("===> Subscribe {0}", subject.ToString());
                Response response = _client.DoSubscribe(subject);
                _logger.Info("<=== Subscribe response {0}", response.ToString());
                if (response.Code == 0)
                {
                    //
                    //
                    //
                }
                else
                {
                    _logger.Error("Subscribe fail!");
                }
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
        }

        public void Unsubscribe()
        {
            try
            {
                Subject subject = new Subject
                {
                    SessionId = _sessionId,
                    DataType = DataType.Location,
                    Port = 13001
                };

                Response response = _client.DoSubscribe(subject);
                if (response.Code == 0)
                {
                    //
                    //
                    //
                }
                else
                {
                    _logger.Error("Unsubscribe fail!");
                }
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
        }

        private Scenes getScenes()
        {
            Scenes scenes = new Scenes();
            try
            {
                DataRequest dataRequest = new DataRequest
                {
                    DataType = DataType.Scenes
                };
                _logger.Info("===> GetScenes {0}", dataRequest.ToString());
                DataResponse dataResponse = _client.GetScenes(dataRequest);
                _logger.Info("<=== GetScenes response");
                scenes = dataResponse.Scenes;
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
            return scenes;
        }

        private Anchors getAnchors()
        {
            Anchors anchors = new Anchors();
            try
            {
                DataRequest dataRequest = new DataRequest
                {
                    DataType = DataType.Anchors
                };
                _logger.Info("===> GetAnchors {0}", dataRequest.ToString());
                DataResponse dataResponse = _client.GetAnchors(dataRequest);
                _logger.Info("<=== GetAnchors response");
                anchors = dataResponse.Anchors;
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
            return anchors;
        }

        private Tags getTags()
        {
            Tags tags = new Tags();
            try
            {
                DataRequest dataRequest = new DataRequest
                {
                    DataType = DataType.Tags
                };
                _logger.Info("===> GetTags {0}", dataRequest.ToString());
                DataResponse dataResponse = _client.GetTags(dataRequest);
                _logger.Info("<=== GetTags response");
                tags = dataResponse.Tags;
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
            return tags;
        }

        public List<SceneModel> GetScenes()
        {
            List<SceneModel> list = new List<SceneModel>();
            Scenes scenes = getScenes();

            foreach (Scene scene in scenes.Scene)
            {
                SceneModel sceneModel = new SceneModel
                {
                    Id = scene.Id,
                    Name = scene.Name,
                    OriginalPoint = new Point(scene.OriginalX, scene.OriginalY),
                    MapImage = BitmapImageHelper.ByteArrayToBitmapImage(scene.MapImage.ToByteArray()),
                    MapOffsetX = scene.MapOffsetX,
                    MapOffsetY = scene.MapOffsetY,
                    MapScaleH = scene.MapScaleH,
                    MapScaleV = scene.MapScaleV,
                    MapFlipH = scene.MapFlipH,
                    MapFlipV = scene.MapFlipV
                };

                list.Add(sceneModel);
            }
            return list;
        }
        public List<AnchorModel> GetAnchors()
        {
            List<AnchorModel> list = new List<AnchorModel>();
            Anchors anchors = getAnchors();

            foreach (Anchor anchor in anchors.Anchor)
            {
                AnchorModel anchorModel = new AnchorModel
                {
                    Id = anchor.Id,
                    Sn = anchor.Sn,
                    SceneId = anchor.SceneId,
                    X = anchor.X,
                    Y = anchor.Y,
                    Z = anchor.Z
                };

                list.Add(anchorModel);
            }
            return list;
        }
        public List<TagModel> GetTags()
        {
            List<TagModel> list = new List<TagModel>();
            Tags tags = getTags();

            foreach (Tag tag in tags.Tag)
            {
                TagModel tagModel = new TagModel
                {
                    Id = tag.Id,
                    Sn = tag.Sn,
                    IsPositioned = false,
                    X = 0,
                    Y = 0,
                    Z = 0
                };

                list.Add(tagModel);
            }
            return list;
        }
    }
}
