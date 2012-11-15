using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SusaninPathFinding.Geometry;
using SusaninPathFinding.Graph;
using SusaninPathFinding.Graph.PathFinding;

namespace SusaninPathFindingUDKInterface
{
    /// <summary>
    /// Structure, representing UDK Vector structure
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct UdkVector
    {
        public float X;
        public float Y;
        public float Z;
    }

    public class CellInfo
    {
        public int PointType { get; set; }
        public Vector3 Point { get; set; }

        public CellInfo()
        {
            Point = new Vector3();
            PointType = -1;
        }

        public override string ToString()
        {
            return String.Format("{0}  {1},{2},{3}", PointType, Point.X, Point.Y, Point.Z);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct UdkDynamicArray
    {
        public IntPtr DataPtr;
        /// <summary>
        /// Number of elements of array
        /// </summary>
        public int Count;
        /// <summary>
        /// Memory allocated for array / each element size
        /// </summary>
        public int MaxSize;
    }

    public class SusaninPathFindingUdkInterface
    {
        #region Properties

        public static List<PolygonGrid3D> Grids { get; set; }

        #endregion

        #region Functions

        /// <summary>
        /// Registers the Susanin Path Finding map and returns the id of the map.
        /// </summary>
        /// <param name="SizeX">X size of the map.</param>
        /// <param name="SizeY">Y size of the map.</param>
        /// <param name="SizeZ">Z size of the map.</param>
        /// <param name="cellSize">Sizes of the cell.</param>
        /// <returns></returns>
        public static int DLLCreateGrid(int sizeX, int sizeY, int sizeZ, ref UdkVector cellSize)
        {
            if(Grids == null)
            {
                Grids = new List<PolygonGrid3D>();
            }
            Grids.Add(new PolygonGrid3D(sizeX, sizeY, sizeZ, new Cell3D(cellSize.X, cellSize.Y, cellSize.Z)));

            return Grids.Count - 1;
        }

        //public static bool DLLResizeMap(int mapId, int sizeX, int sizeY, int sizeZ, ref UdkVector vector)
        //{
        //    if (Grids.Count > 0 && mapId >= 0 && mapId < Grids.Count)
        //    {
        //        if (sizeX > 0) Grids[mapId].SizeX = sizeX;
        //        if (sizeY > 0) Grids[mapId].SizeY = sizeY;
        //        if (sizeZ > 0) Grids[mapId].SizeZ = sizeZ;
        //        if (vector.X > 0) Grids[mapId].Polygon.Bounds.SizeX = vector.X;
        //        if (vector.Y > 0) Grids[mapId].Polygon.Bounds.SizeY = vector.Y;
        //        if (vector.Z > 0) Grids[mapId].Polygon.Bounds.SizeZ = vector.Z;

        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        public static bool DLLCommitCells(int mapId, ref UdkDynamicArray wrapper)
        {
            int valuesCount = wrapper.Count * 4; //*4 => 1 int point type + 3 float from Vector struct

            var intArr = new int[valuesCount];
            var floatArr = new float[valuesCount];

            //int and floats are assumed to be both of 32 bits (but it is not everywhere true)
            Marshal.Copy(wrapper.DataPtr, intArr, 0, valuesCount);
            Marshal.Copy(wrapper.DataPtr, floatArr, 0, valuesCount);

            var cells = new List<CellInfo>();
            for (int i = 0; i < valuesCount; i += 4)
            {
                var cell = new CellInfo();
                cell.PointType = intArr[i];
                cell.Point.X = floatArr[i + 1];
                cell.Point.Y = floatArr[i + 2];
                cell.Point.Z = floatArr[i + 3];
                cells.Add(cell);
            }


        }

        #endregion

        #region Type translation functions

        public Node CellInfoToNode(CellInfo info)
        {
            Node node = new Node(info.Point, null, null, );
        }

        #endregion
    }
}
