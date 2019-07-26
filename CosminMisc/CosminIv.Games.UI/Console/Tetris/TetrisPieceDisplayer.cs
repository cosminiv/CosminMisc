using CosminIv.Games.Common;
using CosminIv.Games.Tetris;
using System;
using System.Collections.Generic;
using System.Text;

namespace CosminIv.Games.UI.Console.Tetris
{
    internal class TetrisPieceDisplayer
    {
        readonly string Brick = "#";
        readonly string Space = " ";
        readonly Coordinates BoardWindowOrigin;
        readonly int BorderWidth;

        public TetrisPieceDisplayer(Coordinates boardWindowOrigin, int borderWidth) {
            BoardWindowOrigin = boardWindowOrigin;
            BorderWidth = borderWidth;
        }

        public void Display(TetrisPiece piece, Coordinates coordinates) {
            DisplayInPlaceOfPiece(piece, coordinates, Brick);
        }

        public void Delete(TetrisPiece piece, Coordinates coordinates) {
            DisplayInPlaceOfPiece(piece, coordinates, Space);
        }

        public void DeleteRows(TetrisFullRowsDeletedResult rowsDeleted) {
            if (rowsDeleted.DeletedRowsIndexes.Count == 0)
                return;

            for (int rowIndex = 0; rowIndex < rowsDeleted.ModifiedRows.Length; rowIndex++) {
                TetrisBrick[] brickRow = rowsDeleted.ModifiedRows[rowIndex];
                int rowIndexRelativeToBoard = rowIndex + rowsDeleted.ModifiedRowsStartIndex;

                for (int columnIndex = 0; columnIndex < brickRow.Length; columnIndex++) {
                    TetrisBrick brick = brickRow[columnIndex];
                    int windowRow = BoardWindowOrigin.Row + BorderWidth + rowIndexRelativeToBoard;
                    int windowColumn = BoardWindowOrigin.Column + BorderWidth + columnIndex;
                    bool isBrick = brick != null;

                    System.Console.SetCursorPosition(windowColumn, windowRow);
                    System.Console.Write(isBrick ? Brick : Space);
                }
            }
        }

        private void DisplayInPlaceOfPiece(TetrisPiece piece, Coordinates pieceBoardCoord, string displayChar) {
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
