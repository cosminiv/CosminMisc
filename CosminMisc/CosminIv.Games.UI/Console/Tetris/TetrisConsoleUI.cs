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
        readonly int WindowHeight = 28;
        readonly int WindowWidth = 60;
        readonly int BorderWidth = 1;
        readonly char Brick = '#';

        public TetrisConsoleUI(TetrisEngine engine) {
            SetWindowSize();
            Engine = engine;
            WireEventHandlers();
        }

        public void Start() {
            DisplayBoard();
            Engine.Start();
            System.Console.CursorVisible = false;
        }


        private void SetWindowSize() {
            System.Console.SetBufferSize(WindowWidth, WindowHeight);
            System.Console.SetWindowSize(WindowWidth, WindowHeight);
        }

        private void WireEventHandlers() {
            Engine.PieceAppeared += Engine_PieceAppeared;
            Engine.PieceMoved += Engine_PieceMoved;
        }

        private void Engine_PieceAppeared(TetrisPieceWithPosition arg) {
            DisplayPiece(arg.Piece, arg.Position);
        }

        private void Engine_PieceMoved(PieceMovedArgs args) {
            DeletePiece(args.Piece, args.OldCoordinates);
            DisplayPiece(args.Piece, args.NewCoordinates);
        }

        private void DisplayPiece(TetrisPiece piece, Coordinates coordinates) {
            DisplayInPlaceOfPiece(piece, coordinates, Brick);
        }

        private void DeletePiece(TetrisPiece piece, Coordinates coordinates) {
            DisplayInPlaceOfPiece(piece, coordinates, ' ');
        }

        private void DisplayInPlaceOfPiece(TetrisPiece piece, Coordinates pieceBoardCoord, char displayChar) {
            int boardOriginColumn = TopLeft.Column + BorderWidth + pieceBoardCoord.Column;
            int boardOriginRow = TopLeft.Row + BorderWidth + pieceBoardCoord.Row;

            for (int pieceRow = 0; pieceRow < piece.MaxHeight; pieceRow++) {
                for (int pieceColumn = 0; pieceColumn < piece.MaxWidth; pieceColumn++) {
                    int windowRow = boardOriginRow + pieceRow;
                    int windowColumn = boardOriginColumn + pieceColumn;
                    bool isBrick = piece[pieceRow, pieceColumn] != null;

                    System.Console.SetCursorPosition(windowColumn, windowRow);
                    System.Console.Write(isBrick ? displayChar.ToString() : "");
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
