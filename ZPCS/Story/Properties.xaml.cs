using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TextGameEditor.Story
{
    public partial class Properties : INodePropertyForm
    {
        Node _bindedNode;

        public Properties()
        {
            InitializeComponent();
        }

        public void SetKeyboardFocus()
        {
            Keyboard.Focus(story);
            story.CaretIndex = story.Text.Length;
        }

        private void ChangeStory(object sender, TextChangedEventArgs e)
        {
            _bindedNode.Text = story.Text;
        }

        private void PreviewText(object sender, TextCompositionEventArgs e)
        {
            if (e.Text.Contains("#") || e.Text.Contains("{") || e.Text.Contains("}"))
                e.Handled = true;
        }

        public void SetSelectedIndex(int index)
        {
            options.SelectedIndex = index;
        }

        private void RequestForNewOption(object sender, RoutedEventArgs e)
        {
            _bindedNode.RequestNewOption();
            LoadOptions(_bindedNode);
        }

        public void RemoveOption(Option o)
        {
            _bindedNode.RemoveOption(o);
            LoadOptions(_bindedNode);
        }

        public void LoadStoryNode(Node n)
        {
            _bindedNode = n;
            story.Text = n.Text;
            LoadOptions(n);
        }

        private void RemoveNode(object sender, RoutedEventArgs e)
        {
            MainWindow w = MainWindow.GetInstance();

            w.RemoveNode(_bindedNode);
        }

        void LoadOptions(Node node)
        {
            int index = 1;
            options.Items.Clear();
            foreach (Option o in node.Options)
            {
                CopyOption(o, index);
                index++;
            }
            options.SelectedIndex = options.Items.Count - 1;
        }

        void CopyOption(Option o, int index)
        {
            OptionProperties of = new OptionProperties(o, this, index);
            of.InputVariable = o.InputVariable;
            of.OutputVariable = o.OutputVariable;
            of.Text = o.Text;
            options.Items.Add(of);
        }
    }
}
