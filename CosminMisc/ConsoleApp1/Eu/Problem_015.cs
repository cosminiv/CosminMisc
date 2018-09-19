using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu
{
    public class Problem_015
    {
        public static decimal Solve()
        {
            Dictionary<string, decimal> cache = new Dictionary<string, decimal>();
            decimal result = PathCountRecursiveWithCache(20, 20, cache);
            //decimal result = PathCountRecursive(45, 45);
            return result;
        }

        private static decimal PathCountRecursive(int width, int height)
        {
            if (width == 0 || height == 0)
                return 1;

            return PathCountRecursive(width - 1, height) + PathCountRecursive(width, height - 1);
        }

        private static decimal PathCountRecursiveWithCache(int width, int height, Dictionary<string, decimal> cache)
        {
            if (width == 0 || height == 0) return 1;

            if (cache.TryGetValue($"{width}_{height}", out decimal value1))
                return value1;

            if (cache.TryGetValue($"{height}_{width}", out decimal value2))
                return value2;

            decimal result = 
                PathCountRecursiveWithCache(width - 1, height, cache) 
                + PathCountRecursiveWithCache(width, height - 1, cache);

            cache.Add($"{width}_{height}", result);

            return result;
        }
    }
}
