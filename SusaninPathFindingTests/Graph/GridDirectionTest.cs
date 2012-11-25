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
    public class GridDirectionTest : TestOf<GridDirection>
    {
        public override void InitializeSystemUnderTest()
        {
            Tester = new GridDirection(CompassDirection.NorthWest);
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
            Tester.Value.Should().Be(CompassDirection.NorthWest);
        }

        [Test]
        public void IsDiagonalTest()
        {
            Tester.IsDiagonal().Should().BeTrue();
            Tester.Value = CompassDirection.West;
            Tester.IsDiagonal().Should().BeFalse();
        }

        [Test]
        public void StaticIsDiagonalDirTest()
        {
            
            GridDirection.IsDiagonal(CompassDirection.NorthEast).Should().BeTrue();
            GridDirection.IsDiagonal(CompassDirection.SouthEast).Should().BeTrue();
            GridDirection.IsDiagonal(CompassDirection.SouthWest).Should().BeTrue();
            GridDirection.IsDiagonal(CompassDirection.NorthWest).Should().BeTrue();

            GridDirection.IsDiagonal(CompassDirection.NorthLower).Should().BeTrue();
            GridDirection.IsDiagonal(CompassDirection.NorthEastLower).Should().BeTrue();
            GridDirection.IsDiagonal(CompassDirection.EastLower).Should().BeTrue();
            GridDirection.IsDiagonal(CompassDirection.SouthEastLower).Should().BeTrue();
            GridDirection.IsDiagonal(CompassDirection.SouthLower).Should().BeTrue();
            GridDirection.IsDiagonal(CompassDirection.SouthWestLower).Should().BeTrue();
            GridDirection.IsDiagonal(CompassDirection.WestLower).Should().BeTrue();
            GridDirection.IsDiagonal(CompassDirection.NorthWestLower).Should().BeTrue();

            GridDirection.IsDiagonal(CompassDirection.NorthRaise).Should().BeTrue();
            GridDirection.IsDiagonal(CompassDirection.NorthEastRaise).Should().BeTrue();
            GridDirection.IsDiagonal(CompassDirection.EastRaise).Should().BeTrue();
            GridDirection.IsDiagonal(CompassDirection.SouthEastRaise).Should().BeTrue();
            GridDirection.IsDiagonal(CompassDirection.SouthRaise).Should().BeTrue();
            GridDirection.IsDiagonal(CompassDirection.SouthWestRaise).Should().BeTrue();
            GridDirection.IsDiagonal(CompassDirection.WestRaise).Should().BeTrue();
            GridDirection.IsDiagonal(CompassDirection.NorthWestRaise).Should().BeTrue();

            GridDirection.IsDiagonal(CompassDirection.West).Should().BeFalse();
            GridDirection.IsDiagonal(CompassDirection.South).Should().BeFalse();
            GridDirection.IsDiagonal(CompassDirection.East).Should().BeFalse();
            GridDirection.IsDiagonal(CompassDirection.North).Should().BeFalse();
            GridDirection.IsDiagonal(CompassDirection.Raise).Should().BeFalse();
            GridDirection.IsDiagonal(CompassDirection.Lower).Should().BeFalse();
        }

        [Test]
        public void StaticIsDiagonalOffsetTest()
        {
            GridDirection.IsDiagonal(new Vector3(1, -1, 0)).Should().BeTrue();
            GridDirection.IsDiagonal(new Vector3(1, 1, 0)).Should().BeTrue();
            GridDirection.IsDiagonal(new Vector3(-1, 1, 0)).Should().BeTrue();
            GridDirection.IsDiagonal(new Vector3(-1, -1, 0)).Should().BeTrue();

            GridDirection.IsDiagonal(new Vector3(0, -1, -1)).Should().BeTrue();
            GridDirection.IsDiagonal(new Vector3(1, -1, -1)).Should().BeTrue();
            GridDirection.IsDiagonal(new Vector3(1, 0, -1)).Should().BeTrue();
            GridDirection.IsDiagonal(new Vector3(1, 1, -1)).Should().BeTrue();
            GridDirection.IsDiagonal(new Vector3(0, 1, -1)).Should().BeTrue();
            GridDirection.IsDiagonal(new Vector3(-1, 1, -1)).Should().BeTrue();
            GridDirection.IsDiagonal(new Vector3(-1, 0, -1)).Should().BeTrue();
            GridDirection.IsDiagonal(new Vector3(-1, -1, -1)).Should().BeTrue();

            GridDirection.IsDiagonal(new Vector3(0, -1, 1)).Should().BeTrue();
            GridDirection.IsDiagonal(new Vector3(1, -1, 1)).Should().BeTrue();
            GridDirection.IsDiagonal(new Vector3(1, 0, 1)).Should().BeTrue();
            GridDirection.IsDiagonal(new Vector3(1, 1, 1)).Should().BeTrue();
            GridDirection.IsDiagonal(new Vector3(0, 1, 1)).Should().BeTrue();
            GridDirection.IsDiagonal(new Vector3(-1, 1, 1)).Should().BeTrue();
            GridDirection.IsDiagonal(new Vector3(-1, 0, 1)).Should().BeTrue();
            GridDirection.IsDiagonal(new Vector3(-1, -1, 1)).Should().BeTrue();

            GridDirection.IsDiagonal(new Vector3(-1, 0, 0)).Should().BeFalse();
            GridDirection.IsDiagonal(new Vector3(0, 1, 0)).Should().BeFalse();
            GridDirection.IsDiagonal(new Vector3(1, 0, 0)).Should().BeFalse();
            GridDirection.IsDiagonal(new Vector3(0, -1, 0)).Should().BeFalse();
            GridDirection.IsDiagonal(new Vector3(0, 0, 1)).Should().BeFalse();
            GridDirection.IsDiagonal(new Vector3(0, 0, -1)).Should().BeFalse();


        }

        [Test]
        public void OppositeTest()
        {
            GridDirection.Opposite(CompassDirection.North).Should().Be(CompassDirection.South);
        }

        [Test]
        public void FromRotatorTest()
        {
            Angle a = new Angle(1, 270, 0, 0);
            Tester.Rotation = new Rotator(0, 0, 0);
            Tester.Value.Should().Be(CompassDirection.East);

            Tester.Rotation = new Rotator(45, 45, 90);
            Tester.Value.Should().Be(CompassDirection.NorthLower);
            //GridDirection.Offsets[(int) Tester.FromRotator(0, 0, 270)].X.Should().BeApproximately(0, IntegerPrec);
            //GridDirection.Offsets[(int) Tester.FromRotator(0, 0, 270)].Y.Should().BeApproximately(1, IntegerPrec);
            //GridDirection.Offsets[(int)Tester.FromRotator(0, 0, 270)].Z.Should().BeApproximately(0, IntegerPrec);

        }
    }
}
