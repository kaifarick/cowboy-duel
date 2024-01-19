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

    
    protected int _roundTimeLeft;
    protected int _roundNum;

    protected IPlayerDamager _iPlayerDamager;
    protected DataManager _dataManager;
    
    
    public event Action<GameEnum.RoundResult, int> OnEndRoundAction;
    public event Action<GameEnum.GameplayType> OnRoundStartAction;
    
    
    public event Action OnNextRoundStepAction;
    public event Action OnEndRoundStepAction;
    
    
    public event Action OnGameEndAction;
    public event Action OnGameStartAction;



    public event Action<GameEnum.PlayersNumber, int, int> OnGetHitAction;
    public event Action<GameEnum.PlayersNumber, GameEnum.GameItem> OnSelectedItemAction;
    public event Action OnPrepareRoundAction;



    public event Action OnStopMoveTimeAction;
    public event Action<int> OnStartMoveTimerAction;
    public event Action<int> OnTimerTickAction;

    

    protected AGameplay(DataManager dataManager, GameEnum.GameplayType gameplayType)
    {
        GameplayType = gameplayType;
        _dataManager = dataManager;
        
#if DEBUG_LOGIC
        _iPlayerDamager = new DebugPlayerDamager();
#else
        _iPlayerDamager = new StandartPlayerDamager();
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
        PlayersWin = null;
        
        _dataManager.CreateRoundData(_roundNum);
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
            DamagePlayer();
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
        
        DamagePlayer(true);
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
        if (hitterPlayer != null && hitterPlayer.Health <= 0)
        {
            EndRound();
        }
    }

    protected virtual void EndRound()
    {
        var strikingPlayer = _iPlayerDamager.GetStrikingPlayer(FirstPlayer, SecondPlayer);
        PlayersWin = strikingPlayer;

        if (strikingPlayer.IsBot)
        {
            _roundResult = GameEnum.RoundResult.BotWin;
        }
        else if (!strikingPlayer.IsBot) _roundResult = GameEnum.RoundResult.PlayerWin;
        
        _dataManager.EndRoundData(strikingPlayer);
        
        OnEndRoundAction?.Invoke(_roundResult, _roundNum);
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

    protected void CallPrepareRoundAction()
    {
        _dataManager.PrepareRoundData(FirstPlayer,SecondPlayer,_roundNum);
        OnPrepareRoundAction?.Invoke();
    }

    protected void DamagePlayer(bool isDebug = false)
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

            _dataManager.DamageData(strikingPlayer.PlayersNumber, playerDamage);

            OnGetHitAction?.Invoke(hitterPlayer.PlayersNumber,hitterPlayer.Health, playerDamage);
            
        }
    }
    
    #endregion
}
