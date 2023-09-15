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
    public int TimeLeft { get; protected set; }
    public int RoundNum { get; protected set; }
    public object GameplayInfo { get; protected set; }
    public GameEnum.RoundResult RoundResult { get; protected set;}
    

    public event Action<APlayer> OnChangeQueueAction;
    public event Action<APlayer, GameEnum.RoundResult> OnEndRoundAction;
    public event Action OnGameEndAction;
    
    public event Action OnStopMoveTimeAction;
    public event Action<int> OnStartMoveTimerAction;
    public event Action<int> OnTimerTickAction;


    public virtual void StartGame()
    {
        Debug.Log($"RoundResult{RoundResult}");
        PlayersTurn = FirstPlayer;
        
        ChangeQueue(FirstPlayer);
    }
    
    public virtual void OnSelectedItem(APlayer playersSelected, GameEnum.GameItem gameItem)
    {
        playersSelected?.SelectItem(gameItem);
    }

    protected virtual void EndRound()
    {
        OnEndRoundAction?.Invoke(PlayersWin, RoundResult);
    }

    protected virtual void EndGame()
    {
        RoundNum = 0;
        OnGameEndAction?.Invoke();
    }

    protected void ChangeQueue(APlayer playerQueue)
    {
        PlayersTurn = playerQueue;
        OnChangeQueueAction?.Invoke(playerQueue);
    }

    protected void StartMoveTimer(int time, Action callback)
    {
        TimeLeft = time;
        TimerManager.WaitAndCall("RoundTimer", time, callback, () =>
        {
            TimeLeft--;
            OnTimerTickAction?.Invoke(TimeLeft);
        });
        OnStartMoveTimerAction?.Invoke(time);
    }


    protected virtual void StopMoveTimer()
    {
        OnStopMoveTimeAction?.Invoke();
    }

    protected void CheckPlayerWin()
    {
        if (FirstPlayer.GameItem == GameEnum.GameItem.Rock)
        {
            switch (SecondPlayer.GameItem)
            {
                case GameEnum.GameItem.Rock:
                    RoundResult = GameEnum.RoundResult.Draw;
                    break;
                case GameEnum.GameItem.Paper:
                    RoundResult = GameEnum.RoundResult.PlayerTwoWin;
                    PlayersWin = SecondPlayer;
                    FirstPlayer.SetWinState(false);
                    SecondPlayer.SetWinState(true);
                    break;
                case GameEnum.GameItem.Scissors:
                    RoundResult = GameEnum.RoundResult.PlayerOneWin;
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
                    RoundResult = GameEnum.RoundResult.PlayerOneWin;
                    PlayersWin = FirstPlayer;
                    FirstPlayer.SetWinState(true);
                    SecondPlayer.SetWinState(false);
                    break;
                case GameEnum.GameItem.Paper:
                    RoundResult = GameEnum.RoundResult.Draw;
                    break;
                case GameEnum.GameItem.Scissors:
                    RoundResult = GameEnum.RoundResult.PlayerTwoWin;
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
                    RoundResult = GameEnum.RoundResult.PlayerTwoWin;
                    PlayersWin = SecondPlayer;
                    FirstPlayer.SetWinState(false);
                    SecondPlayer.SetWinState(true);
                    break;
                case GameEnum.GameItem.Paper:
                    RoundResult = GameEnum.RoundResult.PlayerOneWin;
                    PlayersWin = FirstPlayer;
                    FirstPlayer.SetWinState(true);
                    SecondPlayer.SetWinState(false);
                    break;
                case GameEnum.GameItem.Scissors:
                    RoundResult = GameEnum.RoundResult.Draw;
                    break;
            }
        }
        
        EndRound();
    }
    
}
