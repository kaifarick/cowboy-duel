using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class BaseWindow : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private bool _isFastClose;

    public virtual void Show()
    {
        gameObject.SetActive(true);
        _canvasGroup.alpha = 0;
        
        var sequence = DOTween.Sequence();
        sequence.Append(_canvasGroup.DOFade(1, 0.2f).SetEase(Ease.OutQuad));
    }

    public virtual void Hide()
    {
        if (_isFastClose)
        {
            gameObject.SetActive(false);
            return;
        }

        var sequence = DOTween.Sequence();
        sequence.Append(_canvasGroup.DOFade(0, 0.2f).SetEase(Ease.InQuad));
        sequence.AppendCallback((() => gameObject.SetActive(false)));
    }
}
