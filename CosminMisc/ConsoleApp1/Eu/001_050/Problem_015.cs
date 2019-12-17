using System.Collections.Generic;
using System.Diagnostics;

namespace ConsoleApp1.Eu._001_050
{
    public class Problem_015
    {
        public static decimal Solve()
        {
            Dictionary<string, decimal> cache = new Dictionary<string, decimal>();
            int side = 4;
            decimal result = PathCountRecursiveWithCache(side, side, cache);
            //decimal result2 = PathCountNonRecursive(side);
            //Debug.Assert(result == result2);
            //decimal result = PathCountRecursive(side, side);
            return result;
        }

        
        private static decimal PathCountNonRecursive(int side)
        {
            Dictionary<string, decimal> cache = new Dictionary<string, decimal>()
            {
                { "1_0", 1 },
                { "1_1", 2 }
            };

            for (int crtSide = 2; crtSide <= side; crtSide++)
            {
                int prevSide = crtSide - 1;
                decimal prevSquare = cache[$"{prevSide}_{prevSide}"];
                decimal rect = 0;

                decimal prevSumValue = 1;
                AddToCache(cache, $"{crtSide}_0", 1);
                rect += 1;

                for (int j = 1; j <= crtSide - 1; j++)
                {
                    decimal crtSumValue = cache[$"{prevSide}_{j}"];
                    rect += crtSumValue;

                    string newKey = $"{crtSide}_{j}";
                    AddToCache(cache, newKey, prevSumValue + crtSumValue);

                    prevSumValue = crtSumValue;
                }

                decimal square = 2 * rect;
                AddToCache(cache, $"{crtSide}_{crtSide}", square);
            }

            decimal lastSquare = cache[$"{side}_{side}"];
            return lastSquare;
        }

        private static void AddToCache(Dictionary<string, decimal> cache, string key, decimal value)
        {
            if (!cache.ContainsKey(key))
            {
                Debug.Print($"Adding {key}: {value}");
                cache.Add(key, value);
            }
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
