﻿#pragma checksum "..\..\..\..\src\ui\SettingWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C9FF2F6A163100EF02E6ADEF17D4D747"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using IWMS_LocationServer.Properties;
using IWMS_LocationServer.src.ui;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace IWMS_LocationServer.src.ui {
    
    
    /// <summary>
    /// SettingWindow
    /// </summary>
    public partial class SettingWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\..\..\src\ui\SettingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button mapEditButton;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\..\src\ui\SettingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button anchorsAddButton;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\src\ui\SettingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button anchorClearButton;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\src\ui\SettingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid anchorDataGrid;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\..\src\ui\SettingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button tagsAddButton;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\..\src\ui\SettingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button tagClearButton;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\..\src\ui\SettingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid tagDataGrid;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/IWMS_LocationServer;component/src/ui/settingwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\src\ui\SettingWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.mapEditButton = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\..\..\src\ui\SettingWindow.xaml"
            this.mapEditButton.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.anchorsAddButton = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\..\..\src\ui\SettingWindow.xaml"
            this.anchorsAddButton.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.anchorClearButton = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\..\..\src\ui\SettingWindow.xaml"
            this.anchorClearButton.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.anchorDataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 41 "..\..\..\..\src\ui\SettingWindow.xaml"
            this.anchorDataGrid.RowEditEnding += new System.EventHandler<System.Windows.Controls.DataGridRowEditEndingEventArgs>(this.anchorDataGrid_RowEditEnding);
            
            #line default
            #line hidden
            
            #line 41 "..\..\..\..\src\ui\SettingWindow.xaml"
            this.anchorDataGrid.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.anchorDataGrid_PreviewKeyDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.tagsAddButton = ((System.Windows.Controls.Button)(target));
            
            #line 53 "..\..\..\..\src\ui\SettingWindow.xaml"
            this.tagsAddButton.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.tagClearButton = ((System.Windows.Controls.Button)(target));
            
            #line 54 "..\..\..\..\src\ui\SettingWindow.xaml"
            this.tagClearButton.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.tagDataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 56 "..\..\..\..\src\ui\SettingWindow.xaml"
            this.tagDataGrid.RowEditEnding += new System.EventHandler<System.Windows.Controls.DataGridRowEditEndingEventArgs>(this.tagDataGrid_RowEditEnding);
            
            #line default
            #line hidden
            
            #line 56 "..\..\..\..\src\ui\SettingWindow.xaml"
            this.tagDataGrid.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.tagDataGrid_PreviewKeyDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
