using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextGameEditor
{
    public interface ICanvasNode
    {
        void Clean();
        void Select();
        void Deselect();
        void SelectAsWaiting();
        void ClipInputEdge(Edge e);
        void CanvasMove(double dx, double dy);
        void DrawBorder(BorderStyle style);
        int ID { get; set; }
    }
}
