using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class RuningManAlgorithmTest : TestOf<RuningManAlgorithm>
    {
        public Grid3D Nodes { private get; set; }

        
        public override void InitializeSystemUnderTest()
        {
            Nodes = new Grid3D(3, 3, 2, new Cell3D(92, 92, 128));
            Tester = new RuningManAlgorithm(Nodes);
        }

        public override void Setup()
        {
            //Nodes = new PolygonGrid3D<CellInfo>(new Cell3D());
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
            //PolygonGrid3D grid = new PolygonGrid3D(3, 3, 2, new Cell3D(92, 92, 128));

            for (int i = 0; i < Nodes.SizeY; i++)
            {
                for (int j = 0; j < Nodes.SizeX; j++)
                {
                    Nodes[j, i, 0].Info = new Passable();
                }
            }

            Nodes[1, 0, 0].Info = new Impassable();
            Nodes[0, 1, 0].Info = new Impassable();
            Nodes[1, 2, 0].Info = new Ladder(GridDirection.North);
            Nodes[2, 1, 0].Info = new Impassable();

            var list = Nodes[1, 1, 0].NeighborsPassableBy(Tester);

            Cell result = list.Find
            (
                delegate(Cell node)
                    {
                        return (node == Nodes[0, 0, 0]
                                || node == Nodes[1, 0, 0]
                                || node == Nodes[0, 1, 0]
                                || node == Nodes[2, 1, 0]
                                || node == Nodes[2, 2, 0]);
                    }
            );
            result.Should().BeNull();

            result = list.Find
            (
                delegate(Cell node)
                {
                    return (node == Nodes[1, 2, 0]);
                }
            );
            result.Should().NotBeNull();
            result.Info.GetType().Should().Be(typeof(Ladder));
            //result.Info.GetType().Should().Be(typeof(Impassable));
        }

        [Test]
        public void CanMakeStepWithThinWallsTest()
        {
            //PolygonGrid3D grid = new PolygonGrid3D(3, 3, 2, new Cell3D(92, 92, 128));

            //for (int i = 0; i < Nodes.SizeY; i++)
            //{
            //    for (int j = 0; j < Nodes.SizeX; j++)
            //    {
            //        Nodes[j, i, 0].Info = new Passable();
            //    }
            //}

            //Nodes[1, 0, 0].Info = new Impassable();
            //Nodes[0, 1, 0].Info = new Impassable();
            //Nodes[1, 2, 0].Info = new Ladder(GridDirection.North);
            //Nodes[2, 1, 0].Info = new Impassable();

            Nodes.Edges[Nodes[0, 1, 0], Nodes[1, 1, 0]].Info = new Impassable();
            Nodes.Edges[Nodes[0, 1, 0], Nodes[1, 0, 0]].Info = new Impassable();
            Nodes.Edges[Nodes[0, 1, 0], Nodes[1, 2, 0]].Info = new Impassable();

            Nodes.Edges[Nodes[1, 0, 0], Nodes[1, 1, 0]].Info = new Impassable();
            Nodes.Edges[Nodes[1, 0, 0], Nodes[2, 1, 0]].Info = new Impassable();

            var list = Nodes[0, 1, 0].NeighborsPassableBy(Tester);

            Cell result = list.Find
            (
                delegate(Cell node)
                {
                    return (node == Nodes[1, 0, 0]
                            || node == Nodes[1, 1, 0]);
                }
            );
            result.Should().BeNull();

            result = list.Find
            (
                delegate(Cell node)
                {
                    return (node == Nodes[0, 0, 0]
                        || node == Nodes[0, 2, 0]);
                }
            );
            result.Should().NotBeNull();
            result.Info.GetType().Should().Be(typeof(Passable));
            //result.Info.GetType().Should().Be(typeof(Impassable));
        }

        [Test]
        public void CanOccupyTest()
        {
            Tester.CanPass(new Cell(1, 1, 0, ((Grid3D)Tester. Grid).Polygon, Tester.Grid, new Passable())).
                Should().BeTrue();
            Tester.CanPass(new Cell(1, 1, 0, ((Grid3D)Tester.Grid).Polygon, Tester.Grid, new Ladder(GridDirection.South))).
                Should().BeTrue();
            Tester.CanPass(new Cell(1, 1, 0, ((Grid3D)Tester.Grid).Polygon, Tester.Grid, new Impassable())).
                Should().BeFalse();
        }

        [Test]
        public void GetStepCostTest()
        {

            Tester.GetStepCost(new Cell(1, 0, 0, ((Grid3D)Tester.Grid).Polygon, Tester.Grid, new Passable()), new Cell(1, 1, 0, ((Grid3D)Tester.Grid).Polygon, Tester.Grid, new Passable())).
                Should().BeApproximately(5, FloatPrec);
            Tester.GetStepCost(new Cell(1, 0, 0, ((Grid3D)Tester.Grid).Polygon, Tester.Grid, new Passable()), new Cell(1, 1, 0, ((Grid3D)Tester.Grid).Polygon, Tester.Grid, new Ladder(GridDirection.South))).
                Should().BeApproximately(6, FloatPrec);
            Tester.GetStepCost(new Cell(0, 0, 0, ((Grid3D)Tester.Grid).Polygon, Tester.Grid, new Passable()), new Cell(1, 1, 0, ((Grid3D)Tester.Grid).Polygon, Tester.Grid, new Passable())).
                Should().BeApproximately(7, FloatPrec);
        }

        [Test]
        public void IsNearTarget()
        {
            Cell n1 = new Cell(1, 1, 0, ((Grid3D)Tester.Grid).Polygon, Tester.Grid, new Passable());
            Cell n2 = new Cell(1, 1, 0, ((Grid3D)Tester.Grid).Polygon, Tester.Grid, new Passable());
            Tester.IsNearTarget(n1, n2, n1.Distance(n2)).Should().BeTrue();

            n1 = new Cell(1, 0, 0, ((Grid3D)Tester.Grid).Polygon, Tester.Grid, new Passable());
            n2 = new Cell(1, 1, 0, ((Grid3D)Tester.Grid).Polygon, Tester.Grid, new Passable());
            Tester.IsNearTarget(n1, n2, n1.Distance(n2)).Should().BeFalse();
        }

        [Test]
        public void CanPassTest()
        {
            Tester.CanPass(new Cell(1, 1, 0, ((Grid3D)Tester.Grid).Polygon, Tester.Grid, new Passable())).
                Should().BeTrue();
            Tester.CanPass(new Cell(1, 1, 0, ((Grid3D)Tester.Grid).Polygon, Tester.Grid, new Ladder(GridDirection.South))).
                Should().BeTrue();
            Tester.CanPass(new Cell(1, 1, 0, ((Grid3D)Tester.Grid).Polygon, Tester.Grid, new Impassable())).
                Should().BeFalse();
        }
    }
}
