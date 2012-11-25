using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SusaninPathFinding.Graph;
using SusaninPathFinding.Graph.NodeInfoTypes;
using SusaninPathFinding.Graph.PathFinding;

namespace SusaninPathFindingUDKInterface
{
    /// <summary>
    /// Enumerator for types of cell
    /// </summary>
    public enum CellType
    {
        Na,
        Empty,
        Passable,
        Impassable,
        Ladder
    }

    /// <summary>
    /// Enumerator for a types of graph agent
    /// </summary>
    public enum MovementType
    {
        Na,
        Walking,
    };

    public static class UdkInterfaceUtility
    {
        public static NodeInfo NodeInfoFromCellInfo(CellInfo info)
        {

            switch ((CellType)info.PointType)
            {
                case CellType.Empty:
                    return new Empty();
                case CellType.Passable:
                    return new Passable();
                case CellType.Impassable:
                    return new Impassable();
                case CellType.Ladder:
                    return new Ladder((int)info.Pitch, (int)info.Yaw, (int)info.Roll);
                default:
                    return new Empty();
                //case CellType.Ladder:
                //    return new Ladder();
            }
        }

        public static CellType Type(this NodeInfo info)
        {
            if (info is Empty)
                return CellType.Empty;
            if(info is Passable)
                return CellType.Passable;
            if(info is Impassable)
                    return CellType.Impassable;
            if(info is Ladder)
                return CellType.Ladder;
            return CellType.Na;
        }

        public static IGraphAgent<Node> GraphAgentFromMovementType(MovementType type, PolygonGrid3D graph)
        {
            switch(type)
            {
                case MovementType.Walking:
                    return new RuningManAgent(graph);
                default:
                    return new RuningManAgent(graph);
            }
        }
    }
}
