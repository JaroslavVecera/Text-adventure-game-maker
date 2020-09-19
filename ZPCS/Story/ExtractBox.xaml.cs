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

namespace TextGameEditor.Story
{
    /// <summary>
    /// Interakční logika pro OptionGrid.xaml
    /// </summary>
    public partial class ExtractBox
    {
        public ExtractBox()
        {
            InitializeComponent();
            Canvas.SetLeft(this, 0);
            Canvas.SetTop(this, 0);
        }

        public void WriteInputVariable(Variable v)
        {
            string content = "";
            if (v.Name.Length > 0)
            {
                content += "if ";
                if (!v.State)
                    content += "not ";
                content += v.Name;
            }
            else
                content += "-";
            inputVariable.Content = content;
        }

        public void WriteOutputVariable(Variable v)
        {
            string content = "";
            if (v.Name.Length > 0)
            {
                content += v.Name + " <- ";
                if (v.State)
                    content += "True";
                else
                    content += "False";
            }
            else
                content += "-";
            outputVariable.Content = content;
        }

        public void WriteText(string t)
        {
            text.Content = t;
        }
    }
}
