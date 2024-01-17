using System;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class CowboyView : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private RevolverView _revolverView;
    [SerializeField] private GameEnum.PlayersNumber _playersNumber;
    
    [Inject] private CameraManager _cameraManager;
    [Inject] private GamePresenter _gamePresenter;
    
    private Sequence _moveSequence;
    private Sequence _hitSequence;

    private Vector3 _startPosition;
    private Vector3 _shootPosition;


    private void Start()
    {
        _gamePresenter.OnEndGameAction += OnGameEnd;
        
        _gamePresenter.OnCheckPreparePointsAction += OnCheckPreparePoints;
        _gamePresenter.OnHitPlayerAction += OnHitPlayer;


        var preSpacePercent = 20;
        _startPosition = _playersNumber == GameEnum.PlayersNumber.PlayerOne
            ? _cameraManager.GetLeftPointWithSpace(-preSpacePercent)
            : _cameraManager.GetRightPointWithSpace(-preSpacePercent);
        
        
        var screenSpacePercent = 12;
        _shootPosition = _playersNumber == GameEnum.PlayersNumber.PlayerOne
            ? _cameraManager.GetLeftPointWithSpace(screenSpacePercent)
            : _cameraManager.GetRightPointWithSpace(screenSpacePercent);
    }
    

    private void OnGameEnd()
    {
        _moveSequence.Kill();
        _hitSequence.Kill();

        transform.position = new Vector3(_startPosition.x, transform.position.y, transform.position.z);
        
    }

    private void OnCheckPreparePoints(Action<GameEnum.PrepareGameplayPoint> onComplete)
    {
        transform.position = new Vector3(_startPosition.x, transform.position.y, transform.position.z);
        _animator.SetTrigger("Walk");
        
        _moveSequence = DOTween.Sequence();
        _moveSequence.Append(transform.DOMoveX(_shootPosition.x, 2.5f).SetEase(Ease.Linear))
            .InsertCallback(2f, () => _animator.SetTrigger("Idle"))
            .AppendCallback(() => onComplete?.Invoke(GameEnum.PrepareGameplayPoint.Animations));
    }
    
    
    private void OnHitPlayer(GameEnum.PlayersNumber playersNumber, int currentHealth, int damage)
    {
        if (playersNumber == GameEnum.PlayersNumber.None || playersNumber != _playersNumber)
        {
            Shot();
            return;
        }

        if (currentHealth > 0)
        {
            float shootWaitTime = 0.4f;
            _hitSequence = DOTween.Sequence();
            _hitSequence.AppendInterval(shootWaitTime).AppendCallback(GetHit);
        }
        else
        {
            float shootWaitTime = 0.4f;
            _hitSequence = DOTween.Sequence();
            _hitSequence.AppendInterval(shootWaitTime).AppendCallback(Death);
        }
    }

    private void Shot()
    {
        Debug.Log("Shot");
        _animator.SetTrigger("Shot");
    }
    
    private void Death()
    {
        Debug.Log("Death");
        _animator.SetTrigger("Death");
    }
    
    private void GetHit()
    {
        Debug.Log("GetHit");
        _animator.SetTrigger("GetHit");
    }

    private void TakeGun()
    {
        //call from from animation
        _revolverView.Show();
    }

    private void PutGun()
    {
        //call from from animation
        _revolverView.Hide();
    }

    private void SmokeAndExplosion()
    {
        //call from from animation
        _revolverView.SmokeAndExplosion();
    }

}
