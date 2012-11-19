using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using SusaninPathFinding.Geometry;
using RGiesecke.DllExport;

namespace TestDLLBind
{

    public class CellInfo
    {
        public int PointType { get; set; }
        public int Direction { get; set; }
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

    [StructLayout(LayoutKind.Sequential)]
    public struct UdkVector
    {
        public float X;
        public float Y;
        public float Z;
    }


    public static class TestDllBind
    {
        [DllExport("TestSendArray", CallingConvention = CallingConvention.StdCall)]
        public static int TestSendArray(ref UdkDynamicArray wrapper)
        {
            int valuesCount = wrapper.Count * 5; //*4 => 1 int point type + 3 float from Vector struct

            var intArr = new int[valuesCount];
            var floatArr = new float[valuesCount];
            
            //int and floats are assumed to be both of 32 bits (but it is not everywhere true)
            Marshal.Copy(wrapper.DataPtr, intArr,  0, valuesCount);
            Marshal.Copy(wrapper.DataPtr, floatArr, 0, valuesCount);

            var cells = new List<CellInfo>();
            for (int i = 0; i < valuesCount; i += 5)
            {
                var cell = new CellInfo();
                cell.PointType = intArr[i];
                cell.Direction = intArr[i + 1];
                cell.Point.X = floatArr[i + 2];
                cell.Point.Y = floatArr[i + 3];
                cell.Point.Z = floatArr[i + 4];

                cells.Add(cell);
            }

            //ha ha ha here we have managedArray passed from UDK
            return 123;//managedArray.Length;
        }

        [DllExport("ChangeVector", CallingConvention = CallingConvention.StdCall)]
        public static int ChangeVector(ref UdkVector vector)
        {
            vector.X = 2;
            vector.Y = 3;
            vector.Z = 4;

            //ha ha ha here we have managedArray passed from UDK
            return 123;//managedArray.Length;
        }
        //[DllExport("TestSendTwoArrays", CallingConvention = CallingConvention.StdCall)]
        //public static int TestSendTwoArrays(TArray arr1, TArray arr2)
        //{
        //    return arr2.Data.Length;
        //}

        [DllExport("TestFindPath", CallingConvention = CallingConvention.StdCall)]
        public static int TestFindPath(ref UdkVector from, ref UdkVector to)
        {
            return 3;
        }

        [DllExport("TestGetResult", CallingConvention = CallingConvention.StdCall)]
        public static void TestGetResult(ref UdkDynamicArray wrapper)
        {

            var source = new Vector3[3];
            source[0] = new Vector3() { X = 1, Y = 2, Z = 3 };
            source[1] = new Vector3() { X = 4, Y = 5, Z = 6 };
            source[2] = new Vector3() { X = 53, Y = 1, Z = 16 };

            int floatsLength = 3 * source.Length;

            var outputArray = new float[floatsLength];

            for (int i = 0; i < source.Length; i++)
            {
                outputArray[i * 3 + 0] = (float)source[i].X;
                outputArray[i * 3 + 1] = (float)source[i].Y;
                outputArray[i * 3 + 2] = (float)source[i].Z;
            }

            //wrapper.DataPtr = Marshal.AllocHGlobal(sizeof(float) * floatsLength);

            Marshal.Copy(outputArray, 0, wrapper.DataPtr, floatsLength);

            //wrapper.Count = source.Length;
            //wrapper.MaxSize = source.Length;

            //ha ha ha here we have managedArray passed from UDK
            //return 123;//managedArray.Length;
        }

        [DllExport("TestChangeArray", CallingConvention = CallingConvention.StdCall)]
        public static int TestChangeArray(ref UdkDynamicArray wrapper)
        {
         
            var source = new Vector3[2];
            source[0] = new Vector3() {X = 1, Y = 2, Z = 3};
            source[1] = new Vector3() { X = 4, Y = 5, Z = 6 };


            int floatsLength = 3*source.Length;
          
           

            var outputArray = new float[floatsLength];

            for (int i = 0; i < source.Length; i++)
            {
                outputArray[i * 3 + 0] = (float) source[i].X;
                outputArray[i * 3 + 1] = (float)source[i].Y;
                outputArray[i * 3 + 2] = (float)source[i].Z;
            }

            //wrapper.DataPtr = Marshal.AllocHGlobal(sizeof(float) * floatsLength);
            
            Marshal.Copy(outputArray, 0, wrapper.DataPtr, floatsLength);

            //wrapper.Count = source.Length;
            //wrapper.MaxSize = source.Length;
            
            //ha ha ha here we have managedArray passed from UDK
            return 123;//managedArray.Length;
        }


        //[DllExport("TestSendTwoValues", CallingConvention = CallingConvention.StdCall)]
        //public static double TestSendTwoValues(double val1, double val2)
        //{
        //    return val2;
        //}
    }
}
