using System.Collections.Generic;
using System.Linq;
using ConsoleApp1.Common;

namespace ConsoleApp1.Leet._051_100
{
    public class Leet_077
    {
        int[] _indexes;

        public void Solve()
        {
            var result = Combine(5, 3);
        }

        public IList<IList<int>> Combine(int n, int k)
        {
            List<IList<int>> result = new List<IList<int>>();

            foreach (int[] indexes in EnumerateIndexes(n, k))
            {
                int[] list = new int[k];
                for (int i = 0; i < k; i++)
                    list[i] = indexes[i] + 1;

                //Tools.PrintCollection(list);

                result.Add(list);
            }
            
            return result;
        }

        IEnumerable<int[]> EnumerateIndexes(int n, int k)
        {
            _indexes = Enumerable.Range(0, k).ToArray();
            yield return _indexes;

            while (true)
            {
                if (_indexes[k - 1] < n - 1)
                {
                    _indexes[k - 1]++;
                    yield return _indexes;
                }
                else
                {
                    bool foundNextState = false;

                    for (int i = 2; i <= k; i++)
                    {
                        if (_indexes[k - i] < n - i)
                        {
                            _indexes[k - i]++;

                            // set next indexes
                            for (int j = k - i + 1; j < k; j++)
                            {
                                _indexes[j] = _indexes[j - 1] + 1;
                            }

                            foundNextState = true;
                            break;
                        }
                    }

                    if (foundNextState)
                        yield return _indexes;
                    else
                        yield break;
                }
            }
        }
    }
}
