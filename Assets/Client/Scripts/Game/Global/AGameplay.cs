using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AGameplay
{
    public APlayer FirstPlayer { get; protected set; }
    public APlayer SecondPlayer { get; protected set; }
    public APlayer PlayersTurn { get; protected set; }
    public APlayer PlayersWin { get; protected set; }
    public object GameplayInfo { get; protected set; }
    
    protected GameEnum.RoundResult _roundResult;
    protected int _timeLeft;
    protected int _roundNum;
    

    public event Action<APlayer> OnChangeQueueAction;
    public event Action<APlayer, GameEnum.RoundResult, int> OnEndRoundAction;
    public event Action OnGameEndAction;
    
    public event Action OnStopMoveTimeAction;
    public event Action<int> OnStartMoveTimerAction;
    public event Action<int> OnTimerTickAction;


    public virtual void StartGame()
    {
        Debug.Log($"RoundResult{_roundResult}");
        PlayersTurn = FirstPlayer;
        
        ChangeQueue(FirstPlayer);
    }

    protected virtual void EndRound()
    {
        OnEndRoundAction?.Invoke(PlayersWin, _roundResult, _roundNum);
    }

    public virtual void EndGame()
    {
        _roundNum = 0;
        OnGameEndAction?.Invoke();
    }
    
    public virtual void OnSelectedItem(APlayer playersSelected, GameEnum.GameItem gameItem)
    {
        playersSelected?.SelectItem(gameItem);
    }

    protected void ChangeQueue(APlayer playerQueue)
    {
        PlayersTurn = playerQueue;
        OnChangeQueueAction?.Invoke(playerQueue);
    }
    
    protected virtual void StopMoveTimer()
    {
        OnStopMoveTimeAction?.Invoke();
    }

    protected void StartMoveTimer(int time, Action callback)
    {
        _timeLeft = time;
        TimerManager.WaitAndCall("RoundTimer", time, callback, () =>
        {
            _timeLeft--;
            OnTimerTickAction?.Invoke(_timeLeft);
        });
        OnStartMoveTimerAction?.Invoke(time);
    }

    protected void CheckPlayerWin()
    {
        if (FirstPlayer.GameItem == GameEnum.GameItem.Rock)
        {
            switch (SecondPlayer.GameItem)
            {
                case GameEnum.GameItem.Rock:
                    _roundResult = GameEnum.RoundResult.Draw;
                    break;
                case GameEnum.GameItem.Paper:
                    _roundResult = GameEnum.RoundResult.PlayerTwoWin;
                    PlayersWin = SecondPlayer;
                    FirstPlayer.SetWinState(false);
                    SecondPlayer.SetWinState(true);
                    break;
                case GameEnum.GameItem.Scissors:
                    _roundResult = GameEnum.RoundResult.PlayerOneWin;
                    PlayersWin = FirstPlayer;
                    FirstPlayer.SetWinState(true);
                    SecondPlayer.SetWinState(false);
                    break;
            }
        }

        if (FirstPlayer?.GameItem == GameEnum.GameItem.Paper)
        {
            switch (SecondPlayer.GameItem)
            {
                case GameEnum.GameItem.Rock:
                    _roundResult = GameEnum.RoundResult.PlayerOneWin;
                    PlayersWin = FirstPlayer;
                    FirstPlayer.SetWinState(true);
                    SecondPlayer.SetWinState(false);
                    break;
                case GameEnum.GameItem.Paper:
                    _roundResult = GameEnum.RoundResult.Draw;
                    break;
                case GameEnum.GameItem.Scissors:
                    _roundResult = GameEnum.RoundResult.PlayerTwoWin;
                    PlayersWin = SecondPlayer;
                    FirstPlayer.SetWinState(false);
                    SecondPlayer.SetWinState(true);
                    break;
            }
        }
        
        if (FirstPlayer.GameItem == GameEnum.GameItem.Scissors)
        {
            switch (SecondPlayer?.GameItem)
            {
                case GameEnum.GameItem.Rock:
                    _roundResult = GameEnum.RoundResult.PlayerTwoWin;
                    PlayersWin = SecondPlayer;
                    FirstPlayer.SetWinState(false);
                    SecondPlayer.SetWinState(true);
                    break;
                case GameEnum.GameItem.Paper:
                    _roundResult = GameEnum.RoundResult.PlayerOneWin;
                    PlayersWin = FirstPlayer;
                    FirstPlayer.SetWinState(true);
                    SecondPlayer.SetWinState(false);
                    break;
                case GameEnum.GameItem.Scissors:
                    _roundResult = GameEnum.RoundResult.Draw;
                    break;
            }
        }
        
        EndRound();
    }
    
}
