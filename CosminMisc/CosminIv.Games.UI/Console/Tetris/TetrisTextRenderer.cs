using System;
using System.Collections.Generic;
using System.Text;

namespace CosminIv.Games.UI.Console.Tetris
{
    class TetrisTextRenderer
    {
        internal void DisplayInitial() {
            DisplayScore(0);
            DisplayLineCount(0);
        }

        internal void DisplayMessage(string message) {
            SetTextColor();
            System.Console.SetCursorPosition(left: 0, top: 0);
            System.Console.Write(message);
        }

        internal void DisplayScore(int score) {
            SetTextColor();
            System.Console.SetCursorPosition(left: 0, top: 1);
            string message = $"{TetrisMessage.Score}: {score}";
            System.Console.Write(message);
        }

        internal void DisplayLineCount(int lineCount) {
            SetTextColor();
            System.Console.SetCursorPosition(left: 0, top: 2);
            string message = $"{TetrisMessage.Lines}: {lineCount}";
            System.Console.Write(message);
        }

        private void SetTextColor() {
            System.Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
