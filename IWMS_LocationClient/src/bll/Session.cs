using grpc.location;
using Grpc.Core;
using IWMS_LocationClient.src.common;
using IWMS_LocationClient.src.ui;
using NLog;
using System;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Utils;

namespace IWMS_LocationClient.src.bll
{
    public class Session : IDisposable
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();
        private string _sessionId;
        private LocationClient _client;
        private LocationApi _api;
        private Timer _timer = null;
        private MapView _mapView;
        private Thread _thread;
        private bool _isConnected = false;
        private bool _isDispose = false;

        public string ServerIp
        {
            get { return Properties.Settings.Default.ServerIp; }
            set
            {
                if (Properties.Settings.Default.ServerIp.CompareTo(value) != 0)
                {
                    Properties.Settings.Default.ServerIp = value;
                    Properties.Settings.Default.Save();
                }
            }
        }
        public int ServerPort
        {
            get { return Properties.Settings.Default.ServerPort; }
            set
            {
                if (Properties.Settings.Default.ServerPort != value)
                {
                    Properties.Settings.Default.ServerPort = value;
                    Properties.Settings.Default.Save();
                }
            }
        }

        public Session(MapView mapView)
        {
            Setup();
            _mapView = mapView;
            _timer = new Timer(new TimerCallback(timer_tick), null, 0, 1000);
            _thread = new Thread(run);
        }

        public void Setup()
        {
            string ipaddr = string.Format("{0}:{1}", ServerIp, ServerPort);
            Channel channel = new Channel(ipaddr, ChannelCredentials.Insecure);
            _client = new LocationClient(new Location.LocationClient(channel));
            _api = new LocationApi(_client);
        }

        private void loadData()
        {
            Common.Instance.SetScenes(_api.GetScenes());
            Common.Instance.SetAnchors(_api.GetAnchors());
            Common.Instance.SetTags(_api.GetTags());
        }

        private void resetMap()
        {
            if (Application.Current != null)
            {
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    _mapView.mapControl.SetScene(Common.Instance.Scenes.ElementAt(0));
                    _mapView.mapControl.SetAnchors(Common.Instance.Anchors);
                    _mapView.mapControl.SetTags(Common.Instance.Tags);
                });
            }
        }
        private void run()
        {
            while (!_isDispose)
            {
                bool ret = _client.Regist();
                if (ret)
                {
                    _isConnected = true;
                    loadData();
                    resetMap();
                }

                while (_isConnected)
                {

                    Thread.Sleep(1000);
                }


                Thread.Sleep(1000);
            }
        }

        public void Start()
        {
            _thread.Start();
        }
        private void timer_tick(object state)
        {
            sendHeartBeat();
        }

        private void sendHeartBeat()
        {
            if (_isConnected)
            {
                if (!_client.Heartbeat())
                {
                    _isConnected = false;
                    _logger.Warn("Heartbeat fail! Disconnected!");
                }
            }

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
                _isDispose = true;
                if (_timer != null)
                {
                    _timer.Dispose();
                    _timer = null;
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
