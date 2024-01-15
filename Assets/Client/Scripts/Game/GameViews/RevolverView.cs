using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RevolverView : MonoBehaviour
{
    [SerializeField] private ParticleSystem _smoke;
    [SerializeField] private ParticleSystem _explosion;
    [SerializeField] private GameObject _view;

    [Inject] private GamePresenter _gamePresenter;
    
    private void Start()
    {
        _gamePresenter.OnEndGameAction += OnEndGameAction;
    }
    

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

    private void OnEndGameAction()
    {
        _smoke.Stop();
        _smoke.Clear();
        
        _explosion.Stop();
        _explosion.Clear();
    }
}
