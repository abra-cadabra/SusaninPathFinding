using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SusaninPathFinding.Geometry;
using SusaninPathFinding.Graph;
using SusaninPathFinding.Graph.PathFinding;


namespace SusaninPathFinding
{
    
    public class GraphManager
    {
        #region Fields

        //private int _x = 6;

        //private int _y = 6;

        //private int _z = 2;

        private Dictionary<Vector3, Passable> _nodeCosts;

        //private List<Subdivision> _navMeshes;

        #endregion

        #region Properties

        public int X { get; set; }

        public int Y { get; set; }

        public int Z { get; set; }

        public IGraph<Node> Graph { get; set; }

        //public Dictionary<Vector3, CellInfo> NodeCosts
        //{
        //    get { return _nodeCosts; }
        //    set
        //    {
        //        _nodeCosts = value;

        //    }
        //}

        #endregion

        public GraphManager()
        {
            int x = 6, y = 6, z=2;
            //NodeCosts = new Dictionary<Vector3, CellInfo>();
            //Polygon3D polygon = new Polygon3D(92, 128, 4, PolygonOrientation.OnEdge, true);
            Cell3D polygon = new Cell3D(92, 92, 128);
            PolygonGrid3D grid = new PolygonGrid3D(polygon);

            
            //grid.Size = new Vector3(x, y, z);
            grid.Create(x, y, z);

            Graph = grid;

            foreach (Node node in Graph.Nodes)
            {
                if (node.Z == 0)
                    node.Info = new Passable();
            }
        }

        //private void BuildTriangulations()
        //{
            
        //    for(int i = 0; i < Z; i++)
        //    {
        //        List<Vector3> points = new List<Vector3>();
        //        for(int j = 0; j < Y; j++)
        //        {
        //            for(int k = 0; k < X; k++)
        //            {
        //                Vector3 point = new Vector3{ X = k, Y = j, Z = i};
                        
        //                switch (_nodeCosts[point].Type)
        //                {
        //                    case CellType.Impassable:
        //                        if(_nodeCosts[point+Direction.NorthWest].Type == CellType.Passable)
        //                        {
        //                            if (_nodeCosts[point + Direction.West].Type == _nodeCosts[point + Direction.North].Type)
        //                            {
        //                                points.Add(new PointD(k, j));
        //                            }
        //                        }
        //                        if (_nodeCosts[point + Direction.NorthEast].Type == CellType.Passable)
        //                        {
        //                            if (_nodeCosts[point + Direction.East].Type == _nodeCosts[point + Direction.North].Type)
        //                            {
        //                                points.Add(new PointD(k, j));
        //                            }
        //                        }
        //                        if (_nodeCosts[point + Direction.SouthEast].Type == CellType.Passable)
        //                        {
        //                            if (_nodeCosts[point + Direction.East].Type == _nodeCosts[point + Direction.South].Type)
        //                            {
        //                                points.Add(new PointD(k, j));
        //                            }
        //                        }
        //                        if (_nodeCosts[point + Direction.SouthWest].Type == CellType.Passable)
        //                        {
        //                            if (_nodeCosts[point + Direction.West].Type == _nodeCosts[point + Direction.South].Type)
        //                            {
        //                                points.Add(new PointD(k, j));
        //                            }
        //                        }
        //                        break;
        //                }
        //            }
        //        }
        //        RectD output = new RectD(0, 0, X, Y);
        //        VoronoiResults results = Voronoi.FindAll(points.ToArray(), output);
        //        Subdivision division = results.ToDelaunySubdivision(output, true);

        //        _navMeshes.Add(division);
        //    }
        //}

        public Vector3 FindNode(int x, int y, int z)
        {
            //int X = x*24 + 12;
            //int Y = y*24 + 12;

            //Vector3 loc = new Vector3(x, y, z);

            return Graph.GetNearestNode(x, y, z);
        }

        public IList<Node> FindPath(RuningManAgent agent, Vector3 start, Vector3 end)
        {
            var aStar = new AStar<Node>(Graph);
            aStar.UseWorldDistance = true;
            bool success = aStar.FindBestPath(agent, (Node)start, (Node)end);
            return aStar.Nodes;
        }
    }
}
