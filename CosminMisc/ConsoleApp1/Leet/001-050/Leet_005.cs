using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Leet
{
    class Leet_005
    {
        public int Solve() {
            var tests = new[] {
                ((string)null, (string)null),
                ("", ""),
                ("a", "a"),
                ("aa", "aa"),
                ("ab", "a"),
                ("abc", "a"),
                ("aba", "aba"),
                ("abac", "aba"),
                ("xgag", "gag"),
                ("qwwABCDDCBA65767543465", "ABCDDCBA")
            };

            foreach (var test in tests) {
                string p = LongestPalindrome(test.Item1);
                Debug.Assert(p == test.Item2);
            }

            return 0;
        }

        public string LongestPalindrome(string s) {
            if (s == null) return null;
            if (s.Length == 0) return s;
            if (s.Length == 1) return s;

            for (int len = s.Length; len > 1; len--) {
                string pal = FindPalindrome(s, len);
                if (pal != null)
                    return pal;
            }

            return s[0].ToString();
        }

        private string FindPalindrome(string s, int len) {
            for (int offset = 0; offset <= s.Length - len; offset++) {
                int centerIndex = offset+ len / 2;

                bool isPalindrome = true;

                for (int i = 0; i < len/2; i++) {
                    char c1 = s[offset + i];
                    char c2 = s[offset + (len - i - 1)];
                    if (c1 != c2) {
                        isPalindrome = false;
                        break;
                    }
                }

                if (isPalindrome)
                    return s.Substring(offset, len);
            }

            return null;
        }
    }
}
