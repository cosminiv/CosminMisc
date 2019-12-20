using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ConsoleApp1.Common
{
    public static class Tools
    {
        public static void PrintMatrix(int[][] matrix)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    sb.Append($"{matrix[i][j]}  ");
                }

                sb.AppendLine();
            }

            Debug.Print(sb.ToString());
        }

        public static List<T> CopyAndAdd<T>(this List<T> sourceList, T newElement)
        {
            List<T> result = new List<T>(sourceList.Count + 1);

            foreach (T elem in sourceList)
            {
                result.Add(elem);
            }

            result.Add(newElement);
            return result;
        }
    }
}
