using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using SusaninPathFinding.Geometry;
using SusaninPathFinding.Graph;
using SusaninPathFinding.Graph.NodeInfoTypes;
using SusaninPathFinding.Graph.PathFinding;
using SusaninPathFinding.Graph.Platformer2DGrid;
using SusaninPathFinding.Source.Graph.PathFinding;
using SusaninPathFindingTests.Graph.PathFinding;
using SusaninPathFindingTests.TDDTests;

namespace SusaninPathFindingTests.Graph.Platformer3DTest
{
    [TestFixture]
    class PlatformerCharacterAlgorithmTest : TestOf<PlatformerCharacter>
    {
        public Grid3D Nodes;

        public override void Setup()
        {
            Nodes = Grid3DTest.GeneratePlatformerMap();//new Platformer2DGraph(10, 10, 1, new Cell3D(10, 10, 10), new Empty());

            Tester = new PlatformerCharacter(Nodes, null);
            Tester.G = 9.8f;
            Tester.WalkSpeed = 10f;
        }

        public override void CleenUp()
        {
        }

        [Test]
        public void ConstructorTest()
        {
            Tester.Grid.Should().NotBeNull();
        }

        [Test]
        public void CanMakeStepTest()
        {


            var list = Nodes[3, 5, 0].NeighborsPassableBy(Tester);

            Cell result = list.Find
            (
                delegate(Cell node)
                {
                    return (node == Nodes[3, 6, 0]
                            || node == Nodes[2, 6, 0]
                            || node == Nodes[4, 6, 0]);
                }
            );
            result.Should().NotBeNull();

            result = list.Find
            (
                delegate(Cell node)
                {
                    return (node == Nodes[2, 5, 0]
                            || node == Nodes[4, 5, 0]
                            || node == Nodes[2, 4, 0]
                            || node == Nodes[3, 4, 0]
                            || node == Nodes[4, 4, 0]);
                }
            );
            result.Should().BeNull();

        }

        [Test]
        public void CanMakeStepForOpenClosedListTest()
        {
            OpenClosedGrid openClosedGrid = OpenClosedGraphTests.GenerateOpenClosedList(Nodes);
            Tester = new PlatformerCharacter(
                openClosedGrid
                , null);

            openClosedGrid[Nodes[3, 5, 0]] = new PathNodeCell();
            var list = openClosedGrid[Nodes[3, 5, 0]].NeighborsPassableBy(Tester);

            //var list = Nodes[3, 5, 0].NeighborsPassableBy(Tester);

            Cell result = list.Find
            (
                delegate(Cell node)
                {
                    return (node == Nodes[3, 6, 0]
                            || node == Nodes[2, 6, 0]
                            || node == Nodes[4, 6, 0]);
                }
            );
            result.Should().NotBeNull();

            result = list.Find
            (
                delegate(Cell node)
                {
                    return (node == Nodes[2, 5, 0]
                            || node == Nodes[4, 5, 0]
                            || node == Nodes[2, 4, 0]
                            || node == Nodes[3, 4, 0]
                            || node == Nodes[4, 4, 0]);
                }
            );
            result.Should().BeNull();

        }

        [Test]
        public void SubgraphTest()
        {
            Nodes.GetSubgraph(node, Tester);
        }

        [Test]
        public void CanMakeStepWithThinWallsTest()
        {

            //var list = Nodes[0, 1, 0].NeighborsPassableBy(Tester);

            //Cell result = list.Find
            //(
            //    delegate(Cell node)
            //    {
            //        return (node == Nodes[1, 0, 0]
            //                || node == Nodes[1, 1, 0]);
            //    }
            //);
            //result.Should().BeNull();

            //result = list.Find
            //(
            //    delegate(Cell node)
            //    {
            //        return (node == Nodes[0, 0, 0]
            //            || node == Nodes[0, 2, 0]);
            //    }
            //);
            //result.Should().NotBeNull();
            //result.Info.GetType().Should().Be(typeof(Passable));
        }

        [Test]
        public void CanOccupyTest()
        {
            //Tester.CanPass(new Cell(1, 1, 0, ((Grid3D)Tester.Grid).Polygon, Tester.Grid, new Passable())).
            //    Should().BeTrue();
            //Tester.CanPass(new Cell(1, 1, 0, ((Grid3D)Tester.Grid).Polygon, Tester.Grid, new Ladder(GridDirection.South))).
            //    Should().BeTrue();
            //Tester.CanPass(new Cell(1, 1, 0, ((Grid3D)Tester.Grid).Polygon, Tester.Grid, new Impassable())).
            //    Should().BeFalse();
        }

        [Test]
        public void GetStepCostTest()
        {

            //Tester.GetStepCost(new Cell(1, 0, 0, ((Grid3D)Tester.Grid).Polygon, Tester.Grid, new Passable()), new Cell(1, 1, 0, ((Grid3D)Tester.Grid).Polygon, Tester.Grid, new Passable())).
            //    Should().BeApproximately(5, FloatPrec);
            //Tester.GetStepCost(new Cell(1, 0, 0, ((Grid3D)Tester.Grid).Polygon, Tester.Grid, new Passable()), new Cell(1, 1, 0, ((Grid3D)Tester.Grid).Polygon, Tester.Grid, new Ladder(GridDirection.South))).
            //    Should().BeApproximately(6, FloatPrec);
            //Tester.GetStepCost(new Cell(0, 0, 0, ((Grid3D)Tester.Grid).Polygon, Tester.Grid, new Passable()), new Cell(1, 1, 0, ((Grid3D)Tester.Grid).Polygon, Tester.Grid, new Passable())).
            //    Should().BeApproximately(7, FloatPrec);
        }

        [Test]
        public void IsNearTarget()
        {
            //Cell n1 = new Cell(1, 1, 0, ((Grid3D)Tester.Grid).Polygon, Tester.Grid, new Passable());
            //Cell n2 = new Cell(1, 1, 0, ((Grid3D)Tester.Grid).Polygon, Tester.Grid, new Passable());
            //Tester.IsNearTarget(n1, n2, n1.Distance(n2)).Should().BeTrue();

            //n1 = new Cell(1, 0, 0, ((Grid3D)Tester.Grid).Polygon, Tester.Grid, new Passable());
            //n2 = new Cell(1, 1, 0, ((Grid3D)Tester.Grid).Polygon, Tester.Grid, new Passable());
            //Tester.IsNearTarget(n1, n2, n1.Distance(n2)).Should().BeFalse();
        }

        [Test]
        public void CanPassTest()
        {
            //Tester.CanPass(new Cell(1, 1, 0, ((Grid3D)Tester.Grid).Polygon, Tester.Grid, new Passable())).
            //    Should().BeTrue();
            //Tester.CanPass(new Cell(1, 1, 0, ((Grid3D)Tester.Grid).Polygon, Tester.Grid, new Ladder(GridDirection.South))).
            //    Should().BeTrue();
            //Tester.CanPass(new Cell(1, 1, 0, ((Grid3D)Tester.Grid).Polygon, Tester.Grid, new Impassable())).
            //    Should().BeFalse();
        }



        [Test]
        public void CanPass()
        {
            Tester.CanPass(Nodes[1, 9, 0]).Should().BeTrue();
            Tester.CanPass(Nodes[5, 9, 0]).Should().BeFalse();
        }

        [Test]
        public void GetStepCost()
        {
            Tester.GetStepCost(Nodes[2, 9, 0], Nodes[1, 9, 0]).Should().Be(5);
        }

        [Test]
        public void CanMakeStep()
        {

        }
    }
}
