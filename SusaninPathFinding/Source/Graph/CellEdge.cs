using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SusaninPathFinding.Geometry;
using SusaninPathFinding.Graph.NodeInfoTypes;

namespace SusaninPathFinding.Graph
{
    /// <summary>
    /// Class that provides information about cell edge.
    /// </summary>
    public class CellEdge
    {
        #region Properties

        /// <summary>
        /// Source node for this edge
        /// </summary>
        public Cell From { get; set; }

        /// <summary>
        /// Destenation node for this edge
        /// </summary>
        public Cell To { get; set; }

        /// <summary>
        /// Information about this edge
        /// </summary>
        public NodeInfo Info { get; set; }

        #endregion

        #region Constructors

        public CellEdge(NodeInfo info)
        {
            Info = info;
        }

        public CellEdge(Cell from, Cell to, NodeInfo info)
        {
            From = from;
            To = to;
            Info = info;
        }

        #endregion
    }
}
