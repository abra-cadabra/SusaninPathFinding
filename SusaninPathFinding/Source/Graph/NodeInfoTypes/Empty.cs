using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SusaninPathFinding.Graph.NodeInfoTypes;

namespace SusaninPathFinding.Graph.NodeInfoTypes
{
    public class Empty : INodeInfo
    {
        public object Clone()
        {
            return new Empty();
        }
    }
}
