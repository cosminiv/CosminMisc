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
        Coordinates TopLeft = new Coordinates(2, 20);
        readonly int WindowHeight = 30;
        readonly int WindowWidth = 60;
        readonly int BorderWidth = 1;
        readonly string Brick = "#";

        public TetrisConsoleUI(TetrisEngine engine) {
            SetWindowSize();
            Engine = engine;
            Engine.PieceAppeared += Engine_PieceAppeared;
            Engine.PieceMoved += Engine_PieceMoved;
        }

        private void SetWindowSize() {
            System.Console.SetWindowSize(width: WindowWidth, height: WindowHeight);
            System.Console.SetBufferSize(width: WindowWidth, height: WindowHeight);
        }

        public void Start() {
            DisplayBoard();
            Engine.Start();
            System.Console.CursorVisible = false;
        }


        private void Engine_PieceAppeared(TetrisPieceWithPosition arg) {
            DisplayPiece(arg.Piece, arg.Position);
        }

        private void Engine_PieceMoved(PieceMovedArgs args) {
            DeletePiece(args.Piece, args.OldCoordinates);
            DisplayPiece(args.Piece, args.NewCoordinates);
        }

        private void DisplayPiece(TetrisPiece piece, Coordinates coordinates) {
            Func<TetrisPiece, int, int, string> charGenerator = (piece2, row, col) => {
                bool isBrick = piece2[row, col] != null;
                return (isBrick ? Brick : "");
            };

            DisplayBlock(piece, coordinates, charGenerator);
        }

        private void DeletePiece(TetrisPiece piece, Coordinates oldCoordinates) {
            Func<TetrisPiece, int, int, string> charGenerator = (piece2, row, col) => {
                bool isBrick = piece2[row, col] != null;
                return (isBrick ? " " : "");
            };

            DisplayBlock(piece, oldCoordinates, charGenerator);
        }

        // TODO: Make the Func more elegant
        private void DisplayBlock(TetrisPiece piece, Coordinates coordinates, Func<TetrisPiece, int, int, string> charGenerator) {
            int originColumn = TopLeft.Column + BorderWidth + coordinates.Column;
            int originRow = TopLeft.Row + BorderWidth + coordinates.Row;

            for (int pieceRow = 0; pieceRow < piece.MaxHeight; pieceRow++) {
                for (int pieceColumn = 0; pieceColumn < piece.MaxWidth; pieceColumn++) {
                    int windowRow = originRow + pieceRow;
                    int windowColumn = originColumn + pieceColumn;

                    System.Console.SetCursorPosition(windowColumn, windowRow);
                    System.Console.Write(charGenerator(piece, pieceRow, pieceColumn));
                }
            }
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
