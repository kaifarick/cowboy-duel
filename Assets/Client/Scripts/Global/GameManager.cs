#nullable enable
using System;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    private AGameplay _gameplay;
    private GamePresenter _gamePresenter;
    
    public event Action OnGameEndAction;
    public event Action<GamePresenter> OnGameStartAction;
    public event Action<GameEnum.GameplayType> OnRoundStartAction;
    
        
    [Inject] private WindowsManager _windowsManager;


    public void StartGame(AGameplay gameplay)
    {
        _gameplay = gameplay;
        _gamePresenter = new GamePresenter(gameplay, this, _windowsManager);
        
        gameplay.OnRoundStartAction += OnRoundStart;

        OnGameStartAction?.Invoke(_gamePresenter);
        
        StartRound();
    }

    public void StartRound()
    {
        _gameplay.StartRound();
    }
    
    
    public void GameEnd()
    {
        _gameplay.EndGame();
        OnGameEndAction?.Invoke();
        
        _gameplay.OnRoundStartAction -= OnRoundStart;
    }


    private void OnRoundStart(GameEnum.GameplayType gameplayType)
    {
        OnRoundStartAction?.Invoke(gameplayType);
    }
}
