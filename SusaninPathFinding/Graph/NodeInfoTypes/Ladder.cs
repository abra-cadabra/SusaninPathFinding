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

        public Ladder(GridDirection dir)
        {
            Direction = dir;
        }

        public Ladder(int pitch, int yaw, int roll)
        {
            Direction = GridDirectionExtentions.GetDirectionFromRotation(pitch, yaw, roll);
        }
    }
}
