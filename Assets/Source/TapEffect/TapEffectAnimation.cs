using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TapEffectAnimation : MonoBehaviour
{
    public float EndPositionY = 300f;
    public float animationDuration = 1.0f;
    public TMP_Text text;
    
    public TapEffectPool _effectPool;
    private Tween moveTween;
    private Tween fadeTween;

    private void Awake()
    {
        if (text == null)
        {
            text = GetComponent<TMP_Text>();
        }

        if (_effectPool == null)
        {
            _effectPool = FindObjectOfType<TapEffectPool>();
        }
    }

    private void OnEnable()
    {
        PlayEffect();
    }

    private void PlayEffect()
    {
        moveTween = transform.DOMoveY(transform.position.y + EndPositionY, animationDuration);
        fadeTween = text.DOFade(0, animationDuration).OnComplete(() => {
            ResetText();
            transform.localPosition = Vector3.zero;
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
            //ResetAnimation();
            _effectPool.ReturnVisualEffect(this);
        });
    }

    private void ResetAnimation()
    {
        if (moveTween != null && moveTween.IsActive())
        {
            moveTween.Kill();
        }

        if (fadeTween != null && fadeTween.IsActive())
        {
            fadeTween.Kill();
        }

        
        
    }

    private void ResetText()
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
    }
}
