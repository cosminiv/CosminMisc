using System.Collections.Generic;
using CosminIv.Games.Engine.Tetris.DTO;

namespace CosminIv.Games.Engine.Tetris
{
    internal class TetrisPieceWithPositionCopy
    {
        // The key is the piece size
        Dictionary<int, TetrisPieceWithPosition> Copies = new Dictionary<int, TetrisPieceWithPosition>();

        internal void CopyFrom(TetrisPieceWithPosition original) {
            int maxPieceSize = original.Piece.MaxSize;

            if (!Copies.TryGetValue(maxPieceSize, out TetrisPieceWithPosition existingCopy)) {
                existingCopy = new TetrisPieceWithPosition { Piece = new TetrisPiece(maxPieceSize) };
                Copies.Add(maxPieceSize, existingCopy);
            }

            existingCopy.CopyFrom(original);
            Value = existingCopy;
        }

        internal TetrisPieceWithPosition Value { get; private set; }
    }
}
