using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1.Eu._001_050
{
    public class Problem_048
    {
        public static long Solve() {
            LargeNumberCustom result = 0;
            for (int i = 1; i < 1001; i++) {
                result = result + LargeNumberCustom.Power(i, i);
            }
            Console.WriteLine(result);
            return 0;
        }

        // Considers only the least significant 10 digits.
        class LargeNumberCustom
        {
            private List<byte> _digits;
            private int _sign = 1;
            readonly static int MAX_DIGITS = 10;

            public LargeNumberCustom() {
                _digits = new List<byte>();
            }

            public LargeNumberCustom(long number) {
                InitFromLong(number);
            }

            public static implicit operator LargeNumberCustom(long n) {
                return new LargeNumberCustom(n);
            }

            public static LargeNumberCustom operator +(LargeNumberCustom n1, LargeNumberCustom n2) {
                return AddPositives(n1, n2);
            }

            public void InitFromLong(long number) {
                _digits = new List<byte>();

                if (number < 0) {
                    _sign = -1;
                    number = -number;
                }

                for (long n = number; n > 0; n = n / 10) {
                    _digits.Insert(0, (byte)(n % 10));
                }
            }

            public static LargeNumberCustom Multiply(LargeNumberCustom x, LargeNumberCustom y) {
                if (IsZero(x) || IsZero(y)) return 0;

                LargeNumberCustom result = new LargeNumberCustom();
                int maxDigits = Math.Min(y._digits.Count, MAX_DIGITS);

                for (int i = 0; i < maxDigits; i++) {
                    byte yDigit = y._digits[y._digits.Count - i - 1];
                    if (yDigit != 0) {
                        LargeNumberCustom partialProd = MultiplyByOneDigit(x, yDigit);
                        for (int j = 0; j < i; j++)
                            partialProd._digits.Add(0);
                        result = AddPositives(result, partialProd);
                    }
                }

                result._sign = x._sign * y._sign;

                return result;
            }

            public static LargeNumberCustom Power(LargeNumberCustom @base, int exp) {
                LargeNumberCustom result = 1;

                for (int i = 0; i < exp; i++) {
                    result = Multiply(result, @base);
                }

                return result;
            }

            private static LargeNumberCustom MultiplyByOneDigit(LargeNumberCustom x, byte y) {
                if (IsZero(x) || y == 0) return 0;

                LargeNumberCustom result = new LargeNumberCustom();
                int carry = 0;
                int maxDigits = Math.Min(x._digits.Count, MAX_DIGITS);

                for (int i = 0; i < maxDigits; i++) {
                    int prod = x._digits[x._digits.Count - i - 1] * y + carry;
                    result._digits.Insert(0, (byte)(prod % 10));
                    carry = prod / 10;
                }

                if (carry > 0)
                    result._digits.Insert(0, (byte)carry);

                return result;
            }

            private static bool IsZero(LargeNumberCustom n) {
                return n._digits.All(d => d == 0);
            }

            public static LargeNumberCustom AddPositives(LargeNumberCustom x, LargeNumberCustom y) {
                LargeNumberCustom result = new LargeNumberCustom();
                int maxDigits = Math.Max(x._digits.Count, y._digits.Count);
                maxDigits = Math.Min(maxDigits, MAX_DIGITS);
                result._digits = new List<byte>(maxDigits + 1);
                int carry = 0;

                for (int i = 0; i < maxDigits; i++) {
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

            public override string ToString() {
                if (IsZero(this))
                    return "0";

                string result = (_sign < 0 ? "-" : "") + string.Join("", _digits).TrimStart('0');

                return result;
            }
        }
    }
}
