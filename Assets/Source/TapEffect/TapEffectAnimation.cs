using UnityEngine;
using TMPro;
using DG.Tweening;

public class TapEffectAnimation : MonoBehaviour
{
    public float EndPositionY = 300f;
    public float AnimationDuration = 1.0f;
    public TMP_Text Text;
    
    private TapEffectPool _effectPool;
    private Tween _moveTween;
    private Tween _fadeTween;

    private void OnEnable()
    {
        PlayEffect();
    }

    private void Awake()
    {
        if (Text == null)
        {
            Text = GetComponent<TMP_Text>();
        }

        if (_effectPool == null)
        {
            _effectPool = FindObjectOfType<TapEffectPool>();
        }
    }

    private void PlayEffect()
    {
        _moveTween = transform.DOMoveY(transform.position.y + EndPositionY, AnimationDuration);

        _fadeTween = Text.DOFade(0, AnimationDuration).OnComplete(() => {
            ResetText();
            transform.localPosition = Vector3.zero;
            Text.color = new Color(Text.color.r, Text.color.g, Text.color.b, 1);
            _effectPool.ReturnVisualEffect(this);
        });
    }

    private void ResetText()
    {
        Text.color = new Color(Text.color.r, Text.color.g, Text.color.b, 1);
    }
}
