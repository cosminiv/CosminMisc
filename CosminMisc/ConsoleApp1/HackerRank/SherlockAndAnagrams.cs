using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.HackerRank
{
    public class SherlockAndAnagrams
    {
        public static void Test() {
            int a = Solve("abba");
            Debug.Assert(Solve("abba") == 4);

            Debug.Assert(Solve("a") == 0);
            Debug.Assert(Solve("aa") == 1);
            Debug.Assert(Solve("ab") == 0);
            Debug.Assert(Solve("abc") == 0);
            Debug.Assert(Solve("aac") == 1);
            Debug.Assert(Solve("axya") == 2);
            //Debug.Assert(Solve("xayz*a$-a") == 3);
            Debug.Assert(Solve("aba") == 2);
            Debug.Assert(Solve("ifailuhkqq") == 3);
            Debug.Assert(Solve("kkkk") == 10);
        }

        static int Solve(string text) {
            return sherlockAndAnagrams(text);
        }

        public static int sherlockAndAnagrams(string text) {
            int result = CountOneLetterSolutions(text);
            if (result == 0)
                return 0;

            for (int solutionSize = 2; solutionSize < text.Length; solutionSize++) {
                int solCount = CountSolutionsOfSize(text, solutionSize);
                result += solCount;
            }

            return result;
        }

        private static int CountSolutionsOfSize(string text, int size) {
            // Map from normalized anagram hashcode to anagram index
            Dictionary<int, List<int>> map = new Dictionary<int, List<int>>();
            int solCount = 0;

            // Build map
            for (int i = 0; i < text.Length - size + 1; i++) {
                var substrChars = text.Substring(i, size).ToCharArray();
                var substrCharsOrdered = substrChars.OrderBy(c => c).ToArray();
                string str = new string(substrCharsOrdered);
                int strHash = str.GetHashCode();
                
                if (!map.TryGetValue(strHash, out List<int> indexList)) {
                    indexList = new List<int>();
                    map[strHash] = indexList;
                }
                else {
                    solCount += indexList.Count;
                }

                indexList.Add(i);
            }

            return solCount;
        }

        private static int CountOneLetterSolutions(string text) {
            Dictionary<char, List<int>> map = new Dictionary<char, List<int>>();
            int solCount = 0;

            // Build map from char to index list
            for (int i = 0; i < text.Length; i++) {
                char ch = text[i];
                List<int> indexList;
                if (!map.TryGetValue(ch, out indexList)) {
                    indexList = new List<int>();
                    map[ch] = indexList;
                }
                else {
                    solCount += indexList.Count;
                }
                indexList.Add(i);
            }

            return solCount;
        }

    }
}
