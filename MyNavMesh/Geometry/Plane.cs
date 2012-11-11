using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyNavMesh.Geometry
{
    public struct Plane : IEquatable<Plane>
    {
#region Fields

        /// <summary>
        /// The normal which defines the plane
        /// </summary>
        private Vector3 _normal;

#endregion

#region Properties

        /// <summary>
        /// The point which defines the plane
        /// </summary>
        public Vector3 Point
        {
            get; set; }

        /// <summary>
        /// Normal which describes the plane
        /// </summary>
        public Vector3 Normal
        {
            get
            {
                return _normal;
            }
            set
            {
                _normal = value;
                _normal.Normalize();
            }
        }

#endregion

#region IEquatable implementation

        /// <summary>
        /// Defines whether the plane equals other plane
        /// </summary>
        /// <param name="other">A plane which is tested for equality</param>
        /// <returns>True if planes ar equal</returns>
        public bool Equals(Plane other)
        {
            return Normal == other.Normal && Point == other.Point;
        }

#endregion

#region Operators

        public static bool operator == (Plane a, Plane b)
        {
            return a.Equals(b);
        }

        public static bool operator != (Plane a, Plane b)
        {
            return !a.Equals(b);
        }

#endregion
    }
}
