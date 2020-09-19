using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace TextGameEditor.Condition
{
    /// <summary>
    /// Interakční logika pro ConditionForm.xaml
    /// </summary>
    public partial class ConditionProperties
    {
        private static readonly Regex _allowedVariableChars = new Regex("^[a-zA-Z]+$");
        ExtractBox _bindedCondition;
        Properties _form;

        public Variable Variable
        {
            get
            {
                bool state = false;
                if (variableButton1.IsChecked == true)
                    state = true;
                return new Variable { Name = variable.Text, State = state };
            }
            set
            {
                if (value.Name.Length > 0)
                {
                    EnableButton(variableButton1);
                    EnableButton(variableButton2);
                    if (value.State)
                        variableButton1.IsChecked = true;
                    else
                        variableButton2.IsChecked = true;
                }
                variable.Text = value.Name;
            }
        }

        public ConditionProperties(ExtractBox c, Properties form, int index)
        {
            InitializeComponent();
            _bindedCondition = c;
            _form = form;
            Header = index.ToString();
            MainWindow w = MainWindow.GetInstance();
        }

        private void PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Paste)
                e.Handled = true;
        }

        void EnableButton(RadioButton b)
        {
            b.IsEnabled = true;
            b.Background = new SolidColorBrush(Colors.Gray);
        }

        void DisableButton(RadioButton b)
        {
            b.IsEnabled = false;
            b.IsChecked = false;
            b.Background = new SolidColorBrush(Colors.Red);
        }

        private void ChangeVariableName(object sender, TextChangedEventArgs e)
        {
            _bindedCondition.Variable = Variable;
            CheckButtons();
        }

        void CheckButtons()
        {
            if (variable.Text.Length == 0)
            {
                DisableButton(variableButton1);
                DisableButton(variableButton2);
            }
            else if (variableButton1.IsChecked == false && variableButton2.IsChecked == false)
            {
                EnableButton(variableButton1);
                EnableButton(variableButton2);
                variableButton1.IsChecked = true;
            }
        }

        private void ChangeVariableState(object sender, RoutedEventArgs e)
        {
            _bindedCondition.Variable = Variable;
        }

        void RemoveCondition(object sender, EventArgs e)
        {
            _form.RemoveCondition(_bindedCondition);
        }

        private void PreviewInputVariableText(object sender, TextCompositionEventArgs e)
        {
            if (!_allowedVariableChars.IsMatch(e.Text) || (variable.Text.Length > 12 && variable.SelectedText.Length == 0))
                e.Handled = true;
        }

        void PreviewLabelKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
            base.OnPreviewKeyDown(e);
        }
    }
}
