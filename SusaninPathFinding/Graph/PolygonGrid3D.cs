using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using SusaninPathFinding.Collections;
using SusaninPathFinding.Geometry;
using SusaninPathFinding.Graph.PathFinding;

namespace SusaninPathFinding.Graph
{
    public class PolygonGrid3D : IGraph<Node>
    {
        #region Fields

        /// <summary>
        /// Node collection
        /// </summary>
        //private ArrayEx<Node3D<T>> _nodes;

        #endregion

        #region Overloaded operators

        /// <summary>
        /// Provides access to the nodes storage.
        /// </summary>
        /// <param name="x">Coordinate X</param>
        /// <param name="y">Coordinate Y</param>
        /// <param name="z">Coordinate Z</param>
        /// <returns>A node described by three coordinates.</returns>
        public Node this[int x, int y, int z]
        {
            get
            {
                if(x < 0 && x >= SizeX)
                {
                    string message = String.Format(CultureInfo.CurrentCulture, Strings.ArgumentGreaterAndLess, SizeX, 0);
                    throw new ArgumentOutOfRangeException("x", message);
                }
                if (y < 0 && y >= SizeY)
                {
                    string message = String.Format(CultureInfo.CurrentCulture, Strings.ArgumentGreaterAndLess, SizeY, 0);
                    throw new ArgumentOutOfRangeException("y", message);
                }
                if(z < 0 && z >= SizeZ)
                {
                    string message = String.Format(CultureInfo.CurrentCulture, Strings.ArgumentGreaterAndLess, SizeZ, 0);
                    throw new ArgumentOutOfRangeException("z", message);
                }
                return Nodes[x, y, z];
            }

            set
            {
                Nodes[x, y, z] = value;
            }
        }

        public Node this[Vector3 v]
        {
            get
            {
                if (v.X < 0 && v.X > SizeX)
                {
                    string message = String.Format(CultureInfo.CurrentCulture, Strings.ArgumentGreaterAndLess, 0, SizeX);
                    throw new ArgumentOutOfRangeException("x", message);
                }
                if (v.Y < 0 && v.Y > SizeY)
                {
                    string message = String.Format(CultureInfo.CurrentCulture, Strings.ArgumentGreaterAndLess, 0, SizeY);
                    throw new ArgumentOutOfRangeException("y", message);
                }
                if (v.Z < 0 && v.Z > SizeZ)
                {
                    string message = String.Format(CultureInfo.CurrentCulture, Strings.ArgumentGreaterAndLess, 0, SizeZ);
                    throw new ArgumentOutOfRangeException("z", message);
                }
                return Nodes[(int)v.X, (int)v.Y, (int)v.Z];
            }

            set
            {
                Nodes[(int)v.X, (int)v.Y, (int)v.Z] = value;
            }
        }

        
        #endregion

        #region Properties

        public int SizeX { get; set; }

        public int SizeY { get; set; }

        public int SizeZ { get; set; }

        public Vector3 Size
        {
            get
            {
                return new Vector3(SizeX, SizeY, SizeZ);
            }
            //set
            //{
            //    SizeX = (int)value.X;
            //    SizeY = (int)value.Y;
            //    SizeZ = (int)value.Z;

            //}
        }

        public Cell3D Polygon
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public PolygonGrid3D(int x, int y, int z, Cell3D polygon)
        {
            Polygon = polygon;
            Create(x, y, z);
        }

        public PolygonGrid3D(Cell3D polygon)
        {
            Polygon = polygon;
            //_points = new List<Point3D>();
        }
        #endregion

        #region Functions

        /// <summary>
        /// Creates new grid of specefic size
        /// </summary>
        /// <param name="sizeX">X size parameter</param>
        /// <param name="sizeY">Y size parameter</param>
        /// <param name="sizeZ">Z size parameter</param>
        public void Create(int sizeX, int sizeY, int sizeZ)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            SizeZ = sizeZ;
            Nodes = new ArrayEx<Node>(new int[] {SizeX, SizeY, SizeZ});

            for (int k = 0; k < SizeZ; k++ )
            {
                for(int j = 0; j<SizeY; j++)
                {
                    for (int i = 0; i < SizeX; i++)
                    {
                        //_nodes[i, j, k] = new Node3D<T>(i * Polygon.Bounds.SizeX, j * Polygon.Bounds.SizeY, k * Polygon.Bounds.SizeZ, Polygon);
                        Nodes[i, j, k] = new Node(i, j, k, Polygon, this);
                    }
                }
            }
        }
        #endregion

        #region IGraph implementation

        #region Properties

        public int Connectivity
        {
            get { return Polygon.Connectivity; }
        }

        public int NodeCount
        {
            get { throw new NotImplementedException(); }
        }

