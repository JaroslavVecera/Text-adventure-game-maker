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

namespace TextGameEditor.Condition
{
    /// <summary>
    /// Interakční logika pro ConditionNodePropertyForm.xaml
    /// </summary>
    public partial class Properties : INodePropertyForm
    {
        Node _bindedNode;
        ConnectionState _trueConnectionState = ConnectionState.Disconnected;
        ConnectionState _falseConnectionState = ConnectionState.Disconnected;
        ConditionBranch _bindedTrueBranch;
        ConditionBranch _bindedFalseBranch;

        public Properties()
        {
            InitializeComponent();
        }

        public void LoadConditionNode(Node n)
        {
            _bindedNode = n;
            LoadConditions(n);
            LoadConditionBranches(n);
        }

        void LoadConditionBranches(Node n)
        {
            _bindedTrueBranch = n.TrueBranch;
            _bindedFalseBranch = n.FalseBranch;

            if (_bindedTrueBranch.HasDescendant())
                _trueConnectionState = ConnectionState.Connected;
            else
                _trueConnectionState = ConnectionState.Disconnected;

            if (_bindedFalseBranch.HasDescendant())
                _falseConnectionState = ConnectionState.Connected;
            else
                _falseConnectionState = ConnectionState.Disconnected;

            ChangeConnectionButtons();
        }

        void ChangeConnectionButtons()
        {
            if (_trueConnectionState == ConnectionState.Connected)
                connectionTrueButton.Content = "Disconnect";
            else if (_trueConnectionState == ConnectionState.Connecting)
                connectionTrueButton.Content = "Abort connecting";
            else
                connectionTrueButton.Content = "Connect";


            if (_falseConnectionState == ConnectionState.Connected)
                connectionFalseButton.Content = "Disconnect";
            else if (_falseConnectionState == ConnectionState.Connecting)
                connectionFalseButton.Content = "Abort connecting";
            else
                connectionFalseButton.Content = "Connect";
        }

        void LoadConditions(Node n)
        {
            int index = 1;
            conditions.Items.Clear();
            foreach (ExtractBox c in n.Conditions)
            {
                CopyCondition(c, index);
                index++;
            }
            conditions.SelectedIndex = conditions.Items.Count - 1;
        }

        void CopyCondition(ExtractBox c, int index)
        {
            ConditionProperties cf = new ConditionProperties(c, this, index);
            cf.Variable = c.Variable;
            conditions.Items.Add(cf);
        }
        
        private void RequestForNewCondition(object sender, RoutedEventArgs e)
        {
            _bindedNode.RequestNewCondition();
            LoadConditions(_bindedNode);
        }

        private void RemoveNode(object sender, RoutedEventArgs e)
        {
            MainWindow w = MainWindow.GetInstance();

            w.RemoveNode(_bindedNode);
        }

        private void SetTrueConnection(object sender, RoutedEventArgs e)
        {
            MainWindow w = MainWindow.GetInstance();
            if (_trueConnectionState == ConnectionState.Disconnected)
            {
                Action deleg = delegate { _trueConnectionState = ConnectionState.Connected; ChangeConnectionButtons(); };
                Action deleg2 = delegate { _trueConnectionState = ConnectionState.Disconnected; ChangeConnectionButtons(); };
                w.SetAsWaiting(_bindedTrueBranch, deleg, deleg2);
                _trueConnectionState = ConnectionState.Connecting;
            }
            else if (_trueConnectionState == ConnectionState.Connecting)
            {
                w.AbortWaiting();
                _trueConnectionState = ConnectionState.Disconnected;
            }
            else
            {
                _bindedTrueBranch.Disconnect();
                _trueConnectionState = ConnectionState.Disconnected;
            }
            ChangeConnectionButtons();
        }

        private void SetFalseConnection(object sender, RoutedEventArgs e)
        {
            MainWindow w = MainWindow.GetInstance();
            if (_falseConnectionState == ConnectionState.Disconnected)
            {
                Action deleg = delegate { _falseConnectionState = ConnectionState.Connected; ChangeConnectionButtons(); };
                Action deleg2 = delegate { _falseConnectionState = ConnectionState.Disconnected; ChangeConnectionButtons(); };
                w.SetAsWaiting(_bindedFalseBranch, deleg, deleg2);
                _falseConnectionState = ConnectionState.Connecting;
            }
            else if (_falseConnectionState == ConnectionState.Connecting)
            {
                w.AbortWaiting();
                _falseConnectionState = ConnectionState.Disconnected;
            }
            else
            {
                _bindedFalseBranch.Disconnect();
                _falseConnectionState = ConnectionState.Disconnected;
            }
            ChangeConnectionButtons();
        }

        public void RemoveCondition(ExtractBox c)
        {
            _bindedNode.RemoveCondition(c);
            LoadConditions(_bindedNode);
        }
    }
}
