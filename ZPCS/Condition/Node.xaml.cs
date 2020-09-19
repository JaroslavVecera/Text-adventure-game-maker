using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TextGameEditor.Condition
{
    public partial class Node : ICanvasNode
    {
        List<Edge> _inputEdges = new List<Edge>();
        List<ExtractBox> _conditions = new List<ExtractBox>();
        ConditionBranch _trueBranch;
        ConditionBranch _falseBranch;
        int _maxConditionsCount = 6;
        bool _click = false;

        public ConditionBranch TrueBranch { get { return _trueBranch; } }
        public ConditionBranch FalseBranch { get { return _falseBranch; } }

        public int ID { get; set; }

        List<Edge> OutputEdges
        {
            get
            {
                List<Edge> edges = new List<Edge>();

                if (_trueBranch != null && _trueBranch.Edge != null)
                    edges.Add(_trueBranch.Edge);

                if (_falseBranch!= null && _falseBranch.Edge != null)
                    edges.Add(_falseBranch.Edge);

                return edges;
            }
        }
        public int ConditionsCount { get { return _conditions.Count; } }
        public int MaxConditionsCount { get { return _maxConditionsCount; } }
        public List<ExtractBox> Conditions { get { return _conditions; } }

        public Node()
        {
            InitializeComponent();
            InitializeConditionBranches();
            Canvas.SetTop(this, 0);
            Canvas.SetLeft(this, 0);
        }

        void InitializeConditionBranches()
        {
            _trueBranch = new ConditionBranch(trueLabel);
            _falseBranch = new ConditionBranch(falseLabel);
        }

        public void Clean()
        {
            DisconnectDescendants();
            DisconnectAncestors();
        }

        public void RequestNewCondition()
        {
            if (ConditionsCount < MaxConditionsCount)
            {
                NewCondition();
            }
        }

        void NewCondition()
        {
            ExtractBox g = new ExtractBox();

            conditionList.Children.Add(g);
            _conditions.Add(g);
        }

        public void RemoveCondition(ExtractBox c)
        {
            _conditions.Remove(c);
            conditionList.Children.Remove(c);
        }

        private void Move(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            _click = false;

            CanvasMove(e.HorizontalChange, e.VerticalChange);
        }

        private void Resize(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            double newWidth = ActualWidth + e.HorizontalChange;
            double newHeight = ActualHeight + e.VerticalChange;

            if (newHeight < MinHeight)
                newHeight = ActualHeight;

            if (newWidth < MinWidth)
                newWidth = ActualWidth;

            SetSize(newWidth, newHeight);
        }

        public void Select()
        {
            DrawBorder(BorderStyle.Selected);
            LoadPropertyForm();
            Panel.SetZIndex(this, 2);
        }

        public void Deselect()
        {
            DrawBorder(BorderStyle.Deselected);
            Panel.SetZIndex(this, 1);
        }

        void LoadPropertyForm()
        {
            MainWindow w = MainWindow.GetInstance();

            w.LoadConditionNodeForm(this);
        }

        public void CanvasMove(double dx, double dy)
        {
            MainWindow window = MainWindow.GetInstance();
            double newX = Canvas.GetLeft(this) + dx;
            double newY = Canvas.GetTop(this) + dy;

            SetPosition(newX, newY);
        }

        public void ClipInputEdge(Edge e)
        {
            double x = Canvas.GetLeft(this) + BorderThickness.Left;
            double y = Canvas.GetTop(this) + ActualHeight / 2;
            e.SetDescendantSide(x, y);
            e.Descendant = this;
            _inputEdges.Add(e);
        }

        public void DrawBorder(BorderStyle style)
        {
            if (style == BorderStyle.Selected)
                BorderBrush = new SolidColorBrush(Colors.Yellow);
            else if (style == BorderStyle.Deselected)
                BorderBrush = new SolidColorBrush(Colors.Gray);
            else
                BorderBrush = new SolidColorBrush(Colors.Green);
        }

        public void SelectAsWaiting()
        {
            DrawBorder(BorderStyle.WaitingForConnection);
        }

        void DisconnectDescendants()
        {
            _trueBranch.Disconnect();
            _falseBranch.Disconnect();
        }

        void DisconnectAncestors()
        {
            foreach (Edge e in _inputEdges)
            {
                e.Ancestor.Disconnect();
            }
        }

        void SetPosition(double x, double y)
        {
            MoveClippedLinesPosition(x - Canvas.GetLeft(this), y - Canvas.GetTop(this));
            SetNodePosition(x, y);
        }

        void SetSize(double width, double height)
        {
            ResizeClippedLinesPosition(width - ActualWidth, height - ActualHeight);
            SetValue(WidthProperty, width);
            SetValue(HeightProperty, height);
        }

        void SetNodePosition(double x, double y)
        {
            Canvas.SetLeft(this, x);
            Canvas.SetTop(this, y);
        }

        void ResizeClippedLinesPosition(double deltaW, double deltaH)
        {
            foreach (Edge b in _inputEdges)
            {
                b.MoveDescendantSide(0, deltaH);
            }
            if (_trueBranch.Edge != null)
                _trueBranch.Edge.SetAncestorSide(Canvas.GetLeft(this) + ActualWidth, Canvas.GetTop(this) + ActualHeight / 4);

            if (_falseBranch.Edge != null)
                _falseBranch.Edge.SetAncestorSide(Canvas.GetLeft(this) + ActualWidth, Canvas.GetTop(this) + ActualHeight * 3 / 4);
        }

        void MoveClippedLinesPosition(double deltaX, double deltaY)
        {
            foreach (Edge b in _inputEdges)
            {
                b.MoveDescendantSide(deltaX, deltaY);
            }
            foreach (Edge b in OutputEdges)
            {
                b.MoveAncestorSide(deltaX, deltaY);
            }
        }

        private void grabThumb_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _click = true;
        }

        private void grabThumb_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_click)
            {
                MainWindow w = MainWindow.GetInstance();
                w.HandleNodeClick(this);
            }
            _click = false;
        }
    }
}