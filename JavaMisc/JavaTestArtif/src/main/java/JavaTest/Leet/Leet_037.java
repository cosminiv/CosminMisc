// Solve Sudoku board
//
package JavaTest.Leet;

//import java.util.Arrays;

public class Leet_037 {
    int[][] initialBoard;
    int[][] currentBoard;
    int[][] existingRows = new int[BOARD_SIZE][];
    int[][] existingCols = new int[BOARD_SIZE][];
    int[][] existingSquares = new int[BOARD_SIZE][];
    static final int BOARD_SIZE = 9;

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
        printMatrix(currentBoard, "Current:");
    }

    public void solveSudoku(char[][] board) {
        initialBoard = copyBoard(board);
        currentBoard = copyBoard(board);
        initExistingData();
        printState();

        for (int i = 0; i < BOARD_SIZE; i++) {
            for (int j = 0; j < BOARD_SIZE; j++) {
                if (currentBoard[i][j] != 0) continue;
                for (int d = 1; d <= BOARD_SIZE; d++){
                    if (isValidSudoku(currentBoard, d, i, j)) { 
                        setNumberOnBoard(currentBoard, d, i, j);
                        break;
                    }
                }
            }   
        }
    }    

    private void setNumberOnBoard(int[][] board, int number, int row, int col) {
        board[row][col] = number;
        existingCols[col][number] = 1;
        existingRows[row][number] = 1;
        int squareIndex = getSquareIndex(row, col);
        existingSquares[squareIndex][number] = 1;
    }

    private int getSquareIndex(int row, int col) {
        int overallIndex = row * BOARD_SIZE + col;
        int squareIndex = col / 3;

        if (overallIndex >= 6 * BOARD_SIZE) 
            squareIndex = squareIndex + 6;
        else if (overallIndex >= 3 * BOARD_SIZE)
            squareIndex = squareIndex + 3;

        return squareIndex;
    }

    private void printState() {
        printMatrix(initialBoard, "Initial: ");
        printMatrix(existingRows, "Rows: ");
        printMatrix(existingCols, "Cols: ");
        printMatrix(existingSquares, "Squares: ");
    }

    private void initExistingData() {
        for (int i = 0; i < BOARD_SIZE; i++) {
            existingRows[i] = new int[10];
            existingCols[i] = new int[10];
            existingSquares[i] = new int[10];
        }

        for (int i = 0; i < BOARD_SIZE; i++) {
            for (int j = 0; j < BOARD_SIZE; j++) {
                int digit = initialBoard[i][j];

                existingRows[i][digit] = 1;
                existingCols[j][digit] = 1;
                int squareIndex = getSquareIndex(i, j);
                existingSquares[squareIndex][digit] = 1;
            }
        }        
    }

    boolean isValidSudoku(int[][] board, int n, int row, int col) {
        if (existingRows[row][n] > 0) return false;
        if (existingCols[col][n] > 0) return false;
        int squareIndex = getSquareIndex(row, col);
        if (existingSquares[squareIndex][n] > 0) return false;
        return true;
    }

    private int[][] copyBoard(char[][] board) {
        int[][] copy = new int[BOARD_SIZE][];
        for (int i = 0; i < BOARD_SIZE; i++) {
            copy[i] = new int[BOARD_SIZE];
            for (int j = 0; j < BOARD_SIZE; j++) {
                if (board[i][j] != '.')
                    copy[i][j] = board[i][j] - '0';
                // else - remains zero
            }
        }
        return copy;
    }    

    private void printSudoku(char[][] board) {
        for (int i = 0; i < BOARD_SIZE; i++) {
            if (i == 3 || i == 6)
                System.out.println("                  ");

            for (int j = 0; j < BOARD_SIZE; j++) {
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