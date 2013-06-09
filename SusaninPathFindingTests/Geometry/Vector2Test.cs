using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SusaninPathFinding.Geometry;
using SusaninPathFindingTests.TDDTests;

namespace SusaninPathFindingTests.Geometry
{
    [TestFixture]
    public class Vector2Test : TestOf<Vector2>
    {
        public override void Setup()
        {
            Tester = new Vector2();
        }

        public override void CleenUp()
        {
        }


        ///// <summary>
        ///// 
        ///// </summary>
        //public Vector2()
        //{
        //    X = Y = 0.0;
        //}

        ///// <summary>
        ///// Создает новый vector set to ( x, y )
        ///// </summary>
        //public Vector2(double x, double y)
        //{
        //    this.X = x;
        //    this.Y = y;
        //}

        ///// <summary>
        ///// Создает новый vector set to ( element[0], element[1], element[2] )
        ///// </summary>
        //public Vector2(double[] elements)
        //{
        //    this.X = elements[0];
        //    this.Y = elements[1];
        //}

        ///// <summary>
        ///// Создает новый vector set to the values of the given vector
        ///// </summary>
        //public Vector2(Vector2 vec)
        //{
        //    this.X = vec.X;
        //    this.Y = vec.Y;
        //}

        //public static bool operator ==(Vector2 v1, Vector2 v2)
        //{
        //    if (Object.ReferenceEquals(v1, v2))
        //        return true;

        //    if (Object.ReferenceEquals(v1, null))
        //        return false;

        //    if (Object.ReferenceEquals(v2, null))
        //        return false;

        //    return v1.X == v2.X && v1.Y == v2.Y;
        //}

        //public static bool operator !=(Vector2 v1, Vector2 v2)
        //{
        //    return !(v1 == v2);
        //}


        //public static implicit operator Vector2(PointF p)
        //{
        //    return new Vector2(p.X, p.Y);
        //}

        //public static implicit operator Vector2(Point p)
        //{
        //    return new Vector2(p.X, p.Y);
        //}

        //public static implicit operator PointF(Vector2 p)
        //{
        //    return new PointF((float)p.X, (float)p.Y);
        //}

        //public static implicit operator Point(Vector2 p)
        //{
        //    return new Point((int)p.X, (int)p.Y);
        //}


        //// ================================================================================

        ///// <summary>
        ///// Создает новый vector set to ( x, y )
        ///// </summary>
        //static public Vector2 FromXY(double x, double y)
        //{
        //    return new Vector2(x, y);
        //}

        //// ================================================================================

        ///// <summary>
        ///// Устанавливает координаты вектора в соответствии с указанными значениями
        ///// </summary>
        //public void Set(double x, double y)
        //{
        //    this.X = x;
        //    this.Y = y;
        //}

        ///// <summary>
        ///// Устанавливает координаты вектора в соответствии со значениями указанного вектора
        ///// </summary>
        //public void Set(Vector2 vec)
        //{
        //    this.X = vec.X;
        //    this.Y = vec.Y;
        //}

        //// ================================================================================

        ///// <summary>
        ///// this is a private implementation of an Interface
        ///// </summary>
        //object ICloneable.Clone()
        //{
        //    return new Vector2(this);
        //}

        ///// <summary>
        ///// Порождает копию данного вектора
        ///// </summary>
        //public Vector2 Clone()
        //{
        //    return new Vector2(this);
        //}

        ///// <summary>
        ///// this is a private implementation of an Interface
        ///// </summary>
        ///// <mParam name="obj"></mParam>
        ///// <returns></returns>
        //int IComparable.CompareTo(object obj)
        //{
        //    if (obj is Vector2)
        //    {
        //        Vector2 vec = (Vector2)obj;
        //        double magnitudeSelf = this.GetMagnitude();
        //        double magnitudeOther = vec.GetMagnitude();

        //        if (magnitudeSelf < magnitudeOther)
        //        {
        //            return -1;
        //        }
        //        else if (magnitudeSelf > magnitudeOther)
        //        {
        //            return 1;
        //        }
        //        return 0;
        //    }
        //    return 0;
        //}

        ///// <summary>
        ///// Сравнивает длину вектора с длиной указанного вектора 
        ///// magnitude of the supplied vector.  
        ///// </summary>
        ///// <mParam name="vec">Вектор для сравнения</mParam>
        ///// <returns>Less than 0: The magnitude of this instance is less than the magnitude of point.
        ///// Zero: The magnitude of this instance equals the magnitude of vector.
        ///// Greater than 0: The magnitude of this instance is greater than the magnitude of point.
        ///// </returns>
        //public int CompareTo(Vector2 vec)
        //{
        //    double magnitudeSelf = this.GetMagnitude();
        //    double magnitudeOther = vec.GetMagnitude();

