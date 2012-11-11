using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MyNavMesh
{
    /*

    public enum Latitude // Широта
    {
        West = -1,
        None = 0,
        East = 1
    }

    public enum Longitude // Долгота
    {
        North = -1,
        None = 0,
        South = 1
    }

    public enum Altitude // Высота
    {
        Raise = 1,
        None = 0,
        Lower = -1
    }

    public class DirectionArray
    {
        private Point3D[] _offsets = {
              
                    // edge & vertex connections
                    new Point3D( 0, -1,  0),  // North
                    new Point3D( 1, -1,  0),  // North-East
                    new Point3D( 1,  0,  0),  // East
                    new Point3D( 1,  1,  0),  // South-East
                    new Point3D( 0,  1,  0),  // South
                    new Point3D(-1,  1,  0),  // South-West
                    new Point3D(-1,  0,  0),  // West
                    new Point3D(-1, -1,  0),  // North-West

                    new Point3D( 0, -1, -1),  // North-Lower
                    new Point3D( 1, -1, -1),  // North-East-Lower
                    new Point3D( 1,  0, -1),  // East-Lower
                    new Point3D( 1,  1, -1),  // South-East-Lower
                    new Point3D( 0,  1, -1),  // South-Lower
                    new Point3D(-1,  1, -1),  // South-West-Lower
                    new Point3D(-1,  0, -1),  // West-Lower
                    new Point3D(-1, -1, -1),  // North-West-Lower
                    new Point3D( 0,  0, -1),  // Lower

                    new Point3D( 0, -1,  1),  // North-Raise
                    new Point3D( 1, -1,  1),  // North-East-Raise
                    new Point3D( 1,  0,  1),  // East-Raise
                    new Point3D( 1,  1,  1),  // South-East-Raise
                    new Point3D( 0,  1,  1),  // South-Raise
                    new Point3D(-1,  1,  1),  // South-West-Raise
                    new Point3D(-1,  0,  1),  // West-Raise
                    new Point3D(-1, -1,  1),  // North-West-Raise
                    new Point3D( 0,  0,  1)   // Raise
                };

        private Direction[] _oppositeOffsets = {
              
                    // edge & vertex connections
                    Direction.South,  // North
                    Direction.SouthWest,  // North-East
                    Direction.West,  // East
                    Direction.NorthWest,  // South-East
                    Direction.North,  // South
                    Direction.NorthEast,  // South-West
                    Direction.East,  // West
                    Direction.SouthEast,  // North-West

                    Direction.SouthRaise,  // North-Lower
                    Direction.SouthWestRaise,  // North-East-Lower
                    Direction.WestRaise,  // East-Lower
                    Direction.NorthWestRaise,  // South-East-Lower
                    Direction.NorthRaise,  // South-Lower
                    Direction.NorthEastRaise,  // South-West-Lower
                    Direction.EastRaise,  // West-Lower
                    Direction.SouthEastRaise,  // North-West-Lower
                    Direction.Raise,  // Lower

                    Direction.SouthLower,  // North-Raise
                    Direction.SouthWestLower,  // North-East-Raise
                    Direction.WestLower,  // East-Raise
                    Direction.NorthWestLower,  // South-East-Raise
                    Direction.NorthLower,  // South-Raise
                    Direction.NorthEastLower,  // South-West-Raise
                    Direction.EastLower,  // West-Raise
                    Direction.SouthEastLower,  // North-West-Raise
                    Direction.Lower   // Raise
                };

        private static readonly Direction[][][] _dirsByOffsets =
        {
            // West
            new Direction[][]
            {
                // North
                new Direction[]
                {
                    // Lower
                    Direction.NorthWestLower,  // North-West-Lower
                    // None
                    Direction.NorthWest,  // North-West
                    // Raise
                    Direction.NorthWestRaise,  // North-West-Raise
                },
                // None
                new Direction[]
                {
                    // Lower
                    Direction.WestLower,  // West-Lower
                    // None
                    Direction.West,  // West
                    // Raise
                    Direction.WestRaise,  // West-Raise
                },
                // South
                new Direction[]
                {
                    // Lower
                    Direction.SouthWestLower,  // West-Lower
                    // None
                    Direction.SouthWest,  // West
                    // Raise
                    Direction.SouthWestRaise,  // West-Raise
                },
            },
            // None
            new Direction[][]
            {
                // North
                new Direction[]
                {
                    // Lower
                    Direction.NorthLower,  // North-West-Lower
                    // None
                    Direction.North,  // North-West
                    // Raise
                    Direction.NorthRaise,  // North-West-Raise
                },
                // None
                new Direction[]
                {
                    // Lower
                    Direction.Lower,  // West-Lower
                    // None
                    Direction.NA,  // N/A
                    // Raise
                    Direction.Raise,  // West-Raise
                },
                // South
                new Direction[]
                {
                    // Lower
                    Direction.SouthLower,  // West-Lower
                    // None
                    Direction.South,  // West
                    // Raise
                    Direction.SouthRaise,  // West-Raise
                },
            },
            // East
            new Direction[][]
            {
                // North
                new Direction[]
                {
                    // Lower
                    Direction.NorthEastLower,  // North-West-Lower
                    // None
                    Direction.NorthEast,  // North-West
                    // Raise
                    Direction.NorthEastRaise,  // North-West-Raise
                },
                // None
                new Direction[]
                {
                    // Lower
                    Direction.EastLower,  // West-Lower
                    // None
                    Direction.East,  // N/A
                    // Raise
                    Direction.EastRaise,  // West-Raise
                },
                // South
                new Direction[]
                {
                    // Lower
                    Direction.SouthEastLower,  // West-Lower
                    // None
                    Direction.SouthEast,  // West
                    // Raise
                    Direction.SouthEastRaise,  // West-Raise
                },
            },
        };


        public DirectionArray()
        {
        }
        public Point3D this[Direction i]
        {
            get { return _offsets[(int)i]; }
        }

        //public Point3D this[Latitude x, Longitude y, Altitude z]
        //{
        //    get
        //    {
        //        return new Point3D((int)x, (int)y, (int)z);
        //    }
        //}

        public Direction this[Latitude x, Longitude y, Altitude z]
        {
            get
            {
                //Debug.Assert(((int)x) >= -1 && ((int)x) < 2 && ((int)y) >= -1 && ((int)y) < 2 && ((int)z) >= -1 && ((int)z) < 2);
                //int _x = (x)
                return _dirsByOffsets[((int)x)+1][((int)y)+1][((int)z)+1];
            }
        }

        public Direction this[Point3D point]
        {
            get
            {
                int x = point.X;
                int y = point.Y;
                int z = point.Z;
                //Debug.Assert(((int)x) >= -1 && ((int)x) < 2 && ((int)y) >= -1 && ((int)y) < 2 && ((int)z) >= -1 && ((int)z) < 2);
                //int _x = (x)
                return _dirsByOffsets[((int)x) + 1][((int)y) + 1][((int)z) + 1];
            }
        }

        public int Length
        {
            get { return _offsets.Length; }
        }

        public Direction[] Opposite
        {
            get { return _oppositeOffsets; }
        }

    }*/
}
