using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using SusaninPathFinding.Collections;
using SusaninPathFinding.Geometry;
using SusaninPathFinding.Graph;

namespace SusaninPathFinding.Source.Graph
{
    public interface IGrid : IGraph<Cell>
    {

        #region Properties

        int SizeX { get; set; }

        int SizeY { get; set; }

        int SizeZ { get; set; }

        CellEdgeCollection Edges { get;}
        IPolygon Polygon { get; set; }

        #endregion
    }
}
