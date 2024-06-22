using UnityEngine;
using PuzzleData;
using Grid;

namespace GameManagement
{
    public class Level
    {
        private GridSO _gridSO;
        private int _puzzleID;
        private bool _rotationEnabled;

        public GridSO GridSO => _gridSO;
        public int PuzzleID => _puzzleID; 
        public bool RotationEnabled => _rotationEnabled;

        public Level(GridSO gridSO, int puzzleID, bool rotationEnabled)
        {
            _gridSO = gridSO;
            _puzzleID = puzzleID;
            _rotationEnabled = rotationEnabled;
        }

        public Level()
        { }

        public void SetGridSO(GridSO newGridSO)
        {
            _gridSO = newGridSO;
        }

        public void SetRotation(bool rotation)
        {
            _rotationEnabled = rotation;
        }

        public void SetPuzzleID(int ID)
        {
            _puzzleID = ID;
        }

        public void SetToggleRotation()
        {
            if(_rotationEnabled)
                _rotationEnabled = false;
            else
            {
                _rotationEnabled = true;
            }
        }
    }
}