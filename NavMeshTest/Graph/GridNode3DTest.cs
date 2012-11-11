using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using MyNavMesh.Geometry;
using MyNavMesh.Graph.PathFinding;
using NUnit.Framework;
using TDDTests;

namespace MyNavMesh.Graph
{
    /// <summary>
    /// Defines class of graph node.
    /// </summary>
    /// <typeparam name="TInfo">Type of </typeparam>
    [TestFixture]
    public class GridNode3DTest : TestOf<GridNode3D<CellInfo>> 
    {

        public override void InitializeSystemUnderTest()
        {
            Tester = new GridNode3D<CellInfo>(10, 7, 0, new Cell3D(92, 92, 128));
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
            Tester.Info.Should().NotBeNull();
            Tester.Index.Should().NotBeNull();
            Tester.Index.X.Should().Be(10);
            Tester.Index.Y.Should().Be(7);
            Tester.Index.Z.Should().Be(0);
            Tester.Polygon.Should().NotBeNull();
            Tester.Polygon.Bounds.SizeX.Should().Be(92);
            Tester.Polygon.Bounds.SizeY.Should().Be(92);
            Tester.Polygon.Bounds.SizeZ.Should().Be(128);
            Tester.Neighbors.Should().NotBeNull();
        }

        [Test]
        public void ConstructorVector3Test()
        {
            Tester = new GridNode3D<CellInfo>(new Vector3(10, 7, 0), new Cell3D(92, 92, 128));

            Tester.Info.Should().NotBeNull();
            Tester.Index.Should().NotBeNull();
            Tester.Index.X.Should().Be(10);
            Tester.Index.Y.Should().Be(7);
            Tester.Index.Z.Should().Be(0);
            Tester.Polygon.Should().NotBeNull();
            Tester.Polygon.Bounds.SizeX.Should().Be(92);
            Tester.Polygon.Bounds.SizeY.Should().Be(92);
            Tester.Polygon.Bounds.SizeZ.Should().Be(128);
            Tester.Neighbors.Should().NotBeNull();
        }

        [Test]
        public void WorldLocationTest()
        {
            Tester.WorldLocation.X.Should().Be(920);
            Tester.WorldLocation.Y.Should().Be(644);
            Tester.WorldLocation.Z.Should().Be(0);
        }
    }
}
