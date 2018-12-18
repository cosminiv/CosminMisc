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
        //printMatrix(currentBoard, "Final:");
        printSudoku(board);
    }

    public void solveSudoku(char[][] board) {
        initialBoard = copyBoard(board);
        currentBoard = copyBoard(board);
        initExistingData();

        for (int i = 0; i < BOARD_SIZE; i++) {
            for (int j = 0; j < BOARD_SIZE; j++) {
                if (initialBoard[i][j] != 0) continue;

                // If backtracking, don't start with 1 again
                int firstNumber = (currentBoard[i][j] == 0 ? 1 : currentBoard[i][j] + 1);
                currentBoard[i][j] = 0;

                for (int n = firstNumber; n <= BOARD_SIZE; n++){
                    if (isValidSudoku(currentBoard, n, i, j)) { 
                        setNumber(n, i, j);
                        break;
                    }
                }

                // If no number is good, backtrack.
                if (currentBoard[i][j] == 0){
                    Coords coords = backtrack(i, j);
                    i = coords.Row;
                    j = coords.Col - 1;  // It will be incremented by the for loop
                    //System.out.printf("BT: (%d, %d) ", i, j + 1);
                }
            }   
        }

        copyBoard(currentBoard, board);
    }

    private Coords backtrack(int row, int col) {
        int i = row, j = col - 1;
        while(true){
            if (j == -1) {
                j = BOARD_SIZE - 1;
                i--;
            }            

            if (initialBoard[i][j] == 0) { 
                resetNumber(i, j);
                if (currentBoard[i][j] < BOARD_SIZE)
                    return new Coords(i, j);
                else
                    currentBoard[i][j] = 0;
            }

            j--;
        }
    }

    private void copyBoard(int[][] intBoard, char[][] charBoard) {
        for (int i = 0; i < BOARD_SIZE; i++) {
            for (int j = 0; j < BOARD_SIZE; j++) {
                int n = intBoard[i][j];
                charBoard[i][j] = (char)('0' + n);
            }
        }        
    }

    private void setNumber(int number, int row, int col) {
        currentBoard[row][col] = number;
        existingCols[col][number] = 1;
        existingRows[row][number] = 1;
        int squareIndex = getSquareIndex(row, col);
        existingSquares[squareIndex][number] = 1;
    }

    private void resetNumber(int row, int col) {
        int number = currentBoard[row][col];
        existingCols[col][number] = 0;
        existingRows[row][number] = 0;
        int squareIndex = getSquareIndex(row, col);
        existingSquares[squareIndex][number] = 0;
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
    
    static class Coords {
        int Row;
        int Col;

        Coords(int r, int c){
            Row = r;
            Col = c;
        }
    }
}