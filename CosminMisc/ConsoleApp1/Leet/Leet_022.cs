using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Leet
{
    public class Leet_022
    {
        Dictionary<int, List<string>> _cache = new Dictionary<int, List<string>>();

        public void Test() {
            var res = GenerateParenthesis(4);
            string resStr = string.Join(",", res.Select(r => $"\"{r}\""));
            var res2 = GenerateParenthesis(2);
        }

        public List<string> GenerateParenthesis(int n) {
            bool foundInCache = _cache.TryGetValue(n, out List<string> result);
            if (foundInCache)
                return result;

            //Console.Write($"{n} ");

            List<string> ans = new List<string>();
            if (n == 0) {
                ans.Add("");
            }
            else {
                for (int c = 0; c < n; ++c)
                    foreach (string left in GenerateParenthesis(c))
                        foreach (string right in GenerateParenthesis(n - 1 - c))
                            ans.Add("(" + left + ")" + right);
            }

            _cache.Add(n, ans);
            return ans;
        }

        public IList<string> GenerateParenthesis_Incremental(int n) {
            List<string> prevSizeResults = new List<string>() { "" };
            HashSet<string> resultHash = new HashSet<string>(prevSizeResults);
            if (!_cache.ContainsKey(0))
                _cache.Add(0, prevSizeResults);

            for (int i = 1; i <= n; i++) {
                bool foundInCache = _cache.TryGetValue(i, out List<string> crtSizeResults);

                if (!foundInCache) {
                    crtSizeResults = new List<string>();

                    foreach (string prevSizeResult in prevSizeResults) {
                        string[] candidates = {
                            "(" + prevSizeResult + ")", prevSizeResult + "()", "()" + prevSizeResult
                        };

                        foreach (string candidate in candidates) {
                            if (!resultHash.Contains(candidate)) {
                                crtSizeResults.Add(candidate);
                                resultHash.Add(candidate);
                            }
                        }
                    }

                    _cache.Add(i, crtSizeResults);
                }

                prevSizeResults = crtSizeResults;
            }

            return prevSizeResults;
        }
    }
}
