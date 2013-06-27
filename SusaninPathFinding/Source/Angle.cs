using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SusaninPathFinding
{
    ///<summary>
    /// Angle structure represents angle in radians 
    ///</summary>
    [Serializable]
    public struct Angle : IComparable, IFormattable, IConvertible
    {
        /// <summary>
        /// Angle in radians
        /// </summary>
        private double _angle;

        /// <summary>
        /// Угол в целых градусах/минутах/секундах
        /// </summary>
        public Angle(int sign, int degrees, int minutes, double seconds)
        {
            _angle = ConvertToRadians(sign, degrees, minutes, seconds);
        }

        /// <summary>
        /// Угол в радианах
        /// </summary>
        public Angle(double angleInRadians)
        {
            _angle = angleInRadians;
        }

        /// <summary>
        /// Angle in radians the same as Value
        /// </summary>
        public double InRadian
        {
            get { return _angle; }
            set { _angle = value; }
        }

        ///<summary>
        /// Returns angle in DU units
        ///</summary>
        public double InDu
        {
            get
            {
                return 3000 * InDegrees / 180;
            }
        }

        /// <summary>
        /// Angle in radians
        /// </summary>
        public double Value
        {
            get { return _angle; }
            set { _angle = value; }
        }

        /// <summary>
        /// Angle in degrees
        /// </summary>
        public double InDegrees
        {
            get
            {
                return (_angle / Math.PI) * 180;
            }
            set
            {
                _angle = (value / 180) * Math.PI;
            }
        }

        /// <summary>
        /// Agle in angular minutes
        /// </summary>
        public double InMinutes
        {
            get
            {
                return InDegrees * 60;
            }
            set
            {
                InDegrees = value / 60;
            }
        }

        /// <summary>
        /// Angle in angular seconds
        /// </summary>
        public double InSeconds
        {
            get
            {
                return InDegrees * 3600;
            }
            set
            {
                InDegrees = value / 3600;
            }
        }

        /// <summary>
        /// Знак
        /// </summary>
        public int Sign
        {
            get
            {
                return (_angle < 0) ? -1 : 1;
            }
        }

        /// <summary>
        /// Целое число градусов
        /// </summary>
        public int Degrees
        {
            get
            {
                return (int)Math.Floor(Math.Abs(InDegrees));
            }
        }

        /// <summary>
        /// Целое число минут
        /// </summary>
        public int Minutes
        {
            get
            {
                return (int)Math.Floor((Math.Abs(InDegrees) - Degrees) * 60);
            }
        }

        /// <summary>
        /// число секунд
        /// </summary>
        public double Seconds
        {
            get
            {
                return (((Math.Abs(InDegrees) - Degrees) * 60 - Minutes) * 60);
            }
        }

        /// <summary>
        /// Устанавливает угол
        /// </summary>
        /// <param name="sign">Знак, "-" если значение меньше 0</param>
        /// <param name="degrees">Целая часть градусов</param>
        /// <param name="minutes">Целая часть минут</param>
        /// <param name="seconds">Целая часть секунд</param>
        public void SetByElements(int sign, int degrees, int minutes, double seconds)
        {
            _angle = ConvertToRadians(sign, degrees, minutes, seconds);
        }

        /// <summary>
        /// Приводит угол в предел 2 пи, если он больше (или -2Pi если меньше)
        /// </summary>
        public void Trim()
        {
            while (2 * Math.PI < Math.Abs(_angle))
            {
                _angle = _angle - Sign * 2 * Math.PI;
            }
        }

        /// <summary>
        /// Приводит угол в предел 1 пи, если он больше 1 пи с обратным знаком
        /// т.е. угол 270 градусов станет -90
        /// 
        /// </summary>
        public void CutInPI()
        {
            Trim();
            if (Math.Abs(_angle) > Math.PI) _angle = -(_angle - Sign * Math.PI);
        }

        #region Static Functions

        /// <summary>
        /// Returns Angle object from value that represents an angle given in degrees
        /// </summary>
        public static Angle FromDegrees(double value)
        {
            return (value / 180) * Math.PI;
        }

        /// <summary>
        /// Returns Angle object from value that represents an angle given in angular minutes
        /// </summary>
        public static Angle FromMinutes(double value)
        {
            return FromDegrees(value / 60);
        }

        /// <summary>
        /// Returns Angle object from value that represents an angle given in angular seconds
        /// </summary>
        public static Angle FromSeconds(double value)
        {
            return FromDegrees(value / 3600);
        }

        /// <summary>
        /// Get an angle in radians from an angle in [(sign) X° Y' Z"] notation
        /// </summary>
        /// <param name="sign">if sign less than 0 the angle is considered to be negative</param>
        /// <param name="degrees">Integer degrees part</param>
        /// <param name="minutes">Integer angular minutes part</param>
        /// <param name="seconds">Integer angular seconds part</param>
        /// <returns>Angle in radians</returns>
        public static double ConvertToRadians(int sign, int degrees, int minutes, double seconds)
        {
            sign = (sign < 0) ? -1 : 1;
            //Угол
            double angle = minutes / 60d;
            angle += seconds / 3600d;
            angle += degrees;
            angle = (angle * sign / 180.0) * Math.PI;
            return angle;
        }

        /// <summary>
        /// Если значение угла меньше нуля, переводит его 
        /// в 360 - угол
        /// </summary>
        public static Angle ConvertTo2PI(Angle angle)
        {
            while ((2 * Math.PI) < Math.Abs(angle))
            {
                angle = angle - angle.Sign * 2 * Math.PI;
            }

            if (angle < 0) angle = 2 * Math.PI - angle;
            return angle;
        }


        /// <summary>
        /// Возвращает угол в радианах
        /// </summary>
        /// <param name="angleInDegrees">Угол в градусах</param>
        /// <returns>угол в радианах</returns>
        public static double ConvertToRadians(double angleInDegree)
        {
            return (angleInDegree / 180) * Math.PI;
        }

        #endregion //Static Functions

        #region Standard Functions
        /// <summary>
        /// Gets hash code of the class
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return _angle.GetHashCode();
        }

        /// <summary>
        /// Compares that this angle is equal to the other Angle or double or whatever
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return _angle.Equals(obj);
        }

        /// <summary>
        /// Angle string representation
        /// </summary>
        public override string ToString()
        {
            string result = string.Format("{0}°  {1}'  {2}\"",
                double.IsNaN(_angle) ? "  ---" : ((Sign < 0 ? "- " : "  ") + Degrees).PadLeft(5),
                double.IsNaN(_angle) ? "--" : Minutes.ToString("00").PadLeft(2),
                double.IsNaN(_angle) ? "--" : Seconds.ToString("00").PadLeft(2));
            return result;
        }
        #endregion // Standard Functions

        #region IConvertible Members

        TypeCode IConvertible.GetTypeCode()
        {
            return _angle.GetTypeCode();
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            return Convert.ToBoolean(_angle);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(_angle);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            return Convert.ToChar(_angle);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            return Convert.ToDateTime(_angle);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal(_angle);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            return Convert.ToDouble(_angle);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(_angle);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32(_angle);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(_angle);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(_angle);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle(_angle);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            return Convert.ToString(_angle);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            try
            {
                return Convert.ChangeType(_angle, conversionType, provider);
            }
            catch (Exception)
            {

                return null;
            }

        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(_angle);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(_angle);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(_angle);
        }

        #endregion

        #region IComparable Members
        public int CompareTo(object obj)
        {
            return _angle.CompareTo((double)obj);
        }

        #endregion

        #region IFormattable Members

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return _angle.ToString(format, formatProvider);
        }

        #endregion

        #region Operators

        // Boolean operators.
        public static bool operator !=(Angle a, Angle b)
        { return a._angle != b._angle; }

        public static bool operator ==(Angle a, Angle b)
        { return a._angle == b._angle; }

        public static bool operator >(Angle a, Angle b)
        { return a._angle > b._angle; }

        public static bool operator >=(Angle a, Angle b)
        { return a._angle >= b._angle; }

        public static bool operator <(Angle a, Angle b)
        { return a._angle < b._angle; }

        public static bool operator <=(Angle a, Angle b)
        { return a._angle <= b._angle; }

        // Conversion operators.
        public static implicit operator double(Angle a)
        {
            return a._angle;
        }
        public static implicit operator Angle(double a)
        {
            return new Angle(a);
        }

        // Basic unary operators.
        public static Angle operator +(Angle a)
        {
            return a;
        }
        public static Angle operator -(Angle a)
        {
            return new Angle(-a._angle);
        }

        // Basic binary operators for addition, subtraction, multiplication, and division.
        public static Angle operator +(Angle a, Angle b)
        {
            return new Angle(a._angle + b._angle);
        }
        public static Angle operator -(Angle a, Angle b)
        {
            return new Angle(a._angle - b._angle);
        }
        public static Angle operator *(Angle a, Angle b)
        {
            return new Angle(a._angle * b._angle);
        }
        public static Angle operator /(Angle a, Angle b)
        {
            return new Angle(a._angle / b._angle);
        }

        #endregion


    }
}
