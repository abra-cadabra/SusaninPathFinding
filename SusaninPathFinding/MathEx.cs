using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SusaninPathFinding
{
    public static class MathEx
    {
        public static double Modulo(double a, double p)
        {
            return a - Math.Floor(a / p) * p;
        }

        /// <summary>
        /// Compares two double with given Epsilon. Default Epsilon is 0.000001D
        /// </summary>
        /// <param name="double1">First double</param>
        /// <param name="double2">Second double</param>
        /// <param name="epsilon">Epsilon. Default value is 0.000001D</param>
        /// <returns>true if Math.Abs(double1 - double2) less or equal to epsilon </returns>
        public static bool AlmostEquals(this double double1, double double2, double epsilon = 0.000001D)
        {
            return (Math.Abs(double1 - double2) <= epsilon);
        }

        /// <summary>
        /// Compares two floats with given Epsilon. Default Epsilon is 0.00001f
        /// </summary>
        /// <param name="double1">First double</param>
        /// <param name="double2">Second double</param>
        /// <param name="epsilon">Epsilon. Default value is 0.00001f</param>
        /// <returns>true if Math.Abs(double1 - double2) less or equal to epsilon </returns>
        public static bool AlmostEquals(this float double1, float double2, float epsilon = 0.00001f)
        {
            return (Math.Abs(double1 - double2) <= epsilon);
        }


    }
}
