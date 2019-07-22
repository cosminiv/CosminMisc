using CosminIv.Games.Common;
using CosminIv.Games.Tetris;
using System;
using System.Collections.Generic;
using System.Text;

namespace CosminIv.Games.UI.Console.Tetris
{
    public class TetrisConsoleUI
    {
        TetrisEngine Engine;
        Coordinates TopLeft = new Coordinates(0, 0);
        readonly int BorderWidth = 1;
        readonly string Brick = "#";

        public TetrisConsoleUI(TetrisEngine engine) {
            Engine = engine;
            Engine.PieceAppeared += Engine_PieceAppeared;
        }

        private void Engine_PieceAppeared(TetrisPieceWithPosition arg) {
            DisplayPiece(arg);
        }

        private void DisplayPiece(TetrisPieceWithPosition pieceWithPos) {
            int originColumn = TopLeft.Column + BorderWidth + pieceWithPos.Position.Column;
            int originRow = TopLeft.Row + BorderWidth + pieceWithPos.Position.Row;
            
            TetrisPiece piece = pieceWithPos.Piece;

            for (int pieceRow = 0; pieceRow < piece.MaxHeight; pieceRow++) {
                for (int pieceColumn = 0; pieceColumn < piece.MaxWidth; pieceColumn++) {
                    int windowRow = originRow + pieceRow;
                    int windowColumn = originColumn + pieceColumn;

                    System.Console.SetCursorPosition(windowColumn, windowRow);
                    bool isBrick = piece[pieceRow, pieceColumn] != null;
                    System.Console.Write(isBrick ? Brick : " ");
                }
            }
        }

        public void Start() {
            DisplayBoard();
            Engine.Start();
        }

        void DisplayBoard() {
            string initialStateStr = MakeBoardString();
            System.Console.SetCursorPosition(0, TopLeft.Row);
            System.Console.Write(initialStateStr);
        }

        private string MakeBoardString() {
            StringBuilder sb = new StringBuilder();

            AddBoardHorizontalBorder(sb);

            for (int row = 0; row < Engine.Rows; row++) {
                sb.Append(' ', TopLeft.Column);
                sb.Append('|', BorderWidth);
                sb.Append(' ', Engine.Columns);
                sb.Append('|', BorderWidth);
                sb.AppendLine();
            }

            AddBoardHorizontalBorder(sb);

            return sb.ToString();
        }

        private void AddBoardHorizontalBorder(StringBuilder sb) {
            sb.Append(' ', TopLeft.Column);
            sb.Append('-', Engine.Columns + 2 * BorderWidth);
            sb.AppendLine();
        }
    }
}
