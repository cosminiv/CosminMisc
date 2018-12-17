// Solve Sudoku board
//
package JavaTest.Leet;

//import java.util.Arrays;

public class Leet_037 {
    int[][] initialBoard;
    int[][] existingRows = new int[9][];
    int[][] existingCols = new int[9][];
    int[][] existingSquares = new int[9][];

    public void test() {
        char[][] board = {
            {'5','3','.', '.','7','.', '.','.','.'},
            {'6','.','.', '1','9','5', '.','.','.'},
            {'.','9','8', '.','.','.', '.','6','.'},

            {'8','.','.', '.','6','.', '.','.','3'},
            {'4','.','.', '8','.','3', '.','.','1'},
            {'7','.','.', '.','2','.', '.','.','6'},
            
            {'.','6','.', '.','.','.', '2','8','.'},
            {'.','.','.', '4','1','9', '.','.','5'},
            {'.','.','.', '.','8','.', '.','7','9'}
          };

        solveSudoku(board);
        printSudoku(board);

        int a = 8;
    }

    public void solveSudoku(char[][] board) {
        initialBoard = copyBoard(board);
        initExistingData();
        printState();

        // for (int i = 0; i < 9; i++) {
        //     for (int j = 0; j < 9; j++) {
        //         if (board[i][j] != '.') continue;
        //         for (char d = 1; d <= 9; d++){
        //             board[i][j] = (char)('0' + d);
        //             if (isValidSudoku(board, d, i, j)) break;
        //         }
        //     }   
        // }
    }    

    private void printState() {
        printMatrix(initialBoard, "Initial: ");
        printMatrix(existingRows, "Rows: ");
        printMatrix(existingCols, "Cols: ");
        printMatrix(existingSquares, "Squares: ");
    }

    private void initExistingData() {
        for (int i = 0; i < 9; i++) {
            existingRows[i] = new int[10];
            existingCols[i] = new int[10];
            existingSquares[i] = new int[10];
        }

        int overallIndex = 0;
        for (int i = 0; i < 9; i++) {
            for (int j = 0; j < 9; j++) {
                int digit = initialBoard[i][j];

                // Set row and col
                existingRows[i][digit] = 1;
                existingCols[j][digit] = 1;
                
                // Set square
                int squareIndex = j / 3;
                if (overallIndex >= 54) 
                    squareIndex = squareIndex + 6;
                else if (overallIndex >= 27) 
                    squareIndex = squareIndex + 3;

                existingSquares[squareIndex][digit] = 1;
                overallIndex++;
            }
        }        
    }

    public boolean isValidSudoku(char[][] board, int d, int i, int j) {
        if (!isValidRow(board, i)) return false;
        if (!isValidColumn(board, j)) return false;
        if (!isValidSquare(board, i, j)) return false;
        return true;
    }

    private boolean isValidSquare(char[][] board, int squareRow, int squareCol) {
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++) {
                int row = squareRow * 3 + i;
                int col = squareCol * 3 + j;

                int digit = board[row][col] - '0';
                //if (board[row][col] != '.' && ++existingSquares[digit] > 1)
                 //   return false;                        
            }

        return true;
    }

    private boolean isValidRow(char[][] board, int rowIndex) {
        char[] row = board[rowIndex];
        //Arrays.fill(existing, 0);

        for (int i = 0; i < row.length; i++) {
            int digit = row[i] - '0';
           // if (row[i] != '.' && ++existing[digit] > 1)
             //   return false;
        }
        return true;
    }

    private boolean isValidColumn(char[][] board, int colIndex) {
        //Arrays.fill(existing, 0);

        for (int i = 0; i < board.length; i++) {
            int digit = board[i][colIndex] - '0';
            //if (board[i][colIndex] != '.' && ++existing[digit] > 1)
            //    return false;
        }
        return true;
    }


    private int[][] copyBoard(char[][] board) {
        int[][] copy = new int[9][];
        for (int i = 0; i < 9; i++) {
            copy[i] = new int[9];
            for (int j = 0; j < 9; j++) {
                if (board[i][j] != '.')
                    copy[i][j] = board[i][j] - '0';
                // else - remains zero
            }
        }
        return copy;
    }    

    private void printSudoku(char[][] board) {
        for (int i = 0; i < 9; i++) {
            if (i == 3 || i == 6)
                System.out.println("                  ");

            for (int j = 0; j < 9; j++) {
                if (j == 3 || j == 6)
                    System.out.print("  ");

                System.out.printf("%c   ", board[i][j]);
            }

            System.out.println();
        }
    }
    
    private void printMatrix(int[][] matrix, String text) {
        System.out.println(text);

        for (int i = 0; i < matrix.length; i++) {
            if (i == 3 || i == 6)
                System.out.println("                  ");

            for (int j = 0; j < matrix[0].length; j++) {
                if (j == 3 || j == 6)
                    System.out.print("  ");

                System.out.printf("%d   ", matrix[i][j]);
            }

            System.out.println();
        }

        System.out.println("\n");
    }    
}