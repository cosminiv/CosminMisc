using System;
using System.Diagnostics;
using ConsoleApp1.Eu._Common;

namespace ConsoleApp1.Eu._001_050
{
    public class Problem_026
    {
        // TODO: takes about 3s, try to make it faster
        public static int Solve() {
            int MAX = 1000;
            int MAX_DIGITS = 2000;
            LargeNumber N = LargeNumber.Power(10, MAX_DIGITS);
            //int MAX_SKIP = 20;
            var primes = new Primes(MAX).ToList();

            Stopwatch sw = Stopwatch.StartNew();
            Console.Title = "Problem 26";

            for (int i = primes.Count - 1; i > 0; i--) {
                long divisor = primes[i];
                LargeNumber q = LargeNumber.Divide(N, divisor, out LargeNumber remainder);
                string digits = q.ToString();
                long t1 = sw.ElapsedMilliseconds;
                Console.WriteLine($"\n{t1}ms");
                //File.AppendAllLines(@"C:\Users\ivanc\Desktop\aaa\1.txt", new[] { digits });

                int skip = 0;
                int maxLenToCheck = (MAX_DIGITS - skip) / 2 - 2;
                string digitsWithoutPrefix = digits.Substring(skip);
                for (int len = 1; len <= maxLenToCheck; len++) {
                    if (IsRepeating(digitsWithoutPrefix, len)) {
                        Console.WriteLine($"{divisor}: {digitsWithoutPrefix.Substring(0, len)}");
                        if (len == divisor - 1) {
                            Console.WriteLine($"\t{divisor}:\t{digits.Substring(0, skip)}\t{digitsWithoutPrefix.Substring(0, len)}\t{len} digits");
                            return (int)divisor;
                        }
                        goto nextDivisor;
                    }
                }

                nextDivisor:
                ;
            }

            return 0;
        }

        private static bool IsRepeating(string digits, int len) {
            string prevSubstr = digits.Substring(0, len);
            for (int i = len; i < digits.Length - len; i+=len) {
                string substr = digits.Substring(i, len);
                if (substr != prevSubstr)
                    return false;
            }

            return true;
        }

        //private static bool Validate(string candidate) {
        //    // Check if made of two identical halves
        //    int mid = (candidate.Length % 2 == 0) ? candidate.Length / 2 : candidate.Length / 2 - 1;
        //    string candidate1 = candidate.Substring(0, mid);
        //    string candidate2 = candidate.Substring(mid, mid);
        //    if (candidate1 == candidate2)
        //        return false;

        //    // Check if made of one repeated digit.
        //    if (1 == new HashSet<char>(candidate.ToArray()).Count)
        //        return false;

        //    return true;
        //}
    }
}
