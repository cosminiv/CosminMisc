using CosminIv.Games.Tetris.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CosminIv.Games.Tetris
{
    internal class TetrisSolverScoreComputer
    {
        internal double ComputeScore(TetrisState state) {
            double brickScore = ComputeBrickScore(state);
            double holeScore = ComputeHoleScore(state);
            double score = brickScore + holeScore;
            return score;
        }

        private double ComputeBrickScore(TetrisState state) {
            double result = 0 - CountBricks(state);
            return result;
        }

        private static double CountBricks(TetrisState state) {
            int count = 0;

            for (int row = 0; row < state.FixedBricks.Length; row++) {
                for (int col = 0; col < state.FixedBricks[row].Length; col++) {
                    if (state.FixedBricks[row][col] != null)
                        count++;
                }
            }

            return count;
        }

        private double ComputeHoleScore(TetrisState state) {
            double result = 0 - CountHoles(state);
            return result;
        }

        private int CountHoles(TetrisState state) {
            int holes = 0;

            for (int col = 0; col < state.Columns; col++) {
                bool foundBrick = false;
                for (int row = 0; row < state.Rows; row++) {
                    TetrisBrick brick = state.FixedBricks[row][col];

                    if (brick != null)
                        foundBrick = true;

                    if (brick == null && foundBrick)
                        holes++;
                }
            }

            return holes;
        }
    }
}
