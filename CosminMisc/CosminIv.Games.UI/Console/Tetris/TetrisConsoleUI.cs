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
        Coordinates TopLeft = new Coordinates(10, 20);

        public TetrisConsoleUI(TetrisEngine engine) {
            Engine = engine;
        }

        public void Start() {
            DisplayInitial();
            Engine.Start();
        }

        void DisplayInitial() {
            string initialStateStr = MakeInitialState();
            System.Console.Write(initialStateStr);
        }

        private string MakeInitialState() {
            StringBuilder sb = new StringBuilder((Engine.Rows + 2) * (Engine.Columns + 2));

            sb.Append('-', Engine.Columns);
            sb.AppendLine();

            for (int row = 0; row < Engine.Rows; row++) {
                sb.Append("|");
                sb.Append(' ', Engine.Columns);
                sb.Append("|");
                sb.AppendLine();
            }

            sb.Append('-', Engine.Columns);
            sb.AppendLine();

            return sb.ToString();
        }
    }
}
