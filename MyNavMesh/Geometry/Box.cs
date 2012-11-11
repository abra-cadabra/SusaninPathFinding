using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MyNavMesh.Geometry
{

    /// <summary>
    /// Specifies the location of a point relative to a rectangle.</summary>
    /// <remarks>
    /// Every valid <b>RectLocation</b> value is a bitwise combination of an <b>…X</b> value and a
    /// <b>…Y</b> value, indicating the location of each point coordinate relative to the 
    /// corresponding nearest edge of the rectangle.</remarks>

    [Flags]
    public enum RectLocation
    {

        /// <summary>
        /// Specifies that the point’s location is unknown or does not exist.</summary>

        None = 0,

        /// <summary>
        /// Specifies that the point’s x-coordinate is located before the rectangle’s smallest
        /// x-coordinate.</summary>

        BeforeX = 1,

        /// <summary>
        /// Specifies that the point’s x-coordinate equals the rectangle’s smallest x-coordinate.
        /// </summary>

        StartX = 2,

        /// <summary>
        /// Specifies that the point’s x-coordinate is located between the rectangle’s smallest and
        /// greatest x-coordinate, exclusively.</summary>

        InsideX = 4,

        /// <summary>
        /// Specifies that the point’s x-coordinate equals the rectangle’s greatest x-coordinate.
        /// </summary>

        EndX = 8,

        /// <summary>
        /// Specifies that the point’s x-coordinate is located after the rectangle’s greatest
        /// x-coordinate.</summary>

        AfterX = 16,

        /// <summary>
        /// Specifies that the point’s y-coordinate is located before the rectangle’s smallest
        /// y-coordinate.</summary>

        BeforeY = 32,

        /// <summary>
        /// Specifies that the point’s y-coordinate equals the rectangle’s smallest y-coordinate.
        /// </summary>

        StartY = 64,

        /// <summary>
        /// Specifies that the point’s y-coordinate is located between the rectangle’s smallest and
        /// greatest y-coordinate, exclusively.</summary>

        InsideY = 128,

        /// <summary>
        /// Specifies that the point’s y-coordinate equals the rectangle’s greatest y-coordinate.
        /// </summary>

        EndY = 256,

        /// <summary>
        /// Specifies that the point’s y-coordinate is located after the rectangle’s greatest
        /// y-coordinate.</summary>

        AfterY = 512
    }

    /// <summary>
    /// Represents a rectangular region in two-dimensional space, using <see cref="Double"/>
    /// coordinates.</summary>
    /// <remarks><para>
    /// <b>RectD</b> is an immutable structure whose four <see cref="Double"/> coordinates and 
    /// extensions define a rectangular region in two-dimensional space.
    /// </para><para>
    /// The <see cref="MyNavMesh.Geometry.RectD.Left"/>, <see cref="MyNavMesh.Geometry.RectD.Top"/>, <see cref="MyNavMesh.Geometry.RectD.Right"/>, and <see
    /// cref="MyNavMesh.Geometry.RectD.Bottom"/> properties assume drawing orientation rather than mathematical
    /// orientation. That is, x-coordinates increase towards the right but y-coordinates increase
    /// downward. This is the same orientation used by all BCL rectangle structures.
    /// </para><para>
    /// <b>RectD</b> uses a <em>geometric inclusion model</em> to determine which coordinates are
    /// contained within the rectangle, like <b>System.Windows.Rect</b>. That is, <see
    /// cref="MyNavMesh.Geometry.RectD.Width"/> and <see cref="MyNavMesh.Geometry.RectD.Height"/> act like the dimensions of a closed
    /// polygon, indicating the greatest coordinates within the <see cref="MyNavMesh.Geometry.RectD"/>. Therefore, the
    /// coordinates <see cref="MyNavMesh.Geometry.RectD.Right"/> (= <see cref="MyNavMesh.Geometry.RectD.Left"/> + <see
    /// cref="MyNavMesh.Geometry.RectD.Width"/>) and <see cref="MyNavMesh.Geometry.RectD.Bottom"/> (= <see cref="MyNavMesh.Geometry.RectD.Top"/> + <see
    /// cref="MyNavMesh.Geometry.RectD.Height"/>) are still considered part of the <see cref="MyNavMesh.Geometry.RectD"/>.
    /// </para><para>
    /// Use the <see cref="RectI"/> structure to represent rectangles with <see cref="Int32"/>
    /// components, and the <see cref="RectF"/> structure to represent rectangles with <see
    /// cref="Single"/> components. You can convert <see cref="MyNavMesh.Geometry.RectD"/> instances to and from <see
    /// cref="RectI"/> and <see cref="RectF"/> instances, rounding off the <see cref="Double"/>
    /// components as necessary.</para></remarks>

    [Serializable]
    public class Box : IEquatable<Box>
    {


        #region Box(Double, Double, Double, Double, Double, Double)

        /// <overloads>
        /// Initializes a new instance of the <see cref="MyNavMesh.Geometry.RectD"/> structure.</overloads>
        /// <summary>
        /// Initializes a new instance of the <see cref="MyNavMesh.Geometry.RectD"/> structure with the specified
        /// <see cref="Double"/> coordinates and dimensions.</summary>
        /// <param name="x">
        /// The smallest <see cref="X"/> coordinate within the <see cref="MyNavMesh.Geometry.RectD"/>.</param>
        /// <param name="y">
        /// The smallest <see cref="Y"/> coordinate within the <see cref="MyNavMesh.Geometry.RectD"/>.</param>
        /// <param name="width">
        /// The <see cref="SizeX"/> of the <see cref="MyNavMesh.Geometry.RectD"/>.</param>
        /// <param name="height">
        /// The <see cref="SizeZ"/> of the <see cref="MyNavMesh.Geometry.RectD"/>.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="width"/> or <paramref name="height"/> is less than zero.</exception>

        public Box(double x, double y, double z, double sizeX, double sizeY, double sizeZ)
        {
            if (sizeX < 0)
                throw new ArgumentOutOfRangeException(
                    "SizeX", sizeX, Strings.ArgumentNegative);

            if (sizeY < 0)
                throw new ArgumentOutOfRangeException(
                    "SizeY", sizeY, Strings.ArgumentNegative);

            if (sizeZ < 0)
                throw new ArgumentOutOfRangeException(
                    "SizeZ", sizeZ, Strings.ArgumentNegative);


            X = x;
            Y = y;
            Z = z;
            SizeX = sizeX;
            SizeY = sizeY;
            SizeZ = sizeZ;
        }

        #endregion
        #region Box(Point3D, Point3D)

        ///// <summary>
        ///// Initializes a new instance of the <see cref="MyNavMesh.Geometry.RectD"/> structure that contains the
        ///// specified <see cref="PointD"/> coordinates.</summary>
        ///// <param name="point1">
        ///// The first <see cref="PointD"/> that the <see cref="MyNavMesh.Geometry.RectD"/> must contain.</param>
        ///// <param name="point2">
        ///// The second <see cref="PointD"/> that the <see cref="MyNavMesh.Geometry.RectD"/> must contain.</param>
        ///// <remarks>
        ///// <see cref="Location"/> is set to the smaller coordinate of <paramref name="point1"/> and
        ///// <paramref name="point2"/> in each dimension, and <see cref="Size"/> is set to the
        ///// difference between the larger and the smaller coordinate in each dimension.</remarks>

        //public Box(Point3D location, Point3D point2)
        //{
        //    double left, top, raise, right, bottom, height;

        //    if (point1.X < point2.X)
        //    {
        //        left = point1.X;
        //        right = point2.X;
        //    }
        //    else
        //    {
        //        left = point2.X;
        //        right = point1.X;
        //    }

        //    if (point1.Y < point2.Y)
        //    {
        //        top = point1.Y;
        //        bottom = point2.Y;
        //    }
        //    else
        //    {
        //        top = point2.Y;
        //        bottom = point1.Y;
        //    }

        //    if (point1.Z < point2.Z)
        //    {
        //        raise = point1.Z;
        //        height = point2.Z;
        //    }
        //    else
        //    {
        //        raise = point2.Z;
        //        height = point1.Z;
        //    }

        //    X = left;
        //    Y = top;
        //    Z = raise;

        //    SizeX = right - left;
        //    SizeY = bottom - top;
        //    SizeZ = height - raise;
        //}

        //#endregion
        //#region RectD(PointD, SizeD)

        ///// <summary>
        ///// Initializes a new instance of the <see cref="MyNavMesh.Geometry.RectD"/> structure with the specified
        ///// <see cref="PointD"/> coordinates and <see cref="SizeD"/> dimensions.</summary>
        ///// <param name="location">
        ///// The <see cref="Location"/> of the <see cref="MyNavMesh.Geometry.RectD"/>.</param>
        ///// <param name="size">
        ///// The <see cref="Size"/> of the <see cref="MyNavMesh.Geometry.RectD"/>.</param>

        public Box(Vector3 location, Vector3 size)

        {
            X = location.X;
            Y = location.Y;
            Z = location.Z;

            SizeX = size.X;
            SizeY = size.Y;
            SizeZ = size.Z;
        }

        #endregion
        //#region Empty

        ///// <summary>
        ///// An empty read-only <see cref="MyNavMesh.Geometry.RectD"/> instance.</summary>
        ///// <remarks>
        ///// <b>Empty</b> contains a <see cref="MyNavMesh.Geometry.RectD"/> instance that was created with the default
        ///// constructor.</remarks>

        //public static readonly RectD Empty = new RectD();

        //#endregion
        #region Public Properties

        /// <summary>
        /// X coordinate of the box.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Y coordinate of the box.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Z coordinate of the box.
        /// </summary>
        public double Z { get; set; }

        /// <summary>
        /// X size of the box.
        /// </summary>
        public double SizeX;

        /// <summary>
        /// Y size of the box.
        /// </summary>
        public double SizeY;

        /// <summary>
        /// Z size of the box.
        /// </summary>
        public double SizeZ;

        #region Location

        /// <summary>
        /// Gets the coordinates of the upper-left corner of the <see cref="MyNavMesh.Geometry.RectD"/>.</summary>
        /// <value>
        /// A <see cref="PointD"/> containing the <see cref="X"/> and <see cref="Y"/> coordinates.
        /// </value>
        /// <remarks>
        /// <b>Location</b> holds the smallest x- and y-coordinates that are contained within the
        /// <see cref="MyNavMesh.Geometry.RectD"/>.</remarks>

        public Vector3 Location
        {
            get { return new Vector3(X, Y, Z); }
        }

        #endregion
        #region Size

        /// <summary>
        /// Gets the extension of the <see cref="MyNavMesh.Geometry.RectD"/>.</summary>
        /// <value>
        /// A <see cref="SizeD"/> containing the <see cref="SizeX"/> and <see cref="SizeZ"/>
        /// dimensions.</value>
        /// <remarks>
        /// The <see cref="MyNavMesh.Geometry.RectD"/> covers the area beginning at <see cref="Location"/> and
        /// extending over <see cref="Size"/> with increasing x- and y-coordinates.</remarks>

        public Vector3 Size
        {
            get { return new Vector3(SizeX, SizeY, SizeZ); }
        }

        #endregion
        
        #endregion
        #region Public Methods
        #region Circumscribe()

        /// <overloads>
        /// Circumscribes a rectangle around the specified coordinates.</overloads>
        /// <summary>
        /// Circumscribes a <see cref="RectI"/> around the <see cref="MyNavMesh.Geometry.RectD"/>.</summary>
        /// <returns>
        /// A <see cref="RectI"/> that entirely covers the <see cref="MyNavMesh.Geometry.RectD"/>.</returns>
        /// <remarks>
        /// <b>Circumscribe</b> returns a <see cref="RectI"/> that contains the <see
        /// cref="Fortran.Floor"/> of the <see cref="X"/> and <see cref="Y"/> coordinates, and the
        /// <see cref="Fortran.Ceiling"/> of the <see cref="SizeX"/> and <see cref="SizeZ"/>
        /// dimensions. This ensures that the <see cref="MyNavMesh.Geometry.RectD"/> is entirely covered.</remarks>

        public Box Circumscribe()
        {

            return new Box(Math.Floor(X), Math.Floor(Y), Math.Floor(Z), Math.Ceiling(SizeX), Math.Ceiling(SizeY), Math.Ceiling(SizeZ));
        }

        #endregion
        #region Circumscribe(PointD[])

        /// <summary>
        /// Circumscribes a <see cref="MyNavMesh.Geometry.RectD"/> around the specified <see cref="PointD"/>
        /// coordinates.</summary>
        /// <param name="points">
        /// An <see cref="Array"/> containing the <see cref="PointD"/> coordinates whose bounds to
        /// determine.</param>
        /// <returns>
        /// The smallest <see cref="MyNavMesh.Geometry.RectD"/> that contains all specified <paramref name="points"/>.
        /// </returns>
        /// <exception cref="ArgumentNullOrEmptyException">
        /// <paramref name="points"/> is a null reference or an empty array.</exception>

        public static Box Circumscribe(params Vector3[] points)
        {
            if (points == null || points.Length == 0)
                throw new ArgumentNullOrEmptyException("points");

            double x0 = Double.MaxValue, y0 = Double.MaxValue, z0 = Double.MaxValue;
            double x1 = Double.MinValue, y1 = Double.MinValue, z1 = Double.MinValue;

            foreach (Vector3 point in points)
            {
                if (x0 > point.X) x0 = point.X;
                if (y0 > point.Y) y0 = point.Y;
                if (z0 > point.Z) z0 = point.Z;
                if (x1 < point.X) x1 = point.X;
                if (y1 < point.Y) y1 = point.Y;
                if (z1 < point.Z) z1 = point.Z;
            }

            return new Box(x0, y0, z0, x1 - x0, y1 - y0, z1 - z0);
        }

        #endregion
        #region Contains(Double, Double, Double)

        /// <overloads>
        /// Indicates whether the <see cref="MyNavMesh.Geometry.RectD"/> contains the specified coordinates.
        /// </overloads>
        /// <summary>
        /// Indicates whether the <see cref="MyNavMesh.Geometry.RectD"/> contains the specified <see cref="Double"/>
        /// coordinates.</summary>
        /// <param name="x">
        /// The x-coordinate to examine.</param>
        /// <param name="y">
        /// The y-coordinate to examine.</param>
        /// <returns>
        /// <c>true</c> if the <see cref="MyNavMesh.Geometry.RectD"/> contains the specified <paramref name="x"/> and
        /// <paramref name="y"/> coordinates; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// <b>Contains</b> assumes that the <see cref="MyNavMesh.Geometry.RectD"/> contains the <see cref="Right"/>
        /// and <see cref="Bottom"/> coordinates.</remarks>

        public bool Contains(double x, double y, double z)
        {
            return(x >= X && x <= X + SizeX
                && y >= Y && y <= Y + SizeY
                && z >= Z && z <= Z + SizeZ);
        }

        #endregion
        //#region Contains(Point3D)

        ///// <summary>
        ///// Indicates whether the <see cref="MyNavMesh.Geometry.RectD"/> contains the specified <see cref="PointD"/>
        ///// coordinates.</summary>
        ///// <param name="point">
        ///// The <see cref="PointD"/> to examine.</param>
        ///// <returns>
        ///// <c>true</c> if the <see cref="MyNavMesh.Geometry.RectD"/> contains the specified <paramref name="point"/>;
        ///// otherwise, <c>false</c>.</returns>
        ///// <remarks>
        ///// <b>Contains</b> assumes that the <see cref="MyNavMesh.Geometry.RectD"/> contains the <see cref="Right"/>
        ///// and <see cref="Bottom"/> coordinates.</remarks>

        //public bool Contains(Vector3 point)
        //{
        //    return Contains(point.X, point.Y, point.Z);
        //}

        //#endregion
        //#region Contains(Box)

        ///// <summary>
        ///// Indicates whether the <see cref="MyNavMesh.Geometry.RectD"/> contains the specified rectangle.</summary>
        ///// <param name="rect">
        ///// The <see cref="MyNavMesh.Geometry.RectD"/> to examine.</param>
        ///// <returns>
        ///// <c>true</c> if the <see cref="MyNavMesh.Geometry.RectD"/> entirely contains the specified <paramref
        ///// name="rect"/>; otherwise, <c>false</c>.</returns>
        ///// <remarks>
        ///// <b>Contains</b> returns <c>true</c> even if both <see cref="MyNavMesh.Geometry.RectD"/> instances have a
        ///// <see cref="SizeX"/> or <see cref="SizeZ"/> of zero, provided they have the same <see
        ///// cref="Location"/> in the corresponding dimension.</remarks>

        //public bool Contains(Box rect)
        //{

        //    return(rect.X >= X && rect.X + rect.SizeX <= X + SizeX 
        //        && rect.Y >= Y && rect.Y + rect.SizeY <= Y + SizeY
        //        && rect.Z >= Z && rect.Z + rect.SizeZ <= Z + SizeZ);
        //}

        //#endregion
        ////#region ContainsOpen(Double, Double)

        /////// <overloads>
        /////// Indicates whether the <see cref="MyNavMesh.Geometry.RectD"/> contains the specified coordinates, excluding
        /////// <see cref="Right"/> and <see cref="Bottom"/>.</overloads>
        /////// <summary>
        /////// Indicates whether the <see cref="MyNavMesh.Geometry.RectD"/> contains the specified <see cref="Double"/>
        /////// coordinates, excluding <see cref="Right"/> and <see cref="Bottom"/>.</summary>
        /////// <param name="x">
        /////// The x-coordinate to examine.</param>
        /////// <param name="y">
        /////// The y-coordinate to examine.</param>
        /////// <returns>
        /////// <c>true</c> if the <see cref="MyNavMesh.Geometry.RectD"/> contains the specified <paramref name="x"/> and
        /////// <paramref name="y"/> coordinates, excluding <see cref="Right"/> and <see
        /////// cref="Bottom"/>; otherwise, <c>false</c>.</returns>
        /////// <remarks>
        /////// <b>ContainsOpen</b> assumes that the <see cref="MyNavMesh.Geometry.RectD"/> does not contain the <see
        /////// cref="Right"/> and <see cref="Bottom"/> coordinates, emulating <see cref="RectI"/>
        /////// behavior.</remarks>

        ////public bool ContainsOpen(double x, double y)
        ////{
        ////    return (x >= X && y >= Y && x < X + SizeX && y < Y + SizeZ);
        ////}

        ////#endregion
        ////#region ContainsOpen(PointD)

        /////// <summary>
        /////// Indicates whether the <see cref="MyNavMesh.Geometry.RectD"/> contains the specified <see cref="PointD"/>
        /////// coordinates, excluding <see cref="Right"/> and <see cref="Bottom"/>.</summary>
        /////// <param name="point">
        /////// The <see cref="PointD"/> to examine.</param>
        /////// <returns>
        /////// <c>true</c> if the <see cref="MyNavMesh.Geometry.RectD"/> contains the specified <paramref name="point"/>,
        /////// excluding <see cref="Right"/> and <see cref="Bottom"/>; otherwise, <c>false</c>.
        /////// </returns>
        /////// <remarks>
        /////// <b>ContainsOpen</b> assumes that the <see cref="MyNavMesh.Geometry.RectD"/> does not contain the <see
        /////// cref="Right"/> and <see cref="Bottom"/> coordinates, emulating <see cref="RectI"/>
        /////// behavior.</remarks>

        ////public bool ContainsOpen(PointD point)
        ////{
        ////    return ContainsOpen(point.X, point.Y);
        ////}

        ////#endregion
        //#region GetDistanceVector

        ///// <summary>
        ///// Finds the distance vector from the specified <see cref="PointD"/> coordinates to the
        ///// nearest edges of the <see cref="MyNavMesh.Geometry.RectD"/>.</summary>
        ///// <param name="q">
        ///// The <see cref="PointD"/> coordinates to examine.</param>
        ///// <returns>
        ///// A <see cref="PointD"/> vector indicating the distance of each <paramref name="q"/> 
        ///// coordinate from the nearest corresponding edge of the <see cref="MyNavMesh.Geometry.RectD"/>.</returns>
        ///// <remarks><para>
        ///// <b>GetDistanceVector</b> defines the components of the returned <see cref="PointD"/>
        ///// vector as follows, assuming that <em>qx</em> and <em>qy</em> are the coordinates of
        ///// <paramref name="q"/>:
        ///// </para><list type="table"><listheader>
        ///// <term><b>X</b></term><term><b>Y</b></term><description>Condition</description>
        ///// </listheader><item>
        ///// <term>0</term><term></term><description>
        ///// <see cref="Left"/> &lt;= <em>qx</em> &lt;= <see cref="Right"/></description>
        ///// </item><item>
        ///// <term><em>qx</em> – <see cref="Left"/></term><term></term>
        ///// <description><em>qx</em> &lt; <see cref="Left"/></description>
        ///// </item><item>
        ///// <term><em>qx</em> – <see cref="Right"/></term><term></term>
        ///// <description><em>qx</em> &gt; <see cref="Right"/></description>
        ///// </item><item>
        ///// <term/><term>0</term><description>
        ///// <see cref="Top"/> &lt;= <em>qy</em> &lt;= <see cref="Bottom"/></description>
        ///// </item><item>
        ///// <term/><term><em>qy</em> – <see cref="Top"/></term>
        ///// <description><em>qy</em> &lt; <see cref="Top"/></description>
        ///// </item><item>
        ///// <term/><term><em>qy</em> – <see cref="Bottom"/></term>
        ///// <description><em>qy</em> &gt; <see cref="Bottom"/></description>
        ///// </item></list><para>
        ///// Each vector component is zero exactly if the corresponding <paramref name="q"/>
        ///// coordinate lies within the corresponding <see cref="MyNavMesh.Geometry.RectD"/> extension. Otherwise, the
        ///// component’s absolute value indicates the coordinate’s distance from the nearest <see
        ///// cref="MyNavMesh.Geometry.RectD"/> edge, and its sign indicates that edge itself.</para></remarks>

        //public Vector3 GetDistanceVector(Vector3 q)
        //{
        //    double qx = q.X - X, qy = q.Y - Y, qz = q.Z - Z;

        //    double x = ((qx < 0) ? qx : ((qx > SizeX) ? (qx - SizeX) : 0));
        //    double y = ((qy < 0) ? qy : ((qy > SizeY) ? (qy - SizeY) : 0));
        //    double z = ((qz < 0) ? qz : ((qz > SizeZ) ? (qy - SizeZ) : 0));

        //    return new Vector3(x, y, z);
        //}

        //#endregion
        ////#region GetHashCode

        /////// <summary>
        /////// Returns the hash code for this <see cref="MyNavMesh.Geometry.RectD"/> instance.</summary>
        /////// <returns>
        /////// An <see cref="Double"/> hash code.</returns>
        /////// <remarks>
        /////// <b>GetHashCode</b> combines the values of the <see cref="X"/>, <see cref="Y"/>, <see
        /////// cref="SizeX"/>, and <see cref="SizeZ"/> properties.</remarks>

        ////public override unsafe int GetHashCode()
        ////{
        ////    unchecked
        ////    {
        ////        double x = X, y = Y, w = SizeX, h = SizeZ;
        ////        long xi = *((long*)&x), yi = *((long*)&y);
        ////        long wi = *((long*)&w), hi = *((long*)&h);

        ////        return (int)xi ^ (int)(xi >> 32) ^ (int)yi ^ (int)(yi >> 32)
        ////            ^ (int)wi ^ (int)(wi >> 32) ^ (int)hi ^ (int)(hi >> 32);
        ////    }
        ////}

        ////#endregion
        //#region Intersect(LineD)

        ///// <overloads>
        ///// Intersects the <see cref="MyNavMesh.Geometry.RectD"/> with the specified object.</overloads>
        ///// <summary>
        ///// Intersects the <see cref="MyNavMesh.Geometry.RectD"/> with the specified <see cref="LineD"/>.</summary>
        ///// <param name="line">
        ///// The <see cref="LineD"/> to intersect with the <see cref="MyNavMesh.Geometry.RectD"/>.</param>
        ///// <param name="intersection">
        ///// On success, the intersection of the <see cref="MyNavMesh.Geometry.RectD"/> and the specified <paramref
        ///// name="line"/>; otherwise, <see cref="LineD.Empty"/>.</param>
        ///// <returns>
        ///// <c>true</c> if the <see cref="MyNavMesh.Geometry.RectD"/> intersects with the specified <paramref
        ///// name="line"/>; otherwise, <c>false</c>.</returns>
        ///// <remarks>
        ///// <b>Intersect</b> performs the Liang-Barsky line clipping algorithm. This C#
        ///// implementation was adapted from the C implementation by Daniel White, published at <a
        ///// href="http://www.skytopia.com/project/articles/compsci/clipping.html">Skytopia</a>.
        ///// </remarks>

        //public bool Intersect(Line3D line, out Vector3 intersection)
        //{
        //    // TODO: Intersect the box with the line
        //    throw new NotImplementedException();
        ////    double x0 = line.Start.X, y0 = line.Start.Y;
        ////    double dx = line.End.X - x0, dy = line.End.Y - y0;
        ////    double t0 = 0, t1 = 1, p = 0, q = 0;

        ////    // traverse all four rectangle borders
        ////    for (int border = 0; border < 4; border++)
        ////    {
        ////        switch (border)
        ////        {
        ////            case 0: p = -dx; q = x0 - X; break;
        ////            case 1: p = +dx; q = X + SizeX - x0; break;
        ////            case 2: p = -dy; q = y0 - Y; break;
        ////            case 3: p = +dy; q = SizeZ + Y - y0; break;
        ////        }

        ////        if (p == 0)
        ////        {
        ////            // parallel line outside of rectangle
        ////            if (q < 0) goto failure;
        ////        }
        ////        else
        ////        {
        ////            double r = q / p;
        ////            if (p < 0)
        ////            {
        ////                if (r > t1) goto failure;
        ////                if (r > t0) t0 = r;
        ////            }
        ////            else
        ////            {
        ////                if (r < t0) goto failure;
        ////                if (r < t1) t1 = r;
        ////            }
        ////        }
        ////    }

        ////    intersection = new Line3D(
        ////        x0 + t0 * dx, y0 + t0 * dy,
        ////        x0 + t1 * dx, y0 + t1 * dy);
        ////    return true;

        ////failure:
        ////    intersection = Line3D.Empty;
        ////    return false;
        //}

        //#endregion
        //#region Intersect(PointD[])

        ///// <summary>
        ///// Intersects the <see cref="MyNavMesh.Geometry.RectD"/> with the specified arbitrary polygon.</summary>
        ///// <param name="polygon">
        ///// An <see cref="Array"/> containing <see cref="PointD"/> coordinates that are the vertices
        ///// of the polygon to intersect with the <see cref="MyNavMesh.Geometry.RectD"/>.</param>
        ///// <param name="intersection">
        ///// On success, the intersection of the <see cref="MyNavMesh.Geometry.RectD"/> and the specified <paramref
        ///// name="polygon"/>; otherwise, an empty <see cref="Array"/>.</param>
        ///// <returns>
        ///// <c>true</c> if the <see cref="MyNavMesh.Geometry.RectD"/> intersects with the specified <paramref
        ///// name="polygon"/>; otherwise, <c>false</c>.</returns>
        ///// <exception cref="ArgumentNullOrEmptyException">
        ///// <paramref name="polygon"/> is a null reference or an empty array.</exception>
        ///// <remarks><para>
        ///// <b>Intersect</b> performs the Sutherland–Hodgman polygon clipping algorithm, optimized
        ///// for an axis-aligned <see cref="MyNavMesh.Geometry.RectD"/> as the clipping polygon. At intersection points,
        ///// the border coordinates of the <see cref="MyNavMesh.Geometry.RectD"/> are copied rather than computed,
        ///// allowing exact floating-point comparisons.
        ///// </para><para>
        ///// The specified <paramref name="polygon"/> and the returned <paramref
        ///// name="intersection"/> are implicitly assumed to be closed, with an edge connecting the
        ///// first and last vertex. Therefore, all vertices should be different.
        ///// </para><para>
        ///// Unless the specified <paramref name="polygon"/> is convex, the returned <paramref
        ///// name="intersection"/> may represent multiple polygons, connected across the borders of
        ///// the <see cref="MyNavMesh.Geometry.RectD"/>.</para></remarks>

        //public bool Intersect(Vector3[] polygon, out Vector3[] intersection)
        //{
        //    // TODO: Intersect the box with the poligon
        //    throw new NotImplementedException();
            
        //    //if (polygon == null || polygon.Length == 0)
        //    //    throw new ArgumentNullOrEmptyException("polygon");

        //    //// input/output storage for intermediate polygons
        //    //int outputLength = polygon.Length;
        //    //Vector3[] inputVertices = new Vector3[3 * outputLength];
        //    //Vector3[] outputVertices = new Vector3[3 * outputLength];
        //    //Array.Copy(polygon, outputVertices, outputLength);

        //    //double q = 0;
        //    //bool startInside = false, endInside = false;

        //    //// traverse all four rectangle borders
        //    //for (int border = 0; border < 6; border++)
        //    //{
        //    //    switch (border)
        //    //    {
        //    //        case 0: q = X; break;
        //    //        case 1: q = X + SizeX; break;
        //    //        case 2: q = Y; break;
        //    //        case 3: q = Y + SizeY; break;
        //    //        case 4: q = Z; break;
        //    //        case 5: q = Z + SizeZ; break;
        //    //    }

        //    //    // last output is new input for current border
        //    //    Vector3[] swap = inputVertices;
        //    //    inputVertices = outputVertices;
        //    //    outputVertices = swap;
        //    //    int inputLength = outputLength;
        //    //    outputLength = 0;

        //    //    // check all polygon edges against infinite border
        //    //    Vector3 start = inputVertices[inputLength - 1];
        //    //    for (int i = 0; i < inputLength; i++)
        //    //    {
        //    //        Vector3 end = inputVertices[i];

        //    //        switch (border)
        //    //        {
        //    //            case 0: startInside = (start.X >= q); endInside = (end.X >= q); break;
        //    //            case 1: startInside = (start.X <= q); endInside = (end.X <= q); break;
        //    //            case 2: startInside = (start.Y >= q); endInside = (end.Y >= q); break;
        //    //            case 3: startInside = (start.Y <= q); endInside = (end.Y <= q); break;
        //    //        }

        //    //        // store intersection point if border crossed
        //    //        if (startInside != endInside)
        //    //        {
        //    //            double x, y, dx = end.X - start.X, dy = end.Y - start.Y;
        //    //            if (border < 2)
        //    //            {
        //    //                x = q;
        //    //                y = (x == end.X ? end.Y : start.Y + (x - start.X) * dy / dx);
        //    //            }
        //    //            else
        //    //            {
        //    //                y = q;
        //    //                x = (y == end.Y ? end.X : start.X + (y - start.Y) * dx / dy);
        //    //            }
        //    //            outputVertices[outputLength++] = new Vector3(x, y, z);
        //    //        }

        //    //        // also store end point if inside rectangle
        //    //        if (endInside) outputVertices[outputLength++] = end;
        //    //        start = end;
        //    //    }

        //    //    if (outputLength == 0) break;
        //    //}

        //    //intersection = new Vector3[outputLength];
        //    //Array.Copy(outputVertices, intersection, outputLength);

        //    //return (outputLength > 0);
        //}

        //#endregion
        //#region Intersect(RectD)

        ///// <summary>
        ///// Intersects the <see cref="MyNavMesh.Geometry.RectD"/> with the specified rectangle.</summary>
        ///// <param name="rect">
        ///// The <see cref="MyNavMesh.Geometry.RectD"/> to intersect with this instance.</param>
        ///// <param name="intersection">
        ///// On success, the intersection of the <see cref="MyNavMesh.Geometry.RectD"/> and the specified <paramref
        ///// name="rect"/>; otherwise, <see cref="Empty"/>.</param>
        ///// <returns>
        ///// <c>true</c> if the <see cref="MyNavMesh.Geometry.RectD"/> intersects with the specified <paramref
        ///// name="rect"/>; otherwise, <c>false</c>.</returns>

        //public bool Intersect(Box rect, out Box intersection)
        //{
        //    // TODO: Intersect the box with another box
        //    throw new NotImplementedException();
        //    //double x = Math.Max(X, rect.X);
        //    //double y = Math.Max(Y, rect.Y);
        //    //double width = Math.Min(X + SizeX, rect.X + rect.Width) - x;
        //    //double height = Math.Min(Y + SizeZ, rect.Y + rect.Height) - y;

        //    //if (height < 0 || width < 0)
        //    //{
        //    //    intersection = Empty;
        //    //    return false;
        //    //}
        //    //else
        //    //{
        //    //    intersection = new RectD(x, y, width, height);
        //    //    return true;
        //    //}
        //}

        //#endregion
        //#region IntersectsWith(Line3D)

        ///// <overloads>
        ///// Determines whether the <see cref="MyNavMesh.Geometry.RectD"/> intersects with the specified object.
        ///// </overloads>
        ///// <summary>
        ///// Determines whether the <see cref="MyNavMesh.Geometry.RectD"/> intersects with the specified <see
        ///// cref="LineD"/>.</summary>
        ///// <param name="line">
        ///// The <see cref="LineD"/> to examine.</param>
        ///// <returns>
        ///// <c>true</c> if the <see cref="MyNavMesh.Geometry.RectD"/> intersects with the specified <paramref
        ///// name="line"/>; otherwise, <c>false</c>.</returns>
        ///// <remarks>
        ///// <b>IntersectsWith</b> performs the same Liang-Barsky line clipping algorithm as <see
        ///// cref="Intersect(LineD, out LineD)"/>, but without computing the intersecting line
        ///// segment.</remarks>

        //public bool IntersectsWith(Line3D line)
        //{
        //    // TODO: check whether the box intersects with the line
        //    throw new NotImplementedException();
        //    //double x0 = line.Start.X, y0 = line.Start.Y;
        //    //double dx = line.End.X - x0, dy = line.End.Y - y0;
        //    //double t0 = 0, t1 = 1, p = 0, q = 0;

        //    //// traverse all four rectangle borders
        //    //for (int border = 0; border < 4; border++)
        //    //{
        //    //    switch (border)
        //    //    {
        //    //        case 0: p = -dx; q = x0 - X; break;
        //    //        case 1: p = +dx; q = X + SizeX - x0; break;
        //    //        case 2: p = -dy; q = y0 - Y; break;
        //    //        case 3: p = +dy; q = SizeZ + Y - y0; break;
        //    //    }

        //    //    if (p == 0)
        //    //    {
        //    //        // parallel line outside of rectangle
        //    //        if (q < 0) return false;
        //    //    }
        //    //    else
        //    //    {
        //    //        double r = q / p;
        //    //        if (p < 0)
        //    //        {
        //    //            if (r > t1) return false;
        //    //            else if (r > t0) t0 = r;
        //    //        }
        //    //        else
        //    //        {
        //    //            if (r < t0) return false;
        //    //            else if (r < t1) t1 = r;
        //    //        }
        //    //    }
        //    //}

        //    //return true;
        //}

        //#endregion
        //#region IntersectsWith(RectD)

        ///// <summary>
        ///// Determines whether the <see cref="MyNavMesh.Geometry.RectD"/> intersects with the specified rectangle.
        ///// </summary>
        ///// <param name="rect">
        ///// The <see cref="MyNavMesh.Geometry.RectD"/> to examine.</param>
        ///// <returns>
        ///// <c>true</c> if the <see cref="MyNavMesh.Geometry.RectD"/> intersects with the specified <paramref
        ///// name="rect"/>; otherwise, <c>false</c>.</returns>
        ///// <remarks>
        ///// <b>IntersectsWith</b> returns <c>true</c> even if both <see cref="MyNavMesh.Geometry.RectD"/> instances
        ///// have a <see cref="SizeX"/> or <see cref="SizeZ"/> of zero, provided they have the same
        ///// <see cref="Location"/> in the corresponding dimension.</remarks>

        //public bool IntersectsWith(Box rect)
        //{
        //    // TODO: check whether the box intersects with another box
        //    throw new NotImplementedException();
        //    //return (rect.X + rect.Width >= X && rect.X <= X + SizeX
        //    //    && rect.Y + rect.Height >= Y && rect.Y <= Y + SizeZ);
        //}

        //#endregion
        //#region Locate(PointD)

        ///// <overloads>
        ///// Determines the location of the specified <see cref="PointD"/> coordinates relative to
        ///// the <see cref="MyNavMesh.Geometry.RectD"/>.</overloads>
        ///// <summary>
        ///// Determines the location of the specified <see cref="PointD"/> coordinates relative to
        ///// the <see cref="MyNavMesh.Geometry.RectD"/>, using exact coordinate comparisons.</summary>
        ///// <param name="q">
        ///// The <see cref="PointD"/> coordinates to examine.</param>
        ///// <returns>
        ///// A <see cref="RectLocation"/> value indicating the location of <paramref name="q"/>
        ///// relative to the <see cref="MyNavMesh.Geometry.RectD"/>.</returns>
        ///// <remarks>
        ///// <b>Locate</b> never returns <see cref="RectLocation.None"/>, and always returns a
        ///// bitwise combination of an <b>…X</b> and a <b>…Y</b> value.</remarks>

        //public RectLocation Locate(Vector3 q)
        //{
        //    double qx = q.X - X, qy = q.Y - Y;

        //    RectLocation x = (qx < 0 ? RectLocation.BeforeX :
        //        (qx == 0 ? RectLocation.StartX :
        //        (qx < SizeX ? RectLocation.InsideX :
        //        (qx == SizeX ? RectLocation.EndX : RectLocation.AfterX))));

        //    RectLocation y = (qy < 0 ? RectLocation.BeforeY :
        //        (qy == 0 ? RectLocation.StartY :
        //        (qy < SizeZ ? RectLocation.InsideY :
        //        (qy == SizeZ ? RectLocation.EndY : RectLocation.AfterY))));

        //    return x | y;
        //}

        //#endregion
        //#region Locate(PointD, Double)

        ///// <summary>
        ///// Determines the location of the specified <see cref="PointD"/> coordinates relative to
        ///// the <see cref="MyNavMesh.Geometry.RectD"/>, given the specified epsilon for coordinate comparisons.
        ///// </summary>
        ///// <param name="q">
        ///// The <see cref="PointD"/> coordinates to examine.</param>
        ///// <param name="epsilon">
        ///// The maximum absolute distance at which coordinates should be considered equal.</param>
        ///// <returns>
        ///// A <see cref="RectLocation"/> value indicating the location of <paramref name="q"/>
        ///// relative to the <see cref="MyNavMesh.Geometry.RectD"/>.</returns>
        ///// <remarks><para>
        ///// <b>Locate</b> is identical with the basic <see cref="Locate(PointD)"/> overload but uses
        ///// the specified <paramref name="epsilon"/> to compare individual coordinates.
        ///// </para><para>
        ///// The specified <paramref name="epsilon"/> must be greater than zero, but <b>Locate</b>
        ///// does not check this condition.</para></remarks>

        //public RectLocation Locate(Vector3 q, double epsilon)
        //{
        //    double qx = q.X - X, qy = q.Y - Y;

        //    RectLocation x = (Math.Abs(qx) <= epsilon ? RectLocation.StartX :
        //        (Math.Abs(qx - SizeX) <= epsilon ? RectLocation.EndX :
        //        (qx < 0 ? x = RectLocation.BeforeX :
        //        (qx < SizeX ? x = RectLocation.InsideX : RectLocation.AfterX))));

        //    RectLocation y = (Math.Abs(qy) <= epsilon ? RectLocation.StartY :
        //        (Math.Abs(qy - SizeZ) <= epsilon ? RectLocation.EndY :
        //        (qy < 0 ? y = RectLocation.BeforeY :
        //        (qy < SizeZ ? y = RectLocation.InsideY : RectLocation.AfterY))));

        //    return x | y;
        //}

        //#endregion
        //#region Offset(Double, Double)

        ///// <overloads>
        ///// Moves the <see cref="MyNavMesh.Geometry.RectD"/> by the specified offset.</overloads>
        ///// <summary>
        ///// Moves the <see cref="MyNavMesh.Geometry.RectD"/> by the specified <see cref="Double"/> values.</summary>
        ///// <param name="x">
        ///// The horizontal offset applied to the <see cref="MyNavMesh.Geometry.RectD"/>.</param>
        ///// <param name="y">
        ///// The vertical offset applied to the <see cref="MyNavMesh.Geometry.RectD"/>.</param>
        ///// <returns>
        ///// A new <see cref="MyNavMesh.Geometry.RectD"/> with the same <see cref="Size"/> as this instance, and whose
        ///// <see cref="X"/> and <see cref="Y"/> coordinates are offset by the specified <paramref
        ///// name="x"/> and <paramref name="y"/> values.</returns>

        //public Box Offset(double x, double y, double z)
        //{
        //    return new Box(X + x, Y + y, Z + z, SizeX, SizeY, SizeZ);
        //}

        //#endregion
        //#region Offset(PointD)

        ///// <summary>
        ///// Moves the <see cref="MyNavMesh.Geometry.RectD"/> by the specified <see cref="PointD"/> vector.</summary>
        ///// <param name="vector">
        ///// A <see cref="PointD"/> value whose components define the horizontal and vertical offset
        ///// applied to the <see cref="MyNavMesh.Geometry.RectD"/>.</param>
        ///// <returns>
        ///// A new <see cref="MyNavMesh.Geometry.RectD"/> with the same <see cref="Size"/> as this instance, and whose
        ///// <see cref="Location"/> is offset by the specified <paramref name="vector"/>.</returns>

        //public Box Offset(Vector3 vector)
        //{
        //    return new Box(Location + vector, Size);
        //}

        //#endregion
        //#region Round

        ///// <summary>
        ///// Converts the <see cref="MyNavMesh.Geometry.RectD"/> to a <see cref="RectI"/> by rounding coordinates and
        ///// dimensions to the nearest <see cref="Int32"/> values.</summary>
        ///// <returns>
        ///// A <see cref="RectI"/> instance whose <see cref="RectI.Location"/> and <see
        ///// cref="RectI.Size"/> properties equal the corresponding properties of the <see
        ///// cref="MyNavMesh.Geometry.RectD"/>, rounded to the nearest <see cref="Int32"/> values.</returns>
        ///// <remarks>
        ///// The <see cref="Double"/> components of the <see cref="MyNavMesh.Geometry.RectD"/> are converted to <see
        ///// cref="Int32"/> components using <see cref="Fortran.NInt"/> rounding.</remarks>

        //public Box Round()
        //{
        //    return new Box(Math.Round(X), Math.Round(Y), Math.Round(Z),
        //        Math.Round(SizeX), Math.Round(SizeY), Math.Round(SizeZ));
        //}

        //#endregion
        //#region ToString

        ///// <summary>
        ///// Returns a <see cref="String"/> that represents the <see cref="MyNavMesh.Geometry.RectD"/>.</summary>
        ///// <returns>
        ///// A <see cref="String"/> containing the values of the <see cref="X"/>, <see cref="Y"/>,
        ///// <see cref="SizeX"/>, and <see cref="SizeZ"/> properties.</returns>

        //public override string ToString()
        //{
        //    return String.Format(CultureInfo.InvariantCulture,
        //        "{{X={0}, Y={1}, Z={2} SizeX={3}, SizeY={4}, SizeY={5}}}", X, Y, Z, SizeX, SizeY, SizeZ);
        //}

        //#endregion
        //#region Union

        ///// <summary>
        ///// Determines the union of the <see cref="MyNavMesh.Geometry.RectD"/> and the specified rectangle.</summary>
        ///// <param name="rect">
        ///// The <see cref="MyNavMesh.Geometry.RectD"/> to combine with this instance.</param>
        ///// <returns>
        ///// A <see cref="MyNavMesh.Geometry.RectD"/> that contains the union of the specified <paramref name="rect"/>
        ///// and this instance.</returns>

        //public Box Union(Box rect)
        //{

        //    double x = Math.Min(X, rect.X);
        //    double y = Math.Min(Y, rect.Y);
        //    double z = Math.Min(Z, rect.Z);
        //    double sizeX = Math.Max(X + SizeX, rect.X + rect.SizeX) - x;
        //    double sizeY = Math.Max(Y + SizeY, rect.Y + rect.SizeY) - y;
        //    double sizeZ = Math.Max(Y + SizeZ, rect.Y + rect.SizeZ) - y;

        //    return new Box(x, y, z, sizeX, sizeY, sizeZ);
        //}

        //#endregion
        #endregion
        #region Public Operators
        #region operator==

        /// <summary>
        /// Determines whether two <see cref="MyNavMesh.Geometry.RectD"/> instances have the same value.</summary>
        /// <param name="a">
        /// The first <see cref="MyNavMesh.Geometry.RectD"/> to compare.</param>
        /// <param name="b">
        /// The second <see cref="MyNavMesh.Geometry.RectD"/> to compare.</param>
        /// <returns>
        /// <c>true</c> if the value of <paramref name="a"/> is the same as the value of <paramref
        /// name="b"/>; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// This operator invokes the <see cref="Equals(MyNavMesh.Geometry.RectD)"/> method to test the two <see
        /// cref="MyNavMesh.Geometry.RectD"/> instances for value equality.</remarks>

        public static bool operator ==(Box a, Box b)
        {
            return a.Equals(b);
        }

        #endregion
        #region operator!=

        /// <summary>
        /// Determines whether two <see cref="MyNavMesh.Geometry.RectD"/> instances have different values.</summary>
        /// <param name="a">
        /// The first <see cref="MyNavMesh.Geometry.RectD"/> to compare.</param>
        /// <param name="b">
        /// The second <see cref="MyNavMesh.Geometry.RectD"/> to compare.</param>
        /// <returns>
        /// <c>true</c> if the value of <paramref name="a"/> is different from the value of
        /// <paramref name="b"/>; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// This operator invokes the <see cref="Equals(MyNavMesh.Geometry.RectD)"/> method to test the two <see
        /// cref="MyNavMesh.Geometry.RectD"/> instances for value inequality.</remarks>

        public static bool operator !=(Box a, Box b)
        {
            return !a.Equals(b);
        }

        #endregion

        #endregion
        #region IEquatable Members
        #region Equals(Object)

        /// <overloads>
        /// Determines whether two <see cref="MyNavMesh.Geometry.RectD"/> instances have the same value.</overloads>
        /// <summary>
        /// Determines whether this <see cref="MyNavMesh.Geometry.RectD"/> instance and a specified object, which must
        /// be a <see cref="MyNavMesh.Geometry.RectD"/>, have the same value.</summary>
        /// <param name="obj">
        /// An <see cref="Object"/> to compare to this <see cref="MyNavMesh.Geometry.RectD"/> instance.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="obj"/> is another <see cref="MyNavMesh.Geometry.RectD"/> instance and its
        /// value is the same as this instance; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// If the specified <paramref name="obj"/> is another <see cref="MyNavMesh.Geometry.RectD"/> instance,
        /// <b>Equals</b> invokes the strongly-typed <see cref="Equals(MyNavMesh.Geometry.RectD)"/> overload to test
        /// the two instances for value equality.</remarks>

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Box))
                return false;

            return Equals((Box)obj);
        }

        #endregion
        #region Equals(RectD)

        /// <summary>
        /// Determines whether this instance and a specified <see cref="MyNavMesh.Geometry.RectD"/> have the same
        /// value.</summary>
        /// <param name="box">
        /// A <see cref="MyNavMesh.Geometry.RectD"/> to compare to this instance.</param>
        /// <returns>
        /// <c>true</c> if the value of <paramref name="box"/> is the same as this instance;
        /// otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// <b>Equals</b> compares the values of the <see cref="X"/>, <see cref="Y"/>, <see
        /// cref="SizeX"/>, and <see cref="SizeZ"/> properties of the two <see cref="MyNavMesh.Geometry.RectD"/>
        /// instances to test for value equality.</remarks>

        public bool Equals(Box box)
        {
            return (X == box.X && Y == box.Y && Z == box.Z
                && SizeX == box.SizeX && SizeY == box.SizeY && SizeZ == box.SizeZ);
        }

        #endregion
        #region Equals(RectD, RectD)

        /// <summary>
        /// Determines whether two specified <see cref="MyNavMesh.Geometry.RectD"/> instances have the same value.
        /// </summary>
        /// <param name="a">
        /// The first <see cref="MyNavMesh.Geometry.RectD"/> to compare.</param>
        /// <param name="b">
        /// The second <see cref="MyNavMesh.Geometry.RectD"/> to compare.</param>
        /// <returns>
        /// <c>true</c> if the value of <paramref name="a"/> is the same as the value of <paramref
        /// name="b"/>; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// <b>Equals</b> invokes the non-static <see cref="Equals(MyNavMesh.Geometry.RectD)"/> overload to test the
        /// two <see cref="MyNavMesh.Geometry.RectD"/> instances for value equality.</remarks>

        public static bool Equals(Box a, Box b)
        {
            return a.Equals(b);
        }

        #endregion
        #region Equals(RectD, RectD, Double)

        /// <summary>
        /// Determines whether two specified <see cref="MyNavMesh.Geometry.RectD"/> instances have the same value,
        /// given the specified epsilon.</summary>
        /// <param name="a">
        /// The first <see cref="MyNavMesh.Geometry.RectD"/> to compare.</param>
        /// <param name="b">
        /// The second <see cref="MyNavMesh.Geometry.RectD"/> to compare.</param>
        /// <param name="epsilon">
        /// The maximum absolute difference at which the coordinates and dimensions of <paramref
        /// name="a"/> and <paramref name="b"/> should be considered equal.</param>
        /// <returns>
        /// <c>true</c> if the absolute difference between the coordinates and dimensions of
        /// <paramref name="a"/> and <paramref name="b"/> is less than or equal to <paramref
        /// name="epsilon"/>; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// The specified <paramref name="epsilon"/> must be greater than zero, but <b>Equals</b>
        /// does not check this condition.</remarks>

        public static bool Equals(Box a, Box b, double epsilon)
        {

            return (Math.Abs(a.X - b.X) <= epsilon
                && Math.Abs(a.Y - b.Y) <= epsilon
                && Math.Abs(a.Z - b.Z) <= epsilon
                && Math.Abs(a.SizeX - b.SizeX) <= epsilon
                && Math.Abs(a.SizeY - b.SizeY) <= epsilon
                && Math.Abs(a.SizeZ - b.SizeZ) <= epsilon);

        }

        #endregion
        #endregion
    }
}
