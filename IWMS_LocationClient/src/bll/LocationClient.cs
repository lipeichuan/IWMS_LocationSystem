using grpc.location;
using NLog;
using RtlsLibrary.src.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                SessionInfo sessionInfo = _client.DoRegist(terminalInfo);
                if (sessionInfo.SessionId.Length != 0)
                {
                    _sessionId = sessionInfo.SessionId;
                    ret = true;
                }
                else
                {
                    _logger.Error("Regist fail!");
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

        public Scenes GetScenes()
        {
            Scenes scenes = new Scenes();
            try
            {
                DataRequest dataRequest = new DataRequest
                {
                    DataType = DataType.Scenes
                };
                DataResponse dataResponse = _client.GetScenes(dataRequest);
                scenes = dataResponse.Scenes;
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
            return scenes;
        }

        public Anchors GetAnchors()
        {
            Anchors anchors = new Anchors();
            try
            {
                DataRequest dataRequest = new DataRequest
                {
                    DataType = DataType.Anchors
                };
                DataResponse dataResponse = _client.GetAnchors(dataRequest);
                anchors = dataResponse.Anchors;
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
            return anchors;
        }

        public Tags GetTags()
        {
            Tags tags = new Tags();
            try
            {
                DataRequest dataRequest = new DataRequest
                {
                    DataType = DataType.Tags
                };
                DataResponse dataResponse = _client.GetTags(dataRequest);
                tags = dataResponse.Tags;
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
            return tags;
        }
    }
}
