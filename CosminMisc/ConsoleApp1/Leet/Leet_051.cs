using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Leet
{
    //
    // Given an integer n, return all distinct solutions to the n-queens puzzle.
    //
    class Leet_051
    {
        public void Test() {
            var sol = SolveNQueens(8);
            StringBuilder text = new StringBuilder();

            foreach (var sol1 in sol) {
                string sol1Str = string.Join("\n", sol1.Select(x => 
                    x.Replace("Q", "Q ").Replace(".", ". ")));  // Add some spaces
                text.AppendLine(sol1Str);
                text.AppendLine();
            }

            Console.WriteLine(text);
            Console.WriteLine($"{sol.Count} solutions");
        }

        public IList<IList<string>> SolveNQueens(int n) {
            List<int[]> solutions = new List<int[]>();
            // index = row, value column
            int[] initialPartialSol = new int[0];

            // Explore the tree of possible solutions
            Stack<int[]> stack = new Stack<int[]>();
            stack.Push(initialPartialSol);

            while (stack.Count > 0) {
                int[] partialSol = stack.Pop();

                if (IsValid(partialSol)) {
                    if (partialSol.Length == n)
                        solutions.Add(partialSol);
                    else {
                        foreach (int[] childSol in MakeChildren(partialSol, n)) {
                            stack.Push(childSol);
                        }
                    }
                }
            }

            return SolutionsToStrings(solutions, n);
        }

        private bool IsValid(int[] partialSol) {
            int N = partialSol.Length;
            if (N == 0 || N == 1)
                return true;

            // It's enough to only verify the last added queen
            // The previous ones were verified on previous steps
            int lastQueenRow = N - 1;
            int lastQueenCol = partialSol[N - 1];

            for (int queenRow = 0; queenRow < N - 1; queenRow++) {
                int queenCol = partialSol[queenRow];

                // NW-SE diagonals
                if (queenRow - queenCol == lastQueenRow - lastQueenCol)
                    return false;

                // NE-SW diagonals
                if (queenRow + queenCol == lastQueenRow + lastQueenCol)
                    return false;
            }

            return true;
        }

        private IEnumerable<int[]> MakeChildren(int[] partialSol, int boardSize) {
            // Create a child for every column which is not already in partialSol
            for (int col = boardSize - 1; col >= 0; col--) {
                if (!partialSol.Contains(col)) {
                    int[] child = new int[partialSol.Length + 1];
                    Array.Copy(partialSol, child, partialSol.Length);
                    child[child.Length - 1] = col;
                    yield return child;
                }
            }
        }

        private IList<IList<string>> SolutionsToStrings(List<int[]> solutions, int boardSize) {
            IList<IList<string>> result = new List<IList<string>>();
            StringBuilder rowStr = new StringBuilder(boardSize);

            foreach (int[] sol in solutions) {
                List<string> solStrings = new List<string>(boardSize);
                for (int row = 0; row < boardSize; row++) {
                    rowStr.Clear();
                    for (int col = 0; col < boardSize; col++) {
                        bool isQueen = sol[row] == col;
                        rowStr.Append(isQueen ? "Q" : ".");
                    }
                    solStrings.Add(rowStr.ToString());
                }
                result.Add(solStrings);
            }

            return result;
        }
    }
}
