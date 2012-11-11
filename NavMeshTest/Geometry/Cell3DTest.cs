using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FluentAssertions;
using TDDTests;

namespace MyNavMesh.Geometry
{
    [TestFixture]
    public class Cell3DTest : TestOf<Cell3D>
    {
        public override void InitializeSystemUnderTest()
        {
            Tester = new Cell3D(1, 1, 1);
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
            Tester.Bounds.SizeX.Should().BeApproximately(1, IntegerPrec);
            Tester.Bounds.SizeX.Should().BeApproximately(1, IntegerPrec);
            Tester.Bounds.SizeX.Should().BeApproximately(1, IntegerPrec);

            Tester.Connectivity.Should().Be(26);
        }
    }
}
