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
        _gamePresenter = new GamePresenter(gameplay, this);

        gameplay.OnEndRoundAction += OnEndRound;
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
        
        _gameplay.OnEndRoundAction -= OnEndRound;
        _gameplay.OnRoundStartAction -= OnRoundStart;
    }


    private void OnRoundStart(GameEnum.GameplayType gameplayType)
    {
        OnRoundStartAction?.Invoke(gameplayType);
    }
    
    private void OnEndRound(GameData gameData, GameEnum.RoundResult roundResult, int roundNum)
    {
        AWinWindow winWindow = null;
        if (_gameplay is PlayerVsComputerGameplay) winWindow = _windowsManager.OpenWindow<WinWindowPlayerVsComputer>();
        if (_gameplay is PlayerVsPlayerGameplay) winWindow = _windowsManager.OpenWindow<WinWindowPlayerVsPlayer>();
        if (_gameplay is SurvivalGameplay) winWindow = _windowsManager.OpenWindow<WinWindowSurvival>();
        if (_gameplay is ChampionshipGameplay) winWindow = _windowsManager.OpenWindow<WinWindowChampionship>();
        
        winWindow.Initialize(gameData, roundResult, roundNum);
    }
}
