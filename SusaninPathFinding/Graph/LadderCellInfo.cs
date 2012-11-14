using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyNavMesh.Graph
{
    public class LadderCellInfo : CellInfo
    {
        public Direction Direction { get; set; }

        public new CellType Type { get; protected set; }

        //public LadderCellInfo()
        //{
        //    //Direction = new GraphDirection();
        //    Type = CellType.Ladder;
        //}

        public LadderCellInfo(Direction dir)
        {
            Direction = dir;
            Type = CellType.Ladder;
        }
    }
}
