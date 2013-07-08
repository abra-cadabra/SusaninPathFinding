using System.Collections.Generic;
using SusaninPathFinding.Geometry;
using SusaninPathFinding.Graph.NodeInfoTypes;

namespace SusaninPathFinding.Graph
{
    public class Cell : Vector3
    {

        #region Properties

        public Grid3D Grid { get; protected set; }

        public IPolygon Polygon { get; set; }

        public Vector3 WorldLocation
        {
            get
            {
                return Grid.GetWorldLocation(this);
            }
            protected set
            {
            }
        }

        public List<Cell> Neighbors
        {
            get
            {
                return (List<Cell>)Grid.GetNeighbors(this);
            }
        }

        //public List<Node> Connections
        //{
        //    get
        //    {
        //        return (List<Node>)Grid.GetConnections(this);
        //    }
        //}

        public INodeInfo Info { get; set; }

        #endregion

        #region Constructors

        private void Init(double x, double y, double z, IPolygon polygon, Grid3D graph, INodeInfo info = null)
        {
            X = x;
            Y = y;
            Z = z;
            if (info != null)
            {
                Info = (INodeInfo)info.Clone();
            }
            else
            {
                Info = new Empty();
            }
            Polygon = polygon;
            Grid = graph;
        }

        public Cell(double x, double y, double z, IPolygon polygon, Grid3D graph, INodeInfo info = null)
        {
            Init(x, y, z, polygon, graph, info);
        }

        public Cell(Vector3 v, IPolygon polygon, Grid3D graph, INodeInfo info = null)
        {
            Init(v.X, v.Y, v.Z, polygon, graph, info);
        }
        #endregion

        #region Functions

        public List<Cell> NeighborsPassableBy(IMovementAlgorithm<Cell> agent)
        {
            return (List<Cell>)Grid.GetNeighbors(this, agent);
        }

        public CellsNeighboringState IsNeighborTo(Cell second)
        {
            Vector3 crd = this - second;

            if(crd.X == 0 && crd.Y == 0 && crd.Z == 0)
                return CellsNeighboringState.Same;

            bool CrdXInRange = crd.X >= -1 && crd.X <= 1;
            bool CrdYInRange = crd.Y >= -1 && crd.Y <= 1;
            bool CrdZInRange = crd.Z >= -1 && crd.Z <= 1;

            if (CrdXInRange && CrdYInRange && CrdZInRange)
                return CellsNeighboringState.Neighbors;

            return CellsNeighboringState.NonNeighbors;
        }

        public CellEdge GetEdge(GridDirection direction)
        {
            Cell neighbor = Grid[this + direction.AsOffset()];
            return Grid.Edges[this, neighbor];
        }
        //public SquareNode GetNeighbor(CompassDirection direction)
        //{
        //    Vector3 v = (Vector3)this.Clone() + GridDirection.Offsets[(int) direction];
        //    return Graph[v];
        //}
        #endregion
    }

    public enum CellsNeighboringState
    {
        Neighbors,
        NonNeighbors,
        Same
    }
}