using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra.Complex;
using SusaninPathFinding.Collections;
using SusaninPathFinding.Geometry;


namespace SusaninPathFinding.Graph
{
    /// <summary>
    /// Specifies the list of main directions, which will be later used to describe an offsets for a cell.
    /// </summary>
    public enum GridDirection
    {
        NA,
        North,
        NorthEast,
        East,
        SouthEast,
        South,
        SouthWest,
        West,
        NorthWest,

        NorthLower,
        NorthEastLower,
        EastLower,
        SouthEastLower,
        SouthLower,
        SouthWestLower,
        WestLower,
        NorthWestLower,
        Lower,

        NorthRaise,
        NorthEastRaise,
        EastRaise,
        SouthEastRaise,
        SouthRaise,
        SouthWestRaise,
        WestRaise,
        NorthWestRaise,
        Raise
    }

    /// <summary>
    /// Extentions for GridDirection and <see cref="VectorD3"/>, which provides methods to get the 
    /// grid offsets for particular GridDirection, or GridDirection, which coresponds the particular offsets.
    /// It also 
    /// </summary>
    public static class GridDirectionExtentions
    {
        #region Fields

        /// <summary>
        /// List of cell offsets.
        /// </summary>
        private static Vector3[] _offsets;

        /// <summary>
        /// List of directions, indexed by offsets
        /// </summary>
        private static ArrayEx<GridDirection> _directions;

        /// <summary>
        /// The currently selected direction
        /// </summary>
        //private Direction _value;

        #endregion

        #region Properties

        //public Rotator Rotation { get; set; }

        //public CompassDirection Value
        //{
        //    get { return Rotate(Rotation.Pitch, Rotation.Yaw, Rotation.Roll); }
        //    set
        //    {
        //        Vector3 v = new Vector3(Offsets[(int) value]);
        //        v.Normalize();


        //        Rotation.Pitch = (int)v.Angle(new Vector3(1, 0, 0));
        //        //Rotation.Yaw = (int)new Angle(-v.Y).InDegrees;
        //        //Rotation.Roll = (int)new Angle(v.Z).InDegrees;
        //    }
        //}

        public static Vector3[] Offsets
        {
            get
            {
                InitOffsetsIfNeeded();
                return _offsets;
            }
        }

        #endregion
        
        #region Constructors

        //public GridDirection(CompassDirection dir)
        //{
        //    Rotation = new Rotator();
        //    Value = dir;
        //}

        //public GridDirection(int pitch, int yaw, int roll)
        //{
        //    Rotation = new Rotator(pitch, yaw, roll);
        //    Value = Rotate(pitch, yaw, roll);
        //}

        #endregion Constructors

        #region Static functions

        private static void InitOffsetsIfNeeded()
        {
            if (_offsets == null)
            {
                _offsets = new Vector3[]
                    {
                        new Vector3( 0,  0,  0),  // N/A
                        new Vector3( 0,  -1,  0),  // North
                        new Vector3( 1,  -1,  0),  // North-East
                        new Vector3( 1,  0,  0),  // East
                        new Vector3( 1,  1,  0),  // South-East
                        new Vector3( 0,  1,  0),  // South
                        new Vector3(-1,  1,  0),  // South-West
                        new Vector3(-1,  0,  0),  // West
                        new Vector3(-1, -1,  0),  // North-West

                        new Vector3( 0, -1, -1),  // North-Lower
                        new Vector3( 1, -1, -1),  // North-East-Lower
                        new Vector3( 1,  0, -1),  // East-Lower
                        new Vector3( 1,  1, -1),  // South-East-Lower
                        new Vector3( 0,  1, -1),  // South-Lower
                        new Vector3(-1,  1, -1),  // South-West-Lower
                        new Vector3(-1,  0, -1),  // West-Lower
                        new Vector3(-1, -1, -1),  // North-West-Lower
                        new Vector3( 0,  0, -1),  // Lower

                        new Vector3( 0, -1,  1),  // North-Raise
                        new Vector3( 1, -1,  1),  // North-East-Raise
                        new Vector3( 1,  0,  1),  // East-Raise
                        new Vector3( 1,  1,  1),  // South-East-Raise
                        new Vector3( 0,  1,  1),  // South-Raise
                        new Vector3(-1,  1,  1),  // South-West-Raise
                        new Vector3(-1,  0,  1),  // West-Raise
                        new Vector3(-1, -1,  1),  // North-West-Raise
                        new Vector3( 0,  0,  1)   // Raise
                    };
            }
        }

