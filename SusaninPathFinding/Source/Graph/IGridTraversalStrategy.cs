using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SusaninPathFinding.Source.Graph;

namespace SusaninPathFinding.Graph
{
    public interface IGridTraversalStrategy : ITraversalStrategy<Cell>
    {
        IGrid Grid { get; set; }
    }
}
