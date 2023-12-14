#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    public AGameplay AGameplay { get; private set; }
    
    public event Action OnGameEndAction;
    public event Action<AGameplay> OnGameStartAction;
    

    public event Action<APlayer> OnChangeQueueAction;
    public event Action<APlayer,GameEnum.GameItem> OnSelectionItemAction;

    
    public event Action<int> OnStartMoveTimerAction;
    public event Action<int> OnTimerTickAction;
    public event Action  OnEndMoveTimerAction;
    

    [Inject] private WindowsManager _windowsManager;

    public void StartGame(AGameplay gameplay)
    {
        AGameplay = gameplay;
        
        gameplay.OnChangeQueueAction += OnChangeQueue;
        gameplay.OnEndRoundAction += OnEndRound;
        gameplay.OnStartMoveTimerAction += OnStartMoveTimer;
        gameplay.OnStopMoveTimeAction += OnStopMoveTimer;
        gameplay.OnTimerTickAction += OnTimerTick;
        
        OnGameStartAction?.Invoke(gameplay);
        
        StartRound();
    }

    public void StartRound()
    {
        AGameplay.StartRound();
    }
    
    public void SelectedItem(GameEnum.GameItem gameItem = GameEnum.GameItem.None)
    {
        var playerTurn = AGameplay.PlayersTurn;
        
        AGameplay.OnSelectedItem(playerTurn, gameItem);
        OnSelectionItemAction?.Invoke(playerTurn,gameItem);
    }
    
    public void GameEnd()
    {
        AGameplay.EndGame();
        OnGameEndAction?.Invoke();
        
        AGameplay.OnChangeQueueAction -= OnChangeQueue;
        AGameplay.OnEndRoundAction -= OnEndRound;
        AGameplay.OnStartMoveTimerAction -= OnStartMoveTimer;
        AGameplay.OnStopMoveTimeAction -= OnStopMoveTimer;
        AGameplay.OnTimerTickAction -= OnTimerTick;
    }

    private void OnStartMoveTimer(int time)
    {
        OnStartMoveTimerAction?.Invoke(time);
    }

    private void OnStopMoveTimer()
    {
        OnEndMoveTimerAction?.Invoke();
    }

    private void OnChangeQueue(APlayer player)
    {
        OnChangeQueueAction?.Invoke(player);
    }

    private void OnTimerTick(int timeLeft)
    {
        OnTimerTickAction?.Invoke(timeLeft);
    }
    
    private void OnEndRound(APlayer winPlayer, GameEnum.RoundResult roundResult, int roundNum)
    {
        AWinWindow winWindow = null;
        if (AGameplay is PlayerVsComputerGameplay) winWindow = _windowsManager.OpenWindow<WinWindowPlayerVsComputer>();
        if (AGameplay is PlayerVsPlayerGameplay) winWindow = _windowsManager.OpenWindow<WinWindowPlayerVsPlayer>();
        if (AGameplay is SurvivalGameplay) winWindow = _windowsManager.OpenWindow<WinWindowSurvival>();
        if (AGameplay is ChampionshipGameplay) winWindow = _windowsManager.OpenWindow<WinWindowChampionship>();
        
        winWindow.Initialize(winPlayer, AGameplay.FirstPlayer, AGameplay.SecondPlayer,
            roundNum, roundResult,AGameplay.GameplayInfo);
    }
}
