using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SusaninPathFinding.Geometry;


namespace SusaninPathFinding.Graph.Platformer2DGrid
{
    public class Platformer2DGraph : Grid3D
    {
        public Platformer2DGraph(int x, int y, int z, Cell3D polygon) : base(x, y, z, polygon)
        {
        }

        public Platformer2DGraph(Cell3D polygon)
            : base(polygon)
        {
        }

        public new IList<Cell> GetNeighbors(Cell node, IMovementAlgorithm<Cell> agent)
        {
            if (!(agent is IGridMovementAlgorithm))
                throw new ArgumentException(String.Format(Strings.ArgumentTypeMismatch, typeof(IGridMovementAlgorithm)));

            return GetNeighbors(node, (IGridMovementAlgorithm)agent);
        }

        public new IList<Cell> GetNeighbors(Cell node, IPlatformer2DMovementAlgorithm agent = null)
        {
            float x, y;
            int dt;

            IList<Cell> neighbors = new List<Cell>();//GraphDirection.Offsets.Length);
            if (agent != null && agent.Grid != this)
                agent.Grid = this;
            for (int i = 1; i < GridDirectionExtentions.Offsets.Length - 1; i++)
            {
                if (Contains(node + GridDirectionExtentions.Offsets[i]))
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

        public List<Vector3> GetFallArc(Vector3 start, float Vx, float g)
        {
            int realStartX = (int)(start.X * Polygon.Bounds.SizeX);
            int realStartY = (int)(start.Y * Polygon.Bounds.SizeY);
            List<Vector3> results = new List<Vector3>();
            results.Add(start);
            for(int t = 1; ; t++)
            {
                int x = (int)(t*Vx);

                int minX = (int)(((realStartX - x < 0) ? 0 : realStartX - x) / Polygon.Bounds.SizeX);
                int maxX = (int)(((realStartX + x >= RealSizeX) ? RealSizeX - 1 : realStartX + x) / Polygon.Bounds.SizeX);


                int Y = (int)(realStartY + (g * Math.Pow(t, 2)) / 2);
                int nextY = (int)(realStartY + (g * Math.Pow(t + 1, 2)) / 2);

                int minY = (int)(((Y >= RealSizeY) ? RealSizeY - 1 : Y) / Polygon.Bounds.SizeY);
                int maxY = (int)(((nextY > RealSizeY) ? RealSizeY - 1 : nextY) / Polygon.Bounds.SizeY);

                for (int i = minY + 1; i <= maxY; i++)
                {
                    for(int j = minX; j <= maxX; j++)
                    {
                        results.Add(this[j, i, (int)start.Z]);
                    }
                }
                if (maxY == SizeY - 1)
                    break;
            }
            return results;
        }

        public List<Vector3> GetFallNeighbors(Cell node, double Vx, double Vy)
        {
            int realNodeX = (int)(node.X * Polygon.Bounds.SizeX);
            int minX = (int)(((realNodeX - Vx < 0) ? 0 : realNodeX - Vx) / Polygon.Bounds.SizeX);
            int maxX = (int)(((realNodeX + Vx >= RealSizeX) ? RealSizeX - 1 : realNodeX + Vx) / Polygon.Bounds.SizeX);

            int realNodeY = (int)(node.Y * Polygon.Bounds.SizeY);
            int nextY = (int)(((realNodeY + Vy >= RealSizeY) ? RealSizeY - 1 : Math.Ceiling(realNodeY + Vy)) / Polygon.Bounds.SizeY);

            List<Vector3> results = new List<Vector3>();
            for (int i = (int)node.Y + 1; i <= nextY; i++)
            {
                for (int j = minX; j <= maxX; j++)
                {
                    results.Add(this[j, i, (int)node.Z]);
                }
            }

            return results;
        }

        /// <summary>
        /// Takes REAL coordinates of the cursor and returns the coordinates of every cell he can move while falling
        /// plus the coordinates of max left and right inclinations.
        /// </summary>
        /// <param name="realCrd">Real coordinates of a moving object or a cursor</param>
        /// <param name="Vx">X velocity of the object</param>
        /// <param name="Vy">Y velocity of the object</param>
        /// <returns></returns>
        public List<Vector3> GetFallStep(Vector3 realCrd, double Vx, double Vy)
        {
            //int realNodeX = (int)(node.X * Polygon.Bounds.SizeX);

            double t = Polygon.Bounds.SizeY/Vy;

            double dX = Vx*t;

            double leftX = ((realCrd.X - dX < 0) ? 0 : realCrd.X - dX);
            double rightX = ((realCrd.X + dX >= RealSizeX) ? RealSizeX - 1 : realCrd.X + dX);

            //int realNodeY = (int)(node.Y * Polygon.Bounds.SizeY);
            //int nextY = (int)(((realNodeY + Vy >= RealSizeY) ? RealSizeY - 1 : Math.Ceiling(realNodeY + Vy)) / Polygon.Bounds.SizeY);

            
            //for (int i = (int)node.Y + 1; i <= nextY; i++)
            //{

            int minX = (int) (leftX/Polygon.Bounds.SizeX);
            int maxX = (int)(rightX / Polygon.Bounds.SizeX);
            List<Vector3> results = new List<Vector3>();
            results.Add(new Vector3(leftX, realCrd.Y + (Vy * t), realCrd.Z));

            for (int i = minX+1; i < maxX; i++)
            {
                results.Add(new Vector3(i * Polygon.Bounds.SizeX, realCrd.Y + (Vy * t), realCrd.Z));
            }
            results.Add(new Vector3(rightX, realCrd.Y + (Vy * t), realCrd.Z));
            //}

            return results;
        }
    }
}
