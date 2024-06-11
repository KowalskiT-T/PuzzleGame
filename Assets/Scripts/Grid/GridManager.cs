using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.GameScene;
using GameManagement;

namespace Grid
{   
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private GridGenerator _gridGenerator;
        [SerializeField] private GridInteractionController _gridInteractionController;
        [SerializeField] private GridSO _gridSO;
        [SerializeField] private ScrollViewController _scrollViewController;
        [SerializeField] private GridField _gridField;

        public GridSO GridSO => _gridSO;
        public GridField GridField => _gridField;

        public ScrollViewController ScrollViewController => _scrollViewController;
        

        private void OnEnable()
        {
            LevelManager.LevelStarted += HandleLevelStarted;
        }

        private void OnDisable()
        {
            LevelManager.LevelStarted -= HandleLevelStarted;
        }

        private void HandleLevelStarted(Level level, PuzzleList puzzleList)
        {
            GenerateGrid(level, puzzleList);
        }

        private void GenerateGrid(Level level, PuzzleList puzzleList)
        {
            _gridSO = level.GridSO;

            _gridField.Initialize(_gridSO);

            if(puzzleList.GetPuzzleByID(level.PuzzleID, out var puzzle))
                    _gridGenerator.InitializeGrid(_gridSO, puzzle.PuzzleImage.texture);
            else { Debug.LogError($"GridManager: ERROR WITH PUZZLE ID");}

            _gridInteractionController.SetRotationEnabled(level.RotationEnabled);

            _scrollViewController.PopulateScrollView(_gridGenerator.GeneratedPieces, level.RotationEnabled);

        }

    }
}
