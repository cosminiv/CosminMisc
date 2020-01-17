using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1.Leet
{
    public class Leet_056
    {
        public int[][] Solve()
        {
            var result = Merge(new[] { new[]{1, 3}, new[] { 2, 6}, new[] { 8, 10}, new[] { 15, 18} });
            return result;
        }

        public int[][] Merge(int[][] intervals)
        {
            List<int[]> result = new List<int[]>();
            intervals = intervals.OrderBy(x => x[0]).ToArray();
            int[] prevInterval = null;

            foreach (var interval in intervals)
            {
                if (prevInterval != null)
                {
                    bool isIntersect = interval[0] <= prevInterval[1];

                    if (!isIntersect)
                    {
                        result.Add(prevInterval);
                        prevInterval = interval;
                    }
                    else
                    {
                        if (interval[1] > prevInterval[1])
                            prevInterval[1] = interval[1];
                    }
                }
                else
                {
                    prevInterval = interval;
                }
            }

            if (prevInterval != null)
                result.Add(prevInterval);

            return result.ToArray();
        }
    }
}
