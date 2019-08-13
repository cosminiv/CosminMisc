using CosminIv.Games.Tetris.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CosminIv.Games.Tetris
{
    public class TetrisSolver
    {
        TetrisEngine Engine;

        public TetrisSolver(int rows, int columns) {
            Engine = new TetrisEngine(new TetrisEngineSettings {
                Rows = rows,
                Columns = columns,
                EnableTimer = false
            });
        }

        public TetrisSolution Solve(TetrisState state) {
            return GetBestSolution(state);
        }

        private TetrisSolution GetBestSolution(TetrisState state) {
            double maxScore = double.MinValue;
            TetrisSolution bestSolution = null;

            foreach (TetrisSolution solution in MakePossibleSolutions(state)) {
                Debug.WriteLine(solution);
                double score = ComputeScore(solution);

                if (score > maxScore) {
                    bestSolution = solution;
                    maxScore = score;
                }
            }

            return bestSolution;
        }

        private IEnumerable<TetrisSolution> MakePossibleSolutions(TetrisState state) {
            int crtPieceColumn = state.CurrentPiece.Position.Column;

            for (int rotations = 0; rotations < 4; rotations++) {
                TetrisSolution sol = MakeSolution(rotations, 0, 0);
                yield return sol;

                for (int leftMoves = 1; leftMoves < crtPieceColumn; leftMoves++) {
                    TetrisSolution solLeft = MakeSolution(rotations, leftMoves, 0);
                    yield return solLeft;
                }

                for (int rightMoves = 1; rightMoves < state.Columns - crtPieceColumn; rightMoves++) {
                    TetrisSolution solRight = MakeSolution(rotations, 0, rightMoves);
                    yield return solRight;
                }
            }
        }

        private TetrisSolution MakeSolution(int rotations, int leftMoves, int rightMoves) {
            TetrisSolution solution = new TetrisSolution();

            for (int i = 0; i < rotations; i++)
                solution.Moves.Add(TetrisMove.Rotate);

            for (int i = 0; i < leftMoves; i++)
                solution.Moves.Add(TetrisMove.MoveLeft);

            for (int i = 0; i < rightMoves; i++)
                solution.Moves.Add(TetrisMove.MoveRight);

            return solution;
        }

        private double ComputeScore(TetrisSolution solution) {
            return 0;
        }
    }
}
