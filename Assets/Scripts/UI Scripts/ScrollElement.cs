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

    [Header("Element Reward")]
    [SerializeField] private TextMeshProUGUI _rewardText;
    [SerializeField] private Color _rewardTextColor;
    [SerializeField] private TextMeshProUGUI _rewardCoins;
    private float _rewardActiveAlpha = 255;
    private float _rewardBasicAlpha = 0;

    private AnimationCurve _scrollElementAnimationCurveSize = new AnimationCurve();
    private AnimationCurve _scrollElementAnimationCurveRedColor = new AnimationCurve();
    private AnimationCurve _scrollElementAnimationCurveBlueColor = new AnimationCurve();
    private AnimationCurve _scrollElementAnimationCurveGreenColor = new AnimationCurve();
    private AnimationCurve _rewardTextColorAlphaAnimationCurve = new AnimationCurve();

    private float _fullWidth;
    private int _scrollElementNumber;

    private void Awake()
    {
        _fullWidth = _paddingScrollElement + _scrollElementSize;
        AdjustScrollElementAnimationCurveSize();
        AdjustScrollElementAnimationCurveColor();
        AdjustRewardTextColor();
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

    private void AdjustRewardTextColor()
    {
        AddingKeys(_rewardTextColorAlphaAnimationCurve, _fullWidth / 2, _rewardBasicAlpha, _rewardActiveAlpha);
    }

    public void SetBasicScrollElementParameters(float scrollPosition)
    {  
        float relPosition = scrollPosition + _scrollElementNumber * _fullWidth;      
        _scrollElementTransform.localScale = new Vector3(_scrollElementAnimationCurveSize.Evaluate(relPosition), 
            _scrollElementAnimationCurveSize.Evaluate(relPosition));
        _scrollElementImage.color = new Color(_scrollElementAnimationCurveRedColor.Evaluate(relPosition), 
            _scrollElementAnimationCurveGreenColor.Evaluate(relPosition), 
            _scrollElementAnimationCurveBlueColor.Evaluate(relPosition));
        _rewardText.color = new Color(_rewardText.color.r, 
            _rewardText.color.g, 
            _rewardText.color.b, 
            _rewardTextColorAlphaAnimationCurve.Evaluate(relPosition));
    }

    public void LoadScrollElement(GridSO difficultyGrid, int scrollnum)
    {
        _scrollElementText.text = difficultyGrid.Area.ToString();
        _scrollElementNumber = scrollnum;
        _rewardCoins.text = difficultyGrid.CoinReward.ToString();

    }

    private void AddingKeys(AnimationCurve AnimationCurve, float time, float basicValue, float activeValue)
    {
        AnimationCurve.ClearKeys();
        AnimationCurve.AddKey(time, basicValue);
        AnimationCurve.AddKey(-time, basicValue);
        AnimationCurve.AddKey(0, activeValue);
    }
}
