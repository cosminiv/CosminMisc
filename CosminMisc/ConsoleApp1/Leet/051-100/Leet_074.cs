using System;
using System.Collections.Generic;

namespace ConsoleApp1.Leet
{
    public class Leet_074
    {
        public void Solve()
        {
            int[][] matrix =
            {
                new[] {1, 3, 5, 7},
                new[] {10, 11, 16, 20},
                new[] {23, 30, 34, 50}
            };

            bool res = SearchMatrix(matrix, 3);
        }

        public bool SearchMatrix(int[][] matrix, int target)
        {
            if (matrix.Length == 0) return false;
            if (matrix[0].Length == 0) return false;

            SearchInArraysComparer searchInArraysComparer = new SearchInArraysComparer();

            int[] targetAsArray = {target};
            int rowIndex = Array.BinarySearch(matrix, targetAsArray, searchInArraysComparer);

            if (rowIndex < 0)
                return false;

            int colIndex = Array.BinarySearch(matrix[rowIndex], target);

            return colIndex >= 0;
        }

        class SearchInArraysComparer : IComparer<int[]>
        {
            public int Compare(int[] x, int[] y)
            {
                bool isXinY = x[0] >= y[0] && x[0] <= y[y.Length - 1];
                bool isYinX = y[0] >= x[0] && y[0] <= x[x.Length - 1];

                if (isXinY || isYinX) return 0;
                return x[0] - y[0];
            }
        }
    }
}
