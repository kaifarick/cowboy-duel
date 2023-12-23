using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolverView : MonoBehaviour
{
    [SerializeField] private ParticleSystem _smoke;
    [SerializeField] private ParticleSystem _explosion;
    [SerializeField] private GameObject _view;
    
    public void Show()
    {
        _view.SetActive(true);
    }
    
    public void Hide()
    {
        _view.SetActive(false);
    }

    public void SmokeAndExplosion()
    {
        _smoke.Play();
        _explosion.Play();
    }
}
