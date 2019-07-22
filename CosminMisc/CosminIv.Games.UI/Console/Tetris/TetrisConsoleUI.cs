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

        public TetrisConsoleUI(TetrisEngine engine) {
            Engine = engine;
        }

        public void Start() {
            DisplayInitial();
            Engine.Start();
        }

        void DisplayInitial() {
            string initialStateStr = MakeInitialState();
            System.Console.SetCursorPosition(0, TopLeft.Row);
            System.Console.Write(initialStateStr);
        }

        private string MakeInitialState() {
            StringBuilder sb = new StringBuilder();

            AddBoardHorizontalBorder(sb);

            for (int row = 0; row < Engine.Rows; row++) {
                sb.Append(' ', TopLeft.Column);
                sb.Append("|");
                sb.Append(' ', Engine.Columns);
                sb.Append("|");
                sb.AppendLine();
            }

            AddBoardHorizontalBorder(sb);

            return sb.ToString();
        }

        private void AddBoardHorizontalBorder(StringBuilder sb) {
            sb.Append(' ', TopLeft.Column);
            sb.Append('-', Engine.Columns + 2);
            sb.AppendLine();
        }
    }
}
