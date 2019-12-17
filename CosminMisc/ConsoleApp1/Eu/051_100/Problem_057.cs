using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Eu._Common;

namespace ConsoleApp1.Eu._051_100
{
    public class Problem_057
    {
        static readonly int MAX = 1000;

        public static int Solve() {
            int result = 0;
            Fraction prevFraction = new Fraction(1, 2);

            for (int n = 2; n <= MAX; n++) {
                Fraction fraction = ComputeFraction(prevFraction);
                Fraction expansion = AddOne(fraction);

                //Console.WriteLine($"{n}: {expansionValue.Numerator} / {expansionValue.Denominator}");

                if (expansion.Numerator.DigitCount > expansion.Denominator.DigitCount)
                    result++;

                prevFraction = fraction;
            }

            return result;
        }

        // Compute 1 / (2 + prevFractionTerm)
        private static Fraction ComputeFraction(Fraction prevFraction) {
            Fraction f = prevFraction;
            return new Fraction(f.Denominator, 2 * f.Denominator + f.Numerator);
        }

        private static Fraction AddOne(Fraction f) {
            return new Fraction(f.Denominator + f.Numerator, f.Denominator);
        }

        class Fraction
        {
            public LargeNumber Numerator;
            public LargeNumber Denominator;

            public Fraction(LargeNumber num, LargeNumber denom) {
                Numerator = num;
                Denominator = denom;
            }
        }
    }
}
