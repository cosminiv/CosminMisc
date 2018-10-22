using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.GCJ
{
    class Year_2017_Qualif_C
    {
        public static string Solve(string line) {
            long[] t = line.Split(' ').Select(x => long.Parse(x)).ToArray();
            Solution sol = Solve(t[0], t[1], true);
            string res = $"{sol.Max} {sol.Min}";
            return res;
        }

        static Solution Solve(long stalls, long persons, bool debug) {
            if (debug) Console.WriteLine($"\n0: ({stalls} 1)");

            List<StallGroup> groups = new List<StallGroup> {
                new StallGroup { Size = stalls, Count = 1 }
            };

            long min = 0, max = 0;
            long persDelta = 1;
            int steps = 0;

            for (long pers = 1; pers <= persons; pers += persDelta) {
                steps++;
                long prevMaxSize = groups[0].Size;
                if (prevMaxSize == 1) {
                    if (debug) PrintStallList(groups, pers);
                    min = max = 0;
                    break;
                }
                min = (prevMaxSize - 1) / 2;
                max = prevMaxSize - 1 - min;

                if (max > 0) AddToOrderedList(groups, max, persDelta);
                if (min > 0) AddToOrderedList(groups, min, persDelta);

                groups[0].Count -= persDelta;
                if (groups[0].Count == 0)
                    groups.RemoveAt(0);

                if (debug) PrintStallList(groups, pers);
                persDelta = (long)Math.Min(persons - pers, groups[0].Count);
                if (persDelta <= 0) persDelta = 1;
            }

            if (debug) Console.WriteLine($"{steps} steps");
            return new Solution { Min = min, Max = max };
        }

        static void PrintStallList(List<StallGroup> groups, long persons) {
            Console.WriteLine($"{persons}: " + string.Join(" ", groups));
        }

        static void AddToOrderedList(List<StallGroup> groups, long groupSize, long groupCount) {
            for (int i = 0; i < groups.Count; i++) {
                if (groups[i].Size == groupSize) {
                    groups[i].Count += groupCount;
                    return;
                }
                else if (groups[i].Size < groupSize) {
                    groups.Insert(i, new StallGroup { Size = groupSize, Count = groupCount });
                    return;
                }
            }
            groups.Add(new StallGroup { Size = groupSize, Count = groupCount });
        }

        class StallGroup
        {
            public long Size;
            public long Count;

            public override string ToString() {
                return $"({Size} {Count})";
            }
        }

        class Solution
        {
            public long Max;
            public long Min;
        }


    }
}
