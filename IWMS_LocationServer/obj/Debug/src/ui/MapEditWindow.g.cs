﻿#pragma checksum "..\..\..\..\src\ui\MapEditWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "448D7FDE6E157B43DB158A7A4DC0A179"
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
    /// MapEditWindow
    /// </summary>
    public partial class MapEditWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 29 "..\..\..\..\src\ui\MapEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal IWMS_LocationServer.src.ui.MapControl mapControl;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\src\ui\MapEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid settingAnchorDataGrid;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\src\ui\MapEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridComboBoxColumn sceneColumn;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\..\src\ui\MapEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button pickMapButton;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\..\src\ui\MapEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button delMapButton;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\..\src\ui\MapEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox setMapOffsetX;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\..\src\ui\MapEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox setMapOffsetY;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\..\src\ui\MapEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox setMapScaleH;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\..\src\ui\MapEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox setMapScaleV;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\..\..\src\ui\MapEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox setMapFlipH;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\..\..\src\ui\MapEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox setMapFlipV;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\..\..\src\ui\MapEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button setOriginButton;
        
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
            System.Uri resourceLocater = new System.Uri("/IWMS_LocationServer;component/src/ui/mapeditwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\src\ui\MapEditWindow.xaml"
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
            this.mapControl = ((IWMS_LocationServer.src.ui.MapControl)(target));
            return;
            case 2:
            this.settingAnchorDataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 39 "..\..\..\..\src\ui\MapEditWindow.xaml"
            this.settingAnchorDataGrid.CellEditEnding += new System.EventHandler<System.Windows.Controls.DataGridCellEditEndingEventArgs>(this.settingAnchorDataGrid_CellEditEnding);
            
            #line default
            #line hidden
            return;
            case 3:
            this.sceneColumn = ((System.Windows.Controls.DataGridComboBoxColumn)(target));
            return;
            case 4:
            this.pickMapButton = ((System.Windows.Controls.Button)(target));
            
            #line 56 "..\..\..\..\src\ui\MapEditWindow.xaml"
            this.pickMapButton.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.delMapButton = ((System.Windows.Controls.Button)(target));
            
            #line 57 "..\..\..\..\src\ui\MapEditWindow.xaml"
            this.delMapButton.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.setMapOffsetX = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.setMapOffsetY = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.setMapScaleH = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.setMapScaleV = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.setMapFlipH = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 11:
            this.setMapFlipV = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 12:
            this.setOriginButton = ((System.Windows.Controls.Button)(target));
            
            #line 88 "..\..\..\..\src\ui\MapEditWindow.xaml"
            this.setOriginButton.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

