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
    /// Interakční logika pro ConditionGrid.xaml
    /// </summary>
    public partial class ExtractBox
    {
        Variable _var = new Variable();

        public ExtractBox()
        {
            InitializeComponent();
        }

        public Variable Variable
        {
            set
            {
                _var = value;
                WriteVariable(value);
            }
            get
            {
                return _var;
            }
        }

        void WriteVariable(Variable v)
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
            condition.Content = content;
        }
    }
}
