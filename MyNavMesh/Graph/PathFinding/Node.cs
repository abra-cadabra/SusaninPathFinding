using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyNavMesh.Geometry;
using MyNavMesh.Graph.NodeInfoTypes;

namespace MyNavMesh.Graph.PathFinding
{
    public class Node : Vector3
    {
        #region Properties

        public IGraph<Node> Graph { get; protected set; }

        public IPolygon Polygon { get; set; }

        public Vector3 WorldLocation
        {
            get
            {
                return Graph.GetWorldLocation(this);
            }
            protected set
            {
            }
        }

        public List<Node> Neighbors
        {
            get
            {
                return (List<Node>)Graph.GetNeighbors(this);
            }
        }

        public NodeInfo Info { get; set; }

        #endregion

        #region Constructors

        private void Init(double x, double y, double z, IPolygon polygon, IGraph<Node> graph, NodeInfo info = null)
        {
            X = x;
            Y = y;
            Z = z;
            if (info != null)
            {
                Info = info;
            }
            else
            {
                if(Z.AlmostEquals(0, 0))
                {
                    Info = new Passable();
                }
                else
                {
                    Info = new Empty();
                }
            }
            Polygon = polygon;
            Graph = graph;
        }

        public Node(double x, double y, double z, IPolygon polygon, IGraph<Node> graph, NodeInfo info = null)
        {
            Init(x, y, z, polygon, graph, info);
        }

        public Node(Vector3 v, IPolygon polygon, IGraph<Node> graph, NodeInfo info = null)
        {
            Init(v.X, v.Y, v.Z, polygon, graph, info);
        }
        #endregion

        #region Functions

        public List<Node> NeighborsPassableBy(IGraphAgent<Node> agent)
        {
            return (List<Node>)Graph.GetNeighbors(this, agent);
        }
        #endregion
    }
}
