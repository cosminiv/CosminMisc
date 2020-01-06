using System;
using System.Diagnostics;
using System.Text;

namespace ConsoleApp1.Leet
{
    class Leet_067
    {
        private int _zeroCode = '0';

        public void Solve()
        {
            string a = AddBinary("111", "11");
            Debug.Assert(a == "1010");

            string b = AddBinary("111", "1");
            Debug.Assert(b == "1000");

            string c = AddBinary("111", "10");
            Debug.Assert(c == "1001");

            string d = AddBinary("1", "1");
            Debug.Assert(d == "10");

            string e = AddBinary("10", "1");
            Debug.Assert(e == "11");
        }

        public string AddBinary(string a, string b)
        {
            int length = Math.Max(a.Length, b.Length);

            char[] result = new char[length + 1];
            int carry = 0;

            for (int i = 0; i < length; i++)
            {
                int indexA = a.Length - i - 1;
                int indexB = b.Length - i - 1;
                int digitA = indexA >= 0 ? Digit(a[indexA]) : 0;
                int digitB = indexB >= 0 ? Digit(b[indexB]) : 0;

                int sum = digitA + digitB + carry;

                carry = sum < 2 ? 0 : 1;
                if (carry > 0)
                    sum -= 2;

                int indexResult = result.Length - i - 1;
                result[indexResult] = (char)(_zeroCode + sum);
            }

            if (carry == 1)
                result[0] = '1';
            else
            {
                char[] result2 = new char[length];
                for (int i = 0; i < length; i++)
                    result2[i] = result[i + 1];
                result = result2;
            }

            return new string(result);
        }

        int Digit(char c)
        {
            return c - _zeroCode;
        }
    }
}
