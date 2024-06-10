using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIscripts
{
    public class PopUpClose : MonoBehaviour
    {
        [SerializeField] GameObject _toClose;

        public void Close()
        {
            UIManager.OnCrossClick?.Invoke(_toClose);
        }
    }
}

