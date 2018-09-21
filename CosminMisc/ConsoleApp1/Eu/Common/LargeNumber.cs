using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu.Common
{
    public class LargeNumber: IComparable
    {
        private List<byte> _digits;
        private int _sign = 1;

        #region constructors

        public LargeNumber()
        {
            _digits = new List<byte>();
        }

        public LargeNumber(int number)
        {
            InitFromLong(number);
        }

        public LargeNumber(long number)
        {
            InitFromLong(number);
        }

        public LargeNumber(LargeNumber n)
        {
            if (n._digits != null)
                _digits = new List<byte>(n._digits);
            else
                _digits = new List<byte>();

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

        public LargeNumber(IEnumerable<byte> digits)
        {
            _digits = new List<byte>(digits);
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

        public static LargeNumber operator *(LargeNumber n1, LargeNumber n2)
        {
            return Multiply(n1, n2);
        }

        public static LargeNumber operator /(LargeNumber n1, LargeNumber n2)
        {
            return Divide(n1, n2, out LargeNumber remainder);
        }

        public static LargeNumber operator %(LargeNumber n1, LargeNumber n2)
        {
            LargeNumber n = Divide(n1, n2, out LargeNumber remainder);
            return remainder;
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
            if (n1 == n2) return false;
            else if (n1 > n2) return false;
            else return true;
        }

        public static bool operator ==(LargeNumber n1, LargeNumber n2)
        {
            n1 = Normalize(n1);
            n2 = Normalize(n2);

            if (n1._digits.Count != n2._digits.Count) return false;
            if (n1._sign != n2._sign) return false;

            for (int i = 0; i < n1._digits.Count; i++)
            {
                if (n1._digits[i] != n2._digits[i])
                    return false;
            }

            return true;
        }

        public static bool operator !=(LargeNumber n1, LargeNumber n2)
        {
            return !(n1 == n2);
        }

        public static bool operator >=(LargeNumber n1, LargeNumber n2)
        {
            return n1 > n2 || n1 == n2;
        }

        public static bool operator <=(LargeNumber n1, LargeNumber n2)
        {
            return n1 < n2 || n1 == n2;
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

        public static LargeNumber Multiply(LargeNumber x, LargeNumber y)
        {
            if (IsZero(x) || IsZero(y)) return 0;

            LargeNumber result = new LargeNumber();

            for (int i = 0; i < y._digits.Count; i++)
            {
                byte yDigit = y._digits[y._digits.Count - i - 1];
                if (yDigit != 0)
                {
                    LargeNumber partialProd = MultiplyByOneDigit(x, yDigit);
                    for (int j = 0; j < i; j++)
                        partialProd._digits.Add(0);
                    result = AddPositives(result, partialProd);
                }
            }

            result._sign = x._sign * y._sign;

            return result;
        }

        private static LargeNumber MultiplyByOneDigit(LargeNumber x, byte y)
        {
            if (IsZero(x) || y == 0) return 0;

            LargeNumber result = new LargeNumber();
            int carry = 0;

            for (int i = 0; i < x._digits.Count; i++)
            {
                int prod = x._digits[x._digits.Count - i - 1] * y + carry;
                result._digits.Insert(0, (byte)(prod % 10));
                carry = prod / 10;
            }

            if (carry > 0)
                result._digits.Insert(0, (byte)carry);

            return result;
        }

        public static LargeNumber Divide(LargeNumber dividend, LargeNumber divisor, out LargeNumber remainder)
        {
            if (divisor == 0) throw new DivideByZeroException();

            if (dividend == 0 || divisor == 1)
            {
                remainder = 0;
                return new LargeNumber(dividend);
            }

            if (dividend < divisor)
            {
                remainder = new LargeNumber(dividend);
                return 0;
            }

            LargeNumber result = new LargeNumber();
            int dividendSign = dividend._sign;
            int divisorSign = divisor._sign;

            try
            {
                // Make the numbers positive, so it's easier to divide them.
                dividend._sign = 1;
                divisor._sign = 1;

                LargeNumber partialDividend = GetFirstDividend(dividend, divisor);
                int digit = GetLargestMultiplier(partialDividend, divisor, out remainder);
                result._digits.Add((byte)digit);

                for (int i = partialDividend._digits.Count; i < dividend._digits.Count; i++)
                {
                    partialDividend = new LargeNumber(remainder._digits.Concat(dividend._digits.Skip(i).Take(1)));
                    digit = GetLargestMultiplier(partialDividend, divisor, out remainder);
                    result._digits.Add((byte)digit);
                }

            }
            finally
            {
                // Restore the numbers signs
                dividend._sign = dividendSign;
                divisor._sign = divisorSign;
            }

            result._sign = dividend._sign * divisor._sign;

            return result;
        }

        private static LargeNumber GetFirstDividend(LargeNumber dividend, LargeNumber divisor)
        {
            int divisorLength = divisor._digits.Count;
            LargeNumber result = new LargeNumber(dividend._digits.Take(divisorLength));

            if (result < divisor)
                result = new LargeNumber(dividend._digits.Take(divisorLength + 1));

            return result;
        }

        private static int GetLargestMultiplier(LargeNumber dividend, LargeNumber divisor, out LargeNumber remainder)
        {
            for (int multiplier = 9; multiplier >= 1; multiplier--)
            {
                LargeNumber multResult = Multiply(divisor, multiplier);
                remainder = dividend - multResult;

                if (remainder >= 0)
                    return multiplier;
            }

            remainder = dividend;
            return 0;
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
            LargeNumber n = 0;

            Debug.Assert(n == zero);
            Debug.Assert(zero + zero == zero);
            Debug.Assert(-zero == zero);
            Debug.Assert(zero.Equals(-zero));

            Debug.Assert(-(LargeNumber)(-20) == 20);
            Debug.Assert(n1 + zero == n1);
            Debug.Assert(n3 + zero == n3);
            Debug.Assert(n3 - zero == n3);
            Debug.Assert(n1 + n2 == 2591);
            Debug.Assert(zero - n2 == -2);
            Debug.Assert(n2 - n3 == 12);
            Debug.Assert(n1 - n1 == zero);
            Debug.Assert(n1 - n2 == 2587);
            Debug.Assert(n2 - n1 == -2587);
            Debug.Assert(n3 - n2 == -12);
            Debug.Assert(n3 - n3 == 0);
            Debug.Assert(n3 + n3 == -20);

            Debug.Assert(n1 * zero == zero);
            Debug.Assert(zero * n3 == zero);
            Debug.Assert(n1 * 2 == 5178);
            Debug.Assert(n1 * 1 == n1);
            Debug.Assert((LargeNumber)5477 * (LargeNumber)(-19903) == 5477 * (-19903));
            Debug.Assert((LargeNumber)23169 * 3103 == 71893407);

            Debug.Assert(n3 < 0);
            Debug.Assert(n2 > 0);
            Debug.Assert(n1 > n3);
            Debug.Assert(n2 > n3);
            Debug.Assert(n2 == 2);

            Debug.Assert(zero / 13 == 0);
            Debug.Assert(zero / 1 == 0);
            Debug.Assert(n1 / 1 == n1);
            Debug.Assert(n1 / n1 == 1);
            Debug.Assert(n1 / 1 == n1);
            Debug.Assert(n1 / n2 == (2589 / 2));
            Debug.Assert(n1 / n3 == (2589 / -10));
            Debug.Assert((LargeNumber)1000 / 10 == 100);
            Debug.Assert((LargeNumber)1000 / 20 == 50);
            Debug.Assert((LargeNumber)1689 / 20 == (1689 / 20));
            Debug.Assert((LargeNumber)3689900342 / 4591 == (3689900342 / 4591));
            Debug.Assert((LargeNumber)"969696969696969696969696969696" / 3 == (LargeNumber)"323232323232323232323232323232");

            Debug.Assert((LargeNumber)9487235 % 2 == 1);
            Debug.Assert((LargeNumber)"8349438234943823293578234654239452348558" % 2 == 0);
            Debug.Assert((LargeNumber)9487235648 % 782 == (9487235648 % 782));
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

        public override bool Equals(object obj)
        {
            var number = obj as LargeNumber;
            return !(number is null) && this == number;
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public int CompareTo(object obj)
        {
            if (this < (LargeNumber)obj)
                return -1;
            else if (this > (LargeNumber)obj)
                return 1;
            else
                return 0;
        }
    }
}
