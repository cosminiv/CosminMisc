using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1.Eu._001_050
{
    public class Problem_050
    {
        public static long Solve() {
            return Solve1();
        }

        // Trying from both ends of primes array. Not working yet.
        private static long Solve2() {
            int MAX = (int)1E+6;

            var primesSet = _Common.Tools.GetPrimesUpTo(MAX);
            var primesInOrder = primesSet.OrderBy(a => a).ToList();
            int maxLength = 0;
            long sumForMaxLength = 0;
            List<long> maxList = new List<long>();
            int start = 0;
            int end = start + 1;
            long sum = primesInOrder[start] + primesInOrder[end];
            //List<long> primeList = new List<long> { sum };

            while (true) {
                CheckIfMaxSum();

                if (sum > MAX) {
                    Console.WriteLine($"Breaking at [{end}] = {primesInOrder[end]}; Sum = {sum}");
                    Console.WriteLine($"Max sum found {sumForMaxLength} and length {maxLength}");
                    Console.WriteLine($"Subtracting from end: [{end}] {primesInOrder[end]} -> {sum - primesInOrder[end]}");
                    sum -= primesInOrder[end--];
                    Console.WriteLine($"Making Sum = {sum}");
                    break;
                }
                else
                    sum += primesInOrder[++end];
            }

            while (start < end) {
                Console.WriteLine($"Subtracting from start: [{start}] {primesInOrder[start]} -> {sum - primesInOrder[start]}");
                sum -= primesInOrder[start++];
                if (CheckIfMaxSum()) break;

                Console.WriteLine($"Subtracting from end: [{end}] {primesInOrder[end]} -> {sum - primesInOrder[end]}");
                sum -= primesInOrder[end--];
                if (CheckIfMaxSum()) break;
            }

            string output = $"{sumForMaxLength} = " + string.Join(" + ", maxList) + $" ({maxLength} terms)";
            Console.WriteLine(output);

            return sumForMaxLength;

            bool CheckIfMaxSum() {
                if (primesSet.Contains(sum)) {
                    int length = end - start + 1;
                    if (length > maxLength) {
                        maxLength = length;
                        sumForMaxLength = sum;
                        return true;
                    }
                }
                return false;
            }
        }

        // Naive solution. Works.
        private static long Solve1() {
            int MAX = (int)1E+6;

            var primesSet = _Common.Tools.GetPrimesUpTo(MAX);
            var primesInOrder = primesSet.OrderBy(a => a).ToList();
            int maxLength = 0;
            long sumForMaxLength = 0;
            List<long> maxList = new List<long>();
            int maxStart = 0;
            int maxEnd = 0;
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
                            maxStart = start;
                            maxEnd = end;
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