        //    if (magnitudeSelf < magnitudeOther)
        //    {
        //        return -1;
        //    }
        //    else if (magnitudeSelf > magnitudeOther)
        //    {
        //        return 1;
        //    }
        //    return 0;
        //}

        //// ================================================================================

        //double m_X, m_Y;

        ///// <summary>
        ///// X-компонента вектора
        ///// </summary>
        //[NotifyParentProperty(true)]
        //[DefaultValue(0)]
        //public double X { get { return m_X; } set { m_X = value; } }

        ///// <summary>
        ///// Y-компонента вектора
        ///// </summary>
        //[NotifyParentProperty(true)]
        //[DefaultValue(0)]
        //public double Y { get { return m_Y; } set { m_Y = value; } }

        //#region public double this[int index]
        ///// <summary>
        ///// Индексатор [0] -> X, [1] -> Y.
        ///// </summary>
        //public double this[int index]
        //{
        //    get
        //    {
        //        if (index == 0)
        //        {
        //            return this.X;
        //        }
        //        return this.Y;
        //    }
        //    set
        //    {
        //        if (index == 0)
        //        {
        //            this.X = value;
        //        }
        //        else
        //        {
        //            this.Y = value;
        //        }
        //    }
        //}
        //#endregion

        //// ================================================================================

        ///// <summary>
        ///// Получаем длину вектора [i.e. Sqrt( X*X + Y*Y) ]
        ///// </summary>
        ///// <returns></returns>
        //public double GetMagnitude()
        //{
        //    return (double)Math.Sqrt(this.X * this.X + this.Y * this.Y);
        //}

        ///// <summary>
        ///// Получаем квадратичную длину вектора [i.e. ( X*X + Y*Y + Z*Z ) ]
        ///// </summary>
        ///// <returns></returns>
        //public double GetMagnitudeSquared()
        //{
        //    return this.X * this.X + this.Y * this.Y;
        //}

        //// ================================================================================

        ///// <summary>
        ///// Получаем вектор,всегда лежащий в первом октанте(все коордианты - положительны)
        ///// </summary>
        ///// <mParam name="v">Исходный вектор</mParam>
        ///// <returns></returns>
        //static public Vector2 Abs(Vector2 v)
        //{
        //    return new Vector2(Math.Abs(v.X), Math.Abs(v.Y));
        //}

        //// ================================================================================

        ///// <summary>
        ///// Получаем единичный вектор для данного вектора
        ///// </summary>
        ///// <returns></returns>
        //public Vector2 GetUnit()
        //{
        //    Vector2 vec = new Vector2(this);
        //    vec.Normalize();
        //    return vec;
        //}

        ///// <summary>
        ///// Нормализует вектор,т.е приводит его к единичной длине
        ///// </summary>
        //public void Normalize()
        //{
        //    double magnitude = GetMagnitude();
        //    if (magnitude == 0)
        //    {
        //        throw new DivideByZeroException("Can not normalize a vector when it's magnitude is zero.");
        //    }

        //    this.X /= magnitude;
        //    this.Y /= magnitude;
        //}

        //// ================================================================================

        ///// <summary>
        ///// Convert the point into the array 'new double[]{ X, Y, Z }'.  Note that this
        ///// function causes a new double[] array to be allocated with each call.  Thus it 
        ///// is somewhat inefficient.
        ///// </summary>
        ///// <mParam name="vec"></mParam>
        ///// <returns></returns>
        //static public explicit operator double[](Vector2 vec)
        //{
        //    double[] elements = new double[3];
        //    elements[0] = vec.X;
        //    elements[1] = vec.Y;
        //    return elements;
        //}

        //static public implicit operator Vector2(string inputString)
        //{
        //    return Vector2.Parse(inputString);
        //}

        //// ================================================================================

        //public const char Separator = ';';


        //public static Vector2 Parse(string text)
        //{
        //    if (String.IsNullOrEmpty(text))
        //        return null;

        //    string[] items = text.Split(Separator);
        //    Vector2 res = new Vector2();

        //    res.X = Double.Parse(items[0], NumberFormatInfo.InvariantInfo);
        //    res.Y = Double.Parse(items[1], NumberFormatInfo.InvariantInfo);

        //    return res;
        //}

