using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using SusaninPathFinding.Geometry;
using SusaninPathFinding.Graph;
using SusaninPathFinding.Graph.NodeInfoTypes;
using SusaninPathFinding.Graph.PathFinding;
using SusaninPathFindingTests.Geometry;
using SusaninPathFindingTests.Graph;
using NUnit.Framework;
using SusaninPathFindingTests.TDDTests;

namespace SusaninPathFindingTests.Graph
{
    [TestFixture]
    class AStarTest : TestOf<AStar<Cell>>
    {
        private IGraph<Cell> _graph;
        private IMovementAlgorithm<Cell> _agent;
 
        public override void Setup()
        {
            _graph = new Grid3D(6, 5, 2, new Cell3D(92, 92, 128));
            
            Grid3D grid = (Grid3D)_graph;

            for (int j = 0; j < 5; j++)
            {
                for (int i = 0; i < 6; i++)
                {
                    grid[i, j, 0].Info = new Passable();
                }
            }

            grid.Edges[grid[3, 0, 0], grid[4, 1, 0]] = new CellEdge(new Impassable());
            grid.Edges[grid[4, 0, 0], grid[4, 1, 0]] = new CellEdge(new Impassable());
            grid.Edges[grid[5, 0, 0], grid[4, 1, 0]] = new CellEdge(new Impassable());

            grid.Edges[grid[3, 1, 0], grid[4, 1, 0]] = new CellEdge(new Impassable());
            grid.Edges[grid[3, 1, 0], grid[4, 0, 0]] = new CellEdge(new Impassable());
            grid.Edges[grid[3, 1, 0], grid[4, 2, 0]] = new CellEdge(new Impassable());

            grid.Edges[grid[4, 1, 0], grid[5, 1, 0]] = new CellEdge(new Impassable());
            grid.Edges[grid[4, 1, 0], grid[5, 2, 0]] = new CellEdge(new Impassable());

            grid.Edges[grid[5, 1, 0], grid[5, 2, 0]] = new CellEdge(new Impassable());

            grid[3, 2, 0].Info = new Impassable();

            grid.Edges[grid[4, 2, 0], grid[5, 1, 0]] = new CellEdge(new Impassable());
            grid.Edges[grid[4, 2, 0], grid[5, 2, 0]] = new CellEdge(new Impassable());
            grid.Edges[grid[4, 2, 0], grid[5, 3, 0]] = new CellEdge(new Impassable());

            //grid.Edges[grid[5, 2, 0], grid[5, 1, 0]].Info = new Impassable();

            grid[1, 3, 0].Info = new Ladder(GridDirection.East);
            grid[2, 3, 0].Info = new Impassable();
            grid[3, 3, 0].Info = new Impassable();

            grid.Edges[grid[4, 3, 0], grid[5, 2, 0]] = new CellEdge(new Impassable());
            grid.Edges[grid[4, 3, 0], grid[5, 3, 0]] = new CellEdge(new Impassable());
            grid.Edges[grid[4, 3, 0], grid[5, 4, 0]] = new CellEdge(new Impassable());


            //===============================================================================

            grid[3, 2, 1].Info = new Passable();

            grid[2, 3, 1].Info = new Ladder(GridDirection.East);
            grid[3, 3, 1].Info = new Passable();

            //((Grid3D)_graph)[3, 1, 0].Info = new Impassable();
            //((Grid3D)_graph)[4, 1, 0].Info = new Impassable();
            //((Grid3D)_graph)[5, 1, 0].Info = new Impassable();

            //((Grid3D)_graph)[3, 2, 0].Info = new Impassable();
            //((Grid3D)_graph)[5, 2, 0].Info = new Impassable();

            //((Grid3D)_graph)[1, 3, 0].Info = new Ladder(GridDirection.East);
            //((Grid3D)_graph)[2, 3, 0].Info = new Impassable();
            //((Grid3D)_graph)[3, 3, 0].Info = new Impassable();
            //((Grid3D)_graph)[5, 3, 0].Info = new Impassable();

            ////===============================================================================

            //((Grid3D)_graph)[3, 1, 1].Info = new Passable();
            //((Grid3D)_graph)[4, 1, 1].Info = new Passable();
            //((Grid3D)_graph)[5, 1, 1].Info = new Passable();

            //((Grid3D)_graph)[3, 2, 1].Info = new Passable();
            //((Grid3D)_graph)[5, 2, 1].Info = new Passable();

            //((Grid3D)_graph)[2, 3, 1].Info = new Ladder(GridDirection.East);
            //((Grid3D)_graph)[3, 3, 1].Info = new Passable();
            //((Grid3D)_graph)[5, 3, 1].Info = new Passable();
            Tester = new AStar<Cell>(_graph);
            Tester.UseWorldDistance = true;
            _agent = new RuningManAlgorithm((Grid3D)_graph);
        }

        public override void CleenUp()
        {
        }

        [Test]
        public void ConstructorTest()
        {
            Tester.Graph.Should().NotBeNull();
        }

        [Test]
        public void FindBestPathTest()
        {
            // How should it look like if there is some visual representation of map.
            //  0 1 2 3 4 5
            //  _ _ _ _ _ _
            //0|0           |
            //1|○     ◘██◙██|
            //2|○     ◘█  ██|
            //3|○ ▥▥ ◘█  ██|
            //4|      ██  ██|
            //5|            |
            // ------------
            //
            // Little o-like signs are waypoints. If our path finder returned this set of way points then it works correctly.
            (Tester != null && Tester.FindBestPath(_agent, ((Grid3D)_graph)[0, 0, 0], ((Grid3D)_graph)[4, 1, 0])).Should().BeTrue();

            var nodes = Tester.Nodes;
            nodes[0].Should().Be(new Vector3(0, 0, 0));
            nodes[1].Should().Be(new Vector3(0, 1, 0));
            nodes[2].Should().Be(new Vector3(0, 2, 0));
            nodes[3].Should().Be(new Vector3(0, 3, 0));
            nodes[4].Should().Be(new Vector3(0, 4, 0));
            nodes[5].Should().Be(new Vector3(1, 4, 0));
            nodes[6].Should().Be(new Vector3(2, 4, 0));
            nodes[7].Should().Be(new Vector3(3, 4, 0));
            nodes[8].Should().Be(new Vector3(4, 4, 0));
            nodes[9].Should().Be(new Vector3(4, 3, 0));
            nodes[10].Should().Be(new Vector3(4, 2, 0));
            nodes[11].Should().Be(new Vector3(4, 1, 0));

            (Tester != null && Tester.FindBestPath(_agent, ((Grid3D) _graph)[0, 0, 0], ((Grid3D) _graph)[3, 2, 1])).
                Should().BeTrue();

            nodes = Tester.Nodes;
            nodes[0].Should().Be(new Vector3(0, 0, 0));
            nodes[1].Should().Be(new Vector3(0, 1, 0));
            nodes[2].Should().Be(new Vector3(0, 2, 0));
            nodes[3].Should().Be(new Vector3(0, 3, 0));
            nodes[4].Should().Be(new Vector3(1, 3, 0));
            nodes[5].Should().Be(new Vector3(2, 3, 1));
            nodes[6].Should().Be(new Vector3(3, 3, 1));
            nodes[7].Should().Be(new Vector3(3, 2, 1));
        }
    }
}
