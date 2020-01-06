using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace ConsoleApp1.Common
{
    public static class Tools
    {
        public static string GetCaller([CallerMemberName] string caller = null)
        {
            return caller;
        }

        public static int ComputeHashCode<T>(this IEnumerable<T> list)
        {
            unchecked
            {
                int hash = 19;
                foreach (var x in list)
                    hash = hash * 31 + x.GetHashCode();
                return hash;
            }
        }

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

        public static T[][] MakeMatrix<T>(int rows, int cols)
        {
            T[][] result = new T[rows][];

            for (var index = 0; index < result.Length; index++)
            {
                result[index] = new T[cols];
            }

            return result;
        }
    }
}