        private static void InitDirectionsIfNeeded()
        {
            if (_directions == null)
            {
                _directions = new ArrayEx<GridDirection>(new int[] { 3, 3, 3 });

                _directions[1, 1, 1] = GridDirection.NA;

                _directions[1, 0, 1] = GridDirection.North;  // North[0, -1,  0]
                _directions[2, 0, 1] = GridDirection.NorthEast;  // North-East[1, -1,  0]
                _directions[2, 1, 1] = GridDirection.East;  // East[1,  0,  0]
                _directions[2, 2, 1] = GridDirection.SouthEast;  // South-East[1,  1,  0]
                _directions[1, 2, 1] = GridDirection.South;  // South[0,  1,  0]
                _directions[0, 2, 1] = GridDirection.SouthWest;  // South-West[-1, 1,  0]
                _directions[0, 1, 1] = GridDirection.West;  // West[-1, 0,  0]
                _directions[0, 0, 1] = GridDirection.NorthWest;  // North-West[-1,-1,  0]

                _directions[1, 0, 0] = GridDirection.NorthLower;  // North-Lower[0, -1, -1]
                _directions[2, 0, 0] = GridDirection.NorthEastLower;  // North-East-Lower[1, -1, -1]
                _directions[2, 1, 0] = GridDirection.EastLower; // East-Lower[1,  0, -1]
                _directions[2, 2, 0] = GridDirection.SouthEastLower;  // South-East-Lower[ 1,  1, -1]
                _directions[1, 2, 0] = GridDirection.SouthLower;  // South-Lower[0,  1, -1]
                _directions[0, 2, 0] = GridDirection.SouthWestLower;  // South-West-Lower[-1, 1, -1]
                _directions[0, 1, 0] = GridDirection.WestLower; // West-Lower[-1, 0, -1]
                _directions[0, 0, 0] = GridDirection.NorthWestLower; // North-West-Lower[-1,-1, -1]
                _directions[1, 1, 0] = GridDirection.Lower;  // Lower[0,  0, -1]

                _directions[1, 0, 2] = GridDirection.NorthRaise;  // North-Raise[0, -1,  1]
                _directions[2, 0, 2] = GridDirection.NorthEastRaise;  // North-East-Raise[1, -1,  1]
                _directions[2, 1, 2] = GridDirection.EastRaise;  // East-Raise[1,  0,  1]
                _directions[2, 2, 2] = GridDirection.SouthEastRaise;  // South-East-Raise[1,  1,  1]
                _directions[1, 2, 2] = GridDirection.SouthRaise;  // South-Raise[0,  1,  1]
                _directions[0, 2, 2] = GridDirection.SouthWestRaise; // South-West-Raise[-1, 1,  1]
                _directions[0, 1, 2] = GridDirection.WestRaise;  // West-Raise[-1,  0,  1]
                _directions[0, 0, 2] = GridDirection.NorthWestRaise;  // North-West-Raise[-1, -1,  1]
                _directions[1, 1, 2] = GridDirection.Raise;  // Raise[ 0,  0,  1]
            }
        }

        public static GridDirection GetDirection(int x, int y, int z)
        {
            if (x > 1 && x < -1)
            {
                string message = String.Format(CultureInfo.CurrentCulture, Strings.ArgumentLessOrGreater, -1, 1);
                throw new ArgumentOutOfRangeException("x", x, message);
            }

            if (y > 1 && y < -1)
            {
                string message = String.Format(CultureInfo.CurrentCulture, Strings.ArgumentLessOrGreater, -1, 1);
                throw new ArgumentOutOfRangeException("y", y, message);
            }

            if (z > 1 && z < -1)
            {
                string message = String.Format(CultureInfo.CurrentCulture, Strings.ArgumentLessOrGreater, -1, 1);
                throw new ArgumentOutOfRangeException("z", z, message);
            }

            InitDirectionsIfNeeded();
            return _directions[x+1, y+1, z+1];
        }

