using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu._051_100
{
    public class Problem_062
    {
        static readonly Dictionary<ulong, List<ulong>> _cubes = new Dictionary<ulong, List<ulong>>();

        public static ulong Solve() {
            for (ulong i = 1; ; i++) {
                ulong cube = i * i * i;
                ulong normalized = NormalizeDigits(cube);
                bool found = _cubes.TryGetValue(normalized, out List<ulong> cubes);

                if (!found) {
                    cubes = new List<ulong>();
                    _cubes.Add(normalized, cubes);
                }

                cubes.Add(cube);
                if (cubes.Count == 5) {
                    Console.WriteLine($"Cubes: " + string.Join(", ", cubes));
                    return cubes.Min();
                }
            }

            return 0;
        }

        private static ulong NormalizeDigits(ulong n) {
            int i = 0;
            ulong multOfTen = 1;
            List<ulong> digits = new List<ulong>();

            for (ulong n2 = n; n2 > 0; n2 = n2 / 10) {
                digits.Add(n2 % 10);
                multOfTen *= 10;
            }
            List<ulong> normalizedDigits = digits.OrderByDescending(d => d).ToList();

            ulong result = 0;
            i = 0;
            for (ulong m = multOfTen / 10; m > 0; m = m / 10) {
                result += normalizedDigits[i++] * m;
            }

            return result;
        }

         
    }
}
