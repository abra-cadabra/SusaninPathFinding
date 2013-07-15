using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using SusaninPathFinding.Collections;
using SusaninPathFinding.Geometry;
using SusaninPathFinding.Graph.NodeInfoTypes;
using SusaninPathFinding.Graph.PathFinding;
using SusaninPathFinding.Graph.Platformer2DGrid;
using SusaninPathFinding.Source.Graph;


namespace SusaninPathFinding.Graph.Platformer2DGrid
{
    public class PlatformerCharacter : IPlatformerTraversalStrategy
    {
        //public PolygonGrid3D Nodes { get; set; }
        #region Properties

        public float G { get; set; }
        public Box BindingBox { get; set; }
        public float JumpAccel { get; set; }
        public float WalkSpeed { get; set; }
        public float Vy { get; protected set; }
        public Vector3 Position { get; protected set; }
        public IGrid Grid { get; set; }

        public bool RelaxedRange
        {
            get { return false; }
        }

        #endregion
        
        #region Constructors

        public PlatformerCharacter(IGrid nodes, Vector3 position)
        {
            Grid = nodes;
            Position = position;
        }

        #endregion

        #region Functions

        public bool CanMakeStep(Cell source, Cell target)
        {

            if (target.X.AlmostEquals(2) && target.Y.AlmostEquals(2) && target.Z.AlmostEquals(0))
            {
                Debug.Write(target);
            }
            CellEdge edge;// = Grid.Edges[source, target];
            Grid.Edges.TryGetValue(source, target, out edge);
            //if ((target.X == 1 && target.Y == 4 && target.Z == 0) && (source.X == 1 && source.Y == 3 && source.Z == 0))
            //{
            //    Debug.Print("Stop");
            //}

            if (!CanPass(target) || ((edge != null) && !(edge.Info is Passable || edge.Info is Empty)))
                return false;
            Vector3 dirOffset, horOffset, wertOffset;
            horOffset = target - source;
            wertOffset = target - source;
            dirOffset = target - source;
            dirOffset.Z = 0;

            if (GridDirectionExtentions.IsSouth(dirOffset))
            {
                // Пока думаю
                double initX;
                double initY;
                double summT;

                if (source is PathNodeCell)
                {
                    initX = ((PathNodeCell)source).XPos;
                    initY = ((PathNodeCell)source).YDist;
                    summT = ((PathNodeCell) source).SummT;
                }
                else
                {
                    initX = source.WorldLocation.X;
                    initY = 0;
                    summT = 0;
                }

                double t = (Math.Sqrt(-2 * G * (-initY - Grid.Polygon.Bounds.SizeY))
                    - Math.Sqrt(-2 * G * (-initY))) / G;
                    //Grid.Polygon.Bounds.SizeY/G*(summT + 1);

                //double t = (Grid.Polygon.Bounds.SizeY-Vy)/G;
                //if(Vy!=0)
                //{
                //    t = Grid.Polygon.Bounds.SizeY / Vy;
                //}
                //else
                //{
                //    t = 1;
                //}
                

                double dX = WalkSpeed * t;

                double leftX = ((initX - dX < 0) ? 0 : initX - dX);
                double rightX = ((initX + dX >= Grid.RealSizeX) ? Grid.RealSizeX - 1 : initX + dX);

                int leftNodeX = (int)(leftX / Grid.Polygon.Bounds.SizeX);
                int rightNodeX = (int)(rightX / Grid.Polygon.Bounds.SizeX);
                if (target.X < leftNodeX || target.X > rightNodeX)
                {
                    return false;
                }
                // Пока думаю закончилось
            }

            if (GridDirectionExtentions.IsDiagonal(dirOffset))
            {
                horOffset.Y = 0;
                wertOffset.X = 0;

                bool hor;
                if (((Grid3D)Grid).Contains(source + horOffset))
                {
                    if (((Grid3D)Grid)[source + horOffset].Info is Empty)
                    {
                        Debug.Write(target + "is empty");
                    }
                    hor = ((Grid3D)Grid)[source + horOffset].Info is Passable;
                }
                else
                {
                    hor = true;
                }


                bool wert;
                if (((Grid3D)Grid).Contains(source + wertOffset))
                {
                    if (((Grid3D)Grid)[source + wertOffset].Info is Empty)
                    {
                        Debug.Write(target + "is empty");
                    }
                    wert = ((Grid3D)Grid)[source + wertOffset].Info is Passable;
                }
                else
                {
                    wert = true;
                }

                if (!(hor && wert && CanPass(target)))
                {
                    return false;
                }
                //return (hor && wert && CanMakeStep(source, target));
            }
            //else
            //{
            //    if (!(CanPass(target)))
            //        return false;
            //}


            if (target.Info is Ladder)
            {
                Vector3 p = (target - source);
                p.Z = 0;
                if (!(source.Info is Ladder))
                {
                    //bool invalidDirection1 = (p.GetDirection() != ((Ladder) target.Info).Direction);
                    //bool invalidDirection2 = (p.GetDirection() != ((Ladder) target.Info).Direction.Opposite());
                    if (target.Z != source.Z || !p.AsDirection().IsColinear(((Ladder)target.Info).Direction))
                    {
                        return false;
                    }
                }
                else
                {
                    if (p.AsDirection() != ((Ladder)target.Info).Direction && p.AsDirection() != ((Ladder)target.Info).Direction.Opposite())
                    {
                        return false;
                    }
                }
            }
            else
            {
                if (target.Z != source.Z)
                {
                    return false;
                }

                if (source.Info is Ladder)
                {
                    Vector3 p = (target - source);
                    if (!p.AsDirection().IsColinear(((Ladder)source.Info).Direction))
                    {
                        return false;
                    }
                }

            }
            return true;
        }


        public bool CanOccupy(Cell target)
        {
            return !(target.Info is Impassable || target.Info is Empty);
        }

        public double GetStepCost(Cell source, Cell target)
        {
            int cost;
            cost = 5;

            if (target.Info is Passable)
                cost = 5;
            else if (target.Info is Ladder)
                cost = 6;

            if(GridDirectionExtentions.IsDiagonal(target - source))
            {
                cost = (int)(cost * 1.4);
            }
            return cost;
        }

        public bool IsNearTarget(Cell source, Cell target, double distance)
        {
            return (distance.AlmostEquals(0, 0));
        }


        /// <summary>
        /// Determines whether the agent can pass through target node or not
        /// </summary>
        /// <param name="target">Target node</param>
        /// <returns>True, if the agent can pass through the node</returns>
        public bool CanPass(Cell target)
        {
            return (target.Info is Passable || target.Info is Ladder);
        }

        #endregion
        
    }
}
