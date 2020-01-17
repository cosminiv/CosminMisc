using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Leet
{
    class Leet_010
    {
        // TODO: Does not work - not general enough
        public int Solve() {
            var tests = new[] {
                //("", "", true),
                //("", "a*", true),
                //("", ".*", true),
                //("", "a*.*", true),
                //("a", "", false),
                //("aa", "", false),
                //("a", "a", true),
                //("aa", "aa", true),
                //("aa", "a", false),
                //("ab", "ab", true),
                //("a", "a*", true),
                //("aaa", "a*", true),
                //("aaa", "a*c", false),
                //("ab", "a*b", true),
                //("aaab", "a*b", true),
                //("caaab", "a*b", false),
                //("a", ".", true),
                //("ab", "..", true),
                //("axoxb", "a...b", true),
                //("abc", ".*", true),
                //("aaaabtywefc", "a*b.*", true),
                //("aab", "c*a*b", true),
                //("mississippi", "mis*is*p*.", false),
                //("mississippi", "mis*is*ip*.", true),
                //("aaa", "a*a", true),
                //("aaa", "a*aa", true),
                //("aaaa", "a*aa", true),
                //("aaaab", "a*aab", true),
                //("ab", ".*c", false),
                //("aaa", "ab*a", false),  // ♠, ♣, ♦, and ♥)
                ("aaa", "ab*a*c*a", true),
            };

            foreach (var test in tests) {
                bool isMatch = IsMatch(test.Item1, test.Item2);
                Debug.Assert(isMatch == test.Item3, $"{test.Item1}, {test.Item2}: {isMatch}, expected {test.Item3}");
            }

            return 0;
        }

        public bool IsMatch(string s, string p) {
            if (p == "") return (s == "");
            
            List<RegexPart> regexParts = ParseRegex(p);
            int stringIndex = 0;
            int regexTokenIndex = 0;
            bool matchedInfinityChar = false;

            while (true) {
                if (stringIndex < s.Length) {
                    char ch = s[stringIndex];

                    if (regexTokenIndex < regexParts.Count) {
                        RegexPart regexPart = regexParts[regexTokenIndex];
                        bool isCharMatch = IsCharMatch(ch, regexPart.Character);
                        bool advanceInInput = true;


                        if (regexPart.MinCount > 0 && !isCharMatch)         // did not match
                            return false;
                        else if (isCharMatch && regexPart.MaxCount == 1)    //   exactly one char matched
                            regexTokenIndex++;
                        else if (!isCharMatch && (regexPart.MinCount == 0 || matchedInfinityChar)) {   // optional char did not match
                            regexTokenIndex++;
                            advanceInInput = false;
                        }

                        matchedInfinityChar = (isCharMatch && regexPart.MaxCount == int.MaxValue);

                        if (advanceInInput)
                            stringIndex++;
                    }
                    else {
                        // We reached the end of the regex
                        RegexPart lastRegexToken = regexParts[regexParts.Count - 1];
                        if (lastRegexToken.MaxCount < int.MaxValue)
                            return false;
                        if (!IsCharMatch(ch, lastRegexToken.Character))
                            return false;
                        stringIndex++;
                    }
                }
                else {
                    // We reached the end of the input string
                    if (regexTokenIndex < regexParts.Count) {
                        if (regexParts[regexTokenIndex].MinCount > 0)
                            return false;
                        regexTokenIndex++;
                    }
                    else
                        return true;
                }
            }
        }

        bool IsCharMatch(char ch, char regexChar) {
            return (ch == regexChar || regexChar == '.');
        }

        private List<RegexPart> ParseRegex(string p) {
            List<RegexPart> result = new List<RegexPart>();
            
            for (int i = 0; i < p.Length; i++) {
                char ch = p[i];
                if (result.Count > 0) {
                    RegexPart prevRegexPart = result[result.Count - 1];
                    if (ch == prevRegexPart.Character && prevRegexPart.MaxCount == int.MaxValue)
                        continue;   // character is included in previous regex part
                }

                RegexPart token = new RegexPart();
                token.Character = ch;

                if (i + 1 < p.Length && p[i+1] == '*') {
                    token.MinCount = 0;
                    token.MaxCount = int.MaxValue;
                    i++;
                }
                else {
                    token.MinCount = 1;
                    token.MaxCount = 1;
                }

                result.Add(token);
            }

            return result;
        }

        public class RegexPart
        {
            public char Character;
            public int MinCount;
            public int MaxCount;

            public override string ToString() {
                return $"{Character} ({MinCount},{MaxCount})";
            }
        }
    }
}
