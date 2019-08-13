using System;
using System.Collections.Generic;
using System.Text;
using CosminIv.Games.Common;
using CosminIv.Games.Tetris.DTO;

namespace CosminIv.Games.UI.Console.Tetris
{
    class TetrisTextRenderer
    {
        readonly int ScoreLine = 1;
        readonly int LineCountLine = 2;
        readonly int SpeedLine = 3;
        readonly int NextPieceLine = 5;
        readonly string NextPieceText = $"{TetrisMessage.Next}: ";

        readonly TetrisPieceRenderer PieceRender = new TetrisPieceRenderer();

        internal void Update(TetrisState oldState, TetrisState newState) {
            DisplayNextPiece(oldState?.NextPiece, newState.NextPiece);
            DisplayScore(newState.Score);
            DisplayLineCount(newState.Lines);
            DisplaySpeed(newState.Speed);
        }

        internal void DisplayMessage(string message) {
            DisplayMessage(message, 0);
        }

        private void DisplayScore(int score) {
            DisplayMessage($"{TetrisMessage.Score}: {score}", ScoreLine);
        }

        private void DisplayLineCount(int lineCount) {
            DisplayMessage($"{TetrisMessage.Lines}: {lineCount}", LineCountLine);
        }

        private void DisplaySpeed(int speed) {
            DisplayMessage($"{TetrisMessage.Speed}: {speed}", SpeedLine);
        }

        private void DisplayNextPieceText() {
            DisplayMessage(NextPieceText, NextPieceLine);
        }

        private void DisplayNextPiece(TetrisPiece oldPiece, TetrisPiece newPiece) {
            DisplayNextPieceText();

            bool arePiecesDifferent = 
                (oldPiece == null && newPiece != null) ||
                (oldPiece != null && newPiece == null) ||
                (oldPiece != null && newPiece != null && !oldPiece.Equals(newPiece));

            if (arePiecesDifferent && oldPiece != null)
                DeleteNextPiece(oldPiece);

            if (arePiecesDifferent && newPiece != null) {
                Coordinates coord = new Coordinates(NextPieceLine, NextPieceText.Length);
                PieceRender.Display(newPiece, coord);
            }
        }

        private void DeleteNextPiece(TetrisPiece piece) {
            Coordinates coord = new Coordinates(NextPieceLine, NextPieceText.Length);
            PieceRender.Delete(piece, coord, true);
        }

        private void DisplayMessage(string message, int line) {
            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.SetCursorPosition(left: 0, top: line);
            System.Console.Write(message);
        }
    }
}
