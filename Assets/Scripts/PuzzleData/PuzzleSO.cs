using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace PuzzleData
{
    [CreateAssetMenu(fileName = "Puzzle Config", menuName = "Create SO/Puzzle Config")]

    public class PuzzleSO : ScriptableObject
    {
        [SerializeField] private int _id;
        [SerializeField] private string _name;
        [SerializeField] private Sprite _puzzleImage;
        [SerializeField] private bool _isLocked;

        public int ID => _id;
        public string Name => _name;
        public Sprite PuzzleImage => _puzzleImage;
        public bool IsLocked => _isLocked;

    }
}

