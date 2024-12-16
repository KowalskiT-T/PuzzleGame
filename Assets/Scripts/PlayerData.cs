using System.Collections.Generic;
using UnityEngine;
using PuzzleData;
using Newtonsoft.Json;
using GameManagement;
using Grid;
using System.Linq;

namespace Player
{
    public class PlayerData : MonoBehaviour
    {

        private readonly string _coinsPrefs = "player_coins";
        private readonly string _hintsPrefs = "player_hints";
        private readonly string _savedPuzzlesPref = "player_savedPuzzle";
        private readonly string _currentLevel = "player_savedCurrentPuzzle";
        //private readonly string _savedCurrentPuzzlePref = "player_savedCurrentPuzzle";
        private readonly string _themePref = "player_theme";

        private int _coinsAmount;
        private int _hintsAmount;
        private List<PuzzleSavingData> _savedPuzzles;
        private Level _currentPuzzle;

        public delegate void OnConsumableChanges();
        public static OnConsumableChanges onCoinsChanged;

        private int _themeID;

        #region Singleton Pattern
        public static PlayerData Instance { get; private set; }

        private void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this;
            LoadAllPlayerData();
        }
        #endregion

        #region Saving
        public void LoadAllPlayerData()
        {
            _coinsAmount = PlayerPrefs.GetInt(_coinsPrefs, 1000);
            Debug.Log("Coins" + _coinsAmount);  
            _hintsAmount = PlayerPrefs.GetInt(_hintsPrefs, 3);
            Debug.Log("Hints" + _hintsAmount);
            _savedPuzzles = JsonConvert.DeserializeObject<List<PuzzleSavingData>>(PlayerPrefs.GetString(_savedPuzzlesPref));            
            if(PlayerPrefs.GetString(_currentLevel) != null)
            {
                _currentPuzzle = JsonConvert.DeserializeObject<Level>(PlayerPrefs.GetString(_currentLevel));
            }
            _savedPuzzles = JsonConvert.DeserializeObject<List<PuzzleSavingData>>(PlayerPrefs.GetString(_savedPuzzlesPref));           
            _themeID = PlayerPrefs.GetInt(_themePref, 0);
        }

        public void SaveThemeID(int id)
        {
            _themeID = id;
            PlayerPrefs.SetInt(_themePref, id);
        }
        public void SavePlayerPuzzleProgress(PuzzleSavingData puzzleToSave)
        {
            _savedPuzzles.Add(puzzleToSave);
            string savedPuzzles = JsonConvert.SerializeObject(_savedPuzzles);
            PlayerPrefs.SetString(_savedPuzzlesPref, savedPuzzles);
        }
        public void SetCurrentPuzzle(Level puzzle)
        {
            _currentPuzzle = puzzle;
            PlayerPrefs.SetString(_currentLevel, JsonConvert.SerializeObject(puzzle));
        }

        public void SavePlayerPuzzleProgress()
        {
            if (_savedPuzzles != null)
            {
                string savedPuzzles = JsonConvert.SerializeObject(_savedPuzzles);
                PlayerPrefs.SetString(_savedPuzzlesPref, savedPuzzles);
            }
        }
        #endregion

        #region AddingRemovingConsumables
        public void AddCoins(int reward)
        {
            _coinsAmount += reward;
            PlayerPrefs.SetInt(_coinsPrefs, _coinsAmount);
            onCoinsChanged?.Invoke();
        }
        public void SpendCoins(int amount)
        {
            _coinsAmount -= amount;
            PlayerPrefs.SetInt(_coinsPrefs, _coinsAmount);
            onCoinsChanged?.Invoke();
        }

        public void AddHints(int reward)
        {
            _hintsAmount += reward;
            PlayerPrefs.SetInt(_coinsPrefs, _hintsAmount);
        }

        public void UseHint()
        {
            _hintsAmount--;
            Debug.Log("Hint Used");
            PlayerPrefs.SetInt(_hintsPrefs, _hintsAmount);
        }

        #endregion

        #region Search

        public bool TryGetSavedPuzzle(int puzzleID, out PuzzleSavingData savedPuzzle)
        {
            savedPuzzle = _savedPuzzles?.FirstOrDefault(puzzle => puzzleID == puzzle.ID);
            if (savedPuzzle == null)
            {
                //Debug.LogError($"PUZZLE CAN NOT BE FOUND id = {puzzleID}");
                return false;
            }
            return savedPuzzle != null;
        }

        #endregion

        public int CoinsAmount => _coinsAmount;
        public int HintsAmount => _hintsAmount;
        public List<PuzzleSavingData> SavedPuzzles => _savedPuzzles;
        public Level CurrentPuzzle => _currentPuzzle;
        public int ThemeID => _themeID;
    }
}