        ///// <summary>
        ///// A human understandable view of the 2d point.
        ///// </summary>
        ///// <returns></returns>
        //public override string ToString()
        //{
        //    return String.Format("{0}{2}{1}",
        //        X.ToString(NumberFormatInfo.InvariantInfo),
        //        Y.ToString(NumberFormatInfo.InvariantInfo),
        //        Separator);
        //}

        //public string ToString(string format)
        //{
        //    return String.Format("{0}{2}{1}",
        //        X.ToString(format, NumberFormatInfo.InvariantInfo),
        //        Y.ToString(format, NumberFormatInfo.InvariantInfo),
        //        Separator);
        //}

        //// ================================================================================

        ///// <summary>
        ///// Is given object equal to current point?
        ///// </summary>
        ///// <mParam name="o"></mParam>
        ///// <returns></returns>
        //public override bool Equals(object o)
        //{
        //    if (o is Vector2)
        //    {
        //        Vector2 vec = (Vector2)o;
        //        return (this.X == vec.X) && (this.Y == vec.Y);
        //    }
        //    return false;
        //}

        ///// <summary>
        ///// Get the hashcode of the point
        ///// </summary>
        ///// <returns></returns>
        //public override int GetHashCode()
        //{
        //    return this.X.GetHashCode() ^ (this.Y.GetHashCode());
        //}

        //// ================================================================================

        ///// <summary>
        ///// Do nothing.
        ///// </summary>
        ///// <mParam name="vec"></mParam>
        ///// <returns></returns>
        //static public Vector2 operator +(Vector2 vec)
        //{
        //    return Vector2.FromXY(+vec.X, +vec.Y);
        //}

        ///// <summary>
        ///// Invert the direction of the point.  Result is ( -vec.X, -vec.Y, -vec.Z ).
        ///// </summary>
        ///// <mParam name="vec"></mParam>
        ///// <returns></returns>
        //static public Vector2 operator -(Vector2 vec)
        //{
        //    return Vector2.FromXY(-vec.X, -vec.Y);
        //}

        ///// <summary>
        ///// Multiply vector vec by f.  Result is ( vec.X*f, vec.Y*f, vec.Z*f ).
        ///// </summary>
        ///// <mParam name="scale"></mParam>
        ///// <mParam name="vec"></mParam>
        ///// <returns></returns>
        //static public Vector2 operator *(double scale, Vector2 vec)
        //{
        //    return new Vector2(vec.X * scale, vec.Y * scale);
        //}

        ///// <summary>
        ///// Multiply vector vec by f.  Result is ( vec.X*f, vec.Y*f, vec.Z*f ).
        ///// </summary>
        ///// <mParam name="vec"></mParam>
        ///// <mParam name="scale"></mParam>
        ///// <returns></returns>
        //static public Vector2 operator *(Vector2 vec, double scale)
        //{
        //    return new Vector2(vec.X * scale, vec.Y * scale);
        //}

        ///// <summary>
        ///// Divide vector vec by f.  Result is ( vec.X/f, vec.Y/f).
        ///// </summary>
        ///// <mParam name="vec"></mParam>
        ///// <mParam name="divisor"></mParam>
        ///// <returns></returns>
        //static public Vector2 operator /(Vector2 vec, double divisor)
        //{
        //    if (divisor == 0)
        //        throw new DivideByZeroException("can not divide a vector by zero");

        //    double inv = 1 / divisor;
        //    return new Vector2(vec.X * inv, vec.Y * inv);
        //}

        //static public Vector2 operator +(Vector2 a, Vector2 b)
        //{
        //    return new Vector2(a.X + b.X, a.Y + b.Y);
        //}

        ///// <summary>
        ///// Subtract two vectors.  Result is ( a.X - b.X, a.Y - b.Y, a.Z - b.Z )
        ///// </summary>
        ///// <mParam name="a"></mParam>
        ///// <mParam name="b"></mParam>
        ///// <returns></returns>
        //static public Vector2 operator -(Vector2 a, Vector2 b)
        //{
        //    return new Vector2(a.X - b.X, a.Y - b.Y);
        //}

        //// ================================================================================

        ///// <summary>
        ///// Инвертирует направление вектора
        ///// </summary>
        ///// <returns>The calling object</returns>
        //public Vector2 Invert()
        //{
        //    this.X = -this.X;
        //    this.Y = -this.Y;
        //    return this;
        //}

