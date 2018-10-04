using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu
{
    public class Problem_050
    {
        public static long Solve() {
            int MAX = (int)1E+7;

            var primesSet = Tools.GetPrimesUpTo(MAX);
            var primesInOrder = primesSet.OrderBy(a => a).ToList();
            int maxLength = 0;
            long sumForMaxLength = 0;
            List<long> maxList = new List<long>();
            long checks = 0;

            for (int start = 0; start < primesInOrder.Count; start++) {
                long sum = primesInOrder[start];
                List<long> primeList = new List<long> { sum };
                for (int end = start + 1; end < primesInOrder.Count; end++) {
                    sum += primesInOrder[end];
                    primeList.Add(primesInOrder[end]);
                    checks++;
                    if (primesSet.Contains(sum)) {
                        int length = end - start + 1;
                        if (length > maxLength) {
                            maxLength = length;
                            sumForMaxLength = sum;
                            maxList = primeList;
                        }
                    }
                    if (sum > MAX)
                        break;
                }
            }

            string output = $"{sumForMaxLength} = " + string.Join(" + ", maxList) + $" ({maxLength} terms)";
            Console.WriteLine(output);

            return sumForMaxLength;
        }

        public static long Solve2() {
            int MAX = (int)1E+7;

            var primesSet = Tools.GetPrimesUpTo(MAX);
            var primesInOrder = primesSet.OrderBy(a => a).ToList();
            int maxLength = 0;
            long sumForMaxLength = 0;
            List<long> maxList = new List<long>();
            long checks = 0;

            for (int start = 0; start < primesInOrder.Count; start++) {
                long sum = primesInOrder[start];
                List<long> primeList = new List<long> { sum };
                for (int end = start + 1; end < primesInOrder.Count; end++) {
                    sum += primesInOrder[end];
                    primeList.Add(primesInOrder[end]);
                    checks++;
                    if (primesSet.Contains(sum)) {
                        int length = end - start + 1;
                        if (length > maxLength) {
                            maxLength = length;
                            sumForMaxLength = sum;
                            maxList = primeList;
                        }
                    }
                    if (sum > MAX)
                        break;
                }
            }

            string output = $"{sumForMaxLength} = " + string.Join(" + ", maxList) + $" ({maxLength} terms)";
            Console.WriteLine(output);

            return sumForMaxLength;
        }
    }
}
