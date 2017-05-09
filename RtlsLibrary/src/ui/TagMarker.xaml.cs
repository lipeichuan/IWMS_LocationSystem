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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RtlsLibrary.src.ui
{
    /// <summary>
    /// TagMarker.xaml 的交互逻辑
    /// </summary>
    public partial class TagMarker : UserControl
    {
        private TagModel _tagModel = null;
        //private ColorAnimation _animation = null;
        private DoubleAnimation _animation = null;

        public string ToolTipsInfo;
        public TagMarker(TagModel tag)
        {
            InitializeComponent();

            _tagModel = tag;

            ToolTipsInfo = "sn: " + tag.Sn;

            //BasePersonnel personnel = PersonnelManager.instance.getPersonnelByTag(tag.PersonnelId);
            //if (personnel != null)
            //{
            //    labeltext.Text = personnel.Name + "(" + tag.Sn.ToString() + ")";
            //    ToolTipsInfo += "\nname: " + personnel.Name;
            //    ToolTipsInfo += "\nsex: " + (personnel.Sex == SexOpt.Male ? "male" : (personnel.Sex == SexOpt.Female ? "female" : ""));
            //    ToolTipsInfo += "\nphone: " + personnel.Phone;
            //}
            //else
            //{
            //    labeltext.Text = "(" + tag.Sn.ToString() + ")";
            //}

        }

        public TagModel TagModel { get { return _tagModel; } }
        public int OffsetX { get { return (int)image.Width / 2; } }
        public int OffsetY { get { return (int)image.Height / 2; } }

        public void playAnimation()
        {
            if (_animation == null)
            {
                //_animation = new ColorAnimation();
                //_animation.From = (Color)ColorConverter.ConvertFromString("#FFEA070E");
                //_animation.To = (Color)ColorConverter.ConvertFromString("#FFD900D9");
                //_animation.Duration = new Duration(TimeSpan.FromMilliseconds(600));
                //_animation.AutoReverse = true;
                //_animation.RepeatBehavior = RepeatBehavior.Forever;
                //_animation.FillBehavior = FillBehavior.Stop;

                //baseEllipse.Fill.BeginAnimation(SolidColorBrush.ColorProperty, _animation, HandoffBehavior.Compose);

                _animation = new DoubleAnimation();
                _animation.From = 16;
                _animation.To = 32;
                _animation.Duration = new Duration(TimeSpan.FromMilliseconds(600));
                _animation.AutoReverse = true;
                _animation.RepeatBehavior = RepeatBehavior.Forever;
                _animation.FillBehavior = FillBehavior.Stop;

                baseEllipse.BeginAnimation(Ellipse.WidthProperty, _animation);
                baseEllipse.BeginAnimation(Ellipse.HeightProperty, _animation);
            }
        }
        public void stopAnimation()
        {
            //baseEllipse.Fill.BeginAnimation(SolidColorBrush.ColorProperty, null);

            baseEllipse.BeginAnimation(Ellipse.WidthProperty, null);
            baseEllipse.BeginAnimation(Ellipse.HeightProperty, null);

            _animation = null;
        }
    }
}
