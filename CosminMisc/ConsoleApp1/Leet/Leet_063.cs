
namespace ConsoleApp1.Leet
{
    class Leet_063
    {
        public void Solve()
        {
            long a = UniquePathsWithObstacles(new[]
            {
                new [] { 0, 0, 0},
                new [] { 0, 1, 0},
                new [] { 0, 0, 0},
            });
        }

        public int UniquePathsWithObstacles(int[][] obstacleGrid)
        {
            int[][] matrix = MakeMatrix(obstacleGrid);
            //Common.Tools.PrintMatrix(matrix);
            return matrix[0][0];
        }

        private int[][] MakeMatrix(int[][] obstacleGrid)
        {
            int rows = obstacleGrid.Length;
            int cols = obstacleGrid[0].Length;

            int[][] matrix = new int[rows][];

            for (int i = 0; i < rows; i++)
                matrix[i] = new int[cols];

            InitEdges(obstacleGrid, rows, cols, matrix);

            for (int row = rows - 2; row >= 0; row--)
            {
                for (int col = cols - 2; col >= 0; col--)
                {
                    long x = 0;
                    bool isCellFree = obstacleGrid[row][col] == 0;

                    if (isCellFree)
                    {
                        x = matrix[row + 1][col] + matrix[row][col + 1];
                        if (x > int.MaxValue)
                            x = 0;
                    }

                    matrix[row][col] = (int)x;
                }
            }

            return matrix;
        }

        private static void InitEdges(int[][] obstacleGrid, int rows, int cols, int[][] matrix)
        {
            // Init bottom-right element
            bool isCellFree = obstacleGrid[rows - 1][cols - 1] == 0;
            matrix[rows - 1][cols - 1] = isCellFree ? 1 : 0;

            // Init last column
            for (int row = rows - 2; row >= 0; row--)
            {
                isCellFree = obstacleGrid[row][cols - 1] == 0;
                bool wasPreviousFree = matrix[row + 1][cols - 1] == 1;
                matrix[row][cols - 1] = isCellFree && wasPreviousFree ? 1 : 0;
            }

            // Init last row
            for (int col = cols - 2; col >= 0; col--)
            {
                isCellFree = obstacleGrid[rows - 1][col] == 0;
                bool wasPreviousFree = matrix[rows - 1][col + 1] == 1;
                matrix[rows - 1][col] = isCellFree && wasPreviousFree ? 1 : 0;
            }
        }
    }
}
