using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TextGameEditor.Condition
{
    public class ConditionBranch : DockBranch
    {
        Edge _edge;
        Label _label;

        public Edge Edge { get { return _edge; } }

        public ConditionBranch(Label l)
        {
            _label = l;
        }

        public bool HasDescendant()
        {
            return _edge != null && _edge.Descendant != null;
        }

        public void ClipOutputEdge(Edge e)
        {
            MainWindow w = MainWindow.GetInstance();
            Point relativePoint = _label.TransformToAncestor(w.canvas)
                          .Transform(new Point(0, 0));
            double x = relativePoint.X + _label.ActualWidth;
            double y = relativePoint.Y + _label.ActualHeight / 2;
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
    }
}
