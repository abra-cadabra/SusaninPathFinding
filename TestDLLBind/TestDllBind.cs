using System;
using System.Runtime.InteropServices;
using RGiesecke.DllExport;

namespace TestDLLBind
{
    [StructLayout(LayoutKind.Sequential)]
    public struct TArray
    {
        public IntPtr DataPtr;
        public int ArrayNum;
        public int ArrayMax;
    }
    

    public static class TestDllBind
    {
        [DllExport("TestSendArray", CallingConvention = CallingConvention.StdCall)]
        public static int TestSendArray(ref TArray wrapper)
        {
            var managedArray = new int[wrapper.ArrayNum];

            Marshal.Copy(wrapper.DataPtr, managedArray, 0, wrapper.ArrayNum);

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
