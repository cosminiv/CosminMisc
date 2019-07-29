using CosminIv.Games.Common;
using CosminIv.Games.Tetris;
using CosminIv.Games.Tetris.EventArguments;
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
        TetrisFixedBricksState FixedBricks;

        public TetrisBoardRenderer(TetrisEngine engine, Coordinates topLeft, int borderWidth) {
            Engine = engine;
            BoardWindowOrigin = topLeft;
            BorderWidth = borderWidth;
        }

        public void Display(GameInitializedArgs args) {
            FixedBricks = args.FixedBricks;
            string boardString = MakeBoardString();
            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.SetCursorPosition(left: 0, top: BoardWindowOrigin.Row);
            System.Console.Write(boardString);
        }

        private string MakeBoardString() {
            StringBuilder sb = new StringBuilder();

            AddBoardHorizontalBorder(sb);

            for (int row = 0; row < Engine.Rows; row++) {
                sb.Append(' ', BoardWindowOrigin.Column);
                sb.Append('|', BorderWidth);

                for (int column = 0; column < Engine.Columns; column++)
                    AddCharacter(sb, row, column);
                
                sb.Append('|', BorderWidth);
                sb.AppendLine();
            }

            AddBoardHorizontalBorder(sb);

            return sb.ToString();
        }

        private void AddCharacter(StringBuilder sb, int row, int column) {
            if (FixedBricks.RowsStartIndex < 0) {
                sb.Append(" ");
                return;
            }

            int row2 = row - FixedBricks.RowsStartIndex;
            if (row2 < 0) {
                sb.Append(" ");
                return;
            }

            TetrisBrick brick = FixedBricks.Rows[row2][column];

            if (brick != null) {
                sb.Append("#");
            }
            else
                sb.Append(" ");
        }

        private void AddBoardHorizontalBorder(StringBuilder sb) {
            sb.Append(' ', BoardWindowOrigin.Column);
            sb.Append('-', Engine.Columns + 2 * BorderWidth);
            sb.AppendLine();
        }
    }
}
