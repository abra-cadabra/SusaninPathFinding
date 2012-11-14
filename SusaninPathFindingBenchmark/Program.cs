using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SusaninPathFindingBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            SusaninPathFindingBenchmarkClass benchmark = new SusaninPathFindingBenchmarkClass();
            Console.WriteLine("Количество точек в тесте: 80x80x1");
            Console.WriteLine("Время выполнения: {0} секунд", ((float)benchmark.Benchmark()) / 1000.0f);
            Console.ReadLine();
        }
    }
}
