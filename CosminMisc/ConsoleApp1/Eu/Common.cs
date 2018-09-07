﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu
{
    public class Common
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
    }
}
