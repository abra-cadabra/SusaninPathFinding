using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using SusaninPathFinding.Geometry;
using SusaninPathFinding.Graph;
using SusaninPathFindingTests.TDDTests;
using SusaninPathFinding;
using SusaninPathFinding.Graph.Platformer2DGrid;

namespace SusaninPathFindingTests.Graph
{
    class Platformer2DGraphTests : TestOf<Platformer2DGraph>
    {

        public override void Setup()
        {
            Tester = new Platformer2DGraph(10, 10, 1, new Cell3D(10, 10, 1));
        }

        public override void CleenUp()
        {
        }

        [Test]
        public void GetFallArcTest()
        {
            Vector3 s = Tester[5, 5, 0];
            Tester.GetFallArc(s, 10, 9.8f);
        }

        [Test]
        public void GetFallNeighborsTest()
        {
            float g = 9.8f;
            Cell s = Tester[5, 5, 0];
            List<Vector3> result = Tester.GetFallNeighbors(s, 10, g);
            result[0].Should().Be(Tester[4, 6, 0]);
            result[1].Should().Be(Tester[5, 6, 0]);
            result[2].Should().Be(Tester[6, 6, 0]);

            double vY = (g*Math.Pow(3 + 1, 2))/2;

            result = Tester.GetFallNeighbors(Tester[5, 5, 0], 10, vY);
            result[0].Should().Be(Tester [4, 6, 0]);
            result[1].Should().Be(Tester [5, 6, 0]);
            result[2].Should().Be(Tester [6, 6, 0]);

            result = Tester.GetFallNeighbors(Tester[5, 6, 0], 10, vY);
            result[0].Should().Be(Tester [4, 7, 0]);
            result[1].Should().Be(Tester [5, 7, 0]);
            result[2].Should().Be(Tester [6, 7, 0]);

            result = Tester.GetFallNeighbors(Tester[5, 7, 0], 10, vY);
            result[0].Should().Be(Tester [4, 8, 0]);
            result[1].Should().Be(Tester [5, 8, 0]);
            result[2].Should().Be(Tester [6, 8, 0]);

            result = Tester.GetFallNeighbors(Tester[5, 8, 0], 10, vY);
            result[0].Should().Be(Tester [4, 9, 0]);
            result[1].Should().Be(Tester[5, 9, 0]);
            result[2].Should().Be(Tester[6, 9, 0]);
        }

        [Test]
        public void GetFallStepTest()
        {
            float g = 9.8f;
            Vector3 s = new Vector3(5 * Tester.Polygon.Bounds.SizeX, 5 * Tester.Polygon.Bounds.SizeY, 0);

            List<Vector3> result = Tester.GetFallStep(s, 10, g);

            int t = 2;
            double vY = (g * Math.Pow(t, 2)) / 2;

            foreach (Vector3 point in result)
            {
                List<Vector3> result2 = Tester.GetFallStep(point, 10, vY);
                foreach (Vector3 point2 in result2)
                {
                    List<Vector3> result3 = Tester.GetFallStep(point2, 10, vY);
                }
            }
        }
    }
}
