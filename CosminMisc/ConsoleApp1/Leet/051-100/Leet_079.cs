using System.Collections.Generic;

namespace ConsoleApp1.Leet._051_100
{
    public class Leet_079
    {
        private bool[][] _visited;
        private int _rows;
        private int _cols;

        public void Solve()
        {
            char[][] board = 
            {
                new [] {'A','B','C','E'},
                new [] {'S','F','C','S'},
                new [] {'A','D','E','E'},
            };

            char[][] board2 = {new[] {'A'}};

            bool x = Exist(board2, "A");
            bool a = Exist(board, "ABCCED");
            bool b = Exist(board, "SEE");
            bool c = Exist(board, "ABCB");
            bool d = Exist(board, "FDECCESE");
        }

        public bool Exist(char[][] board, string word)
        {
            if (string.IsNullOrEmpty(word)) return false;

            InitializeMembers(board);

            for (int rowIndex = 0; rowIndex < board.Length; rowIndex++)
            {
                char[] row = board[rowIndex];

                for (int colIndex = 0; colIndex < row.Length; colIndex++)
                {
                    if (ExistSuffix(board, rowIndex, colIndex, word, 0))
                        return true;
                }
            }

            return false;
        }

        bool ExistSuffix(char[][] board, int rowIndex, int colIndex, string word, int letterIndex)
        {
            if (letterIndex >= word.Length)
            {
                return true;
            }

            char wordLetter = word[letterIndex];
            char boardLetter = board[rowIndex][colIndex];

            if (boardLetter != wordLetter || _visited[rowIndex][colIndex])
                return false;

            if (letterIndex == word.Length - 1)
                return true;

            _visited[rowIndex][colIndex] = true;
            bool existsSuffix = false;

            foreach (var (nextRowIndex, nextColIndex) in GetNextCell(rowIndex, colIndex))
            {
                if (ExistSuffix(board, nextRowIndex, nextColIndex, word, letterIndex + 1))
                {
                    existsSuffix = true;
                    break;
                }
            }

            if (existsSuffix)
                return true;

            _visited[rowIndex][colIndex] = false;

            return false;
        }

        IEnumerable<(int, int)> GetNextCell(int row, int col)
        {
            if (col + 1 < _cols) yield return (row, col + 1);
            if (row + 1 < _rows) yield return (row + 1, col);
            if (col - 1 >= 0) yield return (row, col - 1);
            if (row - 1 >= 0) yield return (row - 1, col);
        }

        private void InitializeMembers(char[][] board)
        {
            InitializeVisited(board);
            _rows = board.Length;
            _cols = board[0].Length;
        }

        private void InitializeVisited(char[][] board)
        {
            _visited = new bool[board.Length][];
            int columns = board[0].Length;

            for (var index = 0; index < _visited.Length; index++)
            {
                _visited[index] = new bool[columns];
            }
        }
    }
}
