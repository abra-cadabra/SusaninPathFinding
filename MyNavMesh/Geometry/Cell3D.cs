using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyNavMesh.Geometry
{
    public class Cell3D : IPolygon
    {
        public Box Bounds { get; set; }

        public int Connectivity { get; private set; }

        public Cell3D(double sizeX, double sizeY, double sizeZ)
        {
            Bounds = new Box(0, 0, 0, sizeX, sizeY, sizeZ);
            Connectivity = 3 * 3 * 3 - 1;
        }
    }
}
