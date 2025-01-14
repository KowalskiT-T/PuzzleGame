using DG.Tweening;
using Grid;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzlePrepareUI : MonoBehaviour
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private RectTransform _contentPanel;
    [SerializeField] private RectTransform _sampleListItem;

    [SerializeField] private HorizontalLayoutGroup _horizontalLayoutGroup;

    [SerializeField] private float _snapForce;

    [SerializeField] private GridSOList _difficiltiesList;

    public static Action<float> ItemChanging;
    public static Action<int> ScrollItemChanged;
    public static Action<int> OnElementClick;

    private float _snapSpeed;
    private bool _isSnapped;
    private int _currentItemSnapping;
    private float _deltaPosition;
    private float _fullwidth;
    private int _maxScrollvelocity = 200;

    private void OnEnable()
    {
        OnElementClick += MoveToElement;
    }

    private void OnDisable()
    {
        _contentPanel.localPosition = new Vector3(0, _contentPanel.localPosition.y, _contentPanel.localPosition.z);
        ItemChanging?.Invoke(_contentPanel.localPosition.x);
        OnElementClick -= MoveToElement;
    }

    private void Start()
    {
        ItemChanging?.Invoke(_contentPanel.localPosition.x);
        _fullwidth = _sampleListItem.rect.width + _horizontalLayoutGroup.spacing;
    }

    private void Update()
    {
        SnappingItem();
    }

    private void SnappingItem()
    {
        _deltaPosition = -_contentPanel.localPosition.x / _fullwidth;
        int currentItemSnapping = Mathf.RoundToInt(_deltaPosition);
        currentItemSnapping = Mathf.Clamp(currentItemSnapping, 0, _difficiltiesList.GridDiffucultiesList.Count - 1);

        ChagingDifficulty(currentItemSnapping);        

        ChangingScrollItem();

        if (_scrollRect.velocity.magnitude < _maxScrollvelocity && !_isSnapped)
        {
            _scrollRect.velocity = Vector2.zero;
            _snapSpeed += _snapForce * Time.deltaTime;
            _contentPanel.localPosition = new Vector3(
                Mathf.MoveTowards(_contentPanel.localPosition.x, -_currentItemSnapping * _fullwidth, _snapSpeed),
                 _contentPanel.localPosition.y,
                 _contentPanel.localPosition.z);

            ItemChanging?.Invoke(_contentPanel.localPosition.x);

            if (_contentPanel.localPosition.x == -_currentItemSnapping * _fullwidth)
                _isSnapped = true;
        }
        if (_scrollRect.velocity.magnitude > _maxScrollvelocity)
        {
            _isSnapped = false;
            _snapSpeed = 0;
        }
    }

    public void MoveToElement(int elementIndex)
    {
        _isSnapped = true;
        _contentPanel.DOLocalMoveX(-elementIndex * _fullwidth, 0.25f).OnUpdate(() =>
        {

            ItemChanging?.Invoke(_contentPanel.localPosition.x);

        }).OnComplete(() =>
        {
            _isSnapped = false;
        });
    }

    private void ChagingDifficulty(int currentItemSnapping)
    {
        if (_currentItemSnapping != currentItemSnapping)
        {
            ScrollItemChanged?.Invoke(currentItemSnapping);
            _currentItemSnapping = currentItemSnapping;
        }
    }

    private void ChangingScrollItem()
    {
        if (_scrollRect.velocity.magnitude > 1)
             ItemChanging?.Invoke(_contentPanel.localPosition.x);
    }


}
