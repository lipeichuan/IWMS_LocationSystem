using IWMS_LocationServer.src.bll;
using IWMS_LocationServer.src.common;
using Microsoft.Win32;
using NLog;
using RtlsLibrary.src.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace IWMS_LocationServer.src.ui
{
    /// <summary>
    /// BatAddWindow.xaml 的交互逻辑
    /// </summary>
    public partial class BatAddWindow : Window
    {
        public enum BatAddType
        {
            AnchorAdd = 0,
            TagAdd
        }

        public class AddItem : INotifyPropertyChanged
        {
            private string _sn;
            private string _addResult;
            public string Sn
            {
                get { return _sn; }
                set
                {
                    _sn = value;
                    OnPropertyChanged();
                }
            }
            public string AddResult
            {
                get { return _addResult; }
                set
                {
                    _addResult = value;
                    OnPropertyChanged();
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;
            public virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        }
        private bool _adding = false;//用于中断任务，以及防止添加方法异步重复调用
        private ObservableCollection<AddItem> _items = new ObservableCollection<AddItem>();
        private BatAddType _type;
        private Logger _logger = LogManager.GetCurrentClassLogger();

        public BatAddWindow(BatAddType type)
        {
            InitializeComponent();
            List<string> snlist = loadSnFromXls();
            foreach (string sn in snlist)
            {
                AddItem item = new AddItem
                {
                    Sn = sn,
                    AddResult = ""
                };
                _items.Add(item);
            }
            _type = type;
            snListView.ItemsSource = _items;
        }
        private List<string> loadSnFromXls()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = @"c:\";
            dlg.RestoreDirectory = true;
            dlg.Title = "Select Image File";
            dlg.Filter = "Image Files (*.xls; *.xlsx; *.csv;) | *.xls; *.xlsx; *.csv;";
            dlg.ShowDialog();

            if (File.Exists(dlg.FileName))
            {
                return XlsReader.readSn(dlg.FileName);
            }
            return new List<string>();
        }

        private void add()
        {
            if (_adding)
            {
                _logger.Warn("Please add later.");
                return;
            }
            _adding = true;
            if (_type == BatAddType.AnchorAdd)
            {
                addAnchors();
            }
            else if (_type == BatAddType.TagAdd)
            {
                addTags();
            }
            _adding = false;
        }

        private void addAnchors()
        {
            foreach (AddItem item in _items)
            {
                if (_adding)
                {
                    if (AnchorManager.Instance.isAnchorSnExist(item.Sn))
                    {
                        item.AddResult = "已存在";
                    }
                    else
                    {
                        AnchorModel am = new AnchorModel
                        {
                            Sn = item.Sn
                        };
                        AnchorManager.Instance.addAnchor(am);
                        if (am.Id != 0)
                        {
                            item.AddResult = "OK";
                        }
                        else
                        {
                            item.AddResult = "FAIL";
                        }
                    }
                }
            }
        }

        private void addTags()
        {
            foreach (AddItem item in _items)
            {
                if (_adding)//用于关闭窗口时中断任务
                {
                    if (TagManager.Instance.isTagSnExist(item.Sn))
                    {
                        item.AddResult = "已存在";
                    }
                    else
                    {
                        TagModel am = new TagModel
                        {
                            Sn = item.Sn
                        };
                        TagManager.Instance.addTag(am);
                        if (am.Id != 0)
                        {
                            item.AddResult = "OK";
                        }
                        else
                        {
                            item.AddResult = "FAIL";
                        }
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                if (button.Name.CompareTo("addButton") == 0)
                {
                    //启动任务
                    Task.Run(() => { add(); });
                }
                else if (button.Name.CompareTo("returnButton") == 0)
                {
                    Close();
                }
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            //结束任务
            if (_adding)
            {
                _adding = false;
            }
        }
    }
}
