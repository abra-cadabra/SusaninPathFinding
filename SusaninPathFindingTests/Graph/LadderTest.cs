using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using SusaninPathFinding.Graph;
using SusaninPathFindingTests.Graph;
using SusaninPathFindingTests.TDDTests;

namespace SusaninPathFindingTests.Graph
{
    public class LadderTest : TestOf<Ladder>
    {
        public override void InitializeSystemUnderTest()
        {
            Tester = new Ladder(GridDirection.NorthEast);
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
            Tester.Direction.Should().Be(GridDirection.NorthEast);
        }
    }
}
