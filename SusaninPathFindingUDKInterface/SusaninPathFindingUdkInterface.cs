using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using RGiesecke.DllExport;
using SusaninPathFinding;
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
        public float Pitch { get; set; }
        public float Yaw { get; set; }
        public float Roll { get; set; }
        public Vector3 Point { get; set; }
        

        public CellInfo()
        {
            Point = new Vector3();
            PointType = -1;
            Pitch = 0;
            Yaw = 0;
            Roll = 0;
        }

        public override string ToString()
        {
            return String.Format("{0}  {1},{2},{3}", PointType, Point.X, Point.Y, Point.Z);
        }
    }

    public class EdgeInfo
    {
        public float Type { get; set; }
        public Vector3 From { get; set; }
        public Vector3 To { get; set; }

        public EdgeInfo()
        {
            Type = -1;
            From = new Vector3();
            To = new Vector3();
        }

        public override string ToString()
        {
            return String.Format("{0}  {1},{2},{3} {4},{5},{6}", Type, From.X, From.Y, From.Z, To.X, To.Y, To.Z);
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

        /// <summary>
        /// List of the grids
        /// </summary>
        public static List<Grid3D> Grids { get; set; }


        public static AStar<Cell> AStarPathFinder { get; set; }

        #endregion

        #region Functions

        #region Grid menegement functions

        /// <summary>
        /// Registers the Susanin Path Finding map and returns the id of the map.
        /// </summary>
        /// <param name="SizeX">X size of the map.</param>
        /// <param name="SizeY">Y size of the map.</param>
        /// <param name="SizeZ">Z size of the map.</param>
        /// <param name="cellSize">Sizes of the cell.</param>
        /// <returns></returns>
        [DllExport("DLLCreateGrid", CallingConvention = CallingConvention.StdCall)]
        public static int CreateGrid(int sizeX, int sizeY, int sizeZ, ref UdkVector cellSize)
        {
            if(Grids == null)
            {
                Grids = new List<Grid3D>();
            }
            Grids.Add(new Grid3D(sizeX, sizeY, sizeZ, new Cell3D(cellSize.X, cellSize.Y, cellSize.Z)));

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
                cell.Pitch = floatArr[i + 1];
                cell.Yaw = floatArr[i + 2];
                cell.Roll = floatArr[i + 3];
                cell.Point.X = floatArr[i + 4];
                cell.Point.Y = floatArr[i + 5];
                cell.Point.Z = floatArr[i + 6];
                
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
            int valuesCount = wrapper.Count * 7; //*5 => 2 int point type + 3 float from Vector struct

            //var intArr = new int[valuesCount];
            var floatArr = new float[valuesCount];

            //int and floats are assumed to be both of 32 bits (but it is not everywhere true)
            //Marshal.Copy(wrapper.DataPtr, intArr, 0, valuesCount);
            Marshal.Copy(wrapper.DataPtr, floatArr, 0, valuesCount);

            var cell = new CellInfo();
            // Checking the commited data for consistency
            for (int i = 0; i < valuesCount; i += 7)
            {
                cell.PointType = floatArr[i];
                cell.Pitch = floatArr[i + 1];
                cell.Yaw = floatArr[i + 2];
                cell.Roll = floatArr[i + 3];
                cell.Point.X = floatArr[i + 4];
                cell.Point.Y = floatArr[i + 5];
                cell.Point.Z = floatArr[i + 6];

                if ((cell.Point.X < 0 || cell.Point.X >= Grids[mapId].SizeX)
                 || (cell.Point.Y < 0 || cell.Point.Y >= Grids[mapId].SizeY)
                 || (cell.Point.Z < 0 || cell.Point.Z >= Grids[mapId].SizeZ))
                    return false; // coordinates out of range

                if (cell.PointType < 0 || cell.PointType > 4)
                    return false;

                if (cell.Pitch < 0)
                    return false;

                if (cell.Yaw < 0)
                    return false;

                if (cell.Roll < 0)
                    return false;
            }

            // Commiting data
            for (int i = 0; i < valuesCount; i += 7)
            {
                cell.PointType = floatArr[i];
                cell.Pitch = floatArr[i + 1];
                cell.Yaw = floatArr[i + 2];
                cell.Roll = floatArr[i + 3];
                cell.Point.X = floatArr[i + 4];
                cell.Point.Y = floatArr[i + 5];
                cell.Point.Z = floatArr[i + 6];

                Grids[mapId][(int)cell.Point.X, (int)cell.Point.Y, (int)cell.Point.Z].Info =
                    UdkInterfaceUtility.GetNodeInfo(cell);
            }

            return true;

        }

        [DllExport("DLLUpdateCells", CallingConvention = CallingConvention.StdCall)]
        public static bool UpdateCells(int mapId, ref UdkDynamicArray wrapper)
        {
            try
            {
                int Length = 7 * Grids[mapId].Nodes.Count;

                //var cells = new CellInfo[Grids[mapId].Nodes.Length];
                var floatArray = new float[Length];

                int index = 0;
                for (int i = 0; i < Grids[mapId].SizeZ; i++)
                {
                    for (int j = 0; j < Grids[mapId].SizeY; j++)
                    {
                        for (int k = 0; k < Grids[mapId].SizeX; k++)
                        {
                            floatArray[index + 0] = (float)Grids[mapId].Nodes[k, j, i].Info.Type();
                            if (Grids[mapId].Nodes[k, j, i].Info is Ladder)
                            {
                                Rotator rot = ((Ladder) Grids[mapId].Nodes[k, j, i].Info).Direction.GetRotation();
                                floatArray[index + 1] = rot.Pitch;
                                floatArray[index + 2] = rot.Yaw;
                                floatArray[index + 3] = rot.Roll;
                            }
                                
                            floatArray[index + 4] = (float)Grids[mapId].Nodes[k, j, i].X;
                            floatArray[index + 5] = (float)Grids[mapId].Nodes[k, j, i].Y;
                            floatArray[index + 6] = (float)Grids[mapId].Nodes[k, j, i].Z;
                            index+=7;
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

        [DllExport("DLLCommitCellEdges", CallingConvention = CallingConvention.StdCall)]
        public static bool CommitCellEdges(int mapId, ref UdkDynamicArray wrapper)
        {
            int valuesCount = wrapper.Count * 7; //*5 => 2 int point type + 3 float from Vector struct
            Grid3D grid = Grids[mapId];
            //var intArr = new int[valuesCount];
            var floatArr = new float[valuesCount];

            //int and floats are assumed to be both of 32 bits (but it is not everywhere true)
            //Marshal.Copy(wrapper.DataPtr, intArr, 0, valuesCount);
            Marshal.Copy(wrapper.DataPtr, floatArr, 0, valuesCount);

            var edge = new EdgeInfo();
            // Checking the commited data for consistency
            for (int i = 0; i < valuesCount; i += 7)
            {
                edge.Type = floatArr[i];
                edge.From.X = floatArr[i + 1];
                edge.From.Y = floatArr[i + 2];
                edge.From.Z = floatArr[i + 3];
                edge.To.X = floatArr[i + 4];
                edge.To.Y = floatArr[i + 5];
                edge.To.Z = floatArr[i + 6];


                if ((edge.From.X < 0 || edge.From.X >= Grids[mapId].SizeX)
                 || (edge.From.Y < 0 || edge.From.Y >= Grids[mapId].SizeY)
                 || (edge.From.Z < 0 || edge.From.Z >= Grids[mapId].SizeZ))
                    return false; // coordinates out of range

                if ((edge.To.X < 0 || edge.To.X >= Grids[mapId].SizeX)
                 || (edge.To.Y < 0 || edge.To.Y >= Grids[mapId].SizeY)
                 || (edge.To.Z < 0 || edge.To.Z >= Grids[mapId].SizeZ))
                    return false; // coordinates out of range

                if (edge.Type < 0 || edge.Type > 4)
                    continue;

                grid.Edges[grid[edge.From], grid[edge.To]].Info =
                    edge.GetNodeInfo();
            }
            return true;

        }

        #endregion

        [DllExport("DLLFindPath", CallingConvention = CallingConvention.StdCall)]
        public static int FindPath(int mapId, ref UdkVector from, ref UdkVector to, int senderType)
        {
            AStarPathFinder = new AStar<Cell>(Grids[mapId]);
            AStarPathFinder.UseWorldDistance = false;
            AStarPathFinder.FindBestPath(UdkInterfaceUtility.GraphAgentFromMovementType((MovementType)senderType, Grids[mapId]),
                                         Grids[mapId][(int)from.X, (int)from.Y, (int)from.Z],
                                         Grids[mapId][(int)to.X, (int)to.Y, (int)to.Z]);
            return AStarPathFinder.Nodes.Count;
        }


        [DllExport("DLLFetchPath", CallingConvention = CallingConvention.StdCall)]
        public static void FetchPath(ref UdkDynamicArray wrapper)
        {

            int floatsLength = 3 * AStarPathFinder.Nodes.Count;

            var outputArray = new float[floatsLength];

            for (int i = 0; i < AStarPathFinder.Nodes.Count; i++)
            {
                outputArray[i * 3 + 0] = (float)AStarPathFinder.Nodes[i].WorldLocation.X;
                outputArray[i * 3 + 1] = (float)AStarPathFinder.Nodes[i].WorldLocation.Y;
                outputArray[i * 3 + 2] = (float)AStarPathFinder.Nodes[i].WorldLocation.Z;
            }

            //wrapper.DataPtr = Marshal.AllocHGlobal(sizeof(float) * floatsLength);

            Marshal.Copy(outputArray, 0, wrapper.DataPtr, floatsLength);

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
