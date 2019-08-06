using CosminIv.Games.Common;
using CosminIv.Games.Tetris;
using CosminIv.Games.Tetris.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CosminIv.Games.UI.Console.Tetris
{
    internal class TetrisPieceRenderer
    {
        public TetrisPieceRenderer() {
        }

        public void Display(TetrisPiece piece, Coordinates coordinates) {
            DisplayInPlaceOfPiece(piece, coordinates, TetrisConsoleConstants.Brick, "");
        }

        public void Delete(TetrisPiece piece, Coordinates coordinates, bool printSpaces) {
            string blank = printSpaces ? TetrisConsoleConstants.Space : "";
            DisplayInPlaceOfPiece(piece, coordinates, TetrisConsoleConstants.Space, blank);
        }

        private void DisplayInPlaceOfPiece(TetrisPiece piece, Coordinates coord, string displayChar, string blankChar) {
            System.Console.ForegroundColor = (piece.Color as ConsoleColor2).Value;

            for (int pieceRow = 0; pieceRow < piece.MaxSize; pieceRow++) {
                for (int pieceColumn = 0; pieceColumn < piece.MaxSize; pieceColumn++) {
                    int windowRow = coord.Row + pieceRow;
                    int windowColumn = coord.Column + pieceColumn;
                    bool isBrick = piece[pieceRow, pieceColumn] != null;

                    System.Console.SetCursorPosition(windowColumn, windowRow);
                    System.Console.Write(isBrick ? displayChar.ToString() : blankChar);
                }
            }
        }
    }
}
