using System;
using System.Collections.Generic;
using System.Text;

namespace CosminIv.Games.UI.Console.Tetris
{
    class TetrisTextRenderer
    {
        internal void DisplayInitial() {
            DisplayScore(0);
            DisplayLines(0);
        }

        internal void DisplayMessage(string message) {
            System.Console.SetCursorPosition(left: 0, top: 0);
            System.Console.Write(message);
        }

        internal void DisplayScore(int score) {
            System.Console.SetCursorPosition(left: 0, top: 1);
            string message = $"{TetrisMessage.Score}: {score}";
            System.Console.Write(message);
        }

        internal void DisplayLines(int lines) {
            System.Console.SetCursorPosition(left: 0, top: 2);
            string message = $"{TetrisMessage.Lines}: {lines}";
            System.Console.Write(message);
        }
    }
}
