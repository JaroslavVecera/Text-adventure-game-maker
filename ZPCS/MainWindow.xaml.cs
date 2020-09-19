using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Windows.Markup;

namespace TextGameEditor
{
    public partial class MainWindow : Window
    {
        List<ICanvasNode> _nodes = new List<ICanvasNode>();
        ICanvasNode _selectedNode;
        DockBranch _waitingBranch;
        static MainWindow _instance;
        ICanvasNode _waitingNode;
        double _prevDeltaX = 0;
        double _prevDeltaY = 0;
        string _associatedCompiler = null;
        string _sourcePath = null;
        INodePropertyForm _propertyForm;
        NodesToTGBFormatConvertor _convertor = new NodesToTGBFormatConvertor();
        Action _setFormButtonAsConnected = null;
        Action _escapeButtonAbortingState = null;

        public double CanvasWidth { get { return canvas.Width; } }
        public double CanvasHeight { get { return canvas.Height; } }

        public MainWindow()
        {
            InitializeComponent();
            _instance = this;
        }

        public static MainWindow GetInstance()
        {
            return _instance;
        }

        void CreateStoryNode(object sender, EventArgs e)
        {
            Story.Node storyNode = new Story.Node();

            AddNode(storyNode);
            SelectNode(storyNode);
        }

        void CreateConditionNode(object sender, EventArgs e)
        {
            Condition.Node conditionNode = new Condition.Node();

            AddNode(conditionNode);
            SelectNode(conditionNode);
        }

        private void Export(object sender, RoutedEventArgs e)
        {
            Export();
        }

        private void DeleteAll(object sender, RoutedEventArgs e)
        {
            while(_nodes.Count > 0)
            {
                RemoveNode(_nodes[0]);
            }
        }

        private void Compile(object sender, RoutedEventArgs e)
        {
            Export();
            if (_sourcePath == null) return;

            if (_associatedCompiler == null)
                SetCompiler();

            if (_associatedCompiler != null)
                System.Diagnostics.Process.Start(_associatedCompiler, _sourcePath);
        }

        public void LoadStoryNodeForm(Story.Node n)
        {
            propertyFormPlace.Children.Clear();
            Story.Properties form = new Story.Properties();
            form.LoadStoryNode(n);
            propertyFormPlace.Children.Add(form);
            form.SetKeyboardFocus();
            _propertyForm = form;
        }

        public void LoadConditionNodeForm(Condition.Node n)
        {
            propertyFormPlace.Children.Clear();
            Condition.Properties form = new Condition.Properties();
            form.LoadConditionNode(n);
            propertyFormPlace.Children.Add(form);
            _propertyForm = form;
        }

        public void HandleNodeClick(ICanvasNode node)
        {
            if (_waitingBranch == null)
                SelectNode(node);
            else
                ConnectToWaitingBranche(node);
        }

        void Export()
        {
            if (_sourcePath == null)
                SetTGBFileName();
            if (_sourcePath != null)
                File.WriteAllText(_sourcePath, _convertor.Convert(_nodes));
        }

        void AddNode(ICanvasNode node)
        {
            _nodes.Add(node);
            canvas.Children.Add((UIElement)node);
        }

        public void RemoveNode(ICanvasNode node)
        {
            node.Clean();
            if (_selectedNode == node)
                _selectedNode = null;
            TurnOffPropertyWindow();
            canvas.Children.Remove((UIElement)node);
            _nodes.Remove(node);
        }

        void SelectNode(ICanvasNode node)
        {
            if (_selectedNode != null && _waitingBranch == null)
                _selectedNode.Deselect();
            node.Select();
            _selectedNode = node;
        }

        void TurnOffPropertyWindow()
        {
            propertyFormPlace.Children.Clear();
        }

        public void SetAsWaiting(DockBranch b, Action a, Action a2)
        {
            _setFormButtonAsConnected = a;
            if (_escapeButtonAbortingState != null)
            {
                _escapeButtonAbortingState();
                _waitingNode.Deselect();
            }
            _escapeButtonAbortingState = a2;
            _waitingBranch = b;
            _selectedNode.SelectAsWaiting();
            _waitingNode = _selectedNode;
        }

        void ConnectToWaitingBranche(ICanvasNode node)
        {
            if (!(node is Condition.Node) || !(_waitingBranch is Condition.ConditionBranch))
            {
                _setFormButtonAsConnected();
                Edge e = new Edge();
                node.ClipInputEdge(e);
                _waitingBranch.ClipOutputEdge(e);
                _waitingBranch = null;

                if (_waitingNode != _selectedNode)
                    _waitingNode.Deselect();
                else
                    _waitingNode.DrawBorder(BorderStyle.Selected);
                _waitingNode = null;
                _escapeButtonAbortingState = null;
            }
            else
            {
                if (_escapeButtonAbortingState != null)
                    _escapeButtonAbortingState();
                AbortWaiting();
            }
        }

        public void AbortWaiting()
        {
            _setFormButtonAsConnected = null;
            _waitingBranch = null;

            if (_waitingNode != _selectedNode)
                _waitingNode.Deselect();
            else
                _waitingNode.DrawBorder(BorderStyle.Selected);

            _waitingNode = null;
            _escapeButtonAbortingState = null;
        }

        public DockBranch WaitingBranch { get { return _waitingBranch; } }

        public void AddLine(Line l)
        {
            canvas.Children.Add(l);
        }

        public void RemoveLine(Line l)
        {
            canvas.Children.Remove(l);
        }

        private void DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            foreach (UIElement elem in canvas.Children)
            {
                if (elem is ICanvasNode)
                {
                    ((ICanvasNode)elem).CanvasMove(-_prevDeltaX, -_prevDeltaY);
                    ((ICanvasNode)elem).CanvasMove(e.HorizontalChange, e.VerticalChange);
                }
            }
            _prevDeltaX = e.HorizontalChange;
            _prevDeltaY = e.VerticalChange;
            e.Handled = false;
        }

        private void DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            _prevDeltaX = 0;
            _prevDeltaY = 0;
        }

        private void SetCompilerEvent(object sender, RoutedEventArgs e)
        {
            SetCompiler();
        }

        private void SetTGBFileNameEvent(object sender, RoutedEventArgs e)
        {
            SetTGBFileName();
        }

        void SetCompiler()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Title = "Please choose TGB-Compiler aplication.";
            openFileDialog1.InitialDirectory = "";
            openFileDialog1.Filter = "TGB Compiler |*.exe";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;

            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
                _associatedCompiler = openFileDialog1.FileName;
        }

        void SetTGBFileName()
        {
            SaveFileDialog dialog = new SaveFileDialog();

            dialog.Title = "Save source file.";
            dialog.InitialDirectory = "";
            dialog.Filter = "TGB File |*.tgb";
            dialog.FilterIndex = 0;
            dialog.RestoreDirectory = true;

            dialog.ShowDialog();
            if (dialog.FileName != "")
                _sourcePath = dialog.FileName;
        }
    }
}
