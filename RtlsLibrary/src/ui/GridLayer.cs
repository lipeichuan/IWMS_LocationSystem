using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RtlsLibrary.src.ui
{
    public class GridLayer : ContentControl
    {
        public class AliasedDrawingVisual : DrawingVisual
        {
            public AliasedDrawingVisual()
            {
                this.VisualEdgeMode = EdgeMode.Aliased;
            }
        }

        private RtlsMap _map;
        private double _width, _height;
        private RenderTargetBitmap _grid_image = null;

        public GridLayer()
        {
            SnapsToDevicePixels = true;
            SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
        }
        public void SetMap(RtlsMap mapControl)
        {
            _map = mapControl;
        }

        public void Refresh()
        {
            if (Properties.Settings.Default.IsShowGrid)
            {
                createGridImage();
                InvalidateVisual();
            }
        }

        public void createGridImage()
        {
            if (_map == null || ActualWidth == 0 || ActualHeight == 0)
            {
                return;
            }

            AliasedDrawingVisual drawingVisual = new AliasedDrawingVisual();

            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                Rect adjusted = new Rect(0, 0, ActualWidth, ActualHeight);

                SolidColorBrush brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#66414F63"));
                Pen pen = new Pen(brush, 1);
                pen.DashStyle = DashStyles.Dot;
                pen.Freeze();  //冻结画笔，这样能加快绘图速度

                double gridwidth = Properties.Settings.Default.GridWidth * Properties.Settings.Default.PixelsPerMeter * _map.mapScaleTransform.ScaleX;
                double gridheight = Properties.Settings.Default.GridHeight * Properties.Settings.Default.PixelsPerMeter * _map.mapScaleTransform.ScaleY;

                if (gridwidth > 0 && gridheight > 0)
                {
                    Point originalPoint = _map.OriginalPointInPixel;
                    originalPoint.Offset(_map.mapTranslateTransform.X, _map.mapTranslateTransform.Y);
                    originalPoint = _map.mapScaleTransform.Transform(originalPoint);

                    double left = originalPoint.X % gridwidth;
                    double top = originalPoint.Y % gridheight;

                    for (double y = top; y <= adjusted.Bottom; y += gridheight)
                    {
                        drawingContext.DrawLine(pen, new Point(adjusted.Left, y), new Point(adjusted.Right, y));
                    }
                    for (double x = left; x <= adjusted.Right; x += gridwidth)
                    {
                        drawingContext.DrawLine(pen, new Point(x, adjusted.Top), new Point(x, adjusted.Bottom));
                    }
                }

            }
            _grid_image = new RenderTargetBitmap(Convert.ToInt32(ActualWidth), Convert.ToInt32(ActualHeight), 0, 0, PixelFormats.Pbgra32);
            _grid_image.Render(drawingVisual);


            //PngBitmapEncoder encode = new PngBitmapEncoder();
            //encode.Frames.Add(BitmapFrame.Create(_gridImage));
            //using (Stream stm = File.Create(@"C:\grid.png"))
            //{
            //    encode.Save(stm);
            //}

        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            if (_map == null)
            {
                return;
            }

            if (Properties.Settings.Default.IsShowGrid)
            {
                //DrawGrid(dc);
                DrawGridX(dc);  //使用缓存优化绘制速度
            }
            if (Properties.Settings.Default.IsShowOrigin)
            {
                DrawOrigin(dc);
            }

        }

        private void DrawGridX(DrawingContext dc)
        {
            if (_width != ActualWidth || _height != ActualHeight)
            {
                createGridImage();
                _width = ActualWidth;
                _height = ActualHeight;
            }

            if (_grid_image != null)
            {
                Rect adjusted = new Rect(0, 0, _grid_image.Width, _grid_image.Height);
                dc.DrawImage(_grid_image, adjusted);
            }
        }
        private void DrawGrid(DrawingContext dc)
        {
            if (_map == null)
            {
                return;
            }

            Rect adjusted = new Rect(0, 0, ActualWidth, ActualHeight);

            SolidColorBrush brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#66414F63"));
            Pen pen = new Pen(brush, 1);
            pen.DashStyle = DashStyles.Dot;
            pen.Freeze();  //冻结画笔，这样能加快绘图速度

            double gridwidth = Properties.Settings.Default.GridWidth * Properties.Settings.Default.PixelsPerMeter * _map.mapScaleTransform.ScaleX;
            double gridheight = Properties.Settings.Default.GridHeight * Properties.Settings.Default.PixelsPerMeter * _map.mapScaleTransform.ScaleY;

            if (gridwidth > 0 && gridheight > 0)
            {
                Point originalPoint = _map.OriginalPointInPixel;
                originalPoint.Offset(_map.mapTranslateTransform.X, _map.mapTranslateTransform.Y);
                originalPoint = _map.mapScaleTransform.Transform(originalPoint);

                double left = originalPoint.X;
                if (left < adjusted.X)
                {
                    while (left < adjusted.X)
                    {
                        left += gridwidth;
                    }
                }
                else
                {
                    while (left > adjusted.X)
                    {
                        left -= gridwidth;
                    }
                }

                double top = originalPoint.Y;
                if (top < adjusted.Y)
                {
                    while (top < adjusted.Y)
                    {
                        top += gridheight;
                    }
                }
                else
                {
                    while (top > adjusted.Y)
                    {
                        top -= gridheight;
                    }
                }
                for (double y = top; y < adjusted.Bottom; y += gridheight)
                {
                    dc.DrawLine(pen, new Point(adjusted.Left, y), new Point(adjusted.Right, y));
                }
                for (double x = left; x < adjusted.Right; x += gridwidth)
                {
                    dc.DrawLine(pen, new Point(x, adjusted.Top), new Point(x, adjusted.Bottom));
                }
            }
        }
        private void DrawOrigin(DrawingContext dc)
        {
            if (_map == null)
            {
                return;
            }

            Point originalPoint = _map.OriginalPointInPixel;
            originalPoint.Offset(_map.mapTranslateTransform.X, _map.mapTranslateTransform.Y);
            originalPoint = _map.mapScaleTransform.Transform(originalPoint);

            double radius = 6 * _map.mapScaleTransform.ScaleX;
            //dc.DrawEllipse(Brushes.Red, null, originalPoint, radius, radius);
            Pen pen = new Pen(Brushes.Red, 3);
            pen.Freeze();  //冻结画笔，这样能加快绘图速度
            dc.DrawLine(pen, new Point(originalPoint.X, originalPoint.Y - radius), new Point(originalPoint.X, originalPoint.Y + radius));
            dc.DrawLine(pen, new Point(originalPoint.X - radius, originalPoint.Y), new Point(originalPoint.X + radius, originalPoint.Y));

        }
    }
}
