using PuzzleData;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

    [CreateAssetMenu(fileName = "PuzzlesList", menuName = "Create SO/Puzzles List")]
    public class PuzzleList : ScriptableObject
    {

        [SerializeField] private List<PuzzleSO> _puzzleList = new();

        public List<PuzzleSO> List => _puzzleList;

        public bool GetPuzzleByID(int id, out PuzzleSO puzzleSo)
        {
            puzzleSo = _puzzleList.FirstOrDefault(puzzle => puzzle.Id == id);
            if(puzzleSo == null)
            {
                Debug.LogError($"PUZZLE CAN NOT BE FOUND id = {id}");
                return false;
            }
            return puzzleSo != null;
        }
    }


