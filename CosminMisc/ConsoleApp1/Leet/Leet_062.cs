
namespace ConsoleApp1.Leet
{
    class Leet_062
    {
        static long[][] _cacheMatrix;
        static readonly int N = 100;

        public void Solve()
        {
            long a = UniquePaths(4, 5);
        }

        public long UniquePaths(int m, int n)
        {
            if (_cacheMatrix == null)
                MakeMatrix();

            return _cacheMatrix[N - n][N - m];
        }

        private void MakeMatrix()
        {
            _cacheMatrix = new long[N][];

            for (int i = 0; i < N; i++)
            {
                _cacheMatrix[i] = new long[N];
            }

            for (int i = 0; i < N; i++)
            {
                _cacheMatrix[N - 1][i] = 1;
                _cacheMatrix[i][N - 1] = 1;
            }

            for (int row = N - 2; row >= 0; row--)
            {
                for (int col = N - 2; col >= 0; col--)
                {
                    long x = _cacheMatrix[row + 1][col] + _cacheMatrix[row][col + 1];
                    if (x > int.MaxValue)
                        x = 0;

                    _cacheMatrix[row][col] = x;
                }
            }
        }
    }
}
