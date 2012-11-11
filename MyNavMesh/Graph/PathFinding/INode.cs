using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyNavMesh.Geometry;

namespace MyNavMesh.Graph.PathFinding
{
    public interface INode
    {
        #region Properties

        //IGraph<INode> Graph { get; }

        IPolygon Polygon { get; set; }

        Vector3 WorldLocation { get; set; }

        List<INode> Neighbors { get; }

        #endregion

        //public GenericNode(double x, double y, double z, IPolygon polygon, IGraph<GenericNode> graph)
        //    : base(x, y, z)
        //{
        //    Polygon = polygon;
        //    Graph = graph;
        //}

        //public GenericNode(double[] xyz, IPolygon polygon, IGraph<GenericNode> graph)
        //    : base(xyz)
        //{
        //    Polygon = polygon;
        //    Graph = graph;
        //}

        //public GenericNode(Vector3 v1, IPolygon polygon, IGraph<GenericNode> graph)
        //    : base(v1)
        //{
        //    Polygon = polygon;
        //    Graph = graph;
        //}
    }
}
