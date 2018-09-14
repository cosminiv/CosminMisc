using ConsoleApp1.Eu.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu
{
    public class Tools
    {
        public static bool IsOddNumberPrime(long n)
        {
            for (long i = 3; i <= Math.Sqrt(n) + 1; i += 2)
            {
                long q = n / i;
                long r = n % i;

                if (r == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static long MaxProduct(byte[] data, int factorCount)
        {
            long crtProduct = 1;
            long maxProduct = 1;

            int maxIndex = 0;
            int crtFactorCount = 0;

            for (int i = 0; i < data.Length; i++)
            {
                byte crtFactor = data[i];

                if (crtFactor != 0)
                {
                    crtFactorCount++;

                    if (crtFactorCount > factorCount)
                    {
                        byte tailFactor = data[i - factorCount];
                        crtProduct = crtProduct / tailFactor * crtFactor;
                    }
                    else
                        crtProduct = crtProduct * crtFactor;
                }
                else
                {
                    crtFactorCount = 0;
                    crtProduct = 1;
                }

                if (crtFactorCount >= factorCount && crtProduct > maxProduct)
                {
                    maxProduct = crtProduct;
                    maxIndex = i;
                }
            }

            return maxProduct;
        }

        public static int GetDivisorCount(long n)
        {
            int result = 0;
            long n2 = n;
            
            for (int i = 1; i < n / 2 + 1; i++)
            {
                if (n2 % i == 0)
                {
                    result++;
                }
            }

            result++;

            return result;
        }

        public static int GetDivisorCountLarge(LargeNumber n)
        {
            LargeNumber half = n / 2 + 1;
            HashSet<LargeNumber> divisors = new HashSet<LargeNumber>();
            
            for (LargeNumber i = 2; n > 1; )
            {
                LargeNumber quotient = LargeNumber.Divide(n, i, out LargeNumber remainder);

                if (remainder == 0)
                {
                    Debug.Write(i.ToString() + " ");
                    LargeNumber[] divisorsToAdd = divisors.Select(d => d * i).ToArray();

                    foreach (LargeNumber divisorToAdd in divisorsToAdd)
                    {
                        //Debug.Write(divisorToAdd.ToString() + " ");
                        divisors.Add(divisorToAdd);
                    }

                    divisors.Add(i);
                    n = quotient;
                }
                else
                    i = i + 1;
            }

            divisors.Add(1);

            Debug.Print("");
            Debug.Print(string.Join(", ", divisors.OrderBy(d => d).Select(d => d.ToString())));
            return divisors.Count;
        }
    }
}
