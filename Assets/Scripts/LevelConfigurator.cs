using GameManagement;
using UnityEngine;
using Player;


public class LevelConfigurator : MonoBehaviour
{
    private Level _currentLevel;
    private bool _isSavedPuzzle;

    private void Awake()
    {
        if(PlayerData.Instance.CurrentPuzzle == null)
            _currentLevel = new Level();
        else
            _currentLevel = PlayerData.Instance.CurrentPuzzle;

        CheckStartedPlayerPuzzle();
    }

    public void CheckStartedPlayerPuzzle()
    {
        PlayerData.Instance.SavedPuzzles?.ForEach(puzzle =>
        {
            if(puzzle.ID == _currentLevel.PuzzleID)
            {
                _currentLevel.SetGridSO(puzzle.Grid);
                _currentLevel.SetRotation(puzzle.RotationEnabled);
                //_isSavedPuzzle = true;
                return;
            }
        });
    }

    public Level CurrentLevel => _currentLevel;

    public bool IsSavedPuzzle => _isSavedPuzzle;
}
