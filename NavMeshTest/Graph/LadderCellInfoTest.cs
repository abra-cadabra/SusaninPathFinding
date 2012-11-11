using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using TDDTests;

namespace MyNavMesh.Graph
{
    public class LadderCellInfoTest : TestOf<LadderCellInfo>
    {
        public override void InitializeSystemUnderTest()
        {
            Tester = new LadderCellInfo(Direction.NorthEast);
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
            Tester.Type.Should().Be(CellType.Ladder);
            Tester.Direction.Should().Be(Direction.NorthEast);
        }
    }
}