        ///// <summary>
        ///// Прибавляем вектор к текущему. Работает быстрее оператора сложения, т.к нового объекта не создается.
        ///// </summary>
        ///// <mParam name="vec"></mParam>
        ///// <returns>Themselve</returns>
        //public Vector2 Add(Vector2 vec)
        //{
        //    if (vec == null)
        //        throw new ArgumentNullException("ver");

        //    this.X += vec.X;
        //    this.Y += vec.Y;
        //    return this;
        //}

        ///// <summary>
        ///// Прибавляет произведение вектора и скаляра к вызывающему объекту.
        ///// Работает аналогично this + vec*multiply, но быстрее
        ///// </summary>
        ///// <mParam name="vec"></mParam>
        ///// <mParam name="multiply"></mParam>
        //public void Add(Vector2 vec, double multiply)
        //{
        //    X += vec.X * multiply;
        //    Y += vec.Y * multiply;
        //}

        ///// <summary>
        ///// Вычитаем вектор из текущего. Работает быстрее оператора сложения, т.к нового объекта не создается.
        ///// </summary>
        ///// <mParam name="vec"></mParam>
        //public void Subtract(Vector2 vec)
        //{
        //    X -= vec.X;
        //    Y -= vec.Y;
        //}

        ///// <summary>
        ///// Множим вектор на число. Работает быстрее оператора умножения, т.к нового объекта не создается.
        ///// </summary>
        ///// <mParam name="scale"></mParam>
        //public Vector2 Multiply(double scale)
        //{
        //    X *= scale;
        //    Y *= scale;
        //    return this;
        //}

        ///// <summary>
        ///// Делим вектор на число. Работает быстрее оператора деления, т.к нового объекта не создается.
        ///// </summary>
        ///// <mParam name="divisor"></mParam>
        //public Vector2 Divide(double divisor)
        //{
        //    if (divisor == 0)
        //    {
        //        throw new DivideByZeroException("can not divide a vector by zero");
        //    }
        //    double inv = 1 / divisor;
        //    this.X *= inv;
        //    this.Y *= inv;
        //    return this;
        //}

        //// ================================================================================

        ///// <summary>
        ///// Получаем вектор с наименьшей длиной
        ///// </summary>
        ///// <mParam name="a"></mParam>
        ///// <mParam name="b"></mParam>
        ///// <returns></returns>
        //static public Vector2 Min(Vector2 a, Vector2 b)
        //{
        //    return (a.GetMagnitude() >= b.GetMagnitude()) ? b : a;
        //}

        ///// <summary>
        ///// Получаем вектор с наибольшей длиной
        ///// </summary>
        ///// <mParam name="a"></mParam>
        ///// <mParam name="b"></mParam>
        ///// <returns></returns>
        //static public Vector2 Max(Vector2 a, Vector2 b)
        //{
        //    return (a.GetMagnitude() >= b.GetMagnitude()) ? a : b;
        //}

        ///// <summary>
        ///// Получаем вектор, состоящий из наименьших компенент векторов a и b
        ///// </summary>
        ///// <mParam name="a"></mParam>
        ///// <mParam name="b"></mParam>
        ///// <returns></returns>
        //static public Vector2 MinXY(Vector2 a, Vector2 b)
        //{
        //    return new Vector2(Math.Min(a.X, b.X), Math.Min(a.Y, b.Y));
        //}

        ///// <summary>
        ///// Получаем вектор, состоящий из наибольших компенент векторов a и b
        ///// </summary>
        ///// <mParam name="a"></mParam>
        ///// <mParam name="b"></mParam>
        ///// <returns></returns>
        //static public Vector2 MaxXY(Vector2 a, Vector2 b)
        //{
        //    return new Vector2(Math.Max(a.X, b.X), Math.Max(a.Y, b.Y));
        //}

        //// ================================================================================


        ///// <summary>
        ///// Вычисляет расстояние между двумя точками пространства в квадрате
        ///// </summary>
        ///// <mParam name="a">Первая точка</mParam>
        ///// <mParam name="b">Вторая точка</mParam>
        ///// <returns></returns>
        //static public double DistanceSquared(Vector2 a, Vector2 b)
        //{
        //    double dx = a.X - b.X;
        //    double dy = a.Y - b.Y;
        //    return (dx * dx + dy * dy);
        //}

        //// ================================================================================

        ///// <summary>
        ///// Вычисляет скалярное произведение двух векторов
        ///// </summary>
        ///// <mParam name="a"></mParam>
        ///// <mParam name="b"></mParam>
        ///// <returns></returns>
        //static public double Dot(Vector2 a, Vector2 b)
        //{
        //    return DotProduct(a, b);
        //}