        public static GridDirection GetDirectionFromRotation(int pitch, int yaw, int roll)
        {
            Vector3 v = new Vector3(1, 0, 0);

            v.Pitch(new Angle(1, pitch, 0, 0));
            v.Yaw(new Angle(1, yaw, 0, 0));
            v.Roll(new Angle(1, roll, 0, 0));

            v.X = Math.Round(v.X, 5);
            v.Y = Math.Round(v.Y, 5);
            v.Z = Math.Round(v.Z, 5);

            double val = Math.Sqrt(2 - Math.Sqrt(2))/2;

            if (v.X <= 1 && v.X >= val) v.X = 1;
            else
            if (v.X <= val && v.X >= 0) v.X = 0;
            else
            if (v.X >= -1 && v.X <= -val) v.X = -1;
            else
            if (v.X >= -val && v.X <= 0) v.X = 0;

            if (v.Y <= 1 && v.Y >= val) v.Y = -1;
            else
            if (v.Y <= val && v.Y >= 0) v.Y = 0;
            else
            if (v.Y >= -1 && v.Y <= -val) v.Y = 1;
            else
            if (v.Y >= -val && v.Y <= 0) v.Y = 0;

            if (v.Z <= 1 && v.Z >= val) v.Z = 1;
            else
            if (v.Z <= val && v.Z >= 0) v.Z = 0;
            else
            if (v.Z >= -1 && v.Z <= -val) v.Z = -1;
            else
            if (v.Z >= -val && v.Z <= 0) v.Z = 0;

            return v.AsDirection();
        }

        public static GridDirection GetDirectionFromRotation(Angle pitch, Angle yaw, Angle roll)
        {
            Vector3 v = new Vector3(1, 0, 0);

            v.Pitch(pitch);
            v.Yaw(yaw);
            v.Roll(roll);

            v.X = Math.Round(v.X, 5);
            v.Y = Math.Round(v.Y, 5);
            v.Z = Math.Round(v.Z, 5);

            double val = Math.Sqrt(2 - Math.Sqrt(2)) / 2;

            if (v.X <= 1 && v.X >= val) v.X = 1;
            else
                if (v.X <= val && v.X >= 0) v.X = 0;
                else
                    if (v.X >= -1 && v.X <= -val) v.X = -1;
                    else
                        if (v.X >= -val && v.X <= 0) v.X = 0;

            if (v.Y <= 1 && v.Y >= val) v.Y = -1;
            else
                if (v.Y <= val && v.Y >= 0) v.Y = 0;
                else
                    if (v.Y >= -1 && v.Y <= -val) v.Y = 1;
                    else
                        if (v.Y >= -val && v.Y <= 0) v.Y = 0;

            if (v.Z <= 1 && v.Z >= val) v.Z = 1;
            else
                if (v.Z <= val && v.Z >= 0) v.Z = 0;
                else
                    if (v.Z >= -1 && v.Z <= -val) v.Z = -1;
                    else
                        if (v.Z >= -val && v.Z <= 0) v.Z = 0;

            return v.AsDirection();
        }
        #endregion

        #region Extention methods

        public static GridDirection AsDirection(this Vector3 v)
        {
            return GetDirection((int)v.X, (int)v.Y, (int)v.Z);
        }

        public static Vector3 AsOffset(this GridDirection dir)
        {
            return _offsets[(int)dir];//GetDirection((int)v.X, (int)v.Y, (int)v.Z);
        }

        public static Rotator GetRotation(this GridDirection direction)
        {
            Vector3 offset = direction.AsOffset();
            offset.Normalize();

            Rotator rotation = new Rotator((int)((Angle)Math.Atan2(offset.Y, offset.X)).InDegrees, (int)((Angle)Math.Atan2(offset.Z, offset.Y)).InDegrees, 0);
            return rotation;
        }

        public static bool IsDiagonal(this Vector3 offset)
        {
            GridDirection dir = offset.AsDirection();

            return IsDiagonal(dir);
        }

        public static bool IsDiagonal(this GridDirection dir)
        {
            if (dir == GridDirection.West
                || dir == GridDirection.North
                || dir == GridDirection.East
                || dir == GridDirection.South
                || dir == GridDirection.Raise
                || dir == GridDirection.Lower)
                return false;
            else
                return true;
        }

        public static bool IsColinear(this GridDirection dir, GridDirection other)
        {
            if (dir == other || dir == other.Opposite())
                return true;
            else
                return false;
        }

        public static GridDirection Opposite(this Vector3 offset)
        {
            return GetDirection((int)offset.X * -1, (int)offset.Y * -1, (int)offset.Z * -1);
        }

        public static GridDirection Opposite(this GridDirection dir)
        {
            InitOffsetsIfNeeded();
            Vector3 v = _offsets[(int) dir];

            return Opposite(v);
        }

        #endregion
    }
}
