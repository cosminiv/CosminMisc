using System;
using System.Collections.Generic;
using System.Text;

namespace CosminIv.Games.UI.Console.Tetris
{
    class TetrisTextRenderer
    {
        internal void DisplayInitial(int speed) {
            DisplayScore(0);
            DisplayLineCount(0);
            DisplaySpeed(speed);
        }

        internal void DisplayMessage(string message) {
            DisplayMessage(message, 0);
        }

        internal void DisplayScore(int score) {
            DisplayMessage($"{TetrisMessage.Score}: {score}   ", 1);
        }

        internal void DisplayLineCount(int lineCount) {
            DisplayMessage($"{TetrisMessage.Lines}: {lineCount}    ", 2);
        }

        internal void DisplaySpeed(int speed) {
            DisplayMessage($"{TetrisMessage.Speed}: {speed}", 3);
        }

        private void DisplayMessage(string message, int line) {
            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.SetCursorPosition(left: 0, top: line);
            System.Console.Write(message);
        }
    }
}
