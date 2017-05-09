using grpc.location;
using IWMS_LocationCommon.common.net;
using IWMS_LocationServer.src.common;
using NLog;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Utils;

namespace IWMS_LocationServer.src.bll
{
    public class Session : IDisposable
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();

        private string _sessionId = "";
        private Timer _timer;
        private TimeOut _timeout;
        private Socket _udpSocket = null;
        private IPEndPoint _udpAddress = null;

        public string SessionId { get { return _sessionId; } }
        public Session(string sessionId)
        {
            _sessionId = sessionId;

            _timeout = new TimeOut(5000);
            _timer = new Timer(new TimerCallback(timer_tick), null, 0, 1000);
        }

        private void timer_tick(object state)
        {
            checkTimeout();
        }

        private void checkTimeout()
        {
            if (_timeout.IsTimeout())
            {
                _logger.Info("timeout!");
                SessionManager.Instance.RemoveSession(_sessionId);
            }
        }

        public bool UdpSendMsg(Msg msg)
        {
            bool flag = false;
            if (msg != null)
            {
                byte[] bytes = Encoder.Encode(msg);

                _logger.Info("===>: " + msg.ToString());

                if (_udpSocket != null && _udpAddress != null)
                {
                    try
                    {
                        _udpSocket.SendTo(bytes, bytes.Length, SocketFlags.None, _udpAddress);
                        flag = true;
                    }
                    catch (Exception e)
                    {
                        _logger.Error(e);
                    }
                }
                else
                {
                    _logger.Error("Msg{0} not send! (unconnected)", msg.MsgId);
                }
            }
            return flag;
        }

        public void UdpSendLocation(TagLocation tagLocation)
        {
            if (_udpSocket != null && _udpAddress != null)
            {
                UdpSendMsg(MsgFactory.CreateTagLocation(tagLocation));
            }
        }

        public bool HeartBeat(HeartBeat request)
        {
            if (_sessionId.CompareTo(request.SessionId) == 0)
            {
                _timeout.Restart();

                return true;
            }
            return false;
        }

        public bool SubscribeLocation(string address, int port)
        {
            bool ret = false;
            if (_udpSocket != null)
            {
                _udpSocket.Dispose();
            }
            try
            {
                _udpAddress = new IPEndPoint(IPAddress.Parse(address), port);
                _udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                ret = true;
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
            return ret;
        }

        public bool UnsubscribeLocation(string address, int port)
        {
            bool ret = false;
            _udpAddress = null;
            if (_udpSocket != null)
            {
                _udpSocket.Dispose();
                ret = true;
            }
            return ret;
        }
        public void print()
        {
            _logger.Info("session: Session:[{0}]", _sessionId);
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。
                if (_timer != null)
                {
                    _timer.Dispose();
                    _timer = null;
                }
                if (_udpSocket != null)
                {
                    _udpSocket.Dispose();
                    _udpSocket = null;
                }

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        ~Session()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(false);
        }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
