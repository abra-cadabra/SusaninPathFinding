using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using SusaninPathFinding;
//using SusaninPathFinding.Geometry;
using SusaninPathFinding.Geometry;
using SusaninPathFinding.Graph;
using SusaninPathFinding.Graph.NodeInfoTypes;

namespace SusaninPathFindingBenchmark
{
    class SusaninPathFindingBenchmarkClass
    {
        private GraphManager Map;

        public SusaninPathFindingBenchmarkClass()
        {
            Grid3D Grid = new Grid3D(6, 6, 2, new Cell3D(92, 92, 128));

            //Map.NodeCosts[Map.FindNode(0 + 99, 0 + 99, 0)] = CellType.Impassable;
            //Map.NodeCosts[Map.FindNode(1 + 99, 0 + 99, 0)] = CellType.Impassable;
            //Map.NodeCosts[Map.FindNode(2 + 99, 0 + 99, 0)] = CellType.Impassable;
            //Map.NodeCosts[Map.FindNode(4 + 99, 0 + 99, 0)] = CellType.Impassable;
            //Map.NodeCosts[Map.FindNode(5 + 99, 0 + 99, 0)] = CellType.Impassable;
            //Map.NodeCosts[Map.FindNode(6 + 99, 0 + 99, 0)] = CellType.Impassable;
            //Map.NodeCosts[Map.FindNode(7 + 99, 0 + 99, 0)] = CellType.Impassable;

            //Map.NodeCosts[Map.FindNode(0 + 99, 1 + 99, 0)] = CellType.Impassable;
            //Map.NodeCosts[Map.FindNode(7 + 99, 1 + 99, 0)] = CellType.Impassable;

            //Map.NodeCosts[Map.FindNode(0 + 99, 2 + 99, 0)] = CellType.Impassable;
            //Map.NodeCosts[Map.FindNode(7 + 99, 2 + 99, 0)] = CellType.Impassable;

            //Map.NodeCosts[Map.FindNode(7 + 99, 3 + 99, 0)] = CellType.Impassable;

            //Map.NodeCosts[Map.FindNode(0 + 99, 4 + 99, 0)] = CellType.Impassable;
            //Map.NodeCosts[Map.FindNode(7 + 99, 4 + 99, 0)] = CellType.Impassable;

            //Map.NodeCosts[Map.FindNode(0 + 99, 5 + 99, 0)] = CellType.Impassable;
            //Map.NodeCosts[Map.FindNode(2 + 99, 5 + 99, 0)] = CellType.Impassable;
            //Map.NodeCosts[Map.FindNode(3 + 99, 5 + 99, 0)] = CellType.Impassable;
            //Map.NodeCosts[Map.FindNode(4 + 99, 5 + 99, 0)] = CellType.Impassable;
            //Map.NodeCosts[Map.FindNode(5 + 99, 5 + 99, 0)] = CellType.Impassable;
            //Map.NodeCosts[Map.FindNode(6 + 99, 5 + 99, 0)] = CellType.Impassable;
            //Map.NodeCosts[Map.FindNode(7 + 99, 5 + 99, 0)] = CellType.Impassable;

            //IList<Point3D> locations = Map.FindPath(new MovingAgent(Map.NodeCosts), Map.FindNode(123, 289, 0), Map.FindNode(5 + 99, 2 + 99, 0));
            Grid3D graph = (Grid3D)Map.Graph;
            Grid[3, 1, 0].Info = new Impassable();
            Grid[4, 1, 0].Info = new Impassable();
            Grid[5, 1, 0].Info = new Impassable();

            Grid[3, 2, 0].Info = new Impassable();
            Grid[5, 2, 0].Info = new Impassable();

            Grid[1, 3, 0].Info = new Ladder(GridDirection.East);
            Grid[2, 3, 0].Info = new Impassable();
            Grid[3, 3, 0].Info = new Impassable();
            Grid[5, 3, 0].Info = new Impassable();

            //===============================================================================

            Grid[3, 1, 1].Info = new Passable();
            Grid[4, 1, 1].Info = new Passable();
            Grid[5, 1, 1].Info = new Passable();

            Grid[3, 2, 1].Info = new Passable();
            Grid[5, 2, 1].Info = new Passable();

            Grid[2, 3, 1].Info = new Ladder(GridDirection.East);
            Grid[3, 3, 1].Info = new Passable();
            Grid[5, 3, 1].Info = new Passable();

            //IList<Point3D> locations = Map.FindPath(new MovingAgent(Map.NodeCosts), Map.FindNode(0, 0, 0), Map.FindNode(4, 1, 1));
        }

        public long Benchmark()
        {
            Stopwatch watch = new Stopwatch();
            
            watch.Start();
            IList<Vector3> locations = (IList<Vector3>)Map.FindPath(new RuningManAlgorithm((Grid3D)Map.Graph), Map.FindNode(0, 0, 0), Map.FindNode(4, 1, 1));
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }
    }
}
