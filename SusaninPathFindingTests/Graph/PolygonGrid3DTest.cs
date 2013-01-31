using System;
using FluentAssertions;
using SusaninPathFinding.Geometry;
using SusaninPathFinding.Graph;
using SusaninPathFinding.Graph.NodeInfoTypes;
using SusaninPathFinding.Graph.PathFinding;
using NUnit.Framework;
using SusaninPathFindingTests.TDDTests;

namespace SusaninPathFindingTests.Graph
{
    [TestFixture]
    public class PolygonGrid3DTest : TestOf<Grid3D>
    {
        public override void InitializeSystemUnderTest()
        {
            Tester = new Grid3D(new Cell3D(10, 10, 10));
        }

        public override void Setup()
        {
            Tester.Create(10, 10, 10);
        }

        public override void CleenUp()
        {
        }

        [Test]
        public void ConstructorTest()
        {
            Tester.Polygon.Should().NotBeNull();
            Tester.Polygon.Bounds.SizeX.Should().BeApproximately(10, IntegerPrec);
            Tester.Polygon.Bounds.SizeY.Should().BeApproximately(10, IntegerPrec);
            Tester.Polygon.Bounds.SizeZ.Should().BeApproximately(10, IntegerPrec);

        }

        [Test]
        public void IndexerXYZTest()
        {
            

            Tester.Invoking(y => { var dummy = y[1, 2, 10]; }).ShouldThrow<ArgumentOutOfRangeException>();
            Tester.Invoking(y => { var dummy = y[1, 2, 11]; }).ShouldThrow<ArgumentOutOfRangeException>();
            Tester.Invoking(y => { var dummy = y[1, 2, -1]; }).ShouldThrow<ArgumentOutOfRangeException>();
            Tester.Invoking(y => { var dummy = y[1, 10, 8]; }).ShouldThrow<ArgumentOutOfRangeException>();
            Tester.Invoking(y => { var dummy = y[1, 11, 8]; }).ShouldThrow<ArgumentOutOfRangeException>();
            Tester.Invoking(y => { var dummy = y[1, -1, 8]; }).ShouldThrow<ArgumentOutOfRangeException>();
            Tester.Invoking(y => { var dummy = y[10, 0, 8]; }).ShouldThrow<ArgumentOutOfRangeException>();
            Tester.Invoking(y => { var dummy = y[11, 0, 8]; }).ShouldThrow<ArgumentOutOfRangeException>();

            Tester[9, 0, 8].Should().NotBeNull();

        }

        [Test]
        public void IndexerVector3Test()
        {
            //TODO

        }

        [Test]
        public void SizePropertyTest()
        {
            //TODO

        }

        [Test]
        public void CreateTest()
        {
            Tester.SizeX.Should().Be(10);
            Tester.SizeY.Should().Be(10);
            Tester.SizeZ.Should().Be(10);

            Tester[1, 0, 8].X.Should().Be(1);
            Tester[1, 0, 8].Y.Should().Be(0);
            Tester[1, 0, 8].Z.Should().Be(8);
        }

        [Test]
        public void ContainsNodeTest()
        {
            Tester.SizeX.Should().Be(10);
            Tester.SizeY.Should().Be(10);
            Tester.SizeZ.Should().Be(10);

            Tester[new Vector3(1, 0, 8)].X.Should().Be(1);
            Tester[new Vector3(1, 0, 8)].Y.Should().Be(0);
            Tester[new Vector3(1, 0, 8)].Z.Should().Be(8);
        }

        [Test]
        public void ContainsXYZTest()
        {
            Tester.Contains(1, 3, 5).Should().BeTrue();
            Tester.Contains(11, 2, 3).Should().BeFalse();
        }

        [Test]
        public void ContainsVector3Test()
        {
            Tester.Contains(new Vector3(1, 3, 5)).Should().BeTrue();
            Tester.Contains(new Vector3(11, 2, 3)).Should().BeFalse();
        }

        [Test]
        public void GetDistanceNodesTest()
        {
            Tester.GetManhettenDistance(Tester[1, 3, 5], Tester[4, 1, 7]).Should().BeApproximately(70, FloatPrec);
        }

        [Test]
        public void GetNearestNodeTest()
        {
            Tester.GetNearestNode(1, 2, 3).ShouldBeEquivalentTo(Tester[1, 2, 3]);
        }

        [Test]
        public void GetNeighborsTest()
        {
            //foreach (Node3D<CellInfo> node in Tester.Nodes)
            //{
            //    if (node.Z == 0)
            //        node.Info.Type = CellType.Passable;
            //}
            for (int i = 0; i < Tester.SizeY; i++)
            {
                for (int j = 0; j < Tester.SizeX; j++)
                {
                    Tester[j, i, 0].Info = new Passable();
                }
            }
            Tester[1, 0, 0].Info = new Impassable();
            var list = Tester[1, 1, 0].Neighbors;
            Cell result = list.Find
            (
                delegate(Cell node)
                {
                    return node == Tester[1, 0, 0];
                }
            );
            result.Should().NotBeNull();
            result.Info.GetType().Should().Be(typeof(Impassable));
            //(GridNode3D<CellInfo>)((List<GridNode3D<CellInfo>>)list.F).Info.
            //Tester[1, 1, 0]
        }

        [Test]
        public void GetWorldLocationTest()
        {
            Tester.GetWorldLocation(Tester[1, 0, 5]).X.Should().Be(15);
            Tester.GetWorldLocation(Tester[1, 0, 5]).Y.Should().Be(5);
            Tester.GetWorldLocation(Tester[1, 0, 5]).Z.Should().Be(55);

            Tester[1, 0, 5].WorldLocation.X.Should().Be(15);
            Tester[1, 0, 5].WorldLocation.Y.Should().Be(5);
            Tester[1, 0, 5].WorldLocation.Z.Should().Be(55);
        }
    }
}
