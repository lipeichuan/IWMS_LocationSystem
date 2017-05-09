using RtlsLibrary.src.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace RtlsLibrary.src.ui
{
    /// <summary>
    /// RtlsMap.xaml 的交互逻辑
    /// </summary>
    public partial class RtlsMap : UserControl
    {
        private SceneModel _scene = new SceneModel();
        private Point _startPoint;
        private bool _isSettingOriginPoint = false;

        public Point OriginalPointInPixel
        {
            get
            {
                if (_scene != null)
                {
                    Point point = new Point(MeterToPixel(_scene.OriginalPoint.X), MeterToPixel(_scene.OriginalPoint.Y));
                    return point;
                }
                return new Point(0, 0);
            }
        }
        public double MapOffsetX { get; set; }

        public double MapOffsetY { get; set; }

        public double MapScaleH { get; set; }

        public double MapScaleV { get; set; }

        public bool MapFlipH { get; set; }

        public bool MapFlipV { get; set; }
        public static uint PixelsPerMeter
        {
            get { return Properties.Settings.Default.PixelsPerMeter; }
            set
            {
                Properties.Settings.Default.PixelsPerMeter = value;
                Properties.Settings.Default.Save();
            }
        }

        public static double MeterToPixel(double meter)
        {
            return meter * PixelsPerMeter;
        }

        public static double PixelToMeter(double pixel)
        {
            return pixel / PixelsPerMeter;
        }

        public static bool IsShowOrigin
        {
            get { return Properties.Settings.Default.IsShowOrigin; }
            set
            {
                Properties.Settings.Default.IsShowOrigin = value;
                Properties.Settings.Default.Save();
            }
        }
        public static bool IsShowGrid
        {
            get { return Properties.Settings.Default.IsShowGrid; }
            set
            {
                Properties.Settings.Default.IsShowGrid = value;
                Properties.Settings.Default.Save();
            }
        }
        public float GridWidth
        {
            get { return Properties.Settings.Default.GridWidth; }
            set
            {
                Properties.Settings.Default.GridWidth = value;
                Properties.Settings.Default.Save();
            }
        }
        public float GridHeight
        {
            get { return Properties.Settings.Default.GridHeight; }
            set
            {
                Properties.Settings.Default.GridHeight = value;
                Properties.Settings.Default.Save();
            }
        }

        public RtlsMap()
        {
            InitializeComponent();
            grid.SetMap(this);

            MouseLeftButtonDown += MapControl_MouseLeftButtonDown;
            MouseLeftButtonUp += MapControl_MouseLeftButtonUp;
            MouseMove += MapControl_MouseMove;
            MouseWheel += MapControl_MouseWheel;

        }

        public void SetOriginalPoint()
        {
            if (_scene != null)
            {
                if (!_isSettingOriginPoint)
                {
                    _isSettingOriginPoint = true;
                    Cursor = Cursors.Cross;
                }
            }
        }
        private void endSetOriginalPoint(Point point)
        {
            if (_scene != null)
            {
                if (_isSettingOriginPoint)
                {
                    _isSettingOriginPoint = false;
                    Cursor = Cursors.Arrow;

                    double detaX = MeterToPixel(point.X - _scene.OriginalPoint.X);
                    double detaY = MeterToPixel(point.Y - _scene.OriginalPoint.Y);
                    _scene.OriginalPoint = point;
                    MapOffsetX -= detaX;
                    MapOffsetY -= detaY;

                    reload();

                }
            }
        }


        public void SetMapImage(BitmapImage image)
        {
            if (_scene != null)
            {
                _scene.MapImage = image;
                MapOffsetX = 0;
                MapOffsetY = 0;
                MapScaleH = 100;
                MapScaleV = 100;
                MapFlipH = false;
                MapFlipV = false;
                reloadMapImage();
            }
        }

        private void scaleMap(Point point, double scale)
        {
            if (mapScaleTransform.ScaleX + scale > 0.3 && mapScaleTransform.ScaleY + scale > 0.3)
            {
                mapScaleTransform.CenterX = point.X;
                mapScaleTransform.CenterY = point.Y;

                mapScaleTransform.ScaleX += scale;
                mapScaleTransform.ScaleY += scale;

                grid.Refresh();
            }
        }

        private void MapControl_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            double scale = e.Delta * 0.001;
            scaleMap(e.GetPosition(this), scale);
            reloadAnchors();
        }

        private void MapControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && Cursor == Cursors.SizeAll)
            {
                Point p = e.GetPosition(null);

                mapTranslateTransform.X += (p.X - _startPoint.X) * (1 / mapScaleTransform.ScaleX);
                mapTranslateTransform.Y += (p.Y - _startPoint.Y) * (1 / mapScaleTransform.ScaleY);

                _startPoint = p;
                reloadAnchors();
                grid.Refresh();
            }
        }
        

        private void MapControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        private void MapControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_isSettingOriginPoint)
            {
                Point point = new Point(PixelToMeter(getActualPosition(e.GetPosition(this)).X), PixelToMeter(getActualPosition(e.GetPosition(this)).Y));
                endSetOriginalPoint(point);
            }
            else
            {
                _startPoint = e.GetPosition(null);
                Cursor = Cursors.SizeAll;
            }
        }

        public void SetScene(SceneModel scene)
        {
            _scene = scene;
            reload();
        }

        public void SetAnchors(List<AnchorModel> anchors)
        {
            anchorLayer.Children.Clear();

            if (anchors != null && _scene != null)
            {
                foreach (AnchorModel anchor in anchors)
                {
                    if (anchor.SceneId == _scene.Id)
                    {
                        AnchorMarker marker = new AnchorMarker(anchor);
                        marker.SetValue(Canvas.LeftProperty, MeterToPixel(_scene.OriginalPoint.X + anchor.X) - marker.OffsetX);
                        marker.SetValue(Canvas.TopProperty, MeterToPixel(_scene.OriginalPoint.Y + anchor.Y) - marker.OffsetY);
                        anchorLayer.Children.Add(marker);
                    }
                }
            }
        }

        public void SetTags(List<TagModel> tags)
        {
            tagLayer.Children.Clear();

            if (tags != null)
            {
                foreach (TagModel tag in tags)
                {
                    TagMarker marker = new TagMarker(tag);
                    marker.Visibility = tag.IsPositioned ? Visibility.Visible : Visibility.Collapsed;
                    marker.SetValue(Canvas.LeftProperty, MeterToPixel(_scene.OriginalPoint.X + tag.X) - marker.OffsetX);
                    marker.SetValue(Canvas.TopProperty, MeterToPixel(_scene.OriginalPoint.Y + tag.Y) - marker.OffsetY);
                    anchorLayer.Children.Add(marker);
                }
            }
        }

        private void reload()
        {
            reloadMapImage();
            grid.Refresh();
            reloadAnchors();
        }

        private void reloadAnchors()
        {
            foreach (AnchorMarker anchorMarker in anchorLayer.Children)
            {
                anchorMarker.Visibility = anchorMarker.Anchor.SceneId == _scene.Id ? Visibility.Visible : Visibility.Collapsed;
                Point point = mapTransGroup.Transform(new Point(MeterToPixel(_scene.OriginalPoint.X + anchorMarker.Anchor.X), MeterToPixel(_scene.OriginalPoint.Y + anchorMarker.Anchor.Y)));
                anchorMarker.SetValue(Canvas.LeftProperty, point.X - anchorMarker.OffsetX);
                anchorMarker.SetValue(Canvas.TopProperty, point.Y - anchorMarker.OffsetY);
            }
        }

        private void reloadMapImage()
        {
            bgMapLayer.Children.Clear();

            if (_scene.MapImage != null)
            {
                Image image = new Image();
                image.Source = _scene.MapImage;
                image.Stretch = Stretch.Fill;
                image.SetValue(Canvas.LeftProperty, MeterToPixel(_scene.OriginalPoint.X) + MapOffsetX);
                image.SetValue(Canvas.TopProperty, MeterToPixel(_scene.OriginalPoint.Y) + MapOffsetY);
                image.Width = _scene.MapImage.PixelWidth * _scene.MapScaleH / _scene.MapImage.DpiX;
                image.Height = _scene.MapImage.PixelHeight * _scene.MapScaleV / _scene.MapImage.DpiY;
                ScaleTransform scaleTransform = new ScaleTransform();
                if (_scene.MapFlipH)
                {
                    scaleTransform.CenterX = image.Width / 2;
                    scaleTransform.ScaleX = -1;
                }
                if (_scene.MapFlipV)
                {
                    scaleTransform.CenterY = image.Height / 2;
                    scaleTransform.ScaleY = -1;
                }

                image.RenderTransform = scaleTransform;

                bgMapLayer.Children.Add(image);
            }
        }
        private Point getActualPosition(Point point)
        {
            return mapTransGroup.Inverse.Transform(point);
        }


    }
}
