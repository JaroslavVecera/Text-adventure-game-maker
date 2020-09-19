﻿#pragma checksum "..\..\StoryNode.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A59DD132F6FF02D8A1A7A599548DCCCFAA0EFA53"
//------------------------------------------------------------------------------
// <auto-generated>
//     Tento kód byl generován nástrojem.
//     Verze modulu runtime:4.0.30319.42000
//
//     Změny tohoto souboru mohou způsobit nesprávné chování a budou ztraceny,
//     dojde-li k novému generování kódu.
// </auto-generated>
//------------------------------------------------------------------------------

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
using ZPCS;


namespace ZPCS {
    
    
    /// <summary>
    /// StoryNode
    /// </summary>
    public partial class StoryNode : System.Windows.Controls.Border, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\StoryNode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid optionGrid;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\StoryNode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Thumb grabThumb;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\StoryNode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Thumb resizeThumb;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\StoryNode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock story;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\StoryNode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel optionList;
        
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
            System.Uri resourceLocater = new System.Uri("/ZPCS;component/storynode.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\StoryNode.xaml"
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
            this.optionGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.grabThumb = ((System.Windows.Controls.Primitives.Thumb)(target));
            
            #line 13 "..\..\StoryNode.xaml"
            this.grabThumb.DragDelta += new System.Windows.Controls.Primitives.DragDeltaEventHandler(this.Move);
            
            #line default
            #line hidden
            
            #line 13 "..\..\StoryNode.xaml"
            this.grabThumb.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.resizeThumb = ((System.Windows.Controls.Primitives.Thumb)(target));
            
            #line 20 "..\..\StoryNode.xaml"
            this.resizeThumb.DragDelta += new System.Windows.Controls.Primitives.DragDeltaEventHandler(this.Resize);
            
            #line default
            #line hidden
            return;
            case 4:
            this.story = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.optionList = ((System.Windows.Controls.StackPanel)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
