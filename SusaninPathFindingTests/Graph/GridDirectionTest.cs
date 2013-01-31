using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using FluentAssertions;
using SusaninPathFinding;
using SusaninPathFinding.Geometry;
using SusaninPathFinding.Graph;
using SusaninPathFindingTests.Geometry;
using NUnit.Framework;
using SusaninPathFindingTests.Graph;
using SusaninPathFindingTests.TDDTests;


namespace SusaninPathFindingTests.Graph
{
    
    [TestFixture]
    public class GridDirectionTest
    {
        public GridDirection Tester;

        [SetUp]
        public void Setup()
        {
            Tester = GridDirection.NorthWest;
        }

        [Test]
        public void ConstructorTest()
        {
            Tester.Should().Be(GridDirection.NorthWest);
        }

        [Test]
        public void IsDiagonalTest()
        {
            Tester.IsDiagonal().Should().BeTrue();
            Tester = GridDirection.West;
            Tester.IsDiagonal().Should().BeFalse();
        }

        [Test]
        public void StaticIsDiagonalDirTest()
        {

            GridDirection.NorthEast.IsDiagonal().Should().BeTrue();
            GridDirection.SouthEast.IsDiagonal().Should().BeTrue();
            GridDirection.SouthWest.IsDiagonal().Should().BeTrue();
            GridDirection.NorthWest.IsDiagonal().Should().BeTrue();

            GridDirection.NorthLower.IsDiagonal().Should().BeTrue();
            GridDirection.NorthEastLower.IsDiagonal().Should().BeTrue();
            GridDirection.EastLower.IsDiagonal().Should().BeTrue();
            GridDirection.SouthEastLower.IsDiagonal().Should().BeTrue();
            GridDirection.SouthLower.IsDiagonal().Should().BeTrue();
            GridDirection.SouthWestLower.IsDiagonal().Should().BeTrue();
            GridDirection.WestLower.IsDiagonal().Should().BeTrue();
            GridDirection.NorthWestLower.IsDiagonal().Should().BeTrue();

            GridDirection.NorthRaise.IsDiagonal().Should().BeTrue();
            GridDirection.NorthEastRaise.IsDiagonal().Should().BeTrue();
            GridDirection.EastRaise.IsDiagonal().Should().BeTrue();
            GridDirection.SouthEastRaise.IsDiagonal().Should().BeTrue();
            GridDirection.SouthRaise.IsDiagonal().Should().BeTrue();
            GridDirection.SouthWestRaise.IsDiagonal().Should().BeTrue();
            GridDirection.WestRaise.IsDiagonal().Should().BeTrue();
            GridDirection.NorthWestRaise.IsDiagonal().Should().BeTrue();

            GridDirection.West.IsDiagonal().Should().BeFalse();
            GridDirection.South.IsDiagonal().Should().BeFalse();
            GridDirection.East.IsDiagonal().Should().BeFalse();
            GridDirection.North.IsDiagonal().Should().BeFalse();
            GridDirection.Raise.IsDiagonal().Should().BeFalse();
            GridDirection.Lower.IsDiagonal().Should().BeFalse();
        }

        [Test]
        public void StaticIsDiagonalOffsetTest()
        {
            new Vector3(1, -1, 0).IsDiagonal().Should().BeTrue();
            new Vector3(1, 1, 0).IsDiagonal().Should().BeTrue();
            new Vector3(-1, 1, 0).IsDiagonal().Should().BeTrue();
            new Vector3(-1, -1, 0).IsDiagonal().Should().BeTrue();

            new Vector3(0, -1, -1).IsDiagonal().Should().BeTrue();
            new Vector3(1, -1, -1).IsDiagonal().Should().BeTrue();
            new Vector3(1, 0, -1).IsDiagonal().Should().BeTrue();
            new Vector3(1, 1, -1).IsDiagonal().Should().BeTrue();
            new Vector3(0, 1, -1).IsDiagonal().Should().BeTrue();
            new Vector3(-1, 1, -1).IsDiagonal().Should().BeTrue();
            new Vector3(-1, 0, -1).IsDiagonal().Should().BeTrue();
            new Vector3(-1, -1, -1).IsDiagonal().Should().BeTrue();

            new Vector3(0, -1, 1).IsDiagonal().Should().BeTrue();
            new Vector3(1, -1, 1).IsDiagonal().Should().BeTrue();
            new Vector3(1, 0, 1).IsDiagonal().Should().BeTrue();
            new Vector3(1, 1, 1).IsDiagonal().Should().BeTrue();
            new Vector3(0, 1, 1).IsDiagonal().Should().BeTrue();
            new Vector3(-1, 1, 1).IsDiagonal().Should().BeTrue();
            new Vector3(-1, 0, 1).IsDiagonal().Should().BeTrue();
            new Vector3(-1, -1, 1).IsDiagonal().Should().BeTrue();

            new Vector3(-1, 0, 0).IsDiagonal().Should().BeFalse();
            new Vector3(0, 1, 0).IsDiagonal().Should().BeFalse();
            new Vector3(1, 0, 0).IsDiagonal().Should().BeFalse();
            new Vector3(0, -1, 0).IsDiagonal().Should().BeFalse();
            new Vector3(0, 0, 1).IsDiagonal().Should().BeFalse();
            new Vector3(0, 0, -1).IsDiagonal().Should().BeFalse();


        }

        [Test]
        public void OppositeTest()
        {
            GridDirection.North.Opposite().Should().Be(GridDirection.South);
            GridDirection.NorthWest.Opposite().Should().Be(GridDirection.SouthEast);
            GridDirection.NorthWestLower.Opposite().Should().Be(GridDirection.SouthEastRaise);
        }

        [Test]
        public void FromRotatorTest()
        {
            Angle a = new Angle(1, 270, 0, 0);
            Tester = GridDirectionExtentions.GetDirectionFromRotation(0, 0, 0);
            Tester.Should().Be(GridDirection.East);

            Tester = GridDirectionExtentions.GetDirectionFromRotation(45, 45, 90);
            Tester.Should().Be(GridDirection.NorthLower);
            //GridDirection.Offsets[(int) Tester.FromRotator(0, 0, 270)].X.Should().BeApproximately(0, IntegerPrec);
            //GridDirection.Offsets[(int) Tester.FromRotator(0, 0, 270)].Y.Should().BeApproximately(1, IntegerPrec);
            //GridDirection.Offsets[(int)Tester.FromRotator(0, 0, 270)].Z.Should().BeApproximately(0, IntegerPrec);

        }
    }
}
