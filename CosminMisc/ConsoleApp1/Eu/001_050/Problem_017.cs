using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Eu._001_050
{
    public class Problem_017
    {
        static readonly Dictionary<int, string> _basicWords = MakeBasicWords();
        static StringBuilder _strBuilder = new StringBuilder();

        public static int Solve()
        {
            int length = 0;

            for (int i = 1; i <= 1000; i++)
            {
                string numberText = MakeNumberText(i);
                string numberStripped = numberText.Replace(" ", "").Replace("-", "");
                length += numberStripped.Length;
                //Debug.Print($"{i}: {numberText}");
            }

            return length;
        }

        private static string MakeNumberText(int n)
        {
            _strBuilder.Clear();
            bool foundHundreds = false;
            bool foundTens = false;

            int thousands = n / 1000;
            if (thousands > 0)
            {
                _strBuilder.Append(_strBuilder.Length > 0 ? " " : "")
                    .Append(_basicWords[thousands]).Append(" ").Append(_basicWords[1000]);
                n -= thousands * 1000;
                foundHundreds = true;
            }

            int hundreds = n / 100;
            if (hundreds > 0)
            {
                _strBuilder.Append(_strBuilder.Length > 0 ? " " : "")
                    .Append(_basicWords[hundreds]).Append(" ").Append(_basicWords[100]);
                n -= hundreds * 100;
                foundHundreds = true;
            }

            int tens = n / 10;
            if (tens >= 2)
            {
                _strBuilder.Append(_strBuilder.Length > 0 ? " and " : "")
                    .Append(_basicWords[tens * 10]);
                n -= tens * 10;
                foundTens = true;
            }
            else if (tens == 1)
            {
                foundTens = true;
                _strBuilder.Append(_strBuilder.Length > 0 ? " and " : "").Append(_basicWords[10 + n % 10]);
                return _strBuilder.ToString();
            }

            int units = n % 10;
            if (units > 0)
            {
                string separator = (foundHundreds && !foundTens) ? " and " : " ";
                if (foundTens && units > 0)
                    separator = "-";
                _strBuilder.Append(_strBuilder.Length > 0 ? separator : "").Append(_basicWords[units]);
            }

            return _strBuilder.ToString();
        }

        private static Dictionary<int, string> MakeBasicWords()
        {
            return new Dictionary<int, string>
            {
                { 1, "one" },
                { 2, "two" },
                { 3, "three" },
                { 4, "four" },
                { 5, "five" },
                { 6, "six" },
                { 7, "seven" },
                { 8, "eight" },
                { 9, "nine" },
                { 10, "ten" },
                { 11, "eleven" },
                { 12, "twelve" },
                { 13, "thirteen" },
                { 14, "fourteen" },
                { 15, "fifteen" },
                { 16, "sixteen" },
                { 17, "seventeen" },
                { 18, "eighteen" },
                { 19, "nineteen" },
                { 20, "twenty" },
                { 30, "thirty" },
                { 40, "forty" },
                { 50, "fifty" },
                { 60, "sixty" },
                { 70, "seventy" },
                { 80, "eighty" },
                { 90, "ninety" },
                { 100, "hundred" },
                { 1000, "thousand" },
            };
        }
    }
}
