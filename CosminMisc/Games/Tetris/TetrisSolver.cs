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

        public TetrisMoves Solve(TetrisState state) {
            return GetBestMoves(state);
        }

        private TetrisMoves GetBestMoves(TetrisState state) {
            double maxScore = double.MinValue;
            TetrisMoves bestMoves = null;

            foreach (TetrisMoves moves in MakePossibleSolutions(state)) {
                Debug.WriteLine(moves);
                TetrisState solutionState = ComputeStateAfterMoves(state, moves);
                double score = ComputeScore(solutionState);

                if (score > maxScore) {
                    bestMoves = moves;
                    maxScore = score;
                }
            }

            return bestMoves;
        }

        private IEnumerable<TetrisMoves> MakePossibleSolutions(TetrisState state) {
            int crtPieceColumn = state.CurrentPiece.Position.Column;

            for (int rotations = 0; rotations < 4; rotations++) {
                TetrisMoves solution = MakeSolution(rotations, 0, 0);
                yield return solution;

                for (int leftMoves = 1; leftMoves < crtPieceColumn; leftMoves++) {
                    TetrisMoves solutionsLeft = MakeSolution(rotations, leftMoves, 0);
                    yield return solutionsLeft;
                }

                for (int rightMoves = 1; rightMoves < state.Columns - crtPieceColumn; rightMoves++) {
                    TetrisMoves solutionsRight = MakeSolution(rotations, 0, rightMoves);
                    yield return solutionsRight;
                }
            }
        }

        private TetrisMoves MakeSolution(int rotations, int leftMoves, int rightMoves) {
            TetrisMoves solution = new TetrisMoves();

            for (int i = 0; i < rotations; i++)
                solution.Moves.Add(TetrisMove.Rotate);

            for (int i = 0; i < leftMoves; i++)
                solution.Moves.Add(TetrisMove.MoveLeft);

            for (int i = 0; i < rightMoves; i++)
                solution.Moves.Add(TetrisMove.MoveRight);

            return solution;
        }

        private TetrisState ComputeStateAfterMoves(TetrisState initialState, TetrisMoves moves) {
            TetrisState prevState = initialState;
            TetrisState finalState = initialState;

            foreach (TetrisMove move in moves.Moves) {
                finalState = ComputeStateAfterOneMove(initialState, move);
                prevState = finalState;
            }

            return finalState;
        }

        private TetrisState ComputeStateAfterOneMove(TetrisState initialState, TetrisMove move) {
            TetrisState finalState = null;

            switch (move) {
                case TetrisMove.MoveLeft:
                    finalState = Engine.MovePieceLeft(initialState);
                    break;
                case TetrisMove.MoveRight:
                    finalState = Engine.MovePieceRight(initialState);
                    break;
                case TetrisMove.MoveDown:
                    finalState = Engine.MovePieceDown(initialState);
                    break;
                case TetrisMove.MoveAllTheWayDown:
                    finalState = Engine.MovePieceAllTheWayDown(initialState);
                    break;
                case TetrisMove.Rotate:
                    finalState = Engine.RotatePiece(initialState);
                    break;
                default:
                    throw new Exception($"Unknown move: {move}");
            }

            return finalState;
        }

        private double ComputeScore(TetrisState state) {
            return 0;
        }
    }
}
