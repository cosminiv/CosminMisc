using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu
{
    public class Problem_035
    {
        public static int Solve()
        {
            int count = 0;
            HashSet<long> primes = Tools.GetPrimesUpTo(1000000);
            
            foreach (long prime in primes)
            {
                List<int> rot = GenerateRotations((int)prime);
                if (rot != null && rot.All(r => primes.Contains(r)))
                {
                    Debug.Print(prime.ToString());
                    count++;
                }
            }

            return count;
        }

        private static List<int> GenerateRotations(int n)
        {
            List<int> rotations = new List<int>() { n };
            int digitCount = (int)Math.Ceiling(Math.Log10(n));

            for (int i = 1; i < digitCount; i++)
            {
                int lastNumber = rotations[rotations.Count - 1];
                int digit = lastNumber % 10;

                //if (digit != 1 && digit != 3 && digit != 7 && digit != 9)
                //    return null;

                int numberNoDigit = lastNumber / 10;
                rotations.Add(numberNoDigit + digit * (int)Math.Pow(10, digitCount - 1));
            }

            return rotations;
        }
    }
}
