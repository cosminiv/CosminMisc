using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Leet
{
    // Given a string, determine if it is a palindrome, considering only alphanumeric characters and ignoring cases.
    // For the purpose of this problem, we define empty string as valid palindrome.
    class Leet_125
    {
        public void Test() {
            string[] texts = new[] { "", "a", ".", ".;", ".;1", ".;1A", ".;17--1", "abA", "xxyyXx",
                "A man, a plan, a canal: Panama"};
            foreach (string txt in texts) {
                Debug.Print($"{txt}   {IsPalindrome(txt)}");
            }
        }

        public bool IsPalindrome(string s) {
            if (s == "") return true;

            int start = 0;
            int end = s.Length - 1;

            while (start < end) {
                while (start < end && !IsAlphanum(s[start]))
                    start++;
                while (start < end && !IsAlphanum(s[end]))
                    end--;

                if (start >= s.Length || end < 0)
                    break;

                if (!AreEqual(s[start], s[end]))
                    return false;

                start++;
                end--;
            }

            return true;
        }

        private bool IsAlphanum(char c) {
            return IsDigit(c) || IsLowercaseLetter(c) || IsUppercaseLetter(c);
        }

        bool IsDigit(char c) => c >= '0' && c <= '9';
        bool IsLowercaseLetter(char c) => c >= 'a' && c <= 'z';
        bool IsUppercaseLetter(char c) => c >= 'A' && c <= 'Z';

        private bool AreEqual(char c1, char c2) {
            if (c1 == c2) return true;
            if (IsUppercaseLetter(c1) && IsLowercaseLetter(c2) && c1 - 'A' == c2 - 'a') return true;
            if (IsUppercaseLetter(c2) && IsLowercaseLetter(c1) && c1 - 'a' == c2 - 'A') return true;
            return false;
        }
    }
}
