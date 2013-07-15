using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SusaninPathFinding.Graph.PathFinding;
using SusaninPathFinding.Source.Graph;

namespace SusaninPathFinding.Graph
{
    /// TODO это название - пиздец. Нужно что-нибудь другое придумать
    public interface IGridPathFindingMovementAlgorithm : ITraversalStrategy<Cell>
    {
        IGrid Grid { get; set; }
    }
}
