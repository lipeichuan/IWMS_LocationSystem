using IWMS_LocationServer.src.bll;
using IWMS_LocationServer.src.common;
using IWMS_LocationServer.src.dal;
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
    /// <summary>
    /// SettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SettingWindow : Window
    {
        public SettingWindow()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            loadAnchors();
            loadTags();
        }

        private void loadAnchors()
        {
            anchorDataGrid.ItemsSource = new ObservableCollection<AnchorModel>(AnchorManager.Instance.Anchors);
        }
        private void loadTags()
        {
            tagDataGrid.ItemsSource = new ObservableCollection<TagModel>(TagManager.Instance.Tags);
        }

        private void anchorDataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                foreach (var item in anchorDataGrid.SelectedItems)
                {
                    AnchorModel anchor = item as AnchorModel;
                    if (anchor != null)
                    {
                        AnchorManager.Instance.deleteAnchor(anchor);
                    }
                }
            }
        }

        private void tagDataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                foreach (var item in tagDataGrid.SelectedItems)
                {
                    TagModel tag = item as TagModel;
                    if (tag != null)
                    {
                        TagManager.Instance.deleteTag(tag);
                    }
                }
            }
        }

        private void showMapEditWindow()
        {
            MapEditWindow mapEditWindow = new MapEditWindow(1);
            mapEditWindow.Owner = this;
            mapEditWindow.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                if (button.Name.CompareTo("mapEditButton") == 0)
                {
                    showMapEditWindow();
                }
                else if (button.Name.CompareTo("anchorClearButton") == 0)
                {
                    AnchorManager.Instance.deleteAllAnchors();
                    loadAnchors();
                }
                else if (button.Name.CompareTo("anchorsAddButton") == 0)
                {
                    AddAnchors();
                    loadAnchors();
                }
                else if (button.Name.CompareTo("tagClearButton") == 0)
                {
                    TagManager.Instance.deleteAllTags();
                    loadTags();
                }
                else if (button.Name.CompareTo("tagsAddButton") == 0)
                {
                    AddTags();
                    loadTags();
                }
            }
        }

        private void AddAnchors()
        {
            BatAddWindow addWindow = new BatAddWindow(BatAddWindow.BatAddType.AnchorAdd);
            addWindow.Owner = this;
            addWindow.ShowDialog();
        }
        private void AddTags()
        {
            BatAddWindow addWindow = new BatAddWindow(BatAddWindow.BatAddType.TagAdd);
            addWindow.Owner = this;
            addWindow.ShowDialog();
        }

        private void tagDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                TagModel tag = e.Row.Item as TagModel;

                if (tag.Sn != null)
                {
                    if (tag.Id == 0)
                    {
                        if (TagManager.Instance.isTagSnExist(tag.Sn))
                        {
                            MessageBox.Show("Sn is already exist!");
                            tagDataGrid.CancelEdit();
                            e.Cancel = true;
                        }
                        else
                        {
                            TagManager.Instance.addTag(tag);
                        }
                    }
                    else
                    {
                        if (TagManager.Instance.isTagSnExistElse(tag.Id, tag.Sn))
                        {
                            MessageBox.Show("Sn is already exist!");
                            tagDataGrid.CancelEdit();
                            e.Cancel = true;
                        }
                        else
                        {
                            TagManager.Instance.modifyTag(tag);
                        }
                    }

                }
                else
                {
                    tagDataGrid.CancelEdit();
                    e.Cancel = true;
                }
            }
        }
        private void anchorDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                AnchorModel anchor = e.Row.Item as AnchorModel;
                if (anchor.Sn != null)
                {
                    if (anchor.Id == 0)
                    {
                        if (AnchorManager.Instance.isAnchorSnExist(anchor.Sn))
                        {
                            MessageBox.Show("Sn is already exist!");
                            anchorDataGrid.CancelEdit();
                        }
                        else
                        {
                            AnchorManager.Instance.addAnchor(anchor);
                        }
                    }
                    else
                    {
                        if (AnchorManager.Instance.isAnchorSnExistElse(anchor.Id, anchor.Sn))
                        {
                            MessageBox.Show("Sn is already exist!");
                            anchorDataGrid.CancelEdit();
                        }
                        else
                        {
                            AnchorManager.Instance.modifyAnchor(anchor);
                        }
                    }

                }
                else
                {
                    anchorDataGrid.CancelEdit();
                }
            }
        }
    }
}
