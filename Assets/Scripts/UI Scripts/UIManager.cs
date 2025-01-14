using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Player;
using System;
using Grid;
using UnityEngine.SceneManagement;
using PuzzleData;

namespace UIscripts
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private List<Button> _buttonsInteractivity;
        [SerializeField] private Color _buttonTextColorBasic;
        [SerializeField] private Color _buttonTextColorClicked;

        [Space]

        [SerializeField] private PuzzleList _puzzles;
        [SerializeField] private GameObject _puzzleParent;
        [SerializeField] private PuzzlePanelUI _puzzlePrefab;

        [Space]

        [SerializeField] private GameObject _playerPuzzleParent;

        [Space]

        [SerializeField] private GameObject _puzzleToBuyPopUp;
        [SerializeField] private PuzzlePanelUI _puzzleToBuyPopUpObject;

        [Space]
        [SerializeField] private List<TextMeshProUGUI> _coinsText;

        [Space]
        [SerializeField] private GameObject _puzzleLoaderObject;
        [SerializeField] private PuzzlePanelUI _puzzleToChoose;

        public static Action<GameObject> OnCrossClick;
        public static Action<Button> OnPanelsChange;
        public static Action<int> OnLockedPanelClick;
        public static Action<int> OnPanelClick;

        public delegate void OnRotationChange();
        public static OnRotationChange onRotationChange;

        [SerializeField] private GridSOList _diffucultiesList;
        [SerializeField] private LevelConfigurator _levelConfigurator;
        private int startingGridSOindex = 0;
        private bool startingRotationRule;

        [Space]
        [Header("Scroll")]

        [SerializeField] private GameObject _scrollParent;
        [SerializeField] private ScrollElement _scrollPrefab;

        private void OnEnable()
        {
            OnPanelsChange += TurnIterectableButton;
            OnCrossClick += CloseWindow;
            OnLockedPanelClick += LoadBuyPanelPopUp;
            OnPanelClick += LoadPuzzleDifficultyChooser;
            onRotationChange += ChangeRotation;
            PuzzlePrepareUI.ScrollItemChanged += SetCurrentGridSO;
            PlayerData.onCoinsChanged += LoadCoins;
        }
        private void OnDestroy()
        {
            OnPanelsChange -= TurnIterectableButton;
            OnCrossClick -= CloseWindow;
            OnLockedPanelClick -= LoadBuyPanelPopUp;
            OnPanelClick -= LoadPuzzleDifficultyChooser;
            PuzzlePrepareUI.ScrollItemChanged -= SetCurrentGridSO;
            onRotationChange -= ChangeRotation;
            PlayerData.onCoinsChanged -= LoadCoins;
        }

        private void OnDisable()
        {
            OnPanelsChange -= TurnIterectableButton;
            OnCrossClick -= CloseWindow;
            OnLockedPanelClick -= LoadBuyPanelPopUp;
            OnPanelClick -= LoadPuzzleDifficultyChooser;
            PuzzlePrepareUI.ScrollItemChanged -= SetCurrentGridSO;
        }

        private void Start()
        {
            LoadAllPuzzles();
            LoadPlayerPuzzles();
            LoadCoins();
            LoadDifficulties();
        }

        public void LoadAllPuzzles()
        {
            _puzzles.List.ForEach(puzzle => 
             Instantiate(_puzzlePrefab, _puzzleParent.transform)
            .LoadPuzzlePanel(puzzle.PuzzleImage, puzzle.IsLocked, puzzle.ID));
        }
        public void LoadPlayerPuzzles()
        {
            if (PlayerData.Instance.SavedPuzzles == null)
                return;
            
            foreach (var playerPuzzle in PlayerData.Instance.SavedPuzzles)
            {
                _puzzles.List.ForEach(puzzle =>
                {
                    if (playerPuzzle.ID == puzzle.ID)
                    {
                        Instantiate(_puzzlePrefab, _playerPuzzleParent.transform).
                        LoadPuzzlePanel(puzzle.PuzzleImage, playerPuzzle.ID);
                        return;
                    }
                });
            }

        }
        public void StartPuzzle()
        {
            PlayerData.Instance.SetCurrentPuzzle(_levelConfigurator.CurrentLevel);
            SceneManager.LoadScene("Main");
        }
        private void LoadDifficulties()
        {
            for (int i = 0; i < _diffucultiesList.GridDiffucultiesList.Count; i++)
            {
                Instantiate(_scrollPrefab, _scrollParent.transform)
                .LoadScrollElement(_diffucultiesList.GridDiffucultiesList[i], i);
            }
        }

        public void ChangeRotation()
        {
            _levelConfigurator.CurrentLevel.SetToggleRotation();
        }

        #region MenuButtonsInteraction
        private void TurnIterectableButton(Button button)
        {
            foreach(Button buttons in _buttonsInteractivity)
            {
                buttons.interactable = true;
                buttons.GetComponentInChildren<TextMeshProUGUI>().color = _buttonTextColorBasic;
            }
            button.interactable = false;
            button.GetComponentInChildren<TextMeshProUGUI>().color = _buttonTextColorClicked;
        }

        private void CloseWindow(GameObject ToClose)
        {
            ToClose.SetActive(false);
        }
        #endregion

        #region ChoosePuzzleInteraction
        public void LoadBuyPanelPopUp(int puzzleID)
        {
            foreach (var puzzle in _puzzles.List)
            {
                if (puzzleID == puzzle.ID)
                {
                    _puzzleToBuyPopUpObject.LoadPuzzlePanel(puzzle.PuzzleImage, puzzle.ID);
                    _puzzleToBuyPopUp.SetActive(true);
                    break;
                }
            }
        }
        public void LoadPuzzleDifficultyChooser(int puzzleID)
        {
            if (_puzzles.GetPuzzleByID(puzzleID, out var puzzle))
            {
                _puzzleToChoose.LoadPuzzlePanel(puzzle.PuzzleImage, puzzle.ID);
                _puzzleLoaderObject.SetActive(true);
                _levelConfigurator.CurrentLevel.SetGridSO(_diffucultiesList.GridDiffucultiesList[startingGridSOindex]);
                _levelConfigurator.CurrentLevel.SetPuzzleID(puzzle.ID);
                _levelConfigurator.CurrentLevel.SetRotation(startingRotationRule);

            }
            else
            {
                Debug.LogError($"UIManager: ERROR BY PUZZLE ID");
            }
 
        }
        private void SetCurrentGridSO(int index)
        {
            _levelConfigurator.CurrentLevel.SetGridSO(_diffucultiesList.GridDiffucultiesList[index]);
        }
        #endregion

        #region CoinsLoading

        public void LoadCoins()
        {
            _coinsText.ForEach(text => text.text = PlayerData.Instance.CoinsAmount.ToString());
        }

        #endregion

    }
}

