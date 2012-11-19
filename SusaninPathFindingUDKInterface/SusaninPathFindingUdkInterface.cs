using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using RGiesecke.DllExport;
using SusaninPathFinding.Geometry;
using SusaninPathFinding.Graph;
using SusaninPathFinding.Graph.NodeInfoTypes;
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
        public float PointType { get; set; }
        public float Direction { get; set; }
        public Vector3 Point { get; set; }
        

        public CellInfo()
        {
            Point = new Vector3();
            PointType = -1;
            Direction = -1;
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
        [DllExport("DLLCreateGrid", CallingConvention = CallingConvention.StdCall)]
        public static int DLLCreateGrid(int sizeX, int sizeY, int sizeZ, ref UdkVector cellSize)
        {
            if(Grids == null)
            {
                Grids = new List<PolygonGrid3D>();
            }
            Grids.Add(new PolygonGrid3D(sizeX, sizeY, sizeZ, new Cell3D(cellSize.X, cellSize.Y, cellSize.Z)));

            return Grids.Count - 1;
        }

        [DllExport("TestSendArray", CallingConvention = CallingConvention.StdCall)]
        public static int TestSendArray(ref UdkDynamicArray wrapper)
        {
            int valuesCount = wrapper.Count * 5; //*5 => 2 int point type + 3 float from Vector struct

            //var intArr = new int[valuesCount];
            var floatArr = new float[valuesCount];

            //int and floats are assumed to be both of 32 bits (but it is not everywhere true)
            //Marshal.Copy(wrapper.DataPtr, intArr, 0, valuesCount);
            Marshal.Copy(wrapper.DataPtr, floatArr, 0, valuesCount);

            var cells = new List<CellInfo>();
            for (int i = 0; i < valuesCount; i += 5)
            {
                var cell = new CellInfo();
                cell.PointType = floatArr[i];
                cell.Direction = floatArr[i + 1];
                cell.Point.X = floatArr[i + 2];
                cell.Point.Y = floatArr[i + 3];
                cell.Point.Z = floatArr[i + 4];
                
                cells.Add(cell);
            }

            //ha ha ha here we have managedArray passed from UDK
            return 123;//managedArray.Length;
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

        [DllExport("DLLCommitCells", CallingConvention = CallingConvention.StdCall)]
        public static bool CommitCells(int mapId, ref UdkDynamicArray wrapper)
        {
            int valuesCount = wrapper.Count * 5; //*5 => 2 int point type + 3 float from Vector struct

            //var intArr = new int[valuesCount];
            var floatArr = new float[valuesCount];

            //int and floats are assumed to be both of 32 bits (but it is not everywhere true)
            //Marshal.Copy(wrapper.DataPtr, intArr, 0, valuesCount);
            Marshal.Copy(wrapper.DataPtr, floatArr, 0, valuesCount);

            var cell = new CellInfo();
            for (int i = 0; i < valuesCount; i += 5)
            {
                cell.PointType = floatArr[i];
                cell.Direction = floatArr[i + 1];
                cell.Point.X = floatArr[i + 2];
                cell.Point.Y = floatArr[i + 3];
                cell.Point.Z = floatArr[i + 4];

                if ((cell.Point.X < 0 || cell.Point.X >= Grids[mapId].SizeX)
                 || (cell.Point.Y < 0 || cell.Point.Y >= Grids[mapId].SizeY)
                 || (cell.Point.Z < 0 || cell.Point.Z >= Grids[mapId].SizeZ))
                    return false; // coordinates out of range

                if (cell.PointType < 0 || cell.PointType > 4)
                    return false;

                if (cell.Direction < 0 || cell.Direction > 27)
                    return false;

                Grids[mapId][(int) cell.Point.X, (int) cell.Point.Y, (int) cell.Point.Z].Info =
                    UdkInterfaceUtility.NodeInfoFromCellInfo(cell);
                   // NodeInfo.Factory((CellType)cell.PointType);
            }

            return true;

        }

        [DllExport("DLLUpdateCells", CallingConvention = CallingConvention.StdCall)]
        public static bool UpdateCells(int mapId, ref UdkDynamicArray wrapper)
        {
            try
            {
                int Length = 5 * Grids[mapId].Nodes.Length;

                //var cells = new CellInfo[Grids[mapId].Nodes.Length];
                var floatArray = new float[Length];

                int index = 0;
                for (int i = 0; i < Grids[mapId].SizeZ; i++)
                {
                    for (int j = 0; j < Grids[mapId].SizeY; j++)
                    {
                        for (int k = 0; k < Grids[mapId].SizeX; k++)
                        {
                            floatArray[index * 5 + 0] = (float)Grids[mapId].Nodes[k, j, i].Info.Type();
                            if (Grids[mapId].Nodes[k, j, i].Info is Ladder)
                                floatArray[index * 5 + 1] = (float)((Ladder)Grids[mapId].Nodes[k, j, i].Info).Direction.Value;
                            floatArray[index * 5 + 2] = (float)Grids[mapId].Nodes[k, j, i].X;
                            floatArray[index * 5 + 3] = (float)Grids[mapId].Nodes[k, j, i].Y;
                            floatArray[index * 5 + 4] = (float)Grids[mapId].Nodes[k, j, i].Z;
                            index++;
                        }
                    }
                }

                //for (int i = 0; i < Grids[mapId].Nodes.Length; i++)
                //{
                //    floatArray[i * 5 + 0] = (float)Grids[mapId].Nodes[i].Info.Type();
                //    if (Grids[mapId].Nodes[i].Info is Ladder)
                //        floatArray[i * 5 + 1] = (float)((Ladder)Grids[mapId].Nodes[i].Info).Direction.Value;
                //    floatArray[i * 5 + 2] = (float)Grids[mapId].Nodes[i].Y;
                //    floatArray[i * 5 + 3] = (float)Grids[mapId].Nodes[i].X;
                //    floatArray[i * 5 + 4] = (float)Grids[mapId].Nodes[i].Z;
                //}


                //wrapper.DataPtr = Marshal.AllocHGlobal(sizeof(float) * floatsLength);

                //for (int i = 0; i < Grids[mapId].Nodes.Length; i++)
                //{
                //    Marshal.Copy(intArray, i * 5, wrapper.DataPtr, i * 5 + 1);
                //}
                Marshal.Copy(floatArray, 0, wrapper.DataPtr, Length);
                return true;

            }
            catch (Exception)
            {

                return false;
            }
            

            //wrapper.Count = source.Length;
            //wrapper.MaxSize = source.Length;

            //ha ha ha here we have managedArray passed from UDK
            //return 123;//managedArray.Length;
        }
        #endregion

        #region Type translation functions

        
        #endregion
    }
}
