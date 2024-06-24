using Grid;
using PuzzlePiece;
using System.Collections.Generic;

namespace PuzzleData
{
    public class PuzzleSavingData
    {
        public int ID { get; private set; }

        public GridSO Grid { get; private set; }

        public List<PieceSavingData> Pieces { get; private set; }

        public List<PieceGroupsSavingData> PiecesGroups { get; private set; }

        public bool RotationEnabled { get; private set; }

        public int LevelSeed { get; private set; }

        private PuzzleSavingData() { }

        private void SetIndex(int id) => ID = id;

        private void SetGrid(GridSO grid) => Grid = grid;

        private void SetRotation(bool rotation) => RotationEnabled = rotation;

        private void SetPieces(List<PieceSavingData> pieces) => Pieces = pieces;

        private void SetGroups(List<PieceGroupsSavingData> piecesGroups) => PiecesGroups = piecesGroups;

        private void SetSeed(int seed) => LevelSeed = seed;

        public class Builder
        {
            private readonly PuzzleSavingData _puzzleSavingData = new PuzzleSavingData();

            public Builder AddIndex(int ID)
            {
                _puzzleSavingData.SetIndex(ID);
                return this;
            }

            public Builder AddGrid(GridSO grid)
            {
                _puzzleSavingData.SetGrid(grid);
                return this;
            }

            public Builder AddRotation(bool rotation)
            {
                _puzzleSavingData.SetRotation(rotation);
                return this;
            }

            public Builder AddPieces(List<PieceSavingData> pieces)
            {
                _puzzleSavingData.SetPieces(pieces);
                return this;
            }

            public Builder AddPiecesGroups(List<PieceGroupsSavingData> groups)
            {
                _puzzleSavingData.SetGroups(groups);
                return this;
            }

            public Builder AddSeed(int seed)
            {
                _puzzleSavingData.SetSeed(seed);
                return this;
            }

            public PuzzleSavingData Build()
            {
                return _puzzleSavingData;
            }
        }
    }
}
