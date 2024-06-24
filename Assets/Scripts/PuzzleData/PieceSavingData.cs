using UnityEngine;

namespace PuzzleData
{
    public class PieceSavingData
    {
        private Vector2 _worldPostion;
        private Vector2 _gridPosition;
        private float _pieceRotation;
        private bool _isCompleted;

        public PieceSavingData(Vector2 worldPostion, Vector2 gridPosition, float pieceRotation)
        {
            _worldPostion = worldPostion;
            _gridPosition = gridPosition;
            _pieceRotation = pieceRotation;
        }

        public void SetCompelted()
        {
            _isCompleted = true;
        }

        public Vector2 WorldPostion => _worldPostion;

        public Vector2 GridPosition => _gridPosition;

        public float PieceRotation => _pieceRotation;

        public bool IsCompleted => _isCompleted;

    }
}

