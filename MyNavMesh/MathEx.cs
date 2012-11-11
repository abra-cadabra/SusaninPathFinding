using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyNavMesh
{
    public static class MathEx
    {
        public static double Modulo(double a, double p)
        {
            return a - Math.Floor(a / p) * p;
        }
    }
}
