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
            Dictionary<int, List<string>> map = new Dictionary<int, List<string>>();

            foreach (var str in strs) {
                int hash = Hash(str); 

                if (!map.TryGetValue(hash, out List<string> lst)) {
                    lst = new List<string>();
                    map[hash] = lst;
                }
                lst.Add(str);
            }

            IList<IList<string>> result = map.Select(kvp => kvp.Value as IList<string>).ToList();

            return result;
        }

        int Hash(string str) {
            // Counting sort
            int[] charCounts = new int['z' - 'a' + 1];

            foreach (char ch in str) {
                int index = ch - 'a';
                charCounts[index]++;
            }

            // Hash
            int hash = 19;

            for (int chIdx = 0; chIdx < charCounts.Length; chIdx++) {
                for (int count = 0; count < charCounts[chIdx]; count++) {
                    unchecked {
                        hash = hash * 31 + chIdx;
                    }
                }
            }

            return hash;
        }
    }
}
