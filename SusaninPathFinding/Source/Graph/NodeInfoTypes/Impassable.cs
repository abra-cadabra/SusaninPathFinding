using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SusaninPathFinding.Graph.NodeInfoTypes
{
    public class Impassable : INodeInfo
    {
        public object Clone()
        {
            return new Impassable();
        }
    }
}
