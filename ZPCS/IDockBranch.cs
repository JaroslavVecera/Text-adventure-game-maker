using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextGameEditor
{
    public interface DockBranch
    {
        void ClipOutputEdge(Edge e);
        void Disconnect();
    }
}
