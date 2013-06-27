using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SusaninPathFinding.Geometry;
using SusaninPathFinding.Graph.PathFinding;

namespace SusaninPathFinding.Graph.PathFinding
{
    /// <summary>
    /// Defines class of graph node.
    /// </summary>
    /// <typeparam name="TInfo">Type of </typeparam>
    public class OldGridNode3D<TInfo>
    {
        

        //#region Properties

        ///// <summary>
        ///// Defines node's 
        ///// </summary>
        //public TInfo Info { get; set; }

        //public IGraph<GridNode3D<TInfo>> Graph;

        //public Vector3 Index
        //{
        //    get { return this; }
        //    set
        //    {
        //        X = value.X;
        //        Y = value.Y;
        //        Z = value.Z;
        //    }
        //}

        ////public IGraph<INode> Graph { get; private set; }

        //public IPolygon Polygon { get; set; }

        //public Vector3 WorldLocation
        //{
        //    get 
        //    { 
        //        return new Vector3(
        //                        Index.X * Polygon.Bounds.SizeX,
        //                        Index.Y * Polygon.Bounds.SizeY,
        //                        Index.Z * Polygon.Bounds.SizeZ
        //                    );
        //    } 
        //    set
        //    {
        //        Index.X = (int)value.X / Polygon.Bounds.SizeX;
        //        Index.Y = (int)value.X / Polygon.Bounds.SizeY;
        //        Index.Z = (int)value.Z / Polygon.Bounds.SizeZ;
        //    }
        //}

        //public List<INode> Neighbors
        //{
        //    get { return (List<INode>)Graph.GetNeighbors(this); }
        //    private set{}
        //}

        //#endregion

        //#region Constructors

        //public GridNode3D(double x, double y, double z, IPolygon polygon, IGraph<GridNode3D<TInfo>> graph)
        //{
        //    Info = new TInfo();
        //    //Index = new Vector3(x, y, z);
        //    X = x;
        //    Y = y;
        //    Z = z;
        //    Polygon = polygon;
        //    Neighbors = new List<INode>();
        //    Graph = graph;
        //    //Graph = graph;
        //}

        ////public GridNode3D(double[] xyz, IPolygon polygon)
        ////{
        ////    Index.X = x;
        ////    Index.Y = y;
        ////    Index.Z = z;
        ////    Info = new TInfo();
        ////    Polygon = polygon;
        ////}

        //public GridNode3D(Vector3 v1, IPolygon polygon, IGraph<GridNode3D<TInfo>> graph)
        //{
        //    Index = (Vector3)v1.Clone();
        //    Info = new TInfo();
        //    Polygon = polygon;
        //    Neighbors = new List<INode>();
        //    Graph = graph;
        //}
        //#endregion

        
    }
}
