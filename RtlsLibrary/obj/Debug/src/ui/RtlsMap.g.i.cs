﻿#pragma checksum "..\..\..\..\src\ui\RtlsMap.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "4DE7AEA734F2F8FF18215AAC271F5C7F"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using RtlsLibrary.src.ui;
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


namespace RtlsLibrary.src.ui {
    
    
    /// <summary>
    /// RtlsMap
    /// </summary>
    public partial class RtlsMap : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\..\src\ui\RtlsMap.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.TransformGroup mapTransGroup;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\..\src\ui\RtlsMap.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.TranslateTransform mapTranslateTransform;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\..\src\ui\RtlsMap.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.ScaleTransform mapScaleTransform;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\..\src\ui\RtlsMap.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas bgMapLayer;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\..\src\ui\RtlsMap.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas anchorLayer;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\..\src\ui\RtlsMap.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas tagLayer;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\..\src\ui\RtlsMap.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal RtlsLibrary.src.ui.GridLayer grid;
        
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
            System.Uri resourceLocater = new System.Uri("/RtlsLibrary;component/src/ui/rtlsmap.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\src\ui\RtlsMap.xaml"
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
            this.mapTransGroup = ((System.Windows.Media.TransformGroup)(target));
            return;
            case 2:
            this.mapTranslateTransform = ((System.Windows.Media.TranslateTransform)(target));
            return;
            case 3:
            this.mapScaleTransform = ((System.Windows.Media.ScaleTransform)(target));
            return;
            case 4:
            this.bgMapLayer = ((System.Windows.Controls.Canvas)(target));
            return;
            case 5:
            this.anchorLayer = ((System.Windows.Controls.Canvas)(target));
            return;
            case 6:
            this.tagLayer = ((System.Windows.Controls.Canvas)(target));
            return;
            case 7:
            this.grid = ((RtlsLibrary.src.ui.GridLayer)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

