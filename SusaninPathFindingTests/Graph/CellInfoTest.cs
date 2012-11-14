using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using SusaninPathFinding.Graph;
using SusaninPathFinding.Graph.NodeInfoTypes;
using SusaninPathFindingTests.Graph;
using NUnit.Framework;
using SusaninPathFindingTests.TDDTests;

namespace SusaninPathFindingTests.Graph
{
    [TestFixture]
    public class NodeInfoTest : TestOf<NodeInfo>
    {

        public override void InitializeSystemUnderTest()
        {
            Tester = new Passable();
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
            Tester.GetType().Should().Be(typeof(Empty));
        }

        [Test]
        public void TypePropertyTest()
        {
            //Tester = new Passable();
            //Tester.Type.Should().Be(CellType.Passable);
            //Tester.Invoking(y => y.Type = CellType.Ladder).ShouldThrow<ArithmeticException>().WithMessage(
            //    "Type forbidden. Pleace, create new LadderCellInfo insted");
            //Tester.Type.Should().Be(CellType.Passable);

        }
    }
}
