using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyNavMesh.Graph
{
    public class Ladder : PartialyPassable
    {
        public GraphDirection Direction { get; set; }

        public Ladder(Direction dir)
        {
            Direction = new GraphDirection(dir);
        }
    }
}
