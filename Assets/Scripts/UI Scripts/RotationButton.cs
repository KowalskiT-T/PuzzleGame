using DG.Tweening;
using TMPro;
using UIscripts;
using UnityEngine;
using UnityEngine.UI;

public class RotationButton : MonoBehaviour
{
    [SerializeField] private RectTransform _slider;

    [SerializeField] private RectTransform _turnedONRotationRT;
    [SerializeField] private RectTransform _turnedOFFRotationRT;

    [SerializeField] private Image _turnedONRotationIMG;
    [SerializeField] private Image _turnedOFFRotationIMG;
    [SerializeField] private Color _turnedONRotationColor;

    [SerializeField] private Button _toggleButton;

    [SerializeField] private TextMeshProUGUI _rotatePartText;

    private float _moveX = 70f;
    private float _moveXDuration = 0.5f;
    private float _colorDuration = 1f;
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
        _toggleButton.onClick.AddListener(CheckRotation);
        _moveX = _slider.sizeDelta.x / 2;              
    }
    private void OnDisable()
    {
        _toggleButton.onClick.RemoveListener(CheckRotation);
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
        UIManager.onRotationChange?.Invoke();
    }

    private void TurnONRotation()
    {
        _isTurnedON = true;
        _rotatePartText.text = "Rotate parts: Yes";
        _slider.DOLocalMoveX(_moveX, _moveXDuration).OnComplete(()=>
        {
            _turnedONRotationIMG.DOColor(_turnedONRotationColor, _colorDuration);
            _turnedONRotationRT.DOLocalRotate(new Vector3(0, 0, -25), _moveXDuration).OnComplete(() =>
            {
                _turnedONRotationRT.DOLocalRotate(new Vector3(0, 0, 0), _moveXDuration);
            });
        });     
    }

    private void TurnOFFRotation()
    {
        _isTurnedON = false;
        _rotatePartText.text = "Rotate parts: No";
        _slider.DOLocalMoveX(-_moveX, _moveXDuration);
        _turnedONRotationIMG.DOColor(Color.white, _colorDuration);
    }
}
