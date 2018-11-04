using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Leet
{
    public class Leet_032
    {
        public int Solve() {
            var testCases = new[] {
                ("", 0),
                ("(", 0),
                (")", 0),
                ("((", 0),
                ("(()", 2),
                (")((", 0),
                (")(()", 2),
                (")(()(", 2),
                (")(())", 4),
                (")(()))", 4),
                (")(())(", 4),
                (")()())(", 4),
                (")()()))", 4),
                (")(()())(", 6),
                (")((((())(", 4),
                ("((()()))(", 8),
                (")((()()))(", 8),
            };

            foreach (var tc in testCases) {
                int result = Solve(tc.Item1);
                string expl = result == tc.Item2 ? "OK" : $"FAIL, expected {tc.Item2}";
                Debug.Print($"{tc.Item1.PadRight(10, ' ')}  {result}  {expl}");
            }

            return 0;
        }

        public int Solve(string input) {
            int[] parenPairs = ReadParenPairs(input);
            int maxLen = ComputeMaxLength(parenPairs, input);
            return maxLen;
        }

        private int ComputeMaxLength(int[] parenPairs, string input) {
            int maxLen = 0;
            int crtAdditiveLen = 0;
            int prevParenEndIndex = -1;

            for (int i = 0; i < parenPairs.Length; i++) {
                int parenStartIndex = i;
                int parenEndIndex = parenPairs[i];
                if ((parenStartIndex == 0 && parenEndIndex == -1) || 
                    (parenStartIndex > 0 && parenEndIndex == 0))
                    continue;

                int crtLen = (parenEndIndex - parenStartIndex + 1);

                if (prevParenEndIndex < 0 || prevParenEndIndex == i - 1)
                    crtAdditiveLen += crtLen;
                else {
                    if (crtAdditiveLen > maxLen)
                        maxLen = crtAdditiveLen;
                    crtAdditiveLen = crtLen;
                }

                prevParenEndIndex = parenEndIndex;
                i = parenEndIndex;
            }

            if (crtAdditiveLen > maxLen)
                maxLen = crtAdditiveLen;

            return maxLen;
        }

        int[] ReadParenPairs(string input) {
            List<int> parenStack = new List<int>();
            int[] parenPairs = new int[input.Length];
            if (parenPairs.Length > 0)
                parenPairs[0] = -1; 

            for (int i = 0; i < input.Length; i++) {
                char c = input[i];
                if (c == '(') {
                    parenStack.Add(i);
                }
                else {
                    if (parenStack.Count > 0) {
                        int startParenIndex = parenStack[parenStack.Count - 1];
                        parenPairs[startParenIndex] = i;
                        parenStack.RemoveAt(parenStack.Count - 1);
                    }
                }
            }

            return parenPairs;
        }
    }
}
