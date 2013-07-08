using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using SusaninPathFinding.Geometry;
using SusaninPathFinding.Graph;
using SusaninPathFinding.Graph.NodeInfoTypes;
using SusaninPathFindingTests.TDDTests;
using SusaninPathFinding;
using SusaninPathFinding.Graph.Platformer2DGrid;

namespace SusaninPathFindingTests.Graph
{
    [TestFixture]
    public class Platformer2DGraphTests : TestOf<Platformer2DGrid>
    {

        public override void Setup()
        {
            //Tester = new Platformer2DGraph(10, 10, 1, new Cell3D(10, 10, 1));
        }

        public override void CleenUp()
        {
        }

        [Test]
        public void GetFallArcTest()
        {
            //Vector3 s = Tester[5, 5, 0];
            //Tester.GetFallArc(s, 10, 9.8f);
        }

        [Test]
        public void GetNeighborsTest()
        {
            //for (int i = 0; i < Tester.SizeY; i++)
            //{
            //    for (int j = 0; j < Tester.SizeX; j++)
            //    {
            //        Tester[j, i, 0].Info = new Passable();
            //    }
            //}
            //Tester[1, 0, 0].Info = new Impassable();
            //var list = Tester.GetNeighbors(Tester[1, 1, 0], new RuningManAlgorithm(Tester));
            //Cell result = list.Find
            //(
            //    delegate(Cell node)
            //    {
            //        return node == Tester[1, 0, 0];
            //    }
            //);
            //result.Should().NotBeNull();
            //result.Info.GetType().Should().Be(typeof(Impassable));
        }

        [Test]
        public void GetFallNeighborsTest()
        {
            //float g = 9.8f;
            //Cell s = Tester[5, 5, 0];
            //List<Vector3> result = Tester.GetFallNeighbors(s, 10, g);
            //result[0].Should().Be(Tester[4, 6, 0]);
            //result[1].Should().Be(Tester[5, 6, 0]);
            //result[2].Should().Be(Tester[6, 6, 0]);

            //double vY = (g*Math.Pow(3 + 1, 2))/2;

            //result = Tester.GetFallNeighbors(Tester[5, 5, 0], 10, vY);
            //result[0].Should().Be(Tester [4, 6, 0]);
            //result[1].Should().Be(Tester [5, 6, 0]);
            //result[2].Should().Be(Tester [6, 6, 0]);

            //result = Tester.GetFallNeighbors(Tester[5, 6, 0], 10, vY);
            //result[0].Should().Be(Tester [4, 7, 0]);
            //result[1].Should().Be(Tester [5, 7, 0]);
            //result[2].Should().Be(Tester [6, 7, 0]);

            //result = Tester.GetFallNeighbors(Tester[5, 7, 0], 10, vY);
            //result[0].Should().Be(Tester [4, 8, 0]);
            //result[1].Should().Be(Tester [5, 8, 0]);
            //result[2].Should().Be(Tester [6, 8, 0]);

            //result = Tester.GetFallNeighbors(Tester[5, 8, 0], 10, vY);
            //result[0].Should().Be(Tester [4, 9, 0]);
            //result[1].Should().Be(Tester[5, 9, 0]);
            //result[2].Should().Be(Tester[6, 9, 0]);
        }

        [Test]
        public void GetFallStepTest()
        {
            //Tester[3, 9, 0].Info = new Passable();
            //Tester[4, 9, 0].Info = new Passable();
            //Tester[5, 9, 0].Info = new Passable();
            //Tester[6, 9, 0].Info = new Passable();

            //float g = 9.8f;
            //Vector3 s = new Vector3(5 * Tester.Polygon.Bounds.SizeX, 5 * Tester.Polygon.Bounds.SizeY, 0);

            //List<Vector3> result = Tester.GetFallStep(s, 10, g);

            //int t = 2;
            //double vY = (g * Math.Pow(t, 2)) / 2;

            //foreach (Vector3 point in result)
            //{
            //    List<Vector3> result2 = Tester.GetFallStep(point, 10, vY);
            //    foreach (Vector3 point2 in result2)
            //    {
            //        List<Vector3> result3 = Tester.GetFallStep(point2, 10, vY);
            //    }
            //}
        }

        public static Platformer2DGrid GeneratePlatformerMap()
        {
            Platformer2DGrid grid = new Platformer2DGrid(10, 10, 1, new Cell3D(10, 10, 1), new Empty());
            for (int i = 0; i < 10; i++ )
            {
                grid[i, 8, 0].Info = new Passable();
                grid[i, 9, 0].Info = new Impassable();
            }

            grid[3, 6, 0].Info = new Passable();
            grid[3, 7, 0].Info = new Impassable();

            grid[6, 7, 0].Info = new Passable();
            grid[7, 7, 0].Info = new Passable();
            grid[8, 7, 0].Info = new Passable();

            grid[6, 8, 0].Info = new Impassable();
            grid[7, 8, 0].Info = new Impassable();
            grid[8, 8, 0].Info = new Impassable();

            return grid;
        }
    }
}
