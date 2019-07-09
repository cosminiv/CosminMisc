using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Leet
{
    //
    // Given an array of strings, group anagrams together.
    //
    class Leet_049
    {
        public IList<IList<string>> GroupAnagrams(string[] strs) {
            Dictionary<string, List<string>> map = new Dictionary<string, List<string>>();

            foreach (var str in strs) {
                string sorted = CountSortLetters(str); 

                if (!map.TryGetValue(sorted, out List<string> lst)) {
                    lst = new List<string>();
                    map[sorted] = lst;
                }
                lst.Add(str);
            }

            IList<IList<string>> result = map.Select(kvp => kvp.Value as IList<string>).ToList();

            return result;
        }

        string CountSortLetters(string str) {
            int[] charCounts = new int['z' - 'a' + 1];
            foreach (char ch in str) {
                int index = ch - 'a';
                charCounts[index]++;
            }
            StringBuilder sb = new StringBuilder(str.Length);
            for (int chIdx = 0; chIdx < charCounts.Length; chIdx++) {
                char ch = (char)('a' + chIdx);
                for (int count = 0; count < charCounts[chIdx]; count++) {
                    sb.Append(ch);
                }
            }
            return sb.ToString();
        }
    }
}
