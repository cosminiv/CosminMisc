using System.Collections.Generic;

namespace ConsoleApp1.Leet
{
    class Leet_073
    {
        public void SetZeroes(int[][] matrix)
        {
            HashSet<int> rowsWithZeros = new HashSet<int>();
            HashSet<int> columnsWithZeros = new HashSet<int>();

            int rows = matrix.Length;
            int cols = matrix[0].Length;

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    if (matrix[row][col] == 0)
                    {
                        rowsWithZeros.Add(row);
                        columnsWithZeros.Add(col);
                    }
                }
            }

            foreach (int row in rowsWithZeros)
            {
                for (int col = 0; col < cols; col++)
                {
                    matrix[row][col] = 0;
                }
            }

            foreach (int col in columnsWithZeros)
            {
                for (int row = 0; row < rows; row++)
                {
                    matrix[row][col] = 0;
                }
            }
        }
    }
}
