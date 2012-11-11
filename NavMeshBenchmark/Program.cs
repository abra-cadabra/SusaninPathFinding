using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NavMeshBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            NavMeshBenchmarkClass benchmark = new NavMeshBenchmarkClass();
            Console.WriteLine("Количество точек в тесте: 80x80x1");
            Console.WriteLine("Время выполнения: {0} секунд", ((float)benchmark.Benchmark()) / 1000.0f);
            Console.ReadLine();
        }
    }
}
