using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SusaninPathFinding.Graph;
using SusaninPathFinding.Graph.NodeInfoTypes;

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
                    return new Ladder((CompassDirection)info.Direction);
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
    }
}
