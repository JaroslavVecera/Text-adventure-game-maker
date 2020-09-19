using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TextGameEditor.Story
{
    public partial class Node : ICanvasNode
    {
        List<Edge> _inputEdges = new List<Edge>();
        List<Option> _options = new List<Option>();
        int _maxOptionsCount = 6;
        bool _click = false;

        public int ID { get; set; }

        List<Edge> OutputEdges {
            get
            {
                List<Edge> edges = new List<Edge>();
                foreach (Option o in _options)
                {
                    if (o.Edge != null)
                        edges.Add(o.Edge);
                }
                return edges;
            }
        }
        public int OptionsCount { get { return _options.Count; } }
        public int MaxOptionsCount { get { return _maxOptionsCount; } }
        public string Text { set { story.Text = value; } get { return story.Text; } }
        public List<Option> Options { get { return _options; } }

        public Node()
        {
            InitializeComponent();
            Canvas.SetTop(this, 0);
            Canvas.SetLeft(this, 0);
        }

        public void Clean()
        {
            DisconnectDescendants();
            DisconnectAncestors();
        }

        public void RequestNewOption()
        {
            if (OptionsCount < MaxOptionsCount)
            {
                NewOption();
            }
        }

        void NewOption()
        {
            ExtractBox g = new ExtractBox();
            Option o = new Option(g);

            optionList.Children.Add(g);
            _options.Add(o);

            MinHeight += 25;
            Height += 25;
            optionPlace.Height = new GridLength(optionPlace.Height.Value + 25);
        }

        public void RemoveOption(Option o)
        {
            int i = _options.IndexOf(o);
            _options.Remove(o);
            for (; i < _options.Count; i++)
            {
                if (_options[i].Edge != null)
                    _options[i].Edge.MoveAncestorSide(0, -25);
            }
            optionList.Children.Remove(o.OptionGrid);
            MinHeight -= 25;
            Height -= 25;
            optionPlace.Height = new GridLength(optionPlace.Height.Value - 25);
        }

        private void Move(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            MainWindow window = MainWindow.GetInstance();
            double newX = Canvas.GetLeft(this) + e.HorizontalChange;
            double newY = Canvas.GetTop(this) + e.VerticalChange;
            _click = false;

            SetPosition(newX, newY);
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

            w.LoadStoryNodeForm(this);
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

        void DisconnectAncestors()
        {
            foreach (Edge e in _inputEdges)
            {
                e.Ancestor.Disconnect();
            }
        }

        void DisconnectDescendants()
        {
            foreach(Option b in _options)
            {
                b.Disconnect();
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
            foreach (Edge b in OutputEdges)
            {
                b.MoveAncestorSide(deltaW, deltaH);
            }
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

        private void grabThumb_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _click = true;
        }

        private void grabThumb_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
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
