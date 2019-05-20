using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.HackerRank
{
    class FrequencyQueries
    {
        static Dictionary<int, int> _freqMap = new Dictionary<int, int>();
        static Dictionary<int, HashSet<int>> _reverseFreqMap = new Dictionary<int, HashSet<int>>();

        public static void Test() {
            var queries = new List<List<int>> {
                new List<int> { 1, 1 },
                new List<int> { 2, 2 },
                new List<int> { 3, 2 },
                new List<int> { 1, 1 },
                new List<int> { 1, 1 },
                new List<int> { 2, 1 },
                new List<int> { 3, 2 },
            };

            var res = freqQuery(queries);
        }

        static List<int> freqQuery(List<List<int>> queries) {
            List<int> result = new List<int>();

            foreach (var query in queries) {
                int op = query[0];
                int n = query[1];

                if (op == 1)
                    AddNumber(n);
                else if (op == 2)
                    RemoveNumber(n);
                else if (op == 3) 
                    result.Add(QueryFreq(n));
            }

            return result;
        }

        private static void AddNumber(int n) {
            int freq = 1;
            if (_freqMap.TryGetValue(n, out int existingFreq))
                freq = existingFreq + 1;
            _freqMap[n] = freq;

            if (!_reverseFreqMap.TryGetValue(freq, out HashSet<int> list)) {
                list = new HashSet<int>();
                _reverseFreqMap.Add(freq, list);
            }
            list.Add(n);

            if (freq > 1 && _reverseFreqMap.ContainsKey(freq - 1))
                _reverseFreqMap[freq - 1].Remove(n);
        }

        private static void RemoveNumber(int n) {
            int freq = 0;
            if (_freqMap.TryGetValue(n, out int existingFreq))
                freq = existingFreq > 0 ? existingFreq - 1 : 0;
            _freqMap[n] = freq;

            if (existingFreq > 0) {
                _reverseFreqMap[existingFreq].Remove(n);
                if (existingFreq > 1)
                    _reverseFreqMap[existingFreq - 1].Add(n);
            }
        }

        private static int QueryFreq(int f) {
            if (_reverseFreqMap.TryGetValue(f, out HashSet<int> list) && list.Any())
                return 1;
            else
                return 0;
        }
    }
}
