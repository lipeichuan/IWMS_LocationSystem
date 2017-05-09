using IWMS_LocationServer.src.bll;
using IWMS_LocationServer.src.common;
using NLog;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace IWMS_LocationServer.src.ui
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Logger _logger = LogManager.GetCurrentClassLogger();
        private Queue<string> _messages = new Queue<string>(101);
        private SerialPortDevice _currentDevice = null;
        public MainWindow()
        {
            InitializeComponent();

            init();
        }

        private void init()
        {
            reloadSerialPorts();

            foreach (string item in serialPortsCombo.Items)
            {
                if (string.Compare(item, Properties.Settings.Default.SerialPortName, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    serialPortsCombo.SelectedItem = item;
                    break;
                }
            }

            foreach (ComboBoxItem item in baudRatesCombo.Items)
            {
                if (string.Compare(item.Content.ToString(), Properties.Settings.Default.SerialPortBaudRate.ToString(), StringComparison.OrdinalIgnoreCase) == 0)
                {
                    baudRatesCombo.SelectedItem = item;
                    break;
                }
            }

            ServiceManager server = ServiceManager.Instance;

            autoConnectToSerial();
        }

        private void autoConnectToSerial()
        {
            connectToSerial();
        }

        private void reloadSerialPorts()
        {
            if (serialPortsCombo != null)
            {
                serialPortsCombo.ItemsSource = SerialPort.GetPortNames().OrderBy(s => s);
            }
        }
        private void showSettingWindow()
        {
            SettingWindow settingWindow = new SettingWindow();
            settingWindow.Owner = this;
            settingWindow.ShowDialog();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                if (button.Name.CompareTo("settingButton") == 0)
                {
                    showSettingWindow();
                }
                else if (button.Name.CompareTo("connectButton") == 0)
                {
                    connectToSerial();
                }
                else if (button.Name.CompareTo("disconnectButton") == 0)
                {
                    disconnectToSerial();
                }
            }
        }

        //Creates a serial port device from the selected settings
        private void connectToSerial()
        {
            try
            {
                string portName = serialPortsCombo.Text as string;
                int baudRate = int.Parse(baudRatesCombo.Text);
                SerialPortDevice device = new SerialPortDevice(new SerialPort(portName, baudRate));
                startDevice(device);
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
        }

        private void disconnectToSerial()
        {
            stopCurrentDevice();
        }

        private void stopCurrentDevice()
        {
            //Clean up old device
            if (_currentDevice != null)
            {
                _currentDevice.MessageReceived -= device_MessageReceived;
                _currentDevice.CloseSerialPort();
                _currentDevice.Dispose();
                _currentDevice = null;
            }

            msgTextBox.Text = "";
            _messages.Clear();

        }
        private void startDevice(SerialPortDevice device)
        {
            stopCurrentDevice();

            //Start new device
            _currentDevice = device;
            _currentDevice.MessageReceived += device_MessageReceived;
            if (_currentDevice.OpenSerialPort())
            {
                Properties.Settings.Default.SerialPortName = _currentDevice.SerialPortName;
                Properties.Settings.Default.SerialPortBaudRate = _currentDevice.SerialPortBaudRate;
                Properties.Settings.Default.Save();
            }
            else
            {
                _logger.Warn("COM: {0}, {1} open fail!", _currentDevice.SerialPortName, _currentDevice.SerialPortBaudRate);
            }
        }
        private void device_MessageReceived(object sender, MessageReceivedEventArgs args)
        {
            Dispatcher.BeginInvoke((Action)delegate ()
            {
                _messages.Enqueue(args.Message.ToString());
                if (_messages.Count > 100) _messages.Dequeue(); //Keep message queue at 100
                msgTextBox.Text = string.Join("", _messages.ToArray());
                msgTextBox.Select(msgTextBox.Text.Length - 1, 0); //scroll to bottom
            });
        }

        private void serialPortsCombo_DropDownOpened(object sender, EventArgs e)
        {
            reloadSerialPorts();
        }
    }
}
