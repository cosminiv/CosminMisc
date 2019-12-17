using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ConsoleApp1.Eu._Common
{
    public class Tools
    {
        public static bool IsPrime(long n)
        {
            if (n == 2 || n == 3) return true;
            if (n % 2 == 0) return false;

            int max = (int)(Math.Sqrt(n) + 1);

            for (long i = 3; i <= max; i += 2)
            {
                long q = n / i;
                long r = n % i;

                if (r == 0)
                    return false;
            }

            return true;
        }

        public static HashSet<long> GetPrimes(int count)
        {
            HashSet<long> primes = new HashSet<long>();
            if (count <= 0)
                return primes;

            primes.Add(2);
            if (count == 1)
                return primes;

            for (long i = 3; ; i+=2)
            {
                if (IsPrime(i))
                    primes.Add(i);

                if (primes.Count == count)
                    return primes;
            }
        }

        public static int GetDigitCount(long n)
        {
            int count = 0;
            for (long i = n; i > 0; i = i / 10)
                count++;
            return count;
        }

        public static HashSet<long> GetPrimesUpTo(int max)
        {
            // false = prime, true = non-prime
            bool[] candidates = new bool[max + 1];
            candidates[0] = candidates[1] = true;

            for (int i = 2; i < max / 2; i++)
            {
                if (candidates[i] == true)
                    continue;

                for (int j = i + i; j <= max; j = j + i)
                    candidates[j] = true;
            }

            var result = new HashSet<long>();

            for (int i = 2; i < candidates.Length; i++)
                if (candidates[i] == false)
                    result.Add(i);

            return result;
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

        public static HashSet<long> GetDivisors(long n, HashSet<long> primes)
        {
            if (n == 1)
                return new HashSet<long> { 1 };

            long n2 = n;
            HashSet<long> divisors = new HashSet<long>();

            for (long i = 2; i <= n2; )
            {
                if (primes.Contains(n2))
                    i = n2;

                if (n2 % i == 0)
                {
                    long[] divisorsToAdd = divisors.Select(d => d * i).ToArray();

                    foreach (long divisorToAdd in divisorsToAdd)
                    {
                        divisors.Add(divisorToAdd);
                    }

                    divisors.Add(i);
                    n2 = n2 / i;
                }
                else
                    i++;
            }

            divisors.Add(1);

            return divisors;
        }

        public static List<PrimeFactor> GetPrimeFactors(long n, List<long> primes) {
            List<PrimeFactor> primeFactors = new List<PrimeFactor>();
            int primeIndex = 0;
            PrimeFactor prevFactor = null;

            for (long n2 = n; n2 > 1 && primeIndex < primes.Count; ) 
            {
                long prime = primes[primeIndex];
                long quot = Math.DivRem(n2, prime, out long rem);
                if (rem == 0) {
                    if (prevFactor == null || prevFactor.Factor != prime) {
                        prevFactor = new PrimeFactor { Factor = prime, Exponent = 1 };
                        primeFactors.Add(prevFactor);
                    }
                    else
                        prevFactor.Exponent++;
                    n2 = quot;
                }
                else
                    primeIndex++;
            }

            return primeFactors;
        }

        public static long GetDivisorCount(long n, HashSet<long> primes)
        {
            return GetDivisors(n, primes).Count;
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

        static readonly int[] _palindromeDigitCache = new int[50];

        public static bool IsPalindrome(long n, int @base = 10) {
            int j = 0;

            for (long i = n; i > 0; i = i / @base)
                _palindromeDigitCache[j++] = (int)(i % @base);

            for (long i = 0; i < j / 2; i++)
                if (_palindromeDigitCache[i] != _palindromeDigitCache[j - i - 1])
                    return false;

            return true;
        }
    }
}
