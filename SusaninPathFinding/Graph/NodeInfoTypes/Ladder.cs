using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SusaninPathFinding.Graph.NodeInfoTypes;

namespace SusaninPathFinding.Graph
{
    public class Ladder : PartialyPassable
    {
        public GridDirection Direction { get; set; }

        public Ladder(CompassDirection dir)
        {
            Direction = new GridDirection(dir);
        }
    }
}
