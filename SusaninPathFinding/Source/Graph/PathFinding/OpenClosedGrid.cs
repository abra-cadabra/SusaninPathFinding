using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using SusaninPathFinding.Collections;
using SusaninPathFinding.Geometry;
using SusaninPathFinding.Graph;
using SusaninPathFinding.Graph.PathFinding;

namespace SusaninPathFinding.Source.Graph.PathFinding
{
    public class OpenClosedGrid : IGrid//IGraph<PathNodeCell>
    {
        #region Overloaded operators

        /// <summary>
        /// Provides access to the nodes storage.
        /// </summary>
        /// <param name="x">Coordinate X</param>
        /// <param name="y">Coordinate Y</param>
        /// <param name="z">Coordinate Z</param>
        /// <returns>A node described by three coordinates.</returns>
        //public Cell this[int x, int y, int z]
        //{
        //    get
        //    {
        //        if (x < 0 && x >= SizeX)
        //        {
        //            string message = String.Format(CultureInfo.CurrentCulture, Strings.ArgumentGreaterAndLess, SizeX, 0);
        //            throw new ArgumentOutOfRangeException("x", message);
        //        }
        //        if (y < 0 && y >= SizeY)
        //        {
        //            string message = String.Format(CultureInfo.CurrentCulture, Strings.ArgumentGreaterAndLess, SizeY, 0);
        //            throw new ArgumentOutOfRangeException("y", message);
        //        }
        //        if (z < 0 && z >= SizeZ)
        //        {
        //            string message = String.Format(CultureInfo.CurrentCulture, Strings.ArgumentGreaterAndLess, SizeZ, 0);
        //            throw new ArgumentOutOfRangeException("z", message);
        //        }
        //        return Nodes[x, y, z];
        //    }

        //    set
        //    {
        //        Nodes[x, y, z] = value;
        //    }
        //}

        public Cell this[Cell v]
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
                return Nodes[v];
            }

            set
            {
                
                Nodes[v] = (PathNodeCell)value;
            }
        }


        #endregion

        #region Properties

        public int Connectivity { get; private set; }
        public int NodeCount { get; private set; }

        IEnumerable<Cell> IGraph<Cell>.Nodes { get { return (IEnumerable<PathNodeCell>)Nodes; } }

        public Dictionary<Cell, PathNodeCell> Nodes { get; private set; }

        public Grid3D Grid { get; private set; }

        public int SizeX { get; set; }
        public int SizeY { get; set; }
        public int SizeZ { get; set; }

        public CellEdgeCollection Edges { get; private set; }

        public IPolygon Polygon { get; set; }

        #endregion

        #region Constructors

        public OpenClosedGrid(Grid3D grid)
        {
            Grid = grid;
            Nodes = new Dictionary<Cell, PathNodeCell>();
        }

        #endregion

        #region Functions

        public bool Contains(Cell node)
        {
            if (Nodes.ContainsValue((PathNodeCell)node))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public double GetManhettenDistance(Cell source, Cell target)
        {
            throw new NotImplementedException();
        }

        public Cell GetNearestNode(Vector3 location)
        {
            return Nodes[Grid.GetNearestNode(location)];
        }

        public Cell GetNearestNode(int x, int y, int z)
        {
            return Nodes[Grid.GetNearestNode(x, y, z)];
        }

        public IList<Cell> GetNeighbors(Cell node)
        {
            throw new NotImplementedException();
        }

        public IList<Cell> GetNeighbors(Cell node, object agent)
        {
            throw new NotImplementedException();
        }

        public IList<Cell> GetNeighbors(Cell node, IGridMovementAlgorithm agent)
        {
            IList<Cell> neighbors = new List<Cell>();//GraphDirection.Offsets.Length);
            if (agent != null && agent.Grid != Grid)
                agent.Grid = Grid;
            for (int i = 1; i < GridDirectionExtentions.Offsets.Length - 1; i++)
            {
                if (Grid.Contains(node + GridDirectionExtentions.Offsets[i]))
                {
                    Cell neighbor = Grid[node + GridDirectionExtentions.Offsets[i]];
                    //if (!Contains(neighbor))
                    //    continue;

                    if (agent != null && !agent.CanMakeStep(node, neighbor))
                        continue;

                    neighbors.Add(neighbor);
                }

            }
            return neighbors;
        }

        public Vector3 GetWorldLocation(Vector3 node)
        {
            throw new NotImplementedException();
        }

        public Vector3[] GetWorldRegion(Cell node)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
