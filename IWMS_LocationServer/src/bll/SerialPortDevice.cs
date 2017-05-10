using grpc.location;
using IWMS_LocationServer.src.common;
using NLog;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWMS_LocationServer.src.bll
{
    class SerialPortDevice : IDisposable
    {
        Logger _logger = LogManager.GetCurrentClassLogger();

        private SerialPort _serialPort = null;

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        public string SerialPortName
        {
            get
            {
                if (_serialPort != null)
                {
                    return _serialPort.PortName;
                }
                return "";
            }
        }

        public int SerialPortBaudRate
        {
            get
            {
                if (_serialPort != null)
                {
                    return _serialPort.BaudRate;
                }
                return 9600;
            }
        }

        public SerialPortDevice(SerialPort port)
        {
            _serialPort = port;
            _serialPort.DataReceived += dataReceived;
        }



        private void dataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = sender as SerialPort;
            string line = sp.ReadLine();
            TagLocation tagLocation = null;
            if (LocationInfoParser.GetLocation(line, out tagLocation))
            {
                for (int i = 0; i < SessionManager.Instance.Sessions.Count; i++)
                {
                    SessionManager.Instance.Sessions.ElementAt(i).Value.UdpSendLocation(tagLocation);
                }
            }
            if (MessageReceived != null)
            {
                MessageReceived(this, new MessageReceivedEventArgs(line));
            }
        }

        //
        // 摘要:
        //     Gets the active serial port.
        public SerialPort Port { get { return _serialPort; } }

        public void Write(byte[] buffer, int offset, int count)
        {
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        public bool OpenSerialPort()
        {
            bool ret = false;
            try
            {
                _logger.Info("Open SerialPort! {0}, {1}", SerialPortName, SerialPortBaudRate);
                _serialPort.Open();
                ret = true;
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
            return ret;
        }
        public bool CloseSerialPort()
        {
            bool ret = false;
            try
            {
                _logger.Info("Close SerialPort! {0}, {1}", SerialPortName, SerialPortBaudRate);
                _serialPort.Close();
                ret = true;
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
            return ret;
        }

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
                if (_serialPort != null)
                {
                    _serialPort.Dispose();
                    _serialPort = null;
                }

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        ~SerialPortDevice()
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
