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

namespace TextGameEditor.Story
{
    /// <summary>
    /// Interakční logika pro OptionForm.xaml
    /// </summary>
    

    public partial class OptionProperties
    {
        private static readonly Regex _allowedVariableChars = new Regex("^[a-zA-Z]+$");

        Option _bindedOption;
        Properties _form;
        ConnectionState _connectionState;
        

        void ChangeConnectionButton()
        {
            if (_connectionState == ConnectionState.Connected)
                connectionButton.Content = "Disconnect";
            else if (_connectionState == ConnectionState.Connecting)
                connectionButton.Content = "Abort connecting";
            else
                connectionButton.Content = "Connect";
        }

        public Variable InputVariable
        {
            get
            {
                bool state = false;
                if (inputVariableButton1.IsChecked == true)
                    state = true;
                return new Variable { Name = inputVariable.Text, State = state };
            }
            set
            {
                if (value.Name.Length > 0)
                {
                    EnableButton(inputVariableButton1);
                    EnableButton(inputVariableButton2);
                    if (value.State)
                        inputVariableButton1.IsChecked = true;
                    else
                        inputVariableButton2.IsChecked = true;
                }
                inputVariable.Text = value.Name;
            }
        }

        public Variable OutputVariable
        {
            get
            {
                bool state = false;
                if (outputVariableButton1.IsChecked == true)
                    state = true;
                return new Variable { Name = outputVariable.Text, State = state };
            }
            set
            {
                if (value.Name.Length > 0)
                {
                    EnableButton(outputVariableButton1);
                    EnableButton(outputVariableButton2);
                    if (value.State)
                        outputVariableButton1.IsChecked = true;
                    else
                        outputVariableButton2.IsChecked = true;
                }
                outputVariable.Text = value.Name;
            }
        }

        public string Text { set { option.Text = value; } }

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
        }

        public OptionProperties(Option o, Properties f, int index)
        {
            InitializeComponent();
            _bindedOption = o;
            _form = f;
            Header = index.ToString();
            MainWindow w = MainWindow.GetInstance();
            if (_bindedOption.HasDescendant())
                _connectionState = ConnectionState.Connected;
            else if (w.WaitingBranch == _bindedOption)
                _connectionState = ConnectionState.Connecting;
            else
                _connectionState = ConnectionState.Disconnected;
            ChangeConnectionButton();
        }

        private void ChangeText(object sender, TextChangedEventArgs e)
        {
            _bindedOption.Text = option.Text; 
        }

        private void ChangeInputVariableName(object sender, TextChangedEventArgs e)
        {
            _bindedOption.InputVariable = InputVariable;
            CheckInputButtons();
        }

        void CheckInputButtons()
        {
            if (inputVariable.Text.Length == 0)
            {
                DisableButton(inputVariableButton1);
                DisableButton(inputVariableButton2);
            }
            else if (inputVariableButton1.IsChecked == false && inputVariableButton2.IsChecked == false)
            {
                EnableButton(inputVariableButton1);
                EnableButton(inputVariableButton2);
                inputVariableButton1.IsChecked = true;
            }
        }

        void CheckOutputButtons()
        {
            if (outputVariable.Text.Length == 0)
            {
                DisableButton(outputVariableButton1);
                DisableButton(outputVariableButton2);
            }
            else if (outputVariableButton1.IsChecked == false && outputVariableButton2.IsChecked == false)
            {
                EnableButton(outputVariableButton1);
                EnableButton(outputVariableButton2);
                outputVariableButton1.IsChecked = true;
            }
        }

        private void ChangeOutputVariableName(object sender, TextChangedEventArgs e)
        {
            _bindedOption.OutputVariable = OutputVariable;
            CheckOutputButtons();
        }

        private void ChangeInputVariableState(object sender, RoutedEventArgs e)
        {
            _bindedOption.InputVariable = InputVariable;
        }

        private void ChangeOutputVariableState(object sender, RoutedEventArgs e)
        {
            _bindedOption.OutputVariable = OutputVariable;
        }

        void SetConnection(object sender, EventArgs e)
        {
            MainWindow w = MainWindow.GetInstance();
            if (_connectionState == ConnectionState.Disconnected)
            {
                Action deleg = delegate { _connectionState = ConnectionState.Connected; ChangeConnectionButton(); };
                Action deleg2 = delegate { _connectionState = ConnectionState.Disconnected; ChangeConnectionButton(); };
                w.SetAsWaiting(_bindedOption, deleg, deleg2);
                _connectionState = ConnectionState.Connecting;
            }
            else if (_connectionState == ConnectionState.Connecting)
            {
                w.AbortWaiting();
                _connectionState = ConnectionState.Disconnected;
            }
            else
            {
                _bindedOption.Disconnect();
                _connectionState = ConnectionState.Disconnected;
            }
            ChangeConnectionButton();
        }

        void RemoveOption(object sender, EventArgs e)
        {
            _form.RemoveOption(_bindedOption);
            _bindedOption.Disconnect();
            _connectionState = ConnectionState.Disconnected;
            ChangeConnectionButton();
        }

        private void PreviewOptionText(object sender, TextCompositionEventArgs e)
        {
            if (e.Text.Contains("#") || e.Text.Contains("{") || e.Text.Contains("}"))
                e.Handled = true;
        }

        private void PreviewInputVariableText(object sender, TextCompositionEventArgs e)
        {
            if (!_allowedVariableChars.IsMatch(e.Text) || (inputVariable.Text.Length > 12 && inputVariable.SelectedText.Length == 0))
                e.Handled = true;
        }

        private void PreviewOutputVariableText(object sender, TextCompositionEventArgs e)
        {
            if(!_allowedVariableChars.IsMatch(e.Text) || (outputVariable.Text.Length > 12 && outputVariable.SelectedText.Length == 0))
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
