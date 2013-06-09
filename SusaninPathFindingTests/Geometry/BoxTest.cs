using SusaninPathFinding.Geometry;
using NUnit.Framework;
using FluentAssertions;
using SusaninPathFindingTests.TDDTests;


namespace SusaninPathFindingTests.Geometry
{
    [TestFixture]
    class BoxTest: TestOf<Box>
    {

        public override void Setup()
        {
            Tester = new Box(5.2344, 2.432, 3.5634, 8.213, 8.2432, 10.32);
        }

        public override void CleenUp()
        {
        }

        [Test]
        public void ConstractorTest()
        {
            
        }

        [Test]
        public void CircumscribeTest()
        {
            Box b = Tester.Circumscribe();
            b.X.Should().BeApproximately(5, IntegerPrec);
            b.Y.Should().BeApproximately(2, IntegerPrec);
            b.Z.Should().BeApproximately(3, IntegerPrec);

            b.SizeX.Should().BeApproximately(9, IntegerPrec);
            b.SizeY.Should().BeApproximately(9, IntegerPrec);
            b.SizeZ.Should().BeApproximately(11, IntegerPrec);
            //return new Box(Math.Floor(X), Math.Floor(Y), Math.Floor(Z), Math.Ceiling(SizeX), Math.Ceiling(SizeY), Math.Ceiling(SizeZ));
        }

        [Test]
        public void CircumscribePointsTest()
        {
            Vector3[] arr = new Vector3[]
                {
                    new Vector3(4, 2, 3),
                    new Vector3(32, 1, 4),
                    new Vector3(2, 34, 21),
                    new Vector3(34, 2, 12),
                    new Vector3(6, 57, 25),
                };
            Box b = Box.Circumscribe(arr);

            b.X.Should().BeApproximately(2, IntegerPrec);
            b.Y.Should().BeApproximately(1, IntegerPrec);
            b.Z.Should().BeApproximately(3, IntegerPrec);

            b.SizeX.Should().BeApproximately(32, IntegerPrec);
            b.SizeY.Should().BeApproximately(56, IntegerPrec);
            b.SizeZ.Should().BeApproximately(22, IntegerPrec);
        }

        [Test]
        public void Contains()
        {
            Tester.Contains(7.234, 8.243, 10.32).Should().BeTrue();
        }
    }
}
