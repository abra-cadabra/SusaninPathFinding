using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyNavMesh.Geometry
{
    public interface IPolygon
    {
        Box Bounds { get; set; }

        int Connectivity { get; }
    }
}
