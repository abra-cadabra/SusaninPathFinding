using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using FluentAssertions;
using MyNavMesh.Collections;
using MyNavMesh.Geometry;
using NUnit.Framework;
using TDDTests;


namespace MyNavMesh.Graph
{
    
    [TestFixture]
    public class GraphDirectionTest : TestOf<GraphDirection>
    {
        public override void InitializeSystemUnderTest()
        {
            Tester = new GraphDirection(Direction.NorthWest);
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
            Tester.Value.Should().Be(Direction.NorthWest);
        }

        [Test]
        public void IsDiagonalTest()
        {
            Tester.IsDiagonal().Should().BeTrue();
            Tester.Value = Direction.West;
            Tester.IsDiagonal().Should().BeFalse();
        }

        [Test]
        public void StaticIsDiagonalDirTest()
        {
            
            GraphDirection.IsDiagonal(Direction.NorthEast).Should().BeTrue();
            GraphDirection.IsDiagonal(Direction.SouthEast).Should().BeTrue();
            GraphDirection.IsDiagonal(Direction.SouthWest).Should().BeTrue();
            GraphDirection.IsDiagonal(Direction.NorthWest).Should().BeTrue();

            GraphDirection.IsDiagonal(Direction.NorthLower).Should().BeTrue();
            GraphDirection.IsDiagonal(Direction.NorthEastLower).Should().BeTrue();
            GraphDirection.IsDiagonal(Direction.EastLower).Should().BeTrue();
            GraphDirection.IsDiagonal(Direction.SouthEastLower).Should().BeTrue();
            GraphDirection.IsDiagonal(Direction.SouthLower).Should().BeTrue();
            GraphDirection.IsDiagonal(Direction.SouthWestLower).Should().BeTrue();
            GraphDirection.IsDiagonal(Direction.WestLower).Should().BeTrue();
            GraphDirection.IsDiagonal(Direction.NorthWestLower).Should().BeTrue();

            GraphDirection.IsDiagonal(Direction.NorthRaise).Should().BeTrue();
            GraphDirection.IsDiagonal(Direction.NorthEastRaise).Should().BeTrue();
            GraphDirection.IsDiagonal(Direction.EastRaise).Should().BeTrue();
            GraphDirection.IsDiagonal(Direction.SouthEastRaise).Should().BeTrue();
            GraphDirection.IsDiagonal(Direction.SouthRaise).Should().BeTrue();
            GraphDirection.IsDiagonal(Direction.SouthWestRaise).Should().BeTrue();
            GraphDirection.IsDiagonal(Direction.WestRaise).Should().BeTrue();
            GraphDirection.IsDiagonal(Direction.NorthWestRaise).Should().BeTrue();

            GraphDirection.IsDiagonal(Direction.West).Should().BeFalse();
            GraphDirection.IsDiagonal(Direction.South).Should().BeFalse();
            GraphDirection.IsDiagonal(Direction.East).Should().BeFalse();
            GraphDirection.IsDiagonal(Direction.North).Should().BeFalse();
            GraphDirection.IsDiagonal(Direction.Raise).Should().BeFalse();
            GraphDirection.IsDiagonal(Direction.Lower).Should().BeFalse();
        }

        [Test]
        public void StaticIsDiagonalOffsetTest()
        {
            GraphDirection.IsDiagonal(new Vector3(1, -1, 0)).Should().BeTrue();
            GraphDirection.IsDiagonal(new Vector3(1, 1, 0)).Should().BeTrue();
            GraphDirection.IsDiagonal(new Vector3(-1, 1, 0)).Should().BeTrue();
            GraphDirection.IsDiagonal(new Vector3(-1, -1, 0)).Should().BeTrue();

            GraphDirection.IsDiagonal(new Vector3(0, -1, -1)).Should().BeTrue();
            GraphDirection.IsDiagonal(new Vector3(1, -1, -1)).Should().BeTrue();
            GraphDirection.IsDiagonal(new Vector3(1, 0, -1)).Should().BeTrue();
            GraphDirection.IsDiagonal(new Vector3(1, 1, -1)).Should().BeTrue();
            GraphDirection.IsDiagonal(new Vector3(0, 1, -1)).Should().BeTrue();
            GraphDirection.IsDiagonal(new Vector3(-1, 1, -1)).Should().BeTrue();
            GraphDirection.IsDiagonal(new Vector3(-1, 0, -1)).Should().BeTrue();
            GraphDirection.IsDiagonal(new Vector3(-1, -1, -1)).Should().BeTrue();

            GraphDirection.IsDiagonal(new Vector3(0, -1, 1)).Should().BeTrue();
            GraphDirection.IsDiagonal(new Vector3(1, -1, 1)).Should().BeTrue();
            GraphDirection.IsDiagonal(new Vector3(1, 0, 1)).Should().BeTrue();
            GraphDirection.IsDiagonal(new Vector3(1, 1, 1)).Should().BeTrue();
            GraphDirection.IsDiagonal(new Vector3(0, 1, 1)).Should().BeTrue();
            GraphDirection.IsDiagonal(new Vector3(-1, 1, 1)).Should().BeTrue();
            GraphDirection.IsDiagonal(new Vector3(-1, 0, 1)).Should().BeTrue();
            GraphDirection.IsDiagonal(new Vector3(-1, -1, 1)).Should().BeTrue();

            GraphDirection.IsDiagonal(new Vector3(-1, 0, 0)).Should().BeFalse();
            GraphDirection.IsDiagonal(new Vector3(0, 1, 0)).Should().BeFalse();
            GraphDirection.IsDiagonal(new Vector3(1, 0, 0)).Should().BeFalse();
            GraphDirection.IsDiagonal(new Vector3(0, -1, 0)).Should().BeFalse();
            GraphDirection.IsDiagonal(new Vector3(0, 0, 1)).Should().BeFalse();
            GraphDirection.IsDiagonal(new Vector3(0, 0, -1)).Should().BeFalse();


        }

        [Test]
        public void OppositeTest()
        {
            GraphDirection.Opposite(Direction.North).Should().Be(Direction.South);
        }
    }
}
