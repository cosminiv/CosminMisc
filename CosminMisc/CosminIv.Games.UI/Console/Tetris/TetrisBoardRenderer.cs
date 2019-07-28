using CosminIv.Games.Common;
using CosminIv.Games.Tetris;
using System;
using System.Collections.Generic;
using System.Text;

namespace CosminIv.Games.UI.Console.Tetris
{
    internal class TetrisBoardRenderer
    {
        TetrisEngine Engine;
        int BorderWidth;
        Coordinates BoardWindowOrigin;

        public TetrisBoardRenderer(TetrisEngine engine, Coordinates topLeft, int borderWidth) {
            Engine = engine;
            BoardWindowOrigin = topLeft;
            BorderWidth = borderWidth;
        }

        public void Display() {
            string boardString = MakeBoardString();
            System.Console.SetCursorPosition(left: 0, top: BoardWindowOrigin.Row);
            System.Console.Write(boardString);
        }

        private string MakeBoardString() {
            StringBuilder sb = new StringBuilder();

            AddBoardHorizontalBorder(sb);

            for (int row = 0; row < Engine.Rows; row++) {
                sb.Append(' ', BoardWindowOrigin.Column);
                sb.Append('|', BorderWidth);
                sb.Append(' ', Engine.Columns);
                sb.Append('|', BorderWidth);
                sb.AppendLine();
            }

            AddBoardHorizontalBorder(sb);

            return sb.ToString();
        }

        private void AddBoardHorizontalBorder(StringBuilder sb) {
            sb.Append(' ', BoardWindowOrigin.Column);
            sb.Append('-', Engine.Columns + 2 * BorderWidth);
            sb.AppendLine();
        }
    }
}
