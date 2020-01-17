using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ConsoleApp1.Common;

namespace ConsoleApp1.Leet._051_100
{
    public class Leet_078
    {
        public void Solve()
        {
            var a = Subsets(new[] {1, 2, 3, 10, 17, 22});
        }

        public IList<IList<int>> Subsets(int[] nums)
        {
            List<IList<int>> result = new List<IList<int>>();
            result.Add(new List<int>());

            Leet_077 leet77 = new Leet_077();

            for (int size = 1; size <= nums.Length; size++)
            {
                IEnumerable<int[]> indexes = leet77.EnumerateIndexes(nums.Length, size);

                foreach (int[] indexArr in indexes)
                {
                    List<int> solution = new List<int>(size);
                    foreach (var index in indexArr)
                    {
                        solution.Add(nums[index]);
                    }
                    result.Add(solution);
                }
            }

            foreach (IList<int> list in result)
            {
                Tools.PrintCollection(list);
                //Debug.Write("\n");
            }

            return result;
        }
    }
}
