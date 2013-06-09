using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SusaninPathFinding.Graph.Platformer2DGrid
{
    public interface IPlatformer2DMovementAlgorithm : IGridMovementAlgorithm
    {
        float JumpAccel { get; set; }
        float WalkSpeed { get; set; }
    }
}
