﻿using ConsoleApp1.Eu._Common;

namespace ConsoleApp1.Eu._001_050
{
    public class Problem_025
    {
        public static long Solve()
        {
            LargeNumber n1 = 1;
            LargeNumber n2 = 1;
            int index = 3;

            while (true)
            {
                LargeNumber n = n1 + n2;

                if (n.ToString().Length == 1000)
                    return index;

                n1 = n2;
                n2 = n;
                index++;
            }
        }
    }
}
