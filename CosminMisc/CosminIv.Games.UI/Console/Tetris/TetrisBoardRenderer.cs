using CosminIv.Games.Common;
using CosminIv.Games.Tetris;
using CosminIv.Games.Tetris.DTO;
using CosminIv.Games.Tetris.DTO.EventArg;
using System;
using System.Collections.Generic;
using System.Text;

namespace CosminIv.Games.UI.Console.Tetris
{
    internal class TetrisBoardRenderer
    {
        TetrisEngine Engine;
        int BorderWidth;
        Coordinates BoardWindowOrigin;
        TetrisFixedBricksState FixedBricks;
        TetrisPieceRenderer PieceRenderer;

        public TetrisBoardRenderer(TetrisEngine engine, Coordinates topLeft, int borderWidth) {
            Engine = engine;
            BoardWindowOrigin = topLeft;
            BorderWidth = borderWidth;
            PieceRenderer = new TetrisPieceRenderer();
        }

        public void DisplayBoard(GameInitializedArgs args) {
            FixedBricks = args.FixedBricks;
            DisplayBoardBorder();
            DisplayFixedBricks();
        }

        public void DisplayPiece(TetrisPiece piece, Coordinates boardCoord) {
            Coordinates windowCoord = MakeWindowCoordinates(boardCoord);
            PieceRenderer.Display(piece, windowCoord);
        }

        public void DeletePiece(TetrisPiece piece, Coordinates boardCoord) {
            Coordinates windowCoord = MakeWindowCoordinates(boardCoord);
            PieceRenderer.Delete(piece, windowCoord);
        }

        public void UpdateRows(TetrisFixedBricksState modifiedRows) {
            if (modifiedRows.DeletedRowsIndexes.Count == 0)
                return;

            for (int rowIndex = 0; rowIndex < modifiedRows.Rows.Count; rowIndex++) {
                TetrisBrick[] brickRow = modifiedRows.Rows[rowIndex];
                int rowIndexRelativeToBoard = rowIndex + modifiedRows.RowsStartIndex;

                for (int columnIndex = 0; columnIndex < brickRow.Length; columnIndex++) {
                    TetrisBrick brick = brickRow[columnIndex];
                    int windowRow = BoardWindowOrigin.Row + BorderWidth + rowIndexRelativeToBoard;
                    int windowColumn = BoardWindowOrigin.Column + BorderWidth + columnIndex;
                    bool isBrick = brick != null;

                    System.Console.SetCursorPosition(windowColumn, windowRow);
                    if (isBrick)
                        System.Console.ForegroundColor = (brick.Color as ConsoleColor2).Value;
                    System.Console.Write(isBrick ? TetrisConsoleConstants.Brick : TetrisConsoleConstants.Space);
                }
            }
        }

        private Coordinates MakeWindowCoordinates(Coordinates boardCoord) {
            int row = BoardWindowOrigin.Row + BorderWidth + boardCoord.Row;
            int column = BoardWindowOrigin.Column + BorderWidth + boardCoord.Column;
            return new Coordinates(row, column);
        }

        private void DisplayBoardBorder() {
            string boardString = MakeBoardString();
            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.SetCursorPosition(left: 0, top: BoardWindowOrigin.Row);
            System.Console.Write(boardString);
        }

        private string MakeBoardString() {
            StringBuilder sb = new StringBuilder();

            AddBoardHorizontalBorder(sb);

            for (int row = 0; row < Engine.Rows; row++) {
                sb.Append(' ', BoardWindowOrigin.Column);
                sb.Append('|', BorderWidth);

                sb.Append(' ', Engine.Columns);

                sb.Append('|', BorderWidth);
                sb.AppendLine();
            }

            AddBoardHorizontalBorder(sb);

            return sb.ToString();
        }

        private void DisplayFixedBricks() {
            for (int row = 0; row < Engine.Rows; row++) {
                for (int column = 0; column < Engine.Columns; column++) {

                    DisplayFixedBrickCharacter(row, column);
                }
            }
        }

        private void DisplayFixedBrickCharacter(int row, int column) {
            if (FixedBricks.RowsStartIndex < 0) {
                System.Console.Write(" ");
                return;
            }

            int row2 = row - FixedBricks.RowsStartIndex;
            if (row2 < 0) {
                System.Console.Write(" ");
                return;
            }

            TetrisBrick brick = FixedBricks.Rows[row2][column];

            if (brick != null) {
                System.Console.SetCursorPosition(left: column + BoardWindowOrigin.Column + BorderWidth, 
                    top: row + BoardWindowOrigin.Row + BorderWidth);
                System.Console.ForegroundColor = (brick.Color as ConsoleColor2).Value;
                System.Console.Write(TetrisConsoleConstants.Brick);
            }
            else
                System.Console.Write(TetrisConsoleConstants.Space);
        }

        private void AddBoardHorizontalBorder(StringBuilder sb) {
            sb.Append(' ', BoardWindowOrigin.Column);
            sb.Append('-', Engine.Columns + 2 * BorderWidth);
            sb.AppendLine();
        }
    }
}
