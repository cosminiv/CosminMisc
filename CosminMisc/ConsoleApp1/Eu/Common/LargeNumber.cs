using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu.Common
{
    public class LargeNumber
    {
        private List<byte> _digits;
        private int _sign = 1;

        #region constructors

        public LargeNumber()
        {
            _digits = new List<byte>() { 0 };
        }

        public LargeNumber(LargeNumber n)
        {
            _digits = new List<byte>(n._digits);
            _sign = n._sign;
        }

        public LargeNumber(string str)
        {
            if (str == null)
                throw new ArgumentNullException();

            if (str.Length > 0)
            {
                if (str[0] == '-')
                {
                    this._sign = -1;
                    _digits = MakeDigitsFromString(str.Substring(1));
                }
                else if (str[0] == '+')
                    _digits = MakeDigitsFromString(str.Substring(1));
                else
                    _digits = MakeDigitsFromString(str);
            }
        }

        private List<byte> MakeDigitsFromString(string digits)
        {
            List<byte> result = digits.TrimStart('0').Select(d => byte.Parse(d.ToString())).ToList();
            return result;
        }

        public LargeNumber(long number)
        {
            InitFromLong(number);
        }

        public void InitFromLong(long number)
        {
            _digits = new List<byte>();

            if (number < 0)
            {
                _sign = -1;
                number = -number;
            }

            for (long n = number; n > 0; n = n / 10)
            {
                _digits.Insert(0, (byte)(n % 10));
            }
        }

        public LargeNumber(int number)
        {
            InitFromLong(number);
        }

        #endregion

        #region operators
        public static explicit operator LargeNumber(string str)
        {
            return new LargeNumber(str);
        }

        public static implicit operator LargeNumber(long n)
        {
            return new LargeNumber(n);
        }

        public static implicit operator LargeNumber(int n)
        {
            return new LargeNumber(n);
        }

        public static LargeNumber operator -(LargeNumber n)
        {
            return new LargeNumber(n) { _sign = 0 - n._sign };
        }

        public static LargeNumber operator +(LargeNumber n1, LargeNumber n2)
        {
            if (n1 >= 0 && n2 >= 0)
                return AddPositives(n1, n2);
            else if (n1 >= 0 && n2 <= 0)
                return SubtractPositives(n1, -n2);
            else if (n1 <= 0 && n2 >= 0)
                return SubtractPositives(n2, -n1);
            else 
                return -AddPositives(-n1, -n2);
        }

        public static LargeNumber operator -(LargeNumber n1, LargeNumber n2)
        {
            if (n1 >= 0 && n2 >= 0)
                return SubtractPositives(n1, n2);
            else if (n1 >= 0 && n2 <= 0)
                return AddPositives(n1, -n2);
            else if (n1 <= 0 && n2 >= 0)
                return -AddPositives(-n1, n2);
            else 
                return SubtractPositives(-n2, -n1);
        }

        public static LargeNumber operator *(LargeNumber n1, int n2)
        {
            return n1.Multiply(n2);
        }

        public static LargeNumber operator /(LargeNumber n1, LargeNumber n2)
        {
            return n1.Divide(n2);
        }

        public static bool operator >(LargeNumber n1, LargeNumber n2)
        {
            n1 = Normalize(n1);
            n2 = Normalize(n2);
            List<byte> digits1 = n1._digits;
            List<byte> digits2 = n2._digits;

            if (n1._sign > 0 && n2._sign < 0)
                return true;

            if (n1._sign < 0 && n2._sign > 0)
                return false;

            bool result = false;

            // Compare absolute values
            if (digits1.Count > digits2.Count)
                result = true;
            else if (digits1.Count < digits2.Count)
                result = false;
            else
            {
                for (int i = 0; i < digits1.Count; i++)
                {
                    if (digits1[i] > digits2[i])
                    {
                        result = true;
                        break;
                    }

                    if (digits1[i] < digits2[i])
                    {
                        result = false;
                        break;
                    }
                }
            }

            // Consider the signs
            if (n1._sign < 0 && n2._sign < 0)
                result = !result;

            return result;
        }

        public static bool operator <(LargeNumber n1, LargeNumber n2)
        {
            if (n1 == n2)
                return false;
            else if (n1 > n2)
                return false;
            else
                return true;
        }

        public static bool operator ==(LargeNumber n1, LargeNumber n2)
        {
            List<byte> digits1 = n1._digits.SkipWhile(b => b == 0).ToList();
            List<byte> digits2 = n2._digits.SkipWhile(b => b == 0).ToList();

            if (digits1.Count != digits2.Count)
                return false;

            for (int i = 0; i < digits1.Count; i++)
            {
                if (digits1[i] != digits2[i])
                    return false;
            }

            return true;
        }

        public static bool operator >=(LargeNumber n1, LargeNumber n2)
        {
            return n1 > n2 || n1 == n2;
        }

        public static bool operator <=(LargeNumber n1, LargeNumber n2)
        {
            return n1 < n2 || n1 == n2;
        }

        public static bool operator !=(LargeNumber n1, LargeNumber n2)
        {
            return !(n1 == n2);
        }

        #endregion

        private static LargeNumber AddPositives(LargeNumber x, LargeNumber y)
        {
            LargeNumber result = new LargeNumber();
            int maxDigits = Math.Max(x._digits.Count, y._digits.Count);
            result._digits = new List<byte>(maxDigits + 1);
            int carry = 0;

            for (int i = 0; i < maxDigits; i++)
            {
                int digitSum = carry;

                if (x._digits.Count - 1 - i >= 0)
                    digitSum += x._digits[x._digits.Count - 1 - i];

                if (y._digits.Count - 1 - i >= 0)
                    digitSum += y._digits[y._digits.Count - 1 - i];

                result._digits.Insert(0, (byte)(digitSum % 10));
                carry = digitSum / 10;
            }

            if (carry > 0)
                result._digits.Insert(0, (byte)carry);

            return result;
        }

        private static LargeNumber SubtractPositives(LargeNumber x, LargeNumber y)
        {
            bool isFirstBiggerThanSecond = x >= y;
            LargeNumber n1 = isFirstBiggerThanSecond ? x : y;
            LargeNumber n2 = isFirstBiggerThanSecond ? y : x;

            Debug.Assert(y >= 0);

            LargeNumber result = new LargeNumber();
            int maxDigits = n1._digits.Count;
            result._digits = new List<byte>(maxDigits);
            int carry = 0;

            for (int i = 0; i < maxDigits; i++)
            {
                int digit1 = n1._digits[n1._digits.Count - 1 - i] + carry;
                int digit2 = (n2._digits.Count - 1 - i >= 0) ? n2._digits[n2._digits.Count - 1 - i] : 0;

                if (digit2 > digit1)
                {
                    digit1 += 10;
                    carry = -1;
                }
                else
                    carry = 0;

                int digitSum = digit1 - digit2;

                result._digits.Insert(0, (byte)(digitSum));
            }

            if (!isFirstBiggerThanSecond)
                result._sign = -1;

            return result;
        }

        public LargeNumber Multiply(int other)
        {
            if (other > 9 || other < 0)
                throw new ArgumentOutOfRangeException();

            LargeNumber result = new LargeNumber();
            result._digits = new List<byte>(_digits.Count + 1);
            int carry = 0;

            for (int i = 0; i < _digits.Count; i++)
            {
                int prod = _digits[_digits.Count - i - 1] * other + carry;
                result._digits.Insert(0, (byte)(prod % 10));
                carry = prod / 10;
            }

            if (carry > 0)
                result._digits.Insert(0, (byte)carry);

            return result;
        }

        public LargeNumber Divide(LargeNumber divisor)
        {
            if (divisor == 0)
                throw new DivideByZeroException();

            if (this == 0 || divisor == 1)
                return new LargeNumber(this);

            LargeNumber result = new LargeNumber { _sign = this._sign * divisor._sign };
            int divisorLength = divisor._digits.Count;

            for (int i = 0; i < _digits.Count; i++)
            {
                LargeNumber partialDividend = new LargeNumber(){
                    _digits = _digits.Skip(i).Take(divisorLength).ToList() };

                if (partialDividend < divisor)
                {
                    partialDividend = new LargeNumber {
                        _digits = _digits.Skip(i).Take(divisorLength + 1).ToList() };

                    if (partialDividend < divisor)
                        return result;
                }

                for (int multiplier = 9; multiplier >= 1; multiplier--)
                {
                    LargeNumber multResult = divisor.Multiply(multiplier);
                    LargeNumber partialDiff = partialDividend - multResult;

                    if (partialDiff >= 0)
                    {
                        result._digits.Add((byte)multiplier);
                        break;
                    }
                }
            }

            return result;
        }

        public override string ToString()
        {
            if (IsZero(this))
                return "0";

            string result = (_sign < 0 ? "-" : "") + string.Join("", _digits).TrimStart('0');

            return result;
        }

        public static void RunTests()
        {
            LargeNumber n1 = 2589;
            LargeNumber n2 = 2;
            LargeNumber n3 = -10;
            LargeNumber zero = 0;

            Debug.Assert(zero + zero == zero);
            Debug.Assert(-zero == zero);
            Debug.Assert(n1 + zero == n1);
            Debug.Assert(n1 + n2 == 2591);
            Debug.Assert(zero - n2 == -2);
            Debug.Assert(n2 - n3 == 12);
            Debug.Assert(n1 - n1 == zero);
            Debug.Assert(n1 - n2 == 2587);
            Debug.Assert(n2 - n1 == -2587);
            Debug.Assert(n3 - n2 == -12);
            Debug.Assert(n3 - n3 == 0);
            Debug.Assert(n3 + n3 == -20);
            Debug.Assert(n1 * 2 == 5178);
            Debug.Assert(n3 < 0);
            Debug.Assert(n2 > 0);
            Debug.Assert(n1 > n3);
            Debug.Assert(n2 > n3);
            Debug.Assert(n2 == 2);
        }

        private static bool IsZero(LargeNumber n)
        {
            return n._digits.All(d => d == 0);
        }

        private static LargeNumber Normalize(LargeNumber n)
        {
            LargeNumber result = new LargeNumber(n);

            // Trim leading zeroes
            result._digits = n._digits.SkipWhile(b => b == 0).ToList();

            // Set sign for zero
            if (IsZero(result))
                result._sign = 1;

            return result;
        }
    }
}