        IEnumerable<Node> IGraph<Node>.Nodes { get { return Nodes; } }


        public ArrayEx<Node> Nodes { get; private set; }

        #endregion

        #region Functions

        public bool Contains(Node node)
        {
            return Contains((int)node.X, (int)node.Y, (int)node.Z); //(node.X >= 0 && node.X < _x && node.Y >= 0 && node.Y < _y && node.Z >= 0 && node.Z < _z);
        }

        public bool Contains(int x, int y, int z)
        {
            return (x >= 0 && x < SizeX && y >= 0 && y < SizeY && z >= 0 && z < SizeZ) && (!Nodes[x, y, z].Equals(null));
        }

        public bool Contains(Vector3 node)
        {
            return Contains((int)node.X, (int)node.Y, (int)node.Z);
        }

        public double GetManhettenDistance(Node source, Node target)
        {
            Vector3 t = GetWorldLocation(target);
            Vector3 s = GetWorldLocation(source);

            return (Math.Abs(t.X - s.X) + Math.Abs(t.Y - s.Y) + Math.Abs(t.Z - s.Z));
        }



        Node IGraph<Node>.GetNearestNode(Vector3 location)
        {
            //int x = location.X / Polygon.;
            return this[(int)location.X, (int)location.Y, (int)location.Z];
        }

        public Node GetNearestNode(int x, int y, int z)
        {
            return this[x, y, z];
        }

        public IList<Node> GetNeighbors(Node node)
        {
            return GetNeighbors(node, null);
        }

        public IList<Node> GetNeighbors(Node node, IGraphAgent<Node> agent = null)
        {
            IList<Node> neighbors = new List<Node>();//GraphDirection.Offsets.Length);
            if (agent != null && agent.Nodes != this)
                agent.Nodes = this;
            for (int i = 1; i < GridDirection.Offsets.Length - 1; i++)
            {
                if(Contains(node + GridDirection.Offsets[i]))
                {
                    Node neighbor = this[node + GridDirection.Offsets[i]];
                    //if (!Contains(neighbor))
                    //    continue;

                    if (agent != null && !agent.CanMakeStep(node, neighbor))
                        continue;

                    neighbors.Add(neighbor);
                }
                
            }
            return neighbors;
        }

        public Vector3 GetWorldLocation(Node node)
        {
            //throw new NotImplementedException();
            return GetWorldLocation((Vector3)node);
        }

        public Vector3[] GetWorldRegion(Node node)
        {
            throw new NotImplementedException();
        }

        public IList<Vector3> GetNeighbors(Vector3 node)
        {
            throw new NotImplementedException();
        }

        //public Vector3 GetNearestNode(Vector3 location)
        //{
        //    return this[(int)location.X, (int)location.Y, (int)location.Z];
        //}

        //public IList<Vector3> GetNeighbors(Vector3 node, IGraphAgent<Vector3> agent)
        //{
        //    IList<Vector3> neighbors = new List<Vector3>(GraphDirection.Offsets.Length);

        //    for (int i = 0; i < GraphDirection.Offsets.Length-1; i++)
        //    {
        //        Vector3 neighbor = node + GraphDirection.Offsets[i];
        //        if (Contains(neighbor) && agent.CanMakeStep(node, neighbor))
        //        {
        //            neighbors.Add(neighbor);
        //        }
        //    }
        //    return neighbors;
        //}

        //public Vector3 GetWorldLocation(object node)
        //{
        //    if(node.GetType().IsSubclassOf(typeof(Vector3)))
        //    {
        //        return GetWorldLocation((Vector3) node);
        //    }
        //    else
        //    {
        //        var message = String.Format(CultureInfo.CurrentCulture, Strings.ArgumentAncestorMismatch, typeof(Vector3));
        //        throw new ArgumentException(message, "node");
        //    }
            
        //    //return new Vector3(
        //    //    node.X * Polygon.Bounds.SizeX,
        //    //    node.Y * Polygon.Bounds.SizeY,
        //    //    node.Z * Polygon.Bounds.SizeZ);
        //}

        public Vector3 GetWorldLocation(Vector3 node)
        {
            return new Vector3(
                node.X * Polygon.Bounds.SizeX,
                node.Y * Polygon.Bounds.SizeY,
                node.Z * Polygon.Bounds.SizeZ);
        }

        //public Vector3 GetWorldLocation(Vector3 node)
        //{
        //    return new Vector3(
        //        node.X * (int)Polygon.Bounds.SizeX,
        //        node.Y * (int)Polygon.Bounds.SizeY,
        //        node.Z * (int)Polygon.Bounds.SizeZ);
        //}

        public Vector3[] GetWorldRegion(Vector3 node)
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion

        
    }
}
