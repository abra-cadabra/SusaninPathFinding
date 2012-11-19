using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using SusaninPathFinding.Collections;
using SusaninPathFinding.Geometry;
using SusaninPathFinding.Graph.NodeInfoTypes;
using SusaninPathFinding.Graph.PathFinding;


namespace SusaninPathFinding.Graph
{
    public class RuningManAgent : IGraphAgent<Node>
    {
        //public PolygonGrid3D Nodes { get; set; }
        public IGraph<Node> Nodes { get; set; }

        public RuningManAgent(PolygonGrid3D nodes)
        {
            Nodes = nodes;
        }

        public bool CanMakeStep(Node source, Node target)
        {
            if (target.X.AlmostEquals(2) && target.Y.AlmostEquals(2) && target.Z.AlmostEquals(0))
            {
                Debug.Write(target);
            }
            if (!CanPass(target))
                return false;
            Vector3 dirOffset, horOffset, wertOffset;
            horOffset = target - source;
            wertOffset = target - source;
            dirOffset = target - source;
            dirOffset.Z = 0;

            if (GridDirection.IsDiagonal(dirOffset))
            {
                horOffset.Y = 0;
                wertOffset.X = 0;

                bool hor;
                if(((PolygonGrid3D)Nodes).Contains(source + horOffset))
                {
                    if(((PolygonGrid3D) Nodes)[source + horOffset].Info is Empty)
                    {
                        Debug.Write(target + "is empty");
                    }
                    hor = ((PolygonGrid3D) Nodes)[source + horOffset].Info is Passable;
                }
                else
                {
                    hor = true;
                }


                bool wert;
                if (((PolygonGrid3D)Nodes).Contains(source + wertOffset))
                {
                    if (((PolygonGrid3D)Nodes)[source + wertOffset].Info is Empty)
                    {
                        Debug.Write(target + "is empty");
                    }
                    wert = ((PolygonGrid3D)Nodes)[source + wertOffset].Info is Passable;
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
            else
            {
                if (!(CanPass(target)))
                    return false;
            }

            if (target.Info is Ladder)
            {
                Vector3 p = (target - source);
                p.Z = 0;
                if (!(source.Info is Ladder))
                {
                    bool invalidDirection1 = (GridDirection.Directions(p) != ((Ladder) target.Info).Direction.Value);
                    bool invalidDirection2 = (GridDirection.Directions(p) != ((Ladder) target.Info).Direction.Opposite());
                    if (target.Z != source.Z || invalidDirection1 && invalidDirection2)
                    {
                        return false;
                    }
                }
                else
                {
                    if (GridDirection.Directions(p) != ((Ladder)target.Info).Direction.Value && GridDirection.Directions(p) != ((Ladder)target.Info).Direction.Opposite())
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
            }
            return true;
        }


        public bool CanOccupy(Node target)
        {
            return (target.Info is Impassable && target.Info is Empty);
        }

        public double GetStepCost(Node source, Node target)
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
            if(GridDirection.IsDiagonal(target - source))
            {
                cost = (int)(cost * 1.4);
            }
            return cost;
        }

        public bool IsNearTarget(Node source, Node target, double distance)
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
        public bool CanPass(Node target)
        {
            return (target.Info is Passable || target.Info is Ladder);
        }
    }
}
