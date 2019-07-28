using CosminIv.Games.Common;
using CosminIv.Games.Tetris;
using System;
using System.Collections.Generic;
using System.Text;

namespace CosminIv.Games.UI.Console.Tetris
{
    internal class TetrisPieceRenderer
    {
        readonly string Brick = "#";
        readonly string Space = " ";
        readonly Coordinates BoardWindowOrigin;
        readonly int BorderWidth;

        public TetrisPieceRenderer(Coordinates boardWindowOrigin, int borderWidth) {
            BoardWindowOrigin = boardWindowOrigin;
            BorderWidth = borderWidth;
        }

        public void Display(TetrisPiece piece, Coordinates coordinates) {
            DisplayInPlaceOfPiece(piece, coordinates, Brick);
        }

        public void Delete(TetrisPiece piece, Coordinates coordinates) {
            DisplayInPlaceOfPiece(piece, coordinates, Space);
        }

        public void UpdateRows(TetrisModifiedRows modifiedRows) {
            if (modifiedRows.DeletedRowsIndexes.Count == 0)
                return;

            for (int rowIndex = 0; rowIndex < modifiedRows.ModifiedRows.Length; rowIndex++) {
                TetrisBrick[] brickRow = modifiedRows.ModifiedRows[rowIndex];
                int rowIndexRelativeToBoard = rowIndex + modifiedRows.ModifiedRowsStartIndex;

                for (int columnIndex = 0; columnIndex < brickRow.Length; columnIndex++) {
                    TetrisBrick brick = brickRow[columnIndex];
                    int windowRow = BoardWindowOrigin.Row + BorderWidth + rowIndexRelativeToBoard;
                    int windowColumn = BoardWindowOrigin.Column + BorderWidth + columnIndex;
                    bool isBrick = brick != null;

                    System.Console.SetCursorPosition(windowColumn, windowRow);
                    if (isBrick)
                        System.Console.ForegroundColor = (brick.Color as ConsoleColor2).Value;
                    System.Console.Write(isBrick ? Brick : Space);
                }
            }
        }

        private void DisplayInPlaceOfPiece(TetrisPiece piece, Coordinates pieceBoardCoord, string displayChar) {
            System.Console.ForegroundColor = (piece.Color as ConsoleColor2).Value;

            int boardOriginColumn = BoardWindowOrigin.Column + BorderWidth + pieceBoardCoord.Column;
            int boardOriginRow = BoardWindowOrigin.Row + BorderWidth + pieceBoardCoord.Row;

            for (int pieceRow = 0; pieceRow < piece.MaxSize; pieceRow++) {
                for (int pieceColumn = 0; pieceColumn < piece.MaxSize; pieceColumn++) {
                    int windowRow = boardOriginRow + pieceRow;
                    int windowColumn = boardOriginColumn + pieceColumn;
                    bool isBrick = piece[pieceRow, pieceColumn] != null;

                    System.Console.SetCursorPosition(windowColumn, windowRow);
                    System.Console.Write(isBrick ? displayChar.ToString() : "");
                }
            }
        }
    }
}
