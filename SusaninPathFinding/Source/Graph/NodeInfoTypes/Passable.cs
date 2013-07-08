using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SusaninPathFinding.Graph.NodeInfoTypes;

namespace SusaninPathFinding.Graph.NodeInfoTypes
{
    /// <summary>
    /// Типы клеток
    /// </summary>
    //public enum CellType
    //{
    //    /// <summary>
    //    /// Пустота
    //    /// </summary>
    //    Empty = 1,

    //    /// <summary>
    //    /// Проходимая
    //    /// </summary>
    //    Passable = 1 << 1,

    //    /// <summary>
    //    /// Непроходимая
    //    /// </summary>
    //    Impassable = 1 << 2,

    //    /// <summary>
    //    /// Лестница
    //    /// </summary>
    //    Ladder = 1 << 3
    //}

    public class Passable : INodeInfo
    {

        //private CellType _type;

        //public virtual CellType Type
        //{
        //    get { return _type; }
        //    set
        //    {
        //        if (value == CellType.Ladder)
        //            throw new ArithmeticException("Type forbidden. Pleace, create new LadderCellInfo insted");
        //        _type = value;
        //    }
        //}

        //public Passable()
        //{
        //    Type = CellType.Empty;
        //}

        //public Passable(CellType type)
        //{
        //    Type = type;
        //}

        public object Clone()
        {
            return new Passable();
        }
    }
}
