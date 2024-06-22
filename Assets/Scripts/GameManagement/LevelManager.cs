using UnityEngine;
using Grid;
using System;
using PuzzleData;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using Player;

namespace GameManagement
{
    public class LevelManager : MonoBehaviour
    {
        public static Action<Level, PuzzleList> OnLevelStarted;

        [SerializeField] private PuzzleList _puzzleList;
        [SerializeField] private ProgressManager _progressManager;
        [SerializeField] private LevelDebugShell _debugLevel;

        private void OnEnable()
        {
            
        }
        private void Start()
        {
            Level currentLevel = SetupCurrentLevel();

            _progressManager.SetNumberOfPieces(currentLevel.GridSO.Area);
           
            StartLevel(currentLevel);
        }

        private Level SetupCurrentLevel()
        {
            if (PlayerData.Instance == null) 
            {
                return new Level(_debugLevel.GridSO, _debugLevel.PuzzleSO.ID, _debugLevel.RotationEnabled);
            }
            return PlayerData.Instance.CurrentPuzzle;
        }

        private void StartLevel(Level level)
        {
            OnLevelStarted?.Invoke(level, _puzzleList);
        }

    }
}
