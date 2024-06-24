
using System.Collections.Generic;
using System.Numerics;

namespace PuzzleData
{
    public class PieceGroupsSavingData
    {
        private List<PieceSavingData> _piecesGroup;
        private Vector2 _groupWorldPosition;
        private float _groupRotation;

        public PieceGroupsSavingData()
        {
            _piecesGroup = new List<PieceSavingData>();
        }

        public void AddPieceInGroup(PieceSavingData newPiece)
        {
            _piecesGroup.Add(newPiece);
        }

        public void SetGroupPosition(Vector2 position)
        {
            _groupWorldPosition = position;
        }

        public void SetGroupRotation(float rotation)
        {
            _groupRotation = rotation;
        }

        public List<PieceSavingData> PiecesGroup => _piecesGroup;

        public Vector2 GroupWorldPosition => _groupWorldPosition;

        public float GroupRotation => _groupRotation;
    }
}

