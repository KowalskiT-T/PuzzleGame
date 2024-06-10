using Grid;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScrollElement : MonoBehaviour
{
    [Header("Element Colors")]
    [SerializeField] private Color _scrollElementColorActive;
    [SerializeField] private Color _scrollElementColorBasic;

    [Header("Element Components")]
    [SerializeField] private RectTransform _scrollElementTransform;
    [SerializeField] private Image _scrollElementImage;
    [SerializeField] private TextMeshProUGUI _scrollElementText;   

    [Header("Element Parameters")]
    [SerializeField] private float _scrollElementSize;
    [SerializeField] private float _paddingScrollElement;
    [SerializeField] private float _basicScaleScrollElement;
    [SerializeField] private float _activeScaleScrollElement;

    private AnimationCurve _scrollElementAnimationCurveSize = new AnimationCurve();
    private AnimationCurve _scrollElementAnimationCurveRedColor = new AnimationCurve();
    private AnimationCurve _scrollElementAnimationCurveBlueColor = new AnimationCurve();
    private AnimationCurve _scrollElementAnimationCurveGreenColor = new AnimationCurve();

    private float _fullWidth;
    private int _scrollElementNumber;

    private void Awake()
    {
        _fullWidth = _paddingScrollElement + _scrollElementSize;
        AdjustScrollElementAnimationCurveSize();
        AdjustScrollElementAnimationCurveColor();
        PuzzlePrepareUI.ItemChanging += SetBasicScrollElementParameters;
    }
    private void AdjustScrollElementAnimationCurveSize()
    {
        AddingKeys(_scrollElementAnimationCurveSize, _fullWidth, _basicScaleScrollElement, _activeScaleScrollElement);
    }
    private void AdjustScrollElementAnimationCurveColor()
    {
        AddingKeys(_scrollElementAnimationCurveRedColor, _fullWidth, _scrollElementColorBasic.r, _scrollElementColorActive.r);
        AddingKeys(_scrollElementAnimationCurveBlueColor, _fullWidth, _scrollElementColorBasic.b, _scrollElementColorActive.b);
        AddingKeys(_scrollElementAnimationCurveGreenColor, _fullWidth, _scrollElementColorBasic.g, _scrollElementColorActive.g);
    }
    public void SetBasicScrollElementParameters(float scrollPosition)
    {  
        float relPosition = scrollPosition + _scrollElementNumber * _fullWidth;      
        _scrollElementTransform.localScale = new Vector3(_scrollElementAnimationCurveSize.Evaluate(relPosition), 
            _scrollElementAnimationCurveSize.Evaluate(relPosition));
        _scrollElementImage.color = new Color(_scrollElementAnimationCurveRedColor.Evaluate(relPosition), 
            _scrollElementAnimationCurveGreenColor.Evaluate(relPosition), 
            _scrollElementAnimationCurveBlueColor.Evaluate(relPosition));
    }
    public void LoadScrollElement(GridSO difficultyGrid, int scrollnum)
    {
        _scrollElementText.text = difficultyGrid.Area.ToString();
        _scrollElementNumber = scrollnum;
    }
    private void AddingKeys(AnimationCurve AnimationCurve, float time, float basicValue, float activeValue)
    {
        AnimationCurve.ClearKeys();
        AnimationCurve.AddKey(time, basicValue);
        AnimationCurve.AddKey(-time, basicValue);
        AnimationCurve.AddKey(0, activeValue);
    }
}
