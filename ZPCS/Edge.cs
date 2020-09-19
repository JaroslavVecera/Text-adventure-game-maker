using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace TextGameEditor
{
    public class Edge
    {
        public Line Line { get; set; }
        public DockBranch Ancestor { get; set; }
        public ICanvasNode Descendant { get; set; }

        public void Clear()
        {
            MainWindow w = MainWindow.GetInstance();
            w.RemoveLine(Line);
        }

        public Edge()
        {
            Line = new Line();
            Line.Visibility = System.Windows.Visibility.Visible;
            Line.StrokeThickness = 2;
            Line.Stroke = System.Windows.Media.Brushes.Black;
            Panel.SetZIndex(Line, -10);
            MainWindow w = MainWindow.GetInstance();
            w.AddLine(Line);
        }

        public void Remove()
        {
            MainWindow w = MainWindow.GetInstance();
            w.RemoveLine(Line);
        }

        public void MoveDescendantSide(double dx, double dy)
        {
            Line.X2 += dx;
            Line.Y2 += dy;
            ColorByDirection();
        }

        public void MoveAncestorSide(double dx, double dy)
        {

            Line.X1 += dx;
            Line.Y1 += dy;
            ColorByDirection();
        }

        public void SetDescendantSide(double x, double y)
        {
            Line.X2 = x;
            Line.Y2 = y;
            ColorByDirection();
        }

        public void SetAncestorSide(double x, double y)
        {

            Line.X1 = x;
            Line.Y1 = y;
            ColorByDirection();
        }

        void ColorByDirection()
        {
            if (Line.X1 < Line.X2)
                Line.Stroke = System.Windows.Media.Brushes.Black;
            else
                Line.Stroke = System.Windows.Media.Brushes.Red;
        }
    }
}
