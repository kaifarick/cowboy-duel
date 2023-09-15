#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameView _gameView;
    [SerializeField] private MenuView _menuView;
    public AGameplay AGameplay { get; private set; }


    public event Action OnGameEndAction;
    public event Action OnGameStartAction;
    public event Action <APlayer, GameEnum.RoundResult> OnEndRoundAction;
    
    public event Action<APlayer> OnChangeQueueAction;
    public event Action<APlayer,GameEnum.GameItem> OnSelectionItemAction;

    
    public event Action<int> OnStartMoveTimerAction;
    public event Action<int> OnTimerTickAction;
    public event Action  OnEndMoveTimerAction;

    

    public void StartGame(AGameplay gameplay)
    {
        AGameplay = gameplay;
        
        AGameplay.OnChangeQueueAction += OnChangeQueue;
        AGameplay.OnEndRoundAction += OnEndRound;
        AGameplay.OnStartMoveTimerAction += OnStartMoveTimer;
        AGameplay.OnStopMoveTimeAction += OnStopMoveTimer;
        AGameplay.OnTimerTickAction += OnTimerTick;
        
        
        AGameplay.StartGame();
        
        OnGameStartAction?.Invoke();
        
        _gameView.Initialize(gameplay);
        
    }
    
    public void GameEnd()
    {
        OnGameEndAction?.Invoke();
    }

    public void SelectedItem(GameEnum.GameItem gameItem = GameEnum.GameItem.None)
    {
        var playerTurn = AGameplay.PlayersTurn;
        
        AGameplay.OnSelectedItem(playerTurn, gameItem);
        OnSelectionItemAction?.Invoke(playerTurn,gameItem);
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
    

    private void OnEndRound(APlayer aPlayer, GameEnum.RoundResult roundResult)
    {
        OnEndRoundAction?.Invoke(aPlayer, roundResult);
        
        AGameplay.OnChangeQueueAction -= OnChangeQueue;
        AGameplay.OnEndRoundAction -= OnEndRound;
        AGameplay.OnStartMoveTimerAction -= OnStartMoveTimer;
        AGameplay.OnStopMoveTimeAction -= OnStopMoveTimer;
        AGameplay.OnTimerTickAction -= OnTimerTick;
    }
}
