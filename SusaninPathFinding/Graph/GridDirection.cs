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
    public enum CompassDirection
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

    public class GridDirection
    {
        #region Fields

        /// <summary>
        /// List of cell offsets.
        /// </summary>
        private static Vector3[] _offsets;

        /// <summary>
        /// List of directions, indexed by offsets
        /// </summary>
        private static ArrayEx<CompassDirection> _directions;

        /// <summary>
        /// The currently selected direction
        /// </summary>
        //private Direction _value;

        #endregion

        #region Properties

        public CompassDirection Value { get; set; }

        public static Vector3[] Offsets
        {
            get
            {
                InitOffsets();
                return _offsets;
            }
        }

        #endregion
        
        #region Constructors

        public GridDirection(CompassDirection dir)
        {
            Value = dir;
        }

        #endregion Constructors

        #region Static functions

        private static void InitOffsets()
        {
            if (_offsets == null)
            {
                _offsets = new Vector3[]
                    {
                        new Vector3( 0,  0,  0),  // N/A
                        new Vector3( 0, -1,  0),  // North
                        new Vector3( 1, -1,  0),  // North-East
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

        private static void InitDirections()
        {
            if (_directions == null)
            {
                _directions = new ArrayEx<CompassDirection>(new int[] { 3, 3, 3 });

                _directions[0, 0, 0] = CompassDirection.NA;

                _directions[1, 0, 1] = CompassDirection.North;  // North[0, -1,  0]
                _directions[2, 0, 1] = CompassDirection.NorthEast;  // North-East[1, -1,  0]
                _directions[2, 1, 1] = CompassDirection.East;  // East[1,  0,  0]
                _directions[2, 2, 1] = CompassDirection.SouthEast;  // South-East[1,  1,  0]
                _directions[1, 2, 1] = CompassDirection.South;  // South[0,  1,  0]
                _directions[0, 2, 1] = CompassDirection.SouthWest;  // South-West[-1, 1,  0]
                _directions[0, 1, 1] = CompassDirection.West;  // West[-1, 0,  0]
                _directions[0, 0, 1] = CompassDirection.NorthWest;  // North-West[-1,-1,  0]

                _directions[1, 0, 0] = CompassDirection.NorthLower;  // North-Lower[0, -1, -1]
                _directions[2, 0, 0] = CompassDirection.NorthEastLower;  // North-East-Lower[1, -1, -1]
                _directions[2, 1, 0] = CompassDirection.EastLower; // East-Lower[1,  0, -1]
                _directions[2, 2, 0] = CompassDirection.SouthEastLower;  // South-East-Lower[ 1,  1, -1]
                _directions[1, 2, 0] = CompassDirection.SouthLower;  // South-Lower[0,  1, -1]
                _directions[0, 2, 0] = CompassDirection.SouthWestLower;  // South-West-Lower[-1, 1, -1]
                _directions[0, 1, 0] = CompassDirection.WestLower; // West-Lower[-1, 0, -1]
                _directions[0, 0, 0] = CompassDirection.NorthWestLower; // North-West-Lower[-1,-1, -1]
                _directions[1, 1, 0] = CompassDirection.Lower;  // Lower[0,  0, -1]

                _directions[1, 0, 2] = CompassDirection.NorthRaise;  // North-Raise[0, -1,  1]
                _directions[2, 0, 2] = CompassDirection.NorthEastRaise;  // North-East-Raise[1, -1,  1]
                _directions[2, 1, 2] = CompassDirection.EastRaise;  // East-Raise[1,  0,  1]
                _directions[2, 2, 2] = CompassDirection.SouthEastRaise;  // South-East-Raise[1,  1,  1]
                _directions[1, 2, 2] = CompassDirection.SouthRaise;  // South-Raise[0,  1,  1]
                _directions[0, 2, 2] = CompassDirection.SouthWestRaise; // South-West-Raise[-1, 1,  1]
                _directions[0, 1, 2] = CompassDirection.WestRaise;  // West-Raise[-1,  0,  1]
                _directions[0, 0, 2] = CompassDirection.NorthWestRaise;  // North-West-Raise[-1, -1,  1]
                _directions[1, 1, 2] = CompassDirection.Raise;  // Raise[ 0,  0,  1]
            }
        }

        public static CompassDirection Directions(int x, int y, int z)
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

            InitDirections();
            return _directions[x+1, y+1, z+1];
        }

        public static CompassDirection Directions(Vector3 v)
        {
            return Directions((int)v.X, (int)v.Y, (int)v.Z);
            //if (v.X > 1 && v.X < -1)
            //{
            //    string message = String.Format(CultureInfo.CurrentCulture, Strings.ArgumentLessOrGreater, -1, 1);
            //    throw new ArgumentOutOfRangeException("v.X", v.X, message);
            //}

            //if (v.Y > 1 && v.Y < -1)
            //{
            //    string message = String.Format(CultureInfo.CurrentCulture, Strings.ArgumentLessOrGreater, -1, 1);
            //    throw new ArgumentOutOfRangeException("v.Y", v.Y, message);
            //}

            //if (v.Z > 1 && v.Z < -1)
            //{
            //    string message = String.Format(CultureInfo.CurrentCulture, Strings.ArgumentLessOrGreater, -1, 1);
            //    throw new ArgumentOutOfRangeException("v.Z", v.Z, message);
            //}

            //InitDirections();
            //return _directions[(int)v.X + 1, (int)v.Y + 1, (int)v.Z + 1];
        }

        public static bool IsDiagonal(Vector3 offset)
        {
            CompassDirection dir = Directions(offset);

            return IsDiagonal(dir);
            //if  (  dir == Direction.West
            //    || dir == Direction.North
            //    || dir == Direction.East
            //    || dir == Direction.South
            //    || dir == Direction.Raise
            //    || dir == Direction.Lower)
            //    return false;
            //else
            //    return true;
        }

        public static bool IsDiagonal(CompassDirection dir)
        {
            if (dir == CompassDirection.West
                || dir == CompassDirection.North
                || dir == CompassDirection.East
                || dir == CompassDirection.South
                || dir == CompassDirection.Raise
                || dir == CompassDirection.Lower)
                return false;
            else
                return true;
        }

        public static CompassDirection Opposite(CompassDirection dir)
        {
            InitOffsets();
            Vector3 v = _offsets[(int) dir];

            return Opposite(v); //Directions((int)v.X * -1, (int)v.Y * -1, (int)v.Z * -1);
        }

        public static CompassDirection Opposite(Vector3 offset)
        {
            return Directions((int)offset.X * -1, (int)offset.Y * -1, (int)offset.Z * -1);
        }

        public CompassDirection Opposite()
        {
            return Opposite(Value);
        }

        public CompassDirection FromRotator(double pitch, double yaw, double roll)
        {
            Vector3 v = new Vector3(1, 0, 0);

            v.Pitch(((int)pitch));
            v.Yaw(((int)yaw));
            v.Roll(((int)roll));

            return Directions(v);
        }
        #endregion

        public bool IsDiagonal()
        {
            return IsDiagonal(Value);
            //if (Value == Direction.West
            //    || Value == Direction.North
            //    || Value == Direction.East
            //    || Value == Direction.South
            //    || Value == Direction.Raise
            //    || Value == Direction.Lower)
            //    return false;
            //else
            //    return true;
        }
    }
}
