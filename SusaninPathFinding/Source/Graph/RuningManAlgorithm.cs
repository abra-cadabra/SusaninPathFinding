using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using SusaninPathFinding.Collections;
using SusaninPathFinding.Geometry;
using SusaninPathFinding.Graph.NodeInfoTypes;
using SusaninPathFinding.Graph.PathFinding;
using SusaninPathFinding.Source.Graph;


namespace SusaninPathFinding.Graph
{
    public class RuningManAlgorithm : IGridMovementAlgorithm
    {
        //public PolygonGrid3D Nodes { get; set; }
        public IGrid Grid { get; set; }

        public RuningManAlgorithm(Grid3D nodes)
        {
            Grid = nodes;
        }

        public bool CanMakeStep(Cell source, Cell target)
        {
            //if (target.X.AlmostEquals(2) && target.Y.AlmostEquals(2) && target.Z.AlmostEquals(0))
            //{
            //    Debug.Write(target);
            //}

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

            

            if (GridDirectionExtentions.IsDiagonal(dirOffset))
            {
                horOffset.Y = 0;
                wertOffset.X = 0;

                bool hor;
                if(((Grid3D)Grid).Contains(source + horOffset))
                {
                    //if(((Grid3D) Grid)[source + horOffset].Info is Empty)
                    //{
                    //    Debug.Write(target + "is empty");
                    //}

                    hor = ((Grid3D) Grid)[source + horOffset].Info is Passable;
                }
                else
                {
                    hor = true;
                }


                bool wert;
                if (((Grid3D)Grid).Contains(source + wertOffset))
                {
                    //if (((Grid3D)Grid)[source + wertOffset].Info is Empty)
                    //{
                    //    Debug.Write(target + "is empty");
                    //}

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
            return (target.Info is Impassable && target.Info is Empty);
        }

        public double GetStepCost(Cell source, Cell target)
        {
            int cost;
            cost = 5;

            if (target.Info is Passable)
                cost = 5;
            else if (target.Info is Ladder)
                cost = 6;
                
            //switch (Nodes[target].Info.GetType())
            //{
            //    case CellType.Passable:
            //        cost = 5;
            //        break;
            //    case CellType.Ladder:
            //        cost = 6;
            //        break;
            //    default:
            //        cost = 5;
            //        break;
            //}
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

        

        public bool RelaxedRange
        {
            get { return false; }
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

        
    }
}