        ///// <summary>
        ///// Вычисляет скалярное произведение двух векторов
        ///// </summary>
        ///// <mParam name="a"></mParam>
        ///// <mParam name="b"></mParam>
        ///// <returns></returns>
        //static public double DotProduct(Vector2 a, Vector2 b)
        //{
        //    return (a.X * b.X) + (a.Y * b.Y);
        //}

        ///// <summary>
        ///// Вычисляет векторное произведение двух векторов
        ///// </summary>
        ///// <mParam name="a"></mParam>
        ///// <mParam name="b"></mParam>
        ///// <returns></returns>
        //static public double Cross(Vector2 a, Vector2 b)
        //{
        //    return CrossProduct(a, b);
        //}

        ///// <summary>
        ///// Вычисляет векторное произведение двух векторов
        ///// </summary>
        ///// <mParam name="a">Первый вектор</mParam>
        ///// <mParam name="b">Второй вектор</mParam>
        ///// <returns>Результат векторного произведения</returns>
        //static public double CrossProduct(Vector2 a, Vector2 b)
        //{
        //    // make it easier to see what is happening
        //    double x0 = a.X, y0 = a.Y;
        //    double x1 = b.X, y1 = b.Y;

        //    return x0 * y1 - y0 * x1;
        //}

        //// ================================================================================

        ///// <summary>
        ///// Интерполяция между двумя точками
        ///// </summary>
        ///// <mParam name="a"></mParam>
        ///// <mParam name="b"></mParam>
        ///// <mParam name="f"></mParam>
        ///// <returns></returns>
        //static public Vector2 Interpolate(Vector2 a, Vector2 b, double f)
        //{
        //    double alpha = 1 - f;
        //    double beta = f;
        //    return Vector2.FromXY(
        //        a.X * alpha + b.X * beta,
        //        a.Y * alpha + b.Y * beta);
        //}

        //// ================================================================================

        ///// <summary>
        ///// Zero (0,0,0)
        ///// </summary>
        //static public readonly Vector2 Zero = new Vector2(0, 0);

        ///// <summary>
        ///// Origin (0,0,0)
        ///// </summary>
        //static public readonly Vector2 Origin = new Vector2(0, 0);

        ///// <summary>
        ///// X-axis unit vector (1,0,0)
        ///// </summary>
        //static public readonly Vector2 XAxis = new Vector2(1, 0);

        ///// <summary>
        ///// Y-axis unit vector (0,1,0)
        ///// </summary>
        //static public readonly Vector2 YAxis = new Vector2(0, 1);
        //// ================================================================================

        ///// <summary>
        ///// Вычисление координат пересечения двух отрезков
        ///// </summary>
        ///// <param name="start1"></param>
        ///// <param name="end1"></param>
        ///// <param name="start2"></param>
        ///// <param name="end2"></param>
        ///// <returns></returns>
        //public static Vector2 GetLineCross(Vector2 start1, Vector2 end1, Vector2 start2, Vector2 end2)
        //{
        //    Vector2 dir1, dir2;
        //    double a1, b1, d1;
        //    double a2, b2, d2;
        //    double seg1_line2_start, seg1_line2_end;
        //    double seg2_line1_start, seg2_line1_end;
        //    double u;

        //    dir1 = end1 - start1;
        //    dir2 = end2 - start2;

        //    //считаем уравнения прямых проходящих через отрезки
        //    a1 = -dir1.Y;
        //    b1 = +dir1.X;
        //    d1 = -(a1 * start1.X + b1 * start1.Y);

        //    a2 = -dir2.Y;
        //    b2 = +dir2.X;
        //    d2 = -(a2 * start2.X + b2 * start2.Y);

        //    //подставляем концы отрезков, для выяснения в каких полуплоскотях они
        //    seg1_line2_start = a2 * start1.X + b2 * start1.Y + d2;
        //    seg1_line2_end = a2 * end1.X + b2 * end1.Y + d2;

        //    seg2_line1_start = a1 * start2.X + b1 * start2.Y + d1;
        //    seg2_line1_end = a1 * end2.X + b1 * end2.Y + d1;

        //    //если концы одного отрезка имеют один знак, значит он в одной полуплоскости и пересечения нет.
        //    if ((seg1_line2_start * seg1_line2_end > 0) || (seg2_line1_start * seg2_line1_end > 0))
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        u = seg1_line2_start / (seg1_line2_start - seg1_line2_end);

