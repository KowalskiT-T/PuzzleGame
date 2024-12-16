using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
    [CreateAssetMenu(menuName = "Grid/GridSO")]
    public class GridSO : ScriptableObject
    {
        [SerializeField, Min(2)] private int _width;
        [SerializeField, Min(2)] private int _height;
        [SerializeField, Min(1)] private int _coinReward;
        
        public int Width => _width;

        public int Height => _height;

        public int Area => _width * _height;

        public int CoinReward => _coinReward;

        public int Edges => (_width * 2) + (_height * 2) - 4;
    }
}