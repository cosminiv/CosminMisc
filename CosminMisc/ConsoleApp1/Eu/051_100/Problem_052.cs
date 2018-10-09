using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu._051_100
{
    public class Problem_052
    {
        static int[] _digitCounts = new int[10];
        static int[] _digitCountsMult = new int[10];

        public static long Solve() {
            for (int i = 1; ; i++) {
                ParseDigits(i);
                bool areAllMultSameDigits = true;
                for (int j = 2; j <= 6; j++) {
                    bool areSame = ParseDigitsMult(i * j);
                    if (!areSame) {
                        areAllMultSameDigits = false;
                        break;
                    }
                }
                if (areAllMultSameDigits) {
                    Console.WriteLine($"{i}: {i * 2}, {i * 3}, {i * 4}, {i * 5}, {i * 6}");
                    return i;
                }
            }
        }

        private static void ParseDigits(int n) {
            Array.Clear(_digitCounts, 0, _digitCounts.Length);

            for (int i = n; i > 0; i = i / 10) {
                int digit = i % 10;
                _digitCounts[digit]++;
            }
        }

        private static bool ParseDigitsMult(int n) {
            Array.Clear(_digitCountsMult, 0, _digitCountsMult.Length);

            for (int i = n; i > 0; i = i / 10) {
                int digit = i % 10;
                if (++_digitCountsMult[digit] > _digitCounts[digit])
                    return false;
            }

            for (int i = 0; i < _digitCounts.Length; i++)
                if (_digitCounts[i] != _digitCountsMult[i])
                    return false;

            return true;
        }
    }
}
