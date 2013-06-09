using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using SusaninPathFindingTests.TDDTests;
using SusaninPathFindingUDKInterface;

namespace SusaninPathFindingTests.UDKInterface
{
    [TestFixture]
    class SusaninPathFindingUdkInterfaceTest : TestOf<SusaninPathFindingUdkInterface>
    {
        public override void Setup()
        {
            Tester = new SusaninPathFindingUdkInterface();
        }

        public override void CleenUp()
        {
        }

        [Test]
        public void CreateGridTest()
        {
            //Tester.Map.Should().NotBeNull();
        }
    }
}
