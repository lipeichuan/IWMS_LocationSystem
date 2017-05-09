using IWMS_LocationServer.src.common;
using Microsoft.Win32;
using RtlsLibrary.src.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace IWMS_LocationServer.src.ui
{
    public partial class MapEditWindow : Window
    {

        public MapEditWindow(int scene_id)
        {
            InitializeComponent();
            init(scene_id);
        }

        private void loadAnchors()
        {
            mapControl.map.SetAnchors(AnchorManager.Instance.Anchors);
        }
        private void init(int scene_id)
        {
            mapControl.map.SetScene(SceneManager.Instance.getScene(scene_id));
            loadAnchors();

            ObservableCollection<SceneModel> scenes = new ObservableCollection<SceneModel>(SceneManager.Instance.Scenes);
            SceneModel nullScene = new SceneModel
            {
                Name = "空"
            };
            scenes.Add(nullScene);
            sceneColumn.ItemsSource = scenes;
            settingAnchorDataGrid.ItemsSource = new ObservableCollection<AnchorModel>(AnchorManager.Instance.Anchors);

            Binding bindingMapOffsetX = new Binding { Source = mapControl, Path = new PropertyPath("MapOffsetX") };
            setMapOffsetX.SetBinding(TextBox.TextProperty, bindingMapOffsetX);
            Binding bindingMapOffsetY = new Binding { Source = mapControl, Path = new PropertyPath("MapOffsetY") };
            setMapOffsetY.SetBinding(TextBox.TextProperty, bindingMapOffsetY);
            Binding bindingMapScaleH = new Binding { Source = mapControl, Path = new PropertyPath("MapScaleH") };
            setMapScaleH.SetBinding(TextBox.TextProperty, bindingMapScaleH);
            Binding bindingMapScaleV = new Binding { Source = mapControl, Path = new PropertyPath("MapScaleV") };
            setMapScaleV.SetBinding(TextBox.TextProperty, bindingMapScaleV);
            Binding bindingMapFlipH = new Binding { Source = mapControl, Path = new PropertyPath("MapFlipH") };
            setMapFlipH.SetBinding(CheckBox.IsCheckedProperty, bindingMapFlipH);
            Binding bindingMapFlipV = new Binding { Source = mapControl, Path = new PropertyPath("MapFlipV") };
            setMapFlipV.SetBinding(CheckBox.IsCheckedProperty, bindingMapFlipV);
        }

        private BitmapImage LoadImage()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = @"c:\";
            dlg.RestoreDirectory = true;
            dlg.Title = "Select Image File";
            dlg.Filter = "Image Files (*.jpg; *.jpeg; *.png;) | *.jpg; *.jpeg; *.png;";
            dlg.ShowDialog();

            if (File.Exists(dlg.FileName))
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = File.OpenRead(dlg.FileName);
                bitmapImage.EndInit();
                return bitmapImage;
            }
            return null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                if (button.Name.CompareTo("pickMapButton") == 0)
                {
                    BitmapImage image = LoadImage();
                    if (image != null)
                    {
                        mapControl.map.SetMapImage(image);
                    }
                }
                else if (button.Name.CompareTo("delMapButton") == 0)
                {
                    mapControl.map.SetMapImage(null);
                }
                else if (button.Name.CompareTo("setOriginButton") == 0)
                {
                    mapControl.map.SetOriginalPoint();
                }
            }
        }

        private void settingAnchorDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

            if (e.EditAction == DataGridEditAction.Commit)
            {
                AnchorModel anchor = e.Row.Item as AnchorModel;
                if (anchor.Id == 0)
                {
                    if (AnchorManager.Instance.isAnchorSnExist(anchor.Sn))
                    {
                        MessageBox.Show("Sn is already exist!");
                        settingAnchorDataGrid.CancelEdit();
                    }
                    else
                    {
                        AnchorManager.Instance.addAnchor(anchor);
                        loadAnchors();
                    }
                }
                else
                {
                    if (AnchorManager.Instance.isAnchorSnExistElse(anchor.Id, anchor.Sn))
                    {
                        MessageBox.Show("Sn is already exist!");
                        settingAnchorDataGrid.CancelEdit();
                    }
                    else
                    {
                        AnchorManager.Instance.modifyAnchor(anchor);
                        loadAnchors();
                    }
                }

            }
        }
    }
}
