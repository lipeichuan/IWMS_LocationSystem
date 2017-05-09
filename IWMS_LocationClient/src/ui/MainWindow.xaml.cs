using grpc.location;
using Grpc.Core;
using IWMS_LocationClient.src.bll;
using IWMS_LocationClient.src.common;
using RtlsLibrary.src.model;
using RtlsLibrary.src.ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IWMS_LocationClient.src.ui
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            RtlsMap.IsShowGrid = false;
            RtlsMap.IsShowOrigin = false;
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Session session = new Session(mapView);
            session.Start();
        }



        //private void regist()
        //{
        //    _client.Regist();
        //}

        //private void heartbeat()
        //{
        //    _client.Heartbeat();
        //}

        //private void subscribe()
        //{
        //    _client.Subscribe();
        //}

        //private void unsubscribe()
        //{
        //    _client.Unsubscribe();
        //}

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    Button button = sender as Button;
        //    if (button != null)
        //    {
        //        if (button.Name.CompareTo("registButton") == 0)
        //        {
        //            regist();
        //        }
        //        else if (button.Name.CompareTo("heartBeatButton") == 0)
        //        {
        //            heartbeat();
        //        }
        //        else if (button.Name.CompareTo("subscribeButton") == 0)
        //        {
        //            subscribe();
        //        }
        //        else if (button.Name.CompareTo("unsubscribeButton") == 0)
        //        {
        //            unsubscribe();
        //        }

        //    }
        //}
    }
}
