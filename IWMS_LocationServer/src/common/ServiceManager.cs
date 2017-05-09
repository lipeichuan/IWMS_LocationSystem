using grpc.location;
using Grpc.Core;
using IWMS_LocationServer.src.bll;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IWMS_LocationServer.src.common
{
    public class ServiceManager
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();

        private static volatile ServiceManager _instance;
        private static readonly object syncRoot = new object();
        private ServiceManager()
        {
            Server server = new Server
            {
                Services = { Location.BindService(new ServiceImpl()) },
                Ports = { new ServerPort("localhost", Properties.Settings.Default.ServerPort, ServerCredentials.Insecure) }
            };
            server.Start();
        }
        public static ServiceManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new ServiceManager();
                        }
                    }
                }
                return _instance;
            }
        }
        public void Start()
        {
        }
        public void Stop()
        {
        }


        public void SendMsg(Socket client, Msg msg)
        {
        }

    }
}
