#pragma checksum "..\..\Properties.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7FC684211058D00392162DA34E737003C10C4613"
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
using TextGameEditor;


namespace TextGameEditor.Story
{


    /// <summary>
    /// ConditionForm
    /// </summary>
    public partial class Properties : System.Windows.Controls.TabItem, System.Windows.Markup.IComponentConnector
    {

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ZPCS;component/properties.xaml", System.UriKind.Relative);

#line 1 "..\..\Properties.xaml"
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
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.variable = ((System.Windows.Controls.TextBox)(target));

#line 11 "..\..\Properties.xaml"
                    this.variable.AddHandler(System.Windows.Input.CommandManager.PreviewExecutedEvent, new System.Windows.Input.ExecutedRoutedEventHandler(this.PreviewExecuted));

#line default
#line hidden

#line 11 "..\..\Properties.xaml"
                    this.variable.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.PreviewInputVariableText);

#line default
#line hidden

#line 11 "..\..\Properties.xaml"
                    this.variable.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.ChangeVariableName);

#line default
#line hidden

#line 11 "..\..\Properties.xaml"
                    this.variable.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.PreviewLabelKeyDown);

#line default
#line hidden
                    return;
                case 2:
                    this.variableButton1 = ((System.Windows.Controls.RadioButton)(target));

#line 12 "..\..\Properties.xaml"
                    this.variableButton1.Checked += new System.Windows.RoutedEventHandler(this.ChangeVariableState);

#line default
#line hidden
                    return;
                case 3:
                    this.variableButton2 = ((System.Windows.Controls.RadioButton)(target));

#line 13 "..\..\Properties.xaml"
                    this.variableButton2.Checked += new System.Windows.RoutedEventHandler(this.ChangeVariableState);

#line default
#line hidden
                    return;
                case 4:

#line 16 "..\..\Properties.xaml"
                    ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.RemoveCondition);

#line default
#line hidden
                    return;
            }
            this._contentLoaded = true;
        }

        internal System.Windows.Controls.TextBox option;
        internal System.Windows.Controls.TextBox inputVariable;
        internal System.Windows.Controls.RadioButton inputVariableButton1;
        internal System.Windows.Controls.RadioButton inputVariableButton2;
        internal System.Windows.Controls.TextBox outputVariable;
        internal System.Windows.Controls.RadioButton outputVariableButton1;
        internal System.Windows.Controls.RadioButton outputVariableButton2;
        internal System.Windows.Controls.Button connectionButton;
    }
}

