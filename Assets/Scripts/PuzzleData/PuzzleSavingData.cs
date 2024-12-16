using Grid;
using PuzzlePiece;
using System.Collections.Generic;

namespace PuzzleData
{
    public class PuzzleSavingData
    {
        public int ID { get; set; }

        public GridSO Grid { get; set; }

        public List<PieceSavingData> Pieces { get; set; }

        public List<PieceGroupsSavingData> PiecesGroups { get; set; }

        public bool RotationEnabled { get; set; }

        public int LevelSeed { get; set; }     
    }
}
