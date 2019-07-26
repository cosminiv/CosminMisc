using CosminIv.Games.Common;
using CosminIv.Games.Tetris;
using System;
using System.Collections.Generic;
using System.Text;

namespace CosminIv.Games.UI.Console.Tetris
{
    internal class TetrisPieceDisplayer
    {
        readonly char Brick = '#';
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
            DisplayInPlaceOfPiece(piece, coordinates, ' ');
        }

        private void DisplayInPlaceOfPiece(TetrisPiece piece, Coordinates pieceBoardCoord, char displayChar) {
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