        //        //Vector2 outPoint = dir1 * u + start1;
        //        Vector2 outPoint = new Vector2();
        //        outPoint.Add(dir1);
        //        outPoint.Multiply(u);
        //        outPoint.Add(start1);

        //        return outPoint;
        //    }
        //}

        //// ================================================================================


        //#region IEquatable<Vector2> Members

        //public bool Equals(Vector2 other)
        //{
        //    if (other == null)
        //        return false;
        //    return X == other.X && Y == other.Y;
        //}

        //#endregion

        //private delegate double CalcDelegate();

        //#region public Vector2 Multiply(float scaleX, float scaleY)
        ///// <summary>
        ///// Масштабирование вектора
        ///// </summary>
        ///// <param name="scaleX"></param>
        ///// <param name="scaleY"></param>
        ///// <returns></returns>
        //public Vector2 Multiply(double scaleX, double scaleY)
        //{
        //    this.X *= scaleX;
        //    this.Y *= scaleY;
        //    return this;
        //}
        //#endregion

        ///// <summary>
        ///// Get's the unit vector, that is orthogonal to vector, specified by <paramref name="p1"/> and <paramref name="p2"/>.
        ///// </summary>
        ///// <param name="p1">Point, specifies beginning of the line.</param>
        ///// <param name="p2">Point, specifies line end.</param>
        ///// <returns>
        ///// Orthogonal unit vector, rotated counter-clockwise from given line to 90 degrees.
        ///// </returns>
        //public static Vector2 GetOrtho(Vector2 p1, Vector2 p2)
        //{
        //    Vector2 line = p2 - p1;

        //    double x2, y2;

        //    if (line.Y != 0)
        //    {
        //        x2 = 1.0;
        //        y2 = -line.X / line.Y;
        //    }
        //    else if (line.X != 0)
        //    {
        //        y2 = 1;
        //        x2 = -line.Y / line.X;
        //    }
        //    else
        //        throw new ArgumentException("Line magnitude must be greater than zero.");

        //    Vector2 v = Vector2.FromXY(x2, y2).GetUnit();

        //    if (Vector2.Cross(line, v) < 0)
        //        v = v.Invert();

        //    return v;
        //}


        ///// <summary>
        ///// Checks two vectors for equality in given range.
        ///// </summary>
        ///// <param name="v1">The v1.</param>
        ///// <param name="v2">The v2.</param>
        ///// <param name="epsilon">The maximum difference between X or Y components of both vectors</param>
        ///// <returns>true, if object equals, otherwise - false.</returns>
        //public static bool AreEqual(Vector2 v1, Vector2 v2, double epsilon)
        //{
        //    return Math.Abs(v1.X - v2.X) <= epsilon
        //        && Math.Abs(v1.Y - v2.Y) <= epsilon;
        //}

        ///// <summary>
        ///// Ориентация вектора
        ///// </summary>
        ///// <param name="center">Точка, откуда исходит направление "взгляда".</param>
        ///// <param name="target">Точка, куда должен "смотреть" вектор.</param>
        ///// <returns></returns>
        //public Vector2 OrientTo(Vector2 center, Vector2 target)
        //{
        //    double origDist = Vector2.DistanceSquared(target, center);
        //    double posDist = Vector2.DistanceSquared(target, center + this);

        //    if (origDist > posDist)
        //        return this;
        //    else
        //        return this.Invert();
        //}

        ///// <summary>
        ///// Определение пересечения отрезка с лучем
        ///// </summary>
        ///// <param name="beamStart"></param>
        ///// <param name="beamEnd"></param>
        ///// <param name="lineStart"></param>
        ///// <param name="lineEnd"></param>
        ///// <returns></returns>
        //public static Vector2 GetCrossWithBeam(Vector2 beamStart, Vector2 beamUnitDirection, Vector2 lineStart, Vector2 lineEnd)
        //{
        //    double d1 = Vector2.DistanceSquared(beamStart, lineStart);
        //    double d2 = Vector2.DistanceSquared(beamStart, lineEnd);

        //    Vector2 beamUnit = (beamUnitDirection - beamStart).GetUnit();
        //    Vector2 beamInf = beamUnitDirection + beamUnit.Multiply(Math.Max(d1, d2));

        //    return Vector2.GetLineCross(beamStart, beamInf, lineStart, lineEnd);
        //}

        //public static Vector2 ProjectBtoA(Vector2 A, Vector2 B)
        //{
        //    return A * (Vector2.Dot(B, A) / A.GetMagnitudeSquared());
        //}
    }
}
