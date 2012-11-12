using System.Runtime.InteropServices;
using RGiesecke.DllExport;

namespace TestDLLBind
{
    public static class TestDllBind
    {
        [DllExport("TestSendArray", CallingConvention = CallingConvention.StdCall)]
        public static int TestSendArray()
        {
            return 123;
        }

        //[DllExport("TestSendTwoArrays", CallingConvention = CallingConvention.StdCall)]
        //public static int TestSendTwoArrays(TArray arr1, TArray arr2)
        //{
        //    return arr2.Data.Length;
        //}

        //[DllExport("TestSendValue", CallingConvention = CallingConvention.StdCall)]
        //public static double TestSendValue(double val)
        //{
        //    return val;
        //}

        //[DllExport("TestSendTwoValues", CallingConvention = CallingConvention.StdCall)]
        //public static double TestSendTwoValues(double val1, double val2)
        //{
        //    return val2;
        //}
    }
}
