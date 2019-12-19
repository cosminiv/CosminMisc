using System.Diagnostics;
using System.Text;

namespace ConsoleApp1.Common
{
    public class Tools
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
    }
}
