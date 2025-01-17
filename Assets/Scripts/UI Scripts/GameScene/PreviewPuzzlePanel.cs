using UnityEngine;
using UnityEngine.UI;
using GameManagement;

public class PreviewPuzzlePanel : MonoBehaviour
{
    [SerializeField] private Image _previewImage;

    private void OnEnable()
    {
        LevelManager.OnLevelStarted += HandleLevelStarted;
    }

    private void OnDisable()
    {
        LevelManager.OnLevelStarted -= HandleLevelStarted;
    }

    private void HandleLevelStarted(Level level, PuzzleList puzzleList)
    {
        if(puzzleList.GetPuzzleByID(level.PuzzleID, out var puzzle))
        {
            _previewImage.sprite = puzzle.PuzzleImage;
        }
        else
        {
            Debug.LogError($"PreviewPuzzlePanel: ERROR BY PUZZLE ID");
        }
        
    }

}
