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
    class AStarTest : TestOf<AStar<Node>>
    {
        private IGraph<Node> _graph;
        private IGraphAgent<Node> _agent;
 
        public override void InitializeSystemUnderTest()
        {
            _graph = new PolygonGrid3D(6, 6, 2, new Cell3D(92, 92, 128));
            ((PolygonGrid3D)_graph)[3, 1, 0].Info = new Impassable();
            ((PolygonGrid3D)_graph)[4, 1, 0].Info = new Impassable();
            ((PolygonGrid3D)_graph)[5, 1, 0].Info = new Impassable();

            ((PolygonGrid3D)_graph)[3, 2, 0].Info = new Impassable();
            ((PolygonGrid3D)_graph)[5, 2, 0].Info = new Impassable();

            ((PolygonGrid3D)_graph)[1, 3, 0].Info = new Ladder(CompassDirection.East);
            ((PolygonGrid3D)_graph)[2, 3, 0].Info = new Impassable();
            ((PolygonGrid3D)_graph)[3, 3, 0].Info = new Impassable();
            ((PolygonGrid3D)_graph)[5, 3, 0].Info = new Impassable();

            //===============================================================================

            ((PolygonGrid3D)_graph)[3, 1, 1].Info = new Passable();
            ((PolygonGrid3D)_graph)[4, 1, 1].Info = new Passable();
            ((PolygonGrid3D)_graph)[5, 1, 1].Info = new Passable();

            ((PolygonGrid3D)_graph)[3, 2, 1].Info = new Passable();
            ((PolygonGrid3D)_graph)[5, 2, 1].Info = new Passable();

            ((PolygonGrid3D)_graph)[2, 3, 1].Info = new Ladder(CompassDirection.East);
            ((PolygonGrid3D)_graph)[3, 3, 1].Info = new Passable();
            ((PolygonGrid3D)_graph)[5, 3, 1].Info = new Passable();
            Tester = new AStar<Node>(_graph);
            Tester.UseWorldDistance = true;
            _agent = new RuningManAgent((PolygonGrid3D)_graph);
        }

        public override void Setup()
        {
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
            Tester.FindBestPath(_agent, ((PolygonGrid3D)_graph)[0, 0, 0], ((PolygonGrid3D)_graph)[4, 1, 1]).Should().BeTrue();//.For(_agent).From(((PolygonGrid3D)_graph)[0, 0, 0]).To((PolygonGrid3D)_graph)[4, 1, 1]).

            var nodes = Tester.Nodes;
            nodes[0].Should().Be(new Vector3(0, 0, 0));
            nodes[1].Should().Be(new Vector3(0, 1, 0));
            nodes[2].Should().Be(new Vector3(0, 2, 0));
            nodes[3].Should().Be(new Vector3(0, 3, 0));
            nodes[4].Should().Be(new Vector3(1, 3, 0));
            nodes[5].Should().Be(new Vector3(2, 3, 1));
            nodes[6].Should().Be(new Vector3(3, 3, 1));
            nodes[7].Should().Be(new Vector3(3, 2, 1));
            nodes[8].Should().Be(new Vector3(3, 1, 1));
            nodes[9].Should().Be(new Vector3(4, 1, 1));
        }
    }
}
