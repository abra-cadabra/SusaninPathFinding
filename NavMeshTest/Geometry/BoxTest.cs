using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNavMesh;
using MyNavMesh.Geometry;
using NUnit.Framework;
using FluentAssertions;
using TDDTests;


namespace NavMeshTest.Geometry
{
    [TestFixture]
    class BoxTest: TestOf<Box>
    {
        public override void InitializeSystemUnderTest()
        {

            Tester = new Box(5.2344, 2.432, 3.5634, 8.213, 8.2432, 10.32);
        }

        public override void Setup()
        {
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

            //return (x >= X && x <= X + SizeX
            //    && y >= Y && y <= Y + SizeY
            //    && z >= Z && z <= Z + SizeZ);
        }

        //#endregion
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

        //    return (rect.X >= X && rect.X + rect.SizeX <= X + SizeX
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
        //    //    double x0 = line.Start.X, y0 = line.Start.Y;
        //    //    double dx = line.End.X - x0, dy = line.End.Y - y0;
        //    //    double t0 = 0, t1 = 1, p = 0, q = 0;

        //    //    // traverse all four rectangle borders
        //    //    for (int border = 0; border < 4; border++)
        //    //    {
        //    //        switch (border)
        //    //        {
        //    //            case 0: p = -dx; q = x0 - X; break;
        //    //            case 1: p = +dx; q = X + SizeX - x0; break;
        //    //            case 2: p = -dy; q = y0 - Y; break;
        //    //            case 3: p = +dy; q = SizeZ + Y - y0; break;
        //    //        }

        //    //        if (p == 0)
        //    //        {
        //    //            // parallel line outside of rectangle
        //    //            if (q < 0) goto failure;
        //    //        }
        //    //        else
        //    //        {
        //    //            double r = q / p;
        //    //            if (p < 0)
        //    //            {
        //    //                if (r > t1) goto failure;
        //    //                if (r > t0) t0 = r;
        //    //            }
        //    //            else
        //    //            {
        //    //                if (r < t0) goto failure;
        //    //                if (r < t1) t1 = r;
        //    //            }
        //    //        }
        //    //    }

        //    //    intersection = new Line3D(
        //    //        x0 + t0 * dx, y0 + t0 * dy,
        //    //        x0 + t1 * dx, y0 + t1 * dy);
        //    //    return true;

        //    //failure:
        //    //    intersection = Line3D.Empty;
        //    //    return false;
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
    }
}
