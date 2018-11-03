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
            Solve("(()");

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
            int openParen = 0;
            int candidateLen = 0;
            int maxLen = 0;

            for (int i = 0; i < input.Length; i++) {
                char c = input[i];
                if (c == '(') {
                    openParen++;
                    candidateLen++;
                }
                else if (c == ')') {
                    if (openParen > 0) {
                        openParen--;
                        candidateLen++;
                    }

                    if (i == input.Length - 1) {
                        candidateLen -= openParen;
                        if (candidateLen > maxLen)
                            maxLen = candidateLen;
                    }

                    if (openParen == 0) {
                        if (candidateLen > maxLen)
                            maxLen = candidateLen;

                        candidateLen = 0;
                    }
                }
            }

            return maxLen;
        }
    }
}
