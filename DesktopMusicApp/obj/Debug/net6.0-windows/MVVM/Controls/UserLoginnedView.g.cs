﻿#pragma checksum "..\..\..\..\..\MVVM\Controls\UserLoginnedView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "890AF05C1A769C15A0C11BA613E65ADCCB413F6C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Desktop.MVVM.ViewModel;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace Desktop.MVVM.View {
    
    
    /// <summary>
    /// UserLoginnedView
    /// </summary>
    public partial class UserLoginnedView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\..\..\..\MVVM\Controls\UserLoginnedView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid UserPanel;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\..\..\MVVM\Controls\UserLoginnedView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox ShowUserInfoCheckBox;
        
        #line default
        #line hidden
        
        
        #line 94 "..\..\..\..\..\MVVM\Controls\UserLoginnedView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label ProfileLabel;
        
        #line default
        #line hidden
        
        
        #line 95 "..\..\..\..\..\MVVM\Controls\UserLoginnedView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LogoutLabel;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.9.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Desktop;component/mvvm/controls/userloginnedview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\MVVM\Controls\UserLoginnedView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.9.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.UserPanel = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.ShowUserInfoCheckBox = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 3:
            this.ProfileLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.LogoutLabel = ((System.Windows.Controls.Label)(target));
            
            #line 95 "..\..\..\..\..\MVVM\Controls\UserLoginnedView.xaml"
            this.LogoutLabel.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.LogoutLabel_OnMouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

