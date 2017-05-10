using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using IWMS_LocationCommon.common.net;
using System.Threading.Tasks;
using grpc.location;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;

namespace IWMS_LocationClient.src.bll
{
    class UdpClient : IDisposable
    {
        private Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        public class StateObject
        {
            // Client   socket.
            public Socket workSocket = null;
            // Receive buffer.
            public byte[] buffer = new byte[Properties.Settings.Default.BufferSize];
        }

        private Socket _recvSocket;
        private Thread _thread;
        private bool _stopflag;
        private Decoder _decoder;

        public readonly static UdpClient Instance = new UdpClient();
        private UdpClient()
        {
            _stopflag = false;
            _decoder = new Decoder();

        }

        private Socket createSocket()
        {
            Socket socket = null;
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                socket.Bind(new IPEndPoint(IPAddress.Any, Properties.Settings.Default.UdpClientPort));

                _logger.Debug("VideoServer Udp Server started!");
            }
            catch (Exception e)
            {
                _logger.Error(e.ToString());
            }
            return socket;
        }

        private void run()
        {
            _logger.Debug("UdpClient thread running...");

            while (!_stopflag)
            {
                if (_recvSocket != null)
                {
                    _recvSocket.Close();
                }

                _recvSocket = createSocket();

                if (_recvSocket != null)
                {
                    if (!_stopflag)
                    {
                        beginReceive();
                    }

                    while (!_stopflag)
                    {
                        //doReceive();
                        Thread.Sleep(1);
                    }

                    _recvSocket.Close();
                    _recvSocket = null;
                }

                if (!_stopflag)
                {
                    _logger.Debug("reconnect...");

                    Thread.Sleep(10);
                }
            }

            _logger.Debug("UdpClient thread finished!");
        }

        public void Start()
        {
            _logger.Trace("do Start");

            _stopflag = false;
            _thread = new Thread(run);
            _thread.Start();

        }
        public void Stop()
        {
            _logger.Trace("do Stop");

            _stopflag = true;

            if (_thread != null && _thread.ThreadState == System.Threading.ThreadState.Running)
                _thread.Abort();
            _thread = null;
        }

        private void beginReceive()
        {
            _logger.Debug("begin receive...");
            try
            {
                // Create the state object.     
                StateObject state = new StateObject();
                state.workSocket = _recvSocket;
                // Begin receiving the data from the remote device.   

                EndPoint address = new IPEndPoint(IPAddress.Any, 0);
                _recvSocket.BeginReceiveFrom(state.buffer, 0, Properties.Settings.Default.BufferSize, SocketFlags.None, ref address, new AsyncCallback(receiveCallback), state);
            }
            catch (Exception e)
            {
                _logger.Error(e.ToString());
            }
        }
        private void receiveCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the state object and the client socket     
                // from the asynchronous state object.     
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.workSocket;
                // Read data from the remote device.     
                if (client != null)
                {
                    int bytesRead = 0;

                    EndPoint address = new IPEndPoint(IPAddress.Any, 0);
                    bytesRead = client.EndReceiveFrom(ar, ref address);

                    //_logger.Debug("received: {0} Bytes", bytesRead);

                    if (bytesRead > 0)
                    {
                        // There might be more data, so store the data received so far.     
                        doMessageProcess(state.buffer, bytesRead);

                        // Get the rest of the data.   
                        client.BeginReceiveFrom(state.buffer, 0, Properties.Settings.Default.BufferSize, SocketFlags.None, ref address, new AsyncCallback(receiveCallback), state);

                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error(e.ToString());
            }
        }

        private void doReceive()
        {
            int bytesRead = 0;
            byte[] buffer = new byte[Properties.Settings.Default.BufferSize];
            try
            {
                EndPoint address = new IPEndPoint(IPAddress.Any, 0);
                bytesRead = _recvSocket.ReceiveFrom(buffer, ref address);

                _logger.Debug("received: {0} Bytes", bytesRead);
                if (bytesRead > 0)
                {
                    doMessageProcess(buffer, bytesRead);
                }
            }
            catch (Exception e)
            {
                _logger.Error(e.ToString());
            }

        }

        private void doMessageProcess(byte[] buffer, int length)
        {
            //_logger.Debug("doMessageProcess: [{0}]:", length, BitConverter.ToString(buffer.Take(length).ToArray()));
            List<Msg> msgs = _decoder.DecodeEx(buffer.Take(length).ToArray());
            foreach (Msg msg in msgs)
            {
                _logger.Info("<---: " + msg.ToString());
                IMsgProcessor processor = Assembly.GetExecutingAssembly().CreateInstance("IWMS_LocationClient.src.bll.MsgProcessor." + msg.MsgId.ToString() + "Processor") as IMsgProcessor;
                if (processor != null)
                {
                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                    {
                        processor.Process(msg);
                    });
                }
                else
                {
                    _logger.Error("Udp: Unprocessed Msg [{0}]", msg.MsgId.ToString());
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

                if (_thread != null)
                {
                    _thread.Abort();
                    _thread = null;
                }

                if (_recvSocket != null)
                {
                    _recvSocket.Dispose();
                    _recvSocket = null;
                }

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        ~UdpClient()
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
