using SusaninPathFinding.Collections;
using SusaninPathFinding.Geometry;
using NUnit.Framework;
using FluentAssertions;
using SusaninPathFindingTests.TDDTests;


namespace SusaninPathFindingTests.Geometry
{
    [TestFixture]
    class ArrayExTest : TestOf<ArrayEx<int>>
    {
        public override void InitializeSystemUnderTest()
        {

            Tester = new ArrayEx<int>(new []{3, 4, 5});
        }

        public override void Setup()
        {
        }

        public override void CleenUp()
        {
        }

        [Test]
        public void ConstractorTest()
        {
            Tester.Lengths.Should().NotBeNull();
            Tester.Lengths.Length.Should().Be(3);

        }

        [Test]
        public void GetIndexWithThreeIndecesTest()
        {
            Tester.GetIndex(1, 3, 2).Should().Be(34);
        }

        [Test]
        public void GetIndexFromMultipleIndecesTest()
        {
            Tester.GetIndex(new int[] {1, 3, 2}).Should().Be(34);
        }
    }
}
