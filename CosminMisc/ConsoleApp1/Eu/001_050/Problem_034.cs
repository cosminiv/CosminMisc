using System.Diagnostics;

namespace ConsoleApp1.Eu._001_050
{
    public class Problem_034
    {
        static readonly int[] _digits = new int[10];
        static int _digitCount = 1;
        static readonly int[] _factorials = new int[10];

        public static int Solve()
        {
            int sum = 0;
            ComputeFactorials();

            for (int i = 3; i < 1000000; i++)
            {
                GetDigits(i);
                int digitFactorialSum = ComputeDigitFactorialSum();

                if (i == digitFactorialSum)
                {
                    sum += i;
                    Debug.Print(i.ToString());
                }
            }

            return sum;
        }

        private static int ComputeDigitFactorialSum()
        {
            int sum = 0;
            for (int i = 0; i < _digitCount; i++)
                sum += _factorials[_digits[i]];
            return sum;
        }

        private static void ComputeFactorials()
        {
            _factorials[0] = 1;

            for (int i = 1; i < 10; i++)
                _factorials[i] = _factorials[i - 1] * i;
        }

        private static void GetDigits(int n)
        {
            int j = 0;

            for (int i = n; i > 0; i = i / 10)
                _digits[j++] = i % 10;

            _digitCount = j;
        }
    }
}
