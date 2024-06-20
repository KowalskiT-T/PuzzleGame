using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIscripts;
using Player;

public class PuzzlePopUp : MonoBehaviour
{
    [SerializeField] private PuzzlePanelUI _puzzlePanelUI;
    [SerializeField] private GameObject _gameObject;
    
    public void LoadDifficultyPanel()
    {       
        if(PlayerData.Instance.CoinsAmount >= 1000)
        {
            UIManager.OnPanelClick?.Invoke(_puzzlePanelUI.PuzzleID);
            _gameObject.SetActive(false);
            PlayerData.Instance.SpendCoins(1000);
        }

    }
}
