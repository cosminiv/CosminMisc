// Validate Sudoku board
//
package JavaTest.Leet;

import java.util.Arrays;

public class Leet_036 {
    int[] existing = new int[10];

    public void test() {
        char[][] board = {
            {'5','3','.','.','7','.','.','.','.'},
            {'6','.','.','1','9','5','.','.','.'},
            {'.','9','8','.','.','.','.','6','.'},
            {'8','.','.','.','6','.','.','.','3'},
            {'4','.','.','8','.','3','.','.','1'},
            {'7','.','.','.','2','.','.','.','6'},
            {'.','6','.','.','.','.','2','8','.'},
            {'.','.','.','4','1','9','.','.','5'},
            {'.','.','.','.','8','.','.','7','9'}
          };

        boolean isValid = isValidSudoku(board);
        System.out.println(isValid);

        int a = 8;
    }

    public boolean isValidSudoku(char[][] board) {
        for (int i = 0; i < board.length; i++)
            if (!isValidRow(board, i)) return false;
        for (int i = 0; i < board[0].length; i++)
            if (!isValidColumn(board, i)) return false;
        if (!areValidSquares(board))
            return false;
        return true;
    }

    private boolean areValidSquares(char[][] board) {
        for (int squareRow = 0; squareRow < 3; squareRow++)
            for (int squareCol = 0; squareCol < 3; squareCol++){
                Arrays.fill(existing, 0);
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++) {
                        int row = squareRow * 3 + i;
                        int col = squareCol * 3 + j;

                        int digit = board[row][col] - '0';
                        if (board[row][col] != '.' && ++existing[digit] > 1)
                            return false;                        
                    }
            }

        return true;
    }

    private boolean isValidRow(char[][] board, int rowIndex) {
        char[] row = board[rowIndex];
        Arrays.fill(existing, 0);

        for (int i = 0; i < row.length; i++) {
            int digit = row[i] - '0';
            if (row[i] != '.' && ++existing[digit] > 1)
                return false;
        }
        return true;
    }

    private boolean isValidColumn(char[][] board, int colIndex) {
        Arrays.fill(existing, 0);

        for (int i = 0; i < board.length; i++) {
            int digit = board[i][colIndex] - '0';
            if (board[i][colIndex] != '.' && ++existing[digit] > 1)
                return false;
        }
        return true;
    }
}