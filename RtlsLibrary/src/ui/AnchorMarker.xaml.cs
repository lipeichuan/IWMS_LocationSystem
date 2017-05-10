using RtlsLibrary.src.model;
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

namespace RtlsLibrary.src.ui
{
    /// <summary>
    /// AnchorMarker.xaml 的交互逻辑
    /// </summary>
    public partial class AnchorMarker : UserControl
    {
        private AnchorModel _anchorModel = null;
        private bool _isSelected = false;
        private bool _isEditing = false;
        public AnchorMarker(AnchorModel anchor)
        {
            InitializeComponent();

            _anchorModel = anchor;

            labeltext.Text = anchor.Sn.ToString();

            baseEllipse.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Properties.Resources.IDC_ANCHOR_SHAPE));
            labeltext.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Properties.Resources.IDC_TEXT_F));
            label.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Properties.Resources.IDC_TEXT_B));
            label.BorderBrush = labeltext.Foreground;
        }

        public void setDefault()
        {

        }
        public void setHightLight()
        {
            baseEllipse.Fill = Brushes.Cyan;

        }
        public AnchorModel AnchorModel { get { return _anchorModel; } }
        public int OffsetX { get { return (int)baseEllipse.Width / 2; } }
        public int OffsetY { get { return (int)baseEllipse.Height / 2; } }
        public bool IsSelected { get { return _isSelected; } }
        public bool IsEditing { get { return _isEditing; } }

        public void Selected()
        {
            _isSelected = true;
            baseEllipse.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Properties.Resources.IDC_ANCHOR_SHAPE_SELECTED));
            labeltext.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Properties.Resources.IDC_TEXT_F_SELECTED));
            label.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Properties.Resources.IDC_TEXT_B_SELECTED));
            label.BorderBrush = labeltext.Foreground;
        }
        public void UnSelected()
        {
            _isSelected = false;
            baseEllipse.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Properties.Resources.IDC_ANCHOR_SHAPE));
            labeltext.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Properties.Resources.IDC_TEXT_F));
            label.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Properties.Resources.IDC_TEXT_B));
            label.BorderBrush = labeltext.Foreground;
        }
        public void Editing()
        {
            _isEditing = true;
            baseEllipse.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Properties.Resources.IDC_ANCHOR_SHAPE_EDITING));
            labeltext.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Properties.Resources.IDC_TEXT_F_SELECTED));
            label.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Properties.Resources.IDC_TEXT_B_SELECTED));
            label.BorderBrush = labeltext.Foreground;
        }
        public void UnEditing()
        {
            _isEditing = false;
            if (_isSelected)
            {
                Selected();
            }
            else
            {
                UnSelected();
            }
        }
        
    }
}
