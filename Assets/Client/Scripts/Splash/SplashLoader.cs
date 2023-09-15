using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SplashLoader : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private Sequence _sequence;

    public void UpdateView(float time, float process)
    {
        _sequence?.Kill();
        _sequence = DOTween.Sequence();
        _sequence.Append(slider.DOValue(process, time));
    }
}