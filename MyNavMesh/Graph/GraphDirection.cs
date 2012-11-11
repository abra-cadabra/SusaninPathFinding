using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra.Complex;
using MyNavMesh.Collections;
using MyNavMesh.Geometry;


namespace MyNavMesh.Graph
{
    /// <summary>
    /// Specifies the list of main directions, which will be later used to describe an offsets for a cell.
    /// </summary>
    public enum Direction
    {
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
        Raise,

        NA
    }

    public class GraphDirection
    {
        #region Fields

        /// <summary>
        /// List of cell offsets.
        /// </summary>
        private static Vector3[] _offsets;

        /// <summary>
        /// List of directions, indexed by offsets
        /// </summary>
        private static ArrayEx<Direction> _directions;

        /// <summary>
        /// The currently selected direction
        /// </summary>
        //private Direction _value;

        #endregion

        #region Properties

        public Direction Value { get; set; }

        #endregion
        
        #region Constructors

        public GraphDirection(Direction dir)
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

        public static Vector3[] Offsets
        {
            get
            {
                InitOffsets();
                return _offsets;
            }
        }

        private static void InitDirections()
        {
            if (_directions == null)
            {
                _directions = new ArrayEx<Direction>(new int[] { 3, 3, 3 });

                _directions[1, 0, 1] = Direction.North;  // North[0, -1,  0]
                _directions[2, 0, 1] = Direction.NorthEast;  // North-East[1, -1,  0]
                _directions[2, 1, 1] = Direction.East;  // East[1,  0,  0]
                _directions[2, 2, 1] = Direction.SouthEast;  // South-East[1,  1,  0]
                _directions[1, 2, 1] = Direction.South;  // South[0,  1,  0]
                _directions[0, 2, 1] = Direction.SouthWest;  // South-West[-1, 1,  0]
                _directions[0, 1, 1] = Direction.West;  // West[-1, 0,  0]
                _directions[0, 0, 1] = Direction.NorthWest;  // North-West[-1,-1,  0]

                _directions[1, 0, 0] = Direction.NorthLower;  // North-Lower[0, -1, -1]
                _directions[2, 0, 0] = Direction.NorthEastLower;  // North-East-Lower[1, -1, -1]
                _directions[2, 1, 0] = Direction.EastLower; // East-Lower[1,  0, -1]
                _directions[2, 2, 0] = Direction.SouthEastLower;  // South-East-Lower[ 1,  1, -1]
                _directions[1, 2, 0] = Direction.SouthLower;  // South-Lower[0,  1, -1]
                _directions[0, 2, 0] = Direction.SouthWestLower;  // South-West-Lower[-1, 1, -1]
                _directions[0, 1, 0] = Direction.WestLower; // West-Lower[-1, 0, -1]
                _directions[0, 0, 0] = Direction.NorthWestLower; // North-West-Lower[-1,-1, -1]
                _directions[1, 1, 0] = Direction.Lower;  // Lower[0,  0, -1]

                _directions[1, 0, 2] = Direction.NorthRaise;  // North-Raise[0, -1,  1]
                _directions[2, 0, 2] = Direction.NorthEastRaise;  // North-East-Raise[1, -1,  1]
                _directions[2, 1, 2] = Direction.EastRaise;  // East-Raise[1,  0,  1]
                _directions[2, 2, 2] = Direction.SouthEastRaise;  // South-East-Raise[1,  1,  1]
                _directions[1, 2, 2] = Direction.SouthRaise;  // South-Raise[0,  1,  1]
                _directions[0, 2, 2] = Direction.SouthWestRaise; // South-West-Raise[-1, 1,  1]
                _directions[0, 1, 2] = Direction.WestRaise;  // West-Raise[-1,  0,  1]
                _directions[0, 0, 2] = Direction.NorthWestRaise;  // North-West-Raise[-1, -1,  1]
                _directions[1, 1, 2] = Direction.Raise;  // Raise[ 0,  0,  1]
            }
        }

        public static Direction Directions(int x, int y, int z)
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

        public static Direction Directions(Vector3 v)
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
            Direction dir = Directions(offset);

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

        public static bool IsDiagonal(Direction dir)
        {
            if (dir == Direction.West
                || dir == Direction.North
                || dir == Direction.East
                || dir == Direction.South
                || dir == Direction.Raise
                || dir == Direction.Lower)
                return false;
            else
                return true;
        }

        public static Direction Opposite(Direction dir)
        {
            InitOffsets();
            Vector3 v = _offsets[(int) dir];

            return Opposite(v); //Directions((int)v.X * -1, (int)v.Y * -1, (int)v.Z * -1);
        }

        public static Direction Opposite(Vector3 offset)
        {
            return Directions((int)offset.X * -1, (int)offset.Y * -1, (int)offset.Z * -1);
        }

        public Direction Opposite()
        {
            return Opposite(Value);
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
