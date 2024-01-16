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

    protected IPlayerDamager _iPlayerDamager;
    
    
    public event Action<GameData, GameEnum.RoundResult, int> OnEndRoundAction;
    public event Action<GameEnum.GameplayType> OnRoundStartAction;
    
    
    public event Action OnNextRoundStepAction;
    public event Action OnEndRoundStepAction;
    
    
    public event Action OnGameEndAction;
    public event Action OnGameStartAction;



    public event Action<GameEnum.PlayersNumber, int, int> OnGetHitAction;
    public event Action<GameEnum.PlayersNumber, GameEnum.GameItem> OnSelectedItemAction;
    public event Action<GameData> OnPrepareRoundAction;



    public event Action OnStopMoveTimeAction;
    public event Action<int> OnStartMoveTimerAction;
    public event Action<int> OnTimerTickAction;

    

    protected AGameplay(GameEnum.GameplayType gameplayType)
    {
        GameplayType = gameplayType;
        
        GameData = new GameData(){RoundInfos = new Dictionary<int, GameData.RoundInfo>()};
#if DEBUG_LOGIC
        _iPlayerDamager = new DebugPlayerDamager();
#else
        _iPlayerDamager = new StandartDamager();
#endif
    }
    


    public virtual void StartRound()
    {
        OnRoundStartAction?.Invoke(GameplayType);
        
        Debug.Log($"RoundResult{_roundResult} RoundNum{_roundNum}");
    }

    public virtual void PrepareGameRound()
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
        
    }

    public virtual void StartGame()
    {
        OnGameStartAction?.Invoke();
    }


    public virtual void EndGame()
    {
        _roundNum = 0;
        
        OnGameEndAction?.Invoke();
    }
    
    public virtual void SelectItem(GameEnum.PlayersNumber playersNumber, GameEnum.GameItem gameItem = GameEnum.GameItem.None)
    {
        if (playersNumber == GameEnum.PlayersNumber.PlayerOne) FirstPlayer?.SelectItem(gameItem);
        else SecondPlayer?.SelectItem(gameItem);

        OnSelectedItemAction?.Invoke(playersNumber,gameItem);

        

        if (FirstPlayer?.GameItem != GameEnum.GameItem.None && SecondPlayer?.GameItem != GameEnum.GameItem.None)
        {
            SetPlayerDamage();
            EndRoundStep();
        }

    }

    public void DebugWinRound(GameEnum.PlayersNumber winPlayer)
    {
        if (winPlayer == GameEnum.PlayersNumber.PlayerOne)
        {
            FirstPlayer?.SelectItem(GameEnum.GameItem.Rock);
            SecondPlayer?.SelectItem(GameEnum.GameItem.Scissors);
        }

        else
        {
            FirstPlayer?.SelectItem(GameEnum.GameItem.Scissors);
            SecondPlayer?.SelectItem(GameEnum.GameItem.Rock);
        }

        
        OnSelectedItemAction?.Invoke(FirstPlayer.PlayersNumber,FirstPlayer.GameItem);
        OnSelectedItemAction?.Invoke(SecondPlayer.PlayersNumber,SecondPlayer.GameItem);
        
        SetPlayerDamage(true);
        EndRoundStep();
    }

    public virtual void NextRoundStep()
    {
        FirstPlayer.ResetData();
        SecondPlayer.ResetData();
        
        OnNextRoundStepAction?.Invoke();
    }

#region Protected

    protected virtual void EndRoundStep()
    {
        OnEndRoundStepAction?.Invoke();

        var hitterPlayer = _iPlayerDamager.GetHittingPlayer(FirstPlayer,SecondPlayer);
        var strikingPlayer = _iPlayerDamager.GetStrikingPlayer(FirstPlayer, SecondPlayer);
        
        if (hitterPlayer != null && hitterPlayer.Health <= 0)
        {
            PlayersWin = strikingPlayer;

            GameData.RoundInfos[_roundNum].WinnerPlayer.Name = strikingPlayer.Name;
            GameData.RoundInfos[_roundNum].WinnerPlayer.PlayersNumber = strikingPlayer.PlayersNumber;

            if (strikingPlayer.IsBot)
            {
                _roundResult = GameEnum.RoundResult.BotWin;
                GameData.RoundInfos[_roundNum].WinnerPlayer.IsBot = true;
            }
            else if (!strikingPlayer.IsBot) _roundResult = GameEnum.RoundResult.PlayerWin;

            EndRound();
        }
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

    protected void CallPrepareRoundAction(GameData gameData)
    {
        OnPrepareRoundAction?.Invoke(gameData);
    }

    protected void SetPlayerDamage(bool isDebug = false)
    {
        var hitterPlayer = _iPlayerDamager.GetHittingPlayer(FirstPlayer,SecondPlayer);
        var strikingPlayer = _iPlayerDamager.GetStrikingPlayer(FirstPlayer, SecondPlayer);

        if (hitterPlayer == null)
        {
            //if result draw
            OnGetHitAction?.Invoke(GameEnum.PlayersNumber.None, 0,0);
            return;
        }

        else
        {
            var playerDamage = !isDebug ? strikingPlayer.SelectionItemsСharacteristic.DamageValue[strikingPlayer.GameItem] : 1000;
            hitterPlayer.GetDamage(playerDamage);

            if (strikingPlayer.PlayersNumber == GameEnum.PlayersNumber.PlayerOne) GameData.RoundInfos[_roundNum].FirstPlayer.DamageDone += playerDamage;
            else if (strikingPlayer.PlayersNumber == GameEnum.PlayersNumber.PlayerTwo) GameData.RoundInfos[_roundNum].SecondPlayer.DamageDone += playerDamage;

            OnGetHitAction?.Invoke(hitterPlayer.PlayersNumber,hitterPlayer.Health, playerDamage);
            
        }
    }
    
    #endregion
}
