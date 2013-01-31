using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SusaninPathFinding.Graph
{
    public interface IGridMovementAlgorithm : IMovementAlgorithm<Cell>
    {
        Grid3D Grid { get; set; }
    }
}
