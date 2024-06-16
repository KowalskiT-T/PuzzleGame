using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RotationButton : MonoBehaviour
{
    [SerializeField] private RectTransform _slider;

    [SerializeField] private RectTransform _turnedONRotationRT;
    [SerializeField] private RectTransform _turnedOFFRotationRT;

    [SerializeField] private Image _turnedONRotationIMG;
    [SerializeField] private Image _turnedOFFRotationIMG;

    //[SerializeField] private Button _turnedONRotationBTN;
    //[SerializeField] private Button _turnedOFFRotationBTN;

    [SerializeField] private Button _toggleButton;

    private float _moveX = 70f;
    private float _moveXDuration = 0.5f;
    private bool _isTurnedON;

    private Sequence _toggleONSequence;
    private Sequence _toggleOFFSequence;


    private void Awake()
    {
        _toggleONSequence = DOTween.Sequence();
        _toggleOFFSequence = DOTween.Sequence();
    }
    private void OnEnable()
    {
        //_toggleONSequence.
        //    Append(_slider.DOLocalMoveX(_moveX, _moveXDuration)).
        //    Append(_turnedONRotationRT.DOLocalRotate(new Vector3(0, 0, -25), _moveXDuration)).
        //    Append(_turnedONRotationRT.DOLocalRotate(new Vector3(0, 0, 0), _moveXDuration));
        //_toggleOFFSequence.
        //  Append(_slider.DOLocalMoveX(-_moveX, _moveXDuration));
        //_toggleButton.onClick.AddListener(CheckRotation);
        _moveX = _slider.sizeDelta.x / 2;       
       
    }
    private void OnDisable()
    {
        //_toggleButton.onClick.RemoveListener(CheckRotation);
    }
    public void CheckRotation()
    {
        if(_isTurnedON)
        {
            TurnOFFRotation();
        }
        else
        {
            TurnONRotation();
        }
    }

    private void TurnONRotation()
    {
        _isTurnedON = true;
        //_toggleONSequence.Play();
        _slider.DOLocalMoveX(_moveX, _moveXDuration);
        _turnedONRotationRT.DOLocalRotate(new Vector3(0, 0, -25), _moveXDuration);
        _turnedONRotationRT.DOLocalRotate(new Vector3(0, 0, 0), _moveXDuration);
    }

    private void TurnOFFRotation()
    {
        _isTurnedON = false;
        _slider.DOLocalMoveX(-_moveX, _moveXDuration);
    }
}
