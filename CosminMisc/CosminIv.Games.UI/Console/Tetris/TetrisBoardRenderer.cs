using CosminIv.Games.Common;
using CosminIv.Games.Common.Color;
using CosminIv.Games.Tetris;
using CosminIv.Games.Tetris.DTO;
using CosminIv.Games.Tetris.DTO.EventArg;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CosminIv.Games.UI.Console.Tetris
{
    internal class TetrisBoardRenderer
    {
        readonly TetrisEngine Engine;
        readonly int BorderWidth;
        readonly Coordinates BoardWindowOrigin;
        TetrisFixedBricksState FixedBricks;
        readonly TetrisPieceRenderer PieceRenderer;

        public TetrisBoardRenderer(TetrisEngine engine, Coordinates topLeft) {
            Engine = engine;
            BoardWindowOrigin = topLeft;
            BorderWidth = 1;
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

        public void DisplayBoard(TetrisState state) {
            DisplayBricks(state);
        }

        public void DeletePiece(TetrisPiece piece, Coordinates boardCoord) {
            Coordinates windowCoord = MakeWindowCoordinates(boardCoord);
            PieceRenderer.Delete(piece, windowCoord, false);
        }

        public void UpdateRows(TetrisFixedBricksState fixedBricks) {
            if (fixedBricks.DeletedRows == 0)
                return;

            for (int rowIndex = 0; rowIndex < fixedBricks.Rows.Count; rowIndex++) {
                TetrisBrick[] brickRow = fixedBricks.Rows[rowIndex];
                int rowIndexRelativeToBoard = rowIndex + fixedBricks.RowsStartIndex;

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
            System.Console.ForegroundColor = ConsoleColor.White;
            
            DisplayBoardHorizontalBorder(BoardWindowOrigin.Row, TetrisConsoleConstants.BorderCornerUpLeft,
                TetrisConsoleConstants.BorderCornerUpRight);
            string blankRowText = new string(Enumerable.Range(1, Engine.Columns)
                .Select(i => TetrisConsoleConstants.Space[0]).ToArray());
            string borderRowText = TetrisConsoleConstants.VerticalBorder + blankRowText + TetrisConsoleConstants.VerticalBorder;

            for (int row = 0; row < Engine.Rows; row++) {
                int windowRow = BoardWindowOrigin.Row + BorderWidth + row;
                System.Console.SetCursorPosition(BoardWindowOrigin.Column, windowRow);
                System.Console.Write(borderRowText);
            }

            DisplayBoardHorizontalBorder(BoardWindowOrigin.Row + BorderWidth + Engine.Rows, TetrisConsoleConstants.BorderCornerDownLeft, TetrisConsoleConstants.BorderCornerDownRight);
        }

        private void DisplayBricks(TetrisState state) {
            for (int row = 0; row < state.Rows; row++) {
                for (int column = 0; column < state.Columns; column++) {
                    DisplayBrickCharacter(state, row, column);
                }
            }
        }

        private void DisplayFixedBricks() {
            for (int row = 0; row < Engine.Rows; row++) {
                for (int column = 0; column < Engine.Columns; column++) {
                    DisplayFixedBrickCharacter(row, column);
                }
            }
        }

        private void DisplayBrickCharacter(TetrisState state, int row, int column) {
            TetrisBrick brick = state.Bricks[row][column];

            int windowColumn = column + BoardWindowOrigin.Column + BorderWidth;
            int windowRow = row + BoardWindowOrigin.Row + BorderWidth;
            System.Console.SetCursorPosition(left: windowColumn, top: windowRow);

            if (brick != null) {
                System.Console.ForegroundColor = (brick.Color as ConsoleColor2).Value;
                System.Console.Write(TetrisConsoleConstants.Brick);
            }
            else {
                System.Console.Write(TetrisConsoleConstants.Space);
            }
        }

        private void DisplayFixedBrickCharacter(int row, int column) {
            if (FixedBricks.RowsStartIndex < 0) {
                System.Console.Write(TetrisConsoleConstants.Space);
                return;
            }

            int row2 = row - FixedBricks.RowsStartIndex;
            if (row2 < 0) {
                System.Console.Write(TetrisConsoleConstants.Space);
                return;
            }

            TetrisBrick brick = FixedBricks.Rows[row2][column];

            int windowColumn = column + BoardWindowOrigin.Column + BorderWidth;
            int windowRow = row + BoardWindowOrigin.Row + BorderWidth;
            System.Console.SetCursorPosition(left: windowColumn, top: windowRow);

            if (brick != null) {
                System.Console.ForegroundColor = (brick.Color as ConsoleColor2).Value;
                System.Console.Write(TetrisConsoleConstants.Brick);
            }
            else {
                System.Console.Write(TetrisConsoleConstants.Space);
            }
        }

        private void DisplayBoardHorizontalBorder(int windowRow, string leftCorner, string rightCorner) {
            System.Console.SetCursorPosition(left: BoardWindowOrigin.Column, top: windowRow);
            System.Console.Write(leftCorner);
            for (int i = 0; i < Engine.Columns; i++) 
                System.Console.Write(TetrisConsoleConstants.HorizontalBorder);
            System.Console.Write(rightCorner);
        }
    }
}
