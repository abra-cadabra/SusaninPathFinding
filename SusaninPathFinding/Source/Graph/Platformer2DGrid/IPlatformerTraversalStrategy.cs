using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SusaninPathFinding.Graph.Platformer2DGrid
{
    public interface IPlatformerTraversalStrategy : IGridTraversalStrategy
    {
        float G { get; set; }
    }
}
