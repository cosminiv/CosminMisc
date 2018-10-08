using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu
{
    public class Problem_024
    {
        static int[] _digitsFound = new int[10];

        public static long Solve()
        {
            string chars = "0123456789";
            List<string> permutations = GeneratePermutations(chars);

            permutations = permutations.OrderBy(c => c).ToList();
            return Convert.ToInt64(permutations[999999]);
        }

        public static List<string> GeneratePermutations(string chars)
        {
            List<string> result = new List<string>();

            if (chars.Length == 1)
            {
                result.Add(chars);
                goto end;
            }

            result = new List<string>(chars.Length * (chars.Length - 1));
            string firstElem = chars[0].ToString();
            List<string> smallerPermutations = GeneratePermutations(chars.Substring(1));

            for (int j = 0; j <= chars.Length - 1; j++)
            {
                for (long i = 0; i < smallerPermutations.Count; i++)
                {
                    //Debug.Assert(i <= int.MaxValue);
                    string prevPerm = smallerPermutations.ElementAt((int)i);
                    result.Add(prevPerm.Insert(j, firstElem));
                }
            }

            end:
            //foreach (string perm in result)
            //    Debug.Print(perm);
            //Debug.Print("");

            return result;
        }

        private static long SolveBruteForce()
        {
            int countFound = 0;

            for (long i = 123456789; i <= 9876543210; i += 9)
                if (HasUniqueDigits(i) && ++countFound == 1000000)
                    return i;

            return 0;
        }

        private static bool HasUniqueDigits(long n)
        {
            Array.Clear(_digitsFound, 0, _digitsFound.Length);

            if (n < 1000000000)
                _digitsFound[0] = 1;

            for (long i = n; i > 0; i = i / 10)
            {
                long digit = i % 10;
                _digitsFound[digit]++;

                if (_digitsFound[digit] > 1)
                    return false;
            }

            return true;
        }
    }
}
