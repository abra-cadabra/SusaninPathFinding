using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using SusaninPathFinding.Collections;
using SusaninPathFinding.Geometry;
using SusaninPathFinding.Graph.NodeInfoTypes;
using SusaninPathFinding.Graph.PathFinding;
using SusaninPathFinding.Source.Graph;

namespace SusaninPathFinding.Graph
{
    public class Grid3D : IGrid//IGraph<Cell>
    {
        #region Fields

        /// <summary>
        /// Node collection
        /// </summary>
        

        #endregion

        #region Overloaded operators

        /// <summary>
        /// Provides access to the nodes storage.
        /// </summary>
        /// <param name="x">Coordinate X</param>
        /// <param name="y">Coordinate Y</param>
        /// <param name="z">Coordinate Z</param>
        /// <returns>A node described by three coordinates.</returns>
        public Cell this[int x, int y, int z]
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

        public Cell this[Vector3 v]
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

        public int RealSizeX
        {
            get { return (int)(SizeX*Polygon.Bounds.SizeX); }
        }

        public int RealSizeY
        {
            get { return (int)(SizeY * Polygon.Bounds.SizeY); }
        }

        public int RealSizeZ
        {
            get { return (int)(SizeZ * Polygon.Bounds.SizeZ); }
        }

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

        public IPolygon Polygon
        {
            get;
            set;
        }

        public CellEdgeCollection Edges { get; private set; }

        #endregion

        #region Constructors

        public Grid3D(int x, int y, int z, Cell3D polygon, INodeInfo fill)
        {
            Init(polygon);
            Create(x, y, z, polygon, fill);
        }

        public Grid3D(int x, int y, int z, Cell3D polygon)
        {
            Init(polygon);
            Create(x, y, z, polygon);
        }

        public Grid3D(Cell3D polygon)
        {
            Init(polygon);
        }

        public void Init(Cell3D polygon)
        {
            Polygon = polygon;
            Edges = new CellEdgeCollection();
        }
        #endregion

        #region Functions

        /// <summary>
        /// Creates new grid of specefic size
        /// </summary>
        /// <param name="sizeX">X size parameter</param>
        /// <param name="sizeY">Y size parameter</param>
        /// <param name="sizeZ">Z size parameter</param>
        public void Create(int sizeX, int sizeY, int sizeZ, Cell3D polygon, INodeInfo fill = null)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            SizeZ = sizeZ;
            Nodes = new ArrayEx<Cell>(new int[] { SizeX, SizeY, SizeZ });

            for (int k = 0; k < SizeZ; k++)
            {
                for (int j = 0; j < SizeY; j++)
                {
                    for (int i = 0; i < SizeX; i++)
                    {
                        //_nodes[i, j, k] = new Node3D<T>(i * Polygon.Bounds.SizeX, j * Polygon.Bounds.SizeY, k * Polygon.Bounds.SizeZ, Polygon);
                        Nodes[i, j, k] = new Cell(i, j, k, Polygon, this, fill);
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

        IEnumerable<Cell> IGraph<Cell>.Nodes { get { return Nodes; } }


        public ArrayEx<Cell> Nodes { get; private set; }

        #endregion

        #region Functions

        public bool Contains(Cell node)
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

        public double GetManhettenDistance(Cell source, Cell target)
        {
            Vector3 t = GetWorldLocation(target);
            Vector3 s = GetWorldLocation(source);

            return (Math.Abs(t.X - s.X) + Math.Abs(t.Y - s.Y) + Math.Abs(t.Z - s.Z));
        }



        public Cell GetNearestNode(Vector3 location)
        {
            //int x = location.X / Polygon.;
            return this[(int)location.X, (int)location.Y, (int)location.Z];
        }

        public Cell GetNearestNode(int x, int y, int z)
        {
            return this[x, y, z];
        }

        public IList<Cell> GetNeighbors(Cell node)
        {
            return GetNeighbors(node, null);
        }

        public IList<Cell> GetNeighbors(Cell node, Object agent)
        {
            if(!(agent is ITraversalStrategy<Cell>))
                throw new ArgumentException(String.Format(Strings.ArgumentTypeMismatch, typeof(IGridTraversalStrategy)));

            //if (!(agent is IGridMovementAlgorithm))
            //    throw new ArgumentException(String.Format(Strings.ArgumentTypeMismatch, typeof(IGridMovementAlgorithm)));

            return GetNeighbors(node, (IGridTraversalStrategy)agent);
        }

        //public IList<Node> GetConnections(Node node)
        //{
        //    return Edges[(int)node.X, (int)node.Y, (int)node.Z];
        //}

        public new IList<Cell> GetNeighbors(Cell node, IGridTraversalStrategy agent)
        {
            IList<Cell> neighbors = new List<Cell>();//GraphDirection.Offsets.Length);
            if (agent != null && agent.Grid != this)
                agent.Grid = this;
            for (int i = 1; i < GridDirectionExtentions.Offsets.Length - 1; i++)
            {
                if(Contains(node + GridDirectionExtentions.Offsets[i]))
                {
                    Cell neighbor = this[node + GridDirectionExtentions.Offsets[i]];
                    //if (!Contains(neighbor))
                    //    continue;

                    if (agent != null && !agent.CanMakeStep(node, neighbor))
                        continue;

                    neighbors.Add(neighbor);
                }
                
            }
            return neighbors;
        }

        public Vector3 GetWorldLocation(Cell node)
        {
            //throw new NotImplementedException();
            return GetWorldLocation((Vector3)node);
        }

        public Vector3[] GetWorldRegion(Cell node)
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
                (node.X +1.0f/2) * Polygon.Bounds.SizeX,
                (node.Y +1.0f/2) * Polygon.Bounds.SizeY,
                (node.Z +1.0f/2) * Polygon.Bounds.SizeZ);
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
