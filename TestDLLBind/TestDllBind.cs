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
    

    public static class TestDllBind
    {
        [DllExport("TestSendArray", CallingConvention = CallingConvention.StdCall)]
        public static int TestSendArray(ref UdkDynamicArray wrapper)
        {
            int valuesCount = wrapper.Count * 4; //*4 => 1 int point type + 3 float from Vector struct

            var intArr = new int[valuesCount];
            var floatArr = new float[valuesCount];
            
            //int and floats are assumed to be both of 32 bits (but it is not everywhere true)
            Marshal.Copy(wrapper.DataPtr, intArr,  0, valuesCount);
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

            //ha ha ha here we have managedArray passed from UDK
            return 123;//managedArray.Length;
        }

        //[DllExport("TestSendTwoArrays", CallingConvention = CallingConvention.StdCall)]
        //public static int TestSendTwoArrays(TArray arr1, TArray arr2)
        //{
        //    return arr2.Data.Length;
        //}

        [DllExport("TestSendValue", CallingConvention = CallingConvention.StdCall)]
        public static int TestSendValue(int ptr)
        {
            return 321;
        }

        //[DllExport("TestSendTwoValues", CallingConvention = CallingConvention.StdCall)]
        //public static double TestSendTwoValues(double val1, double val2)
        //{
        //    return val2;
        //}
    }
}
