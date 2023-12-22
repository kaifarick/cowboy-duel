using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class AGameplay
{
    protected APlayer FirstPlayer;
    protected APlayer SecondPlayer;
    protected APlayer PlayersWin;
    
    protected GameEnum.GameplayType GameplayType;
    protected GameEnum.RoundResult _roundResult;
    protected GameData GameData;
    
    protected int _roundTimeLeft;
    protected int _roundNum;
    
    
    public event Action<GameData, GameEnum.RoundResult, int> OnEndRoundAction;
    public event Action<GameEnum.GameplayType> OnRoundStartAction;
    public event Action OnGameEndAction;


    public event Action OnStopMoveTimeAction;
    public event Action<int> OnStartMoveTimerAction;
    public event Action<int> OnTimerTickAction;

    
    public event Action<GameEnum.PlayersNumber, GameEnum.GameItem> OnSelectedItemAction;

    protected AGameplay(GameEnum.GameplayType gameplayType)
    {
        GameplayType = gameplayType;
        
        GameData = new GameData(){RoundInfos = new Dictionary<int, GameData.RoundInfo>()};
    }


    public virtual void StartRound()
    {
        if(_roundResult != GameEnum.RoundResult.Draw) _roundNum++;
        if(!GameData.RoundInfos.ContainsKey(_roundNum)) GameData.RoundInfos.Add(_roundNum, new GameData.RoundInfo()
        {
            FirstPlayer = new GameData.PlayerInfo(),
            SecondPlayer = new GameData.PlayerInfo(),
            WinnerPlayer = new GameData.PlayerInfo()
        });

        PlayersWin = null;

        GameData.CurrentRound = _roundNum;
        
        OnRoundStartAction?.Invoke(GameplayType);
        
        Debug.Log($"RoundResult{_roundResult} RoundNum{_roundNum}");
    }


    public virtual void EndGame()
    {
        _roundNum = 0;
        
        OnGameEndAction?.Invoke();
    }
    
    public virtual void SelectItem(GameEnum.PlayersNumber playersNumber, GameEnum.GameItem gameItem = GameEnum.GameItem.None)
    {
        if (playersNumber == GameEnum.PlayersNumber.PlayerOne)
        {
            FirstPlayer?.SelectItem(gameItem);
            GameData.RoundInfos[_roundNum].FirstPlayer.GameItem = FirstPlayer.GameItem;
        }

        if (playersNumber == GameEnum.PlayersNumber.PlayerTwo)
        {
            SecondPlayer?.SelectItem(gameItem);
            GameData.RoundInfos[_roundNum].SecondPlayer.GameItem = SecondPlayer.GameItem;
        }
        
        OnSelectedItemAction?.Invoke(playersNumber,gameItem);
        
        if(FirstPlayer?.GameItem != GameEnum.GameItem.None && SecondPlayer?.GameItem != GameEnum.GameItem.None) CheckRoundResult();
    }
    
    protected virtual void EndRound()
    {
        OnEndRoundAction?.Invoke(GameData, _roundResult, _roundNum);
    }

    protected void StopMoveTimer()
    {
        TimerManager.RemoveWaiter("RoundTimer");
        OnStopMoveTimeAction?.Invoke();
    }

    protected void StartMoveTimer(int time, Action callback)
    {
        _roundTimeLeft = time;
        TimerManager.WaitAndCall("RoundTimer", time, callback, () =>
        {
            _roundTimeLeft--;
            OnTimerTickAction?.Invoke(_roundTimeLeft);
        });
        OnStartMoveTimerAction?.Invoke(time);
    }

    private void CheckRoundResult()
    {
        if (FirstPlayer.GameItem == GameEnum.GameItem.Rock)
        {
            switch (SecondPlayer.GameItem)
            {
                case GameEnum.GameItem.Rock:
                    _roundResult = GameEnum.RoundResult.Draw;
                    break;
                case GameEnum.GameItem.Paper:
                    PlayersWin = SecondPlayer;
                    break;
                case GameEnum.GameItem.Scissors:
                    PlayersWin = FirstPlayer;
                    break;
            }
        }

        if (FirstPlayer?.GameItem == GameEnum.GameItem.Paper)
        {
            switch (SecondPlayer.GameItem)
            {
                case GameEnum.GameItem.Rock:
                    PlayersWin = FirstPlayer;
                    break;
                case GameEnum.GameItem.Paper:
                    _roundResult = GameEnum.RoundResult.Draw;
                    break;
                case GameEnum.GameItem.Scissors:
                    PlayersWin = SecondPlayer;
                    break;
            }
        }
        
        if (FirstPlayer.GameItem == GameEnum.GameItem.Scissors)
        {
            switch (SecondPlayer?.GameItem)
            {
                case GameEnum.GameItem.Rock:
                    PlayersWin = SecondPlayer;
                    break;
                case GameEnum.GameItem.Paper:
                    PlayersWin = FirstPlayer;
                    break;
                case GameEnum.GameItem.Scissors:
                    _roundResult = GameEnum.RoundResult.Draw;
                    break;
            }
        }

        if (PlayersWin != null)
        {
            GameData.RoundInfos[_roundNum].WinnerPlayer.Name = PlayersWin.Name;
            GameData.RoundInfos[_roundNum].WinnerPlayer.GameItem = PlayersWin.GameItem;
            GameData.RoundInfos[_roundNum].WinnerPlayer.PlayersNumber = PlayersWin.PlayersNumber;

            if (PlayersWin is BotPlayer)
            {
                _roundResult = GameEnum.RoundResult.BotWin;
                GameData.RoundInfos[_roundNum].WinnerPlayer.IsBot = true;
            }
            else if (PlayersWin is Player) _roundResult = GameEnum.RoundResult.PlayerWin;
        }

        EndRound();
    }
    
}
