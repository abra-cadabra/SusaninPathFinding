using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SusaninPathFinding.Geometry;
using SusaninPathFinding.Graph;
using SusaninPathFinding.Graph.NodeInfoTypes;
using SusaninPathFinding.Graph.Platformer2DGrid;
using SusaninPathFinding.Source.Graph.PathFinding;
using SusaninPathFindingTests.TDDTests;

namespace SusaninPathFindingTests.Graph.PathFinding
{
    [TestFixture]
    class OpenClosedGraphTests : TestOf<OpenClosedGrid>
    {
        private Grid3D Map;

        public override void Setup()
        {
            Map = Grid3DTest.GenerateTestGrid();
            Tester = new OpenClosedGrid(Map);
        }

        public override void CleenUp()
        {
        }

        [Test]
        public void GetNeighborsTest()
        {
            
        }

        public static OpenClosedGrid GenerateOpenClosedList(Grid3D grid)
        {
            OpenClosedGrid list = new OpenClosedGrid(grid);

            return list;
        }
    }
}
