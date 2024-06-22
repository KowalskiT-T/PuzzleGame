using Grid;
using PuzzleData;
using PuzzlePiece;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace PuzzleData
{
    public class PuzzleSavingData
    {
        private int _puzzleID;
        private GridSO _gridSize;
        private List<Vector3> _completedPieces;//22.06.2024 PieceSavingData       
        private PieceConfiguration[,] _pieceConfiguration;
        private bool _rotationEnabled;

        public PuzzleSavingData(int id, GridSO gridSize/*, PieceConfiguration[,] pieceConfiguration*/)
        {
            _puzzleID = id;
            _gridSize = gridSize;
            //_pieceConfiguration = pieceConfiguration;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pieceData"></param>
        public void AddPieces(Vector3 pieceData)
        { 
            _completedPieces.Add(pieceData);
        }

        public int ID => _puzzleID;

        public GridSO Grid => _gridSize;

        public List<Vector3> UncompletedPieces => _completedPieces;

        public PieceConfiguration[,] PieceConfigurations => _pieceConfiguration;

        public bool RotationEnabled => _rotationEnabled;
    }

}
