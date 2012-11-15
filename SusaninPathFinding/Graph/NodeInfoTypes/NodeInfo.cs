using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SusaninPathFinding.Graph.NodeInfoTypes
{

    /// <summary>
    /// Enumerator for types of cell
    /// </summary>
    public enum CellType
    {
        Empty,
        Passable,
        Impassable,
        Ladder
    }

    public class NodeInfo
    {

        public NodeInfo Factory(CellType type)
        {
            switch (type)
            {
                case CellType.Empty:
                    return new Empty();
                case CellType.Passable:
                    return new Passable();
                case CellType.Impassable:
                    return new Impassable();
                default:
                    return new Empty();
                //case CellType.Ladder:
                //    return new Ladder();
            }
        }
    }
}
