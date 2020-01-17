using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Leet
{
    class Leet_003
    {
        public int Solve() {
            var a1 = LengthOfLongestSubstring(null); Debug.Assert(a1 == 0);
            var a2 = LengthOfLongestSubstring(""); Debug.Assert(a2 == 0);
            var a3 = LengthOfLongestSubstring("a"); Debug.Assert(a3 == 1);
            var a4 = LengthOfLongestSubstring("aa"); Debug.Assert(a4 == 1);
            var a5 = LengthOfLongestSubstring("abb"); Debug.Assert(a5 == 2);
            var a6 = LengthOfLongestSubstring("aab"); Debug.Assert(a6 == 2);
            var a7 = LengthOfLongestSubstring("aabc"); Debug.Assert(a7 == 3);
            var a8 = LengthOfLongestSubstring("abcc"); Debug.Assert(a8 == 3);
            var a9 = LengthOfLongestSubstring("abbcc"); Debug.Assert(a9 == 2);
            var a10 = LengthOfLongestSubstring("abcd"); Debug.Assert(a10 == 4);
            var a11 = LengthOfLongestSubstring("abcdd"); Debug.Assert(a11 == 4);
            var a12 = LengthOfLongestSubstring("abcabcbb"); Debug.Assert(a12 == 3);
            var a13 = LengthOfLongestSubstring("bbbbb"); Debug.Assert(a13 == 1);
            var a14 = LengthOfLongestSubstring("pwwkew"); Debug.Assert(a14 == 3);
            var a15 = LengthOfLongestSubstring("ab"); Debug.Assert(a15 == 2);

            return 0;
        }

        public int LengthOfLongestSubstring(string s) {
            if (s == null) return 0;
            if (s.Length < 2) return s.Length;

            int start = 0;
            int end = 1;
            int len = 1;
            int maxLen = 1;

            Dictionary<char, int> crtChars = new Dictionary<char, int> { { s[0], 0 } };

            while (start < s.Length && end < s.Length) {
                char c = s[end];
                if (!crtChars.ContainsKey(c)) {
                    crtChars.Add(c, end);
                    len++;
                    end++;
                }
                else {
                    if (len > maxLen)
                        maxLen = len;
                    int firstRepeatedIndex = crtChars[c];

                    // Delete chars before and including the first occurrence of c
                    for (int i = start; i <= firstRepeatedIndex; i++) 
                        crtChars.Remove(s[i]);

                    crtChars.Add(c, end);
                    start = firstRepeatedIndex + 1;
                    len = end - start + 1;
                    end++;
                }
            }

            if (len > maxLen)
                maxLen = len;

            return maxLen;
        }
    }
}
