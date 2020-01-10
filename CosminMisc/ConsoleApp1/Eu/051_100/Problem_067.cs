using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApp1.Eu._051_100
{
    public class Problem_067
    {
        public long Solve()
        {
            int[][] data = ParseData(File.ReadAllText(@"C:\Temp\p067_triangle.txt"));
            int[] solutionRow = null;
            int[] prevSolutionRow = { data[0][0] };
            List<int> solutionIndexes = new List<int> {0};

            for (var row = 1; row < data.Length; row++)
            {
                int[] dataRow = data[row];
                solutionRow = new int[dataRow.Length];
                solutionRow[0] = prevSolutionRow[0] + dataRow[0];
                solutionRow[solutionRow.Length - 1] = prevSolutionRow[prevSolutionRow.Length - 1] + dataRow[dataRow.Length - 1];

                for (int col = 1; col < solutionRow.Length - 1; col++)
                {
                    if (prevSolutionRow[col - 1] >= prevSolutionRow[col])
                    {
                        solutionRow[col] = prevSolutionRow[col - 1] + dataRow[col];
                        solutionIndexes.Add(col - 1);
                    }
                    else
                    {
                        solutionRow[col] = prevSolutionRow[col] + dataRow[col];
                        solutionIndexes.Add(col);
                    }
                }

                prevSolutionRow = solutionRow;
            }

            //PrintSolutionPath(data, solutionIndexes);

            int max = solutionRow.Max();
            return max;
        }

        private void PrintSolutionPath(int[][] data, List<int> solutionIndexes)
        {
            Debug.Assert(data.Length == solutionIndexes.Count);
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(' ', solutionIndexes[i] * 3);
                sb.Append(data[i][solutionIndexes[i]]);
                sb.AppendLine();
            }

            Debug.Print(sb.ToString());
        }

        private static int[][] ParseData(string dataStr)
        {
            string[] lines = dataStr.Split('\n');
            int nonEmptyLineCount = lines.Count(line => !string.IsNullOrWhiteSpace(line));
            int[][] data = new int[nonEmptyLineCount][];
            int i = 0;

            foreach (string line in lines)
            {
                string trimmedLine = line.Trim();
                if (trimmedLine.Length > 0)
                {
                    data[i++] = trimmedLine.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                        .Select(a => int.Parse(a)).ToArray();
                }
            }

            return data;
        }
    }
}
