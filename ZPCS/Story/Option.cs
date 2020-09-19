using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace TextGameEditor.Story
{
    public class Option : DockBranch
    {
        ExtractBox _extract;
        Variable _inputVariable;
        Variable _outputVariable;
        string _text;
        Edge _edge;

        public bool HasDescendant()
        {
            return _edge != null && _edge.Descendant != null;
        }
        public Edge Edge { get { return _edge; } }

        public ExtractBox OptionGrid { get { return _extract; } }

        public Option(ExtractBox g)
        {
            _extract = g;
            _inputVariable = new Variable();
            _outputVariable = new Variable();
            _text = "";
        }

        public void ClipOutputEdge(Edge e)
        {
            MainWindow w = MainWindow.GetInstance();
            Point relativePoint = _extract.TransformToAncestor(w.canvas)
                          .Transform(new Point(0, 0));
            double x = relativePoint.X + _extract.ActualWidth;
            double y = relativePoint.Y + _extract.ActualHeight / 2;
            e.SetAncestorSide(x, y);
            e.Ancestor = this;
            _edge = e;
        }

        public void Disconnect()
        {
            if (_edge != null)
            {
                Edge.Clear();
                _edge = null;
            }
        }

        public Variable InputVariable
        {
            get { return _inputVariable; }
            set { _inputVariable = value; _extract.WriteInputVariable(value); }
        }

        public Variable OutputVariable
        {
            get { return _outputVariable; }
            set { _outputVariable = value; _extract.WriteOutputVariable(value); }
        }

        public string Text
        {
            get { return _text; }
            set { _text = value; _extract.WriteText(value); }
        }
    }
}
