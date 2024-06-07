using UnityEngine;
using DG.Tweening;

public class MaterialBloom : MonoBehaviour
{
    private const string BASE_COLOR = "_Color";
    private const string TINT_COLOR = "_Tint";

    [SerializeField] private Material _animationMaterial;
    [SerializeField] private Color _animationTintColor = new Color(1, 1, 1, 0.3f);
    [SerializeField] private Color _animationBaseColor = new Color(1, 1, 1, 1);

    [SerializeField] private float _animationDuration = 0.4f;
    [SerializeField] private float _bloomIntensity = 1.2f;
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _animationMaterial = _renderer.material;
    }

    public void AnimateMaterial()
    {
        Sequence bloomSequence = GetBloomAnimation(_bloomIntensity, _animationDuration);
        Sequence tintSequence = GetColorTintAnimation(_animationTintColor, _animationDuration);

        bloomSequence.Play();
        tintSequence.Play();
    }

    private Sequence GetColorTintAnimation(Color color, float duration)
    {
        Color original = _animationMaterial.GetColor(TINT_COLOR);

        Sequence sequence = DOTween.Sequence();

        sequence.Append(_animationMaterial.DOColor(color, TINT_COLOR, duration/2));
        sequence.Append(_animationMaterial.DOColor(original, TINT_COLOR, duration/2));

        return sequence;
    }


    private Sequence GetBloomAnimation(float intensity, float duration)
    {
        Color bloomColor = _animationBaseColor * intensity;

        Sequence sequence = DOTween.Sequence();

        sequence.Append(_animationMaterial.DOColor(bloomColor, BASE_COLOR, duration/2));
        sequence.Append(_animationMaterial.DOColor(_animationBaseColor, BASE_COLOR, duration/2));

        return sequence;
    }

}