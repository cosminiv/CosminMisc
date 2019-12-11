using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;

namespace ConsoleApp1.Leet
{
    [TestFixture]
    public class Leet_057
    {
        [Test]
        public void Solve()
        {
            Testcase[] testcases = 
            {
                new Testcase { Intervals = new int[0], NewInterval = new int[0], Expected = new int[0], Name = "No intervals, no new interval" },
                new Testcase { Intervals = new int[0], NewInterval = new[]{1, 2}, Expected = new[]{ 1, 2}, Name = "No intervals" },
                new Testcase { Intervals = new[]{ 4, 5 }, NewInterval = new int[0], Expected = new[]{ 4, 5}, Name = "No new interval" },

                new Testcase { Intervals = new[]{ 3, 7, 10, 15 }, NewInterval = new[]{1, 2}, Expected = new[]{ 1, 2, 3, 7, 10, 15}, Name = "Insert at beginning, no intersect" },
                new Testcase { Intervals = new[]{ 1, 3, 7, 9 }, NewInterval = new[]{4, 5}, Expected = new[]{ 1, 3, 4, 5, 7, 9}, Name = "Insert in the middle, no intersect" },
                new Testcase { Intervals = new[]{ 1, 3, 4, 5 }, NewInterval = new[]{7, 8}, Expected = new[]{ 1, 3, 4, 5, 7, 8}, Name = "Insert at the end, no intersect"},

                new Testcase { Intervals = new[]{ 3, 7, 10, 15 }, NewInterval = new[]{1, 3}, Expected = new[]{ 1, 7, 10, 15}, Name = "Insert at beginning, new first" },
                new Testcase { Intervals = new[]{ 3, 7, 10, 15 }, NewInterval = new[]{1, 4}, Expected = new[]{ 1, 7, 10, 15}, Name = "Insert at beginning, new first 2" },
                new Testcase { Intervals = new[]{ 3, 7, 10, 15 }, NewInterval = new[]{4, 6}, Expected = new[]{ 3, 7, 10, 15}, Name = "Insert at beginning, new second included" },
                new Testcase { Intervals = new[]{ 3, 7, 10, 15 }, NewInterval = new[]{4, 8}, Expected = new[]{ 3, 8, 10, 15}, Name = "Insert at beginning, new second not included" },

                new Testcase { Intervals = new[]{ 3, 7, 10, 15 }, NewInterval = new[]{9, 10}, Expected = new[]{ 3, 7, 9, 15}, Name = "Insert at end, new first" },
                new Testcase { Intervals = new[]{ 3, 7, 10, 15 }, NewInterval = new[]{9, 11}, Expected = new[]{ 3, 7, 9, 15}, Name = "Insert at end, new first 2" },
                new Testcase { Intervals = new[]{ 3, 7, 10, 15 }, NewInterval = new[]{13, 15}, Expected = new[]{ 3, 7, 10, 15}, Name = "Insert at end, new second included" },
                new Testcase { Intervals = new[]{ 3, 7, 10, 15 }, NewInterval = new[]{12, 20}, Expected = new[]{ 3, 7, 10, 20}, Name = "Insert at end, new second not included" },

                new Testcase { Intervals = new[]{ 3, 7, 10, 15 }, NewInterval = new[]{4, 11}, Expected = new[]{ 3, 15}, Name = "Multiple intersects" },
                new Testcase { Intervals = new[]{ 3, 7, 10, 15 }, NewInterval = new[]{4, 20}, Expected = new[]{ 3, 20}, Name = "Multiple intersects 2" },
                new Testcase { Intervals = new[]{ 3, 7, 10, 15, 20, 25 }, NewInterval = new[]{4, 22}, Expected = new[]{ 3, 25}, Name = "Multiple intersects 3" },
            };

            foreach (Testcase testcase in testcases)
            {
                RunTestcase(testcase);
            }
        }
        
        void RunTestcase(Testcase testcase)
        {
            // Arrange
            int[][] intervalsAsArrays = MakeArrays(testcase.Intervals);
            int[][] expectedAsArrays = MakeArrays(testcase.Expected);

            // Act
            int[][] result = Insert(intervalsAsArrays, testcase.NewInterval);

            // Assert
            Assert.AreEqual(expectedAsArrays.Length, result.Length, testcase.Name);

            for (var i = 0; i < result.Length; i++)
            {
                int[] interval = result[i];
                int[] expected = expectedAsArrays[i];

                Assert.AreEqual(expected[0], interval[0], testcase.Name);
                Assert.AreEqual(expected[1], interval[1], testcase.Name);
            }
        }

        private int[][] MakeArrays(int[] intervalsOneArray)
        {
            Debug.Assert(intervalsOneArray.Length % 2 == 0);
            int[][] result = new int[intervalsOneArray.Length / 2][];

            for (int i = 0; i < intervalsOneArray.Length; i+=2)
            {
                result[i / 2] = new int[2];
                result[i / 2][0] = intervalsOneArray[i];
                result[i / 2][1] = intervalsOneArray[i + 1];
            }

            return result;
        }

        public int[][] Insert(int[][] intervals, int[] newInterval)
        {
            if (intervals.Length == 0) return newInterval.Length == 0 ? new int[0][] : new []{ newInterval };
            if (newInterval.Length == 0) return intervals;

            List<int[]> result = new List<int[]>(intervals.Length + 1);
            bool addedNewInterval = false;

            foreach (int[] interval in intervals)
            {
                bool isBeforeNewInterval = interval[1] < newInterval[0];
                bool isAfterNewInterval = interval[0] > newInterval[1];

                if (isAfterNewInterval && !addedNewInterval)
                {
                    result.Add(newInterval);
                    addedNewInterval = true;
                }

                if (isBeforeNewInterval || isAfterNewInterval)
                    result.Add(interval);

                bool intersectsNewAtBeginning = newInterval[0] >= interval[0] && newInterval[0] <= interval[1];
                bool intersectsNewAtEnd = newInterval[1] >= interval[0] && newInterval[1] <= interval[1];

                if (intersectsNewAtBeginning)
                    newInterval[0] = interval[0];

                if (intersectsNewAtEnd)
                    newInterval[1] = interval[1];
            }

            if (!addedNewInterval && newInterval.Length > 0)
                result.Add(newInterval);

            return result.ToArray();
        }

        class Testcase
        {
            public int[] Intervals;
            public int[] NewInterval;
            public int[] Expected;
            public string Name;

            public override string ToString()
            {
                return Name;
            }
        }
    }
}
