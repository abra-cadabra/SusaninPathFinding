using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using SusaninPathFinding.Geometry;
using SusaninPathFinding.Graph;
using SusaninPathFinding.Graph.PathFinding;
using SusaninPathFindingTests.Geometry;
using SusaninPathFindingTests.Graph;
using NUnit.Framework;
using SusaninPathFindingTests.TDDTests;

namespace SusaninPathFindingTests.Graph
{
    /// <summary>
    /// Defines class of graph node.
    /// </summary>
    /// <typeparam name="TInfo">Type of </typeparam>
    [TestFixture]
    public class CellTest : TestOf<Cell>
    {

        public override void Setup()
        {
            Tester = new Cell(10, 7, 0, new Cell3D(92, 92, 128), new Grid3D(new Cell3D(92, 92, 128)));
        }

        public override void CleenUp()
        {
        }

        [Test]
        public void ConstructorTest()
        {
            Tester.Info.Should().NotBeNull();
            Tester.Should().NotBeNull();
            Tester.X.Should().Be(10);
            Tester.Y.Should().Be(7);
            Tester.Z.Should().Be(0);
            Tester.Polygon.Should().NotBeNull();
            Tester.Polygon.Bounds.SizeX.Should().Be(92);
            Tester.Polygon.Bounds.SizeY.Should().Be(92);
            Tester.Polygon.Bounds.SizeZ.Should().Be(128);
            //Tester.Neighbors.Should().NotBeNull();
        }

        [Test]
        public void ConstructorVector3Test()
        {
            Tester = new Cell(new Vector3(10, 7, 0), new Cell3D(92, 92, 128), new Grid3D(new Cell3D(92, 92, 128)));

            Tester.Info.Should().NotBeNull();
            Tester.Should().NotBeNull();
            Tester.X.Should().Be(10);
            Tester.Y.Should().Be(7);
            Tester.Z.Should().Be(0);
            Tester.Polygon.Should().NotBeNull();
            Tester.Polygon.Bounds.SizeX.Should().Be(92);
            Tester.Polygon.Bounds.SizeY.Should().Be(92);
            Tester.Polygon.Bounds.SizeZ.Should().Be(128);
            //Tester.Neighbors.Should().NotBeNull();
        }

        [Test]
        public void WorldLocationTest()
        {
            Tester.WorldLocation.X.Should().Be(966);
            Tester.WorldLocation.Y.Should().Be(690);
            Tester.WorldLocation.Z.Should().Be(64);
        }
    }
}
