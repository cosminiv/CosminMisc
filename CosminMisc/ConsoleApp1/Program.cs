using ConsoleApp1.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] data = new[] { 5, 1, 3, 10, 4 };
            MergeSort.Sort(data, out int inversions);
        }
    }
}
