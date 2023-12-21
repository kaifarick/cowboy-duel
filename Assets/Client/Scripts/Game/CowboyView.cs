using DG.Tweening;
using UnityEngine;
using Zenject;

public class CowboyView : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private RevolverView _revolverView;
    [SerializeField] private GameEnum.PlayersNumber _playersNumber;

    [Inject] private GameManager _gameManager;
    [Inject] private MainCamera _mainCamera;

    private GamePresenter _gamePresenter;

    private Vector3 _startPosition;
    private Vector3 _shootPosition;


    private void Start()
    {
        _gameManager.OnGameStartAction += OnGameStart;
        _gameManager.OnGameEndAction += OnGameEnd;
        _gameManager.OnRoundStartAction += OnRoundStart;
        
        
        var preSpace = 500;
        _startPosition = _playersNumber == GameEnum.PlayersNumber.PlayerOne
            ? _mainCamera.GetLeftPointWithSpace(-preSpace)
            : _mainCamera.GetRightPointWithSpace(-preSpace);
        
        
        var screenSpace = 300;
        _shootPosition = _playersNumber == GameEnum.PlayersNumber.PlayerOne
            ? _mainCamera.GetLeftPointWithSpace(screenSpace)
            : _mainCamera.GetRightPointWithSpace(screenSpace);
    }

    private void OnGameStart(GamePresenter gamePresenter)
    {
        _gamePresenter = gamePresenter;

        _gamePresenter.OnEndRoundAction += OnEndRound;
    }

    private void OnGameEnd()
    {
        transform.position = new Vector3(_startPosition.x, transform.position.y, transform.position.z);
        
        _gamePresenter.OnEndRoundAction -= OnEndRound;
    }

    private void OnRoundStart(GameEnum.GameplayType gameplayType)
    {
        transform.position = new Vector3(_startPosition.x, transform.position.y, transform.position.z);
        

        _animator.SetTrigger("Walk");
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMoveX(_shootPosition.x, 2.5f)).InsertCallback(2f, (() => _animator.SetTrigger("Idle")));
    }

    private void OnEndRound(GameData gameData)
    {
        
        if (_playersNumber == gameData.RoundInfos[gameData.CurrentRound].WinnerPlayer.PlayersNumber ||
            gameData.RoundInfos[gameData.CurrentRound].WinnerPlayer.PlayersNumber == GameEnum.PlayersNumber.None)
        {
            Shot();
        }

        else
        {
            float shotWaitTime = 0.4f;
            
            Sequence sequence = DOTween.Sequence();
            sequence.AppendInterval(shotWaitTime).AppendCallback(Death);
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

}
