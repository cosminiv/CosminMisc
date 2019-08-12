using CosminIv.Games.Common;
using CosminIv.Games.Common.Color;
using CosminIv.Games.Tetris;
using CosminIv.Games.Tetris.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CosminIv.Games.UI.Console.Tetris
{
    internal class TetrisBoardRenderer
    {
        readonly int Rows;
        readonly int Columns;
        readonly int BorderWidth;
        readonly Coordinates BoardWindowOrigin;
        TetrisFixedBricksState FixedBricks;
        readonly TetrisPieceRenderer PieceRenderer;

        public TetrisBoardRenderer(TetrisEngine engine, Coordinates topLeft) {
            Rows = engine.Settings.Rows;
            Columns = engine.Settings.Columns;
            BoardWindowOrigin = topLeft;
            BorderWidth = 1;
            PieceRenderer = new TetrisPieceRenderer();
        }

        public void DisplayPiece(TetrisPiece piece, Coordinates boardCoord) {
            Coordinates windowCoord = MakeWindowCoordinates(boardCoord);
            PieceRenderer.Display(piece, windowCoord);
        }

        public void DisplayBoard(TetrisState state) {
            DisplayFixedBricks(state);
            DisplayCurrentPiece(state.CurrentPiece);
        }

        public void DeletePiece(TetrisPiece piece, Coordinates boardCoord) {
            Coordinates windowCoord = MakeWindowCoordinates(boardCoord);
            PieceRenderer.Delete(piece, windowCoord, false);
        }

        private Coordinates MakeWindowCoordinates(Coordinates boardCoord) {
            int row = BoardWindowOrigin.Row + BorderWidth + boardCoord.Row;
            int column = BoardWindowOrigin.Column + BorderWidth + boardCoord.Column;
            return new Coordinates(row, column);
        }

        public void DisplayBoardBorder() {
            System.Console.ForegroundColor = ConsoleColor.White;
            
            DisplayBoardHorizontalBorder(BoardWindowOrigin.Row, TetrisConsoleConstants.BorderCornerUpLeft,
                TetrisConsoleConstants.BorderCornerUpRight);
            string blankRowText = new string(Enumerable.Range(1, Columns)
                .Select(i => TetrisConsoleConstants.Space[0]).ToArray());
            string borderRowText = TetrisConsoleConstants.VerticalBorder + blankRowText + TetrisConsoleConstants.VerticalBorder;

            for (int row = 0; row < Rows; row++) {
                int windowRow = BoardWindowOrigin.Row + BorderWidth + row;
                System.Console.SetCursorPosition(BoardWindowOrigin.Column, windowRow);
                System.Console.Write(borderRowText);
            }

            DisplayBoardHorizontalBorder(BoardWindowOrigin.Row + BorderWidth + Rows, TetrisConsoleConstants.BorderCornerDownLeft, TetrisConsoleConstants.BorderCornerDownRight);
        }

        private void DisplayFixedBricks(TetrisState state) {
            for (int row = 0; row < state.Rows; row++) {
                for (int column = 0; column < state.Columns; column++) {
                    TetrisBrick brick = state.FixedBricks[row][column];
                    DisplayBrickCharacter(brick, row, column, TetrisConsoleConstants.Space);
                }
            }
        }

        private void DisplayCurrentPiece(TetrisPieceWithPosition pieceWithPos) {
            TetrisPiece piece = pieceWithPos.Piece;

            for (int row = 0; row < piece.MaxSize; row++) {
                for (int column = 0; column < piece.MaxSize; column++) {
                    TetrisBrick brick = piece[row, column];
                    int rowRelativeToBoard = row + pieceWithPos.Position.Row;
                    int columnRelativeToBoard = column + pieceWithPos.Position.Column;
                    DisplayBrickCharacter(brick, rowRelativeToBoard, columnRelativeToBoard, "");
                }
            }
        }

        private void DisplayBrickCharacter(TetrisBrick brick, int row, int column, string blank) {
            int windowColumn = column + BoardWindowOrigin.Column + BorderWidth;
            int windowRow = row + BoardWindowOrigin.Row + BorderWidth;
            System.Console.SetCursorPosition(left: windowColumn, top: windowRow);

            if (brick != null) {
                System.Console.ForegroundColor = (brick.Color as ConsoleColor2).Value;
                System.Console.Write(TetrisConsoleConstants.Brick);
            }
            else {
                System.Console.Write(blank);
            }
        }

        private void DisplayBoardHorizontalBorder(int windowRow, string leftCorner, string rightCorner) {
            System.Console.SetCursorPosition(left: BoardWindowOrigin.Column, top: windowRow);
            System.Console.Write(leftCorner);
            for (int i = 0; i < Columns; i++) 
                System.Console.Write(TetrisConsoleConstants.HorizontalBorder);
            System.Console.Write(rightCorner);
        }
    }
}
