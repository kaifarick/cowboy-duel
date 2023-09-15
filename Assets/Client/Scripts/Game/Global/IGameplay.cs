using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameplay
{

    public event Action<APlayer> OnChangeQueueAction;
    public event Action<APlayer,GameEnum.RoundResult> OnEndRoundAction;
    public event Action OnGameEndAction;
    public event Action OnStopMoveTimeAction;
    public event Action<int> OnStartMoveTimerAction; 
    public event Action<int> OnTimerTickAction;
    
    public void StartGame();
    public void EndRound();
    public void EndGame();
    public void OnSelectedItem(APlayer playersSelected, GameEnum.GameItem gameItem);
    public void ChangeQueue(APlayer playerQueue);
    public void StartMoveTimer(int time, Action callback);
    public void StopMoveTimer();
    public void CheckPlayerWin();
}
