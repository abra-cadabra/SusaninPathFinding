using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SusaninPathFinding.Graph
{
    interface IGraphManager
    {

        bool ShowAStar();
        bool ShowCoverage(bool randomize);
        bool ShowFloodFill(bool randomize);
        bool ShowVisibility(bool randomize);

        bool SetVertexNeighbors(bool value);
        //FrameworkElement Renderer { get; }
    }
}
