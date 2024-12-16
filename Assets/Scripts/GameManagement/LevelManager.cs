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
        public static Action<PuzzleSavingData> OnLevelEnd;

        [SerializeField] private PuzzleList _puzzleList;
        [SerializeField] private ProgressManager _progressManager;
        [SerializeField] private LevelDebugShell _debugLevel;

        private PuzzleSavingData _savingPuzzle;

        private void OnEnable()
        {
            
        }

        private void OnDestroy()
        {

        }

        private void Start()
        {
            Level currentLevel = SetupCurrentLevel();

            if(PlayerData.Instance.TryGetSavedPuzzle(currentLevel.PuzzleID, out var puzzle))
            {
                _progressManager.SetNumberOfPieces(puzzle.Grid);
                UnityEngine.Random.InitState(puzzle.LevelSeed);
                Debug.Log($"Load Saved Level {puzzle.ID}");
                //start saved level 24.06.2024
            }
            else
            {
                _progressManager.SetNumberOfPieces(currentLevel.GridSO);
                var seed = DateTime.Now.ToString("yyyyMMddHHmmssfff").GetHashCode();
                UnityEngine.Random.InitState(seed);
                _savingPuzzle = PuzzleSavingDataBuilder.Empty()
                    .WithPuzzleID(currentLevel.PuzzleID)
                    .WithGrid(currentLevel.GridSO)
                    .WithSeed(seed)
                    .Build();
                Debug.Log($"Level Saved {_savingPuzzle.ID}");
                SaveLevel();
                     
                StartLevel(currentLevel);
            }
            
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

        public void SaveLevel()
        {
            PlayerData.Instance.SavePlayerPuzzleProgress(_savingPuzzle);
        }

    }
}
