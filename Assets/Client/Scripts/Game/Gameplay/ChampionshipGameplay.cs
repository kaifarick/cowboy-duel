

using System.Collections.Generic;
using UnityEngine;

public class ChampionshipGameplay : AGameplay
{
    private GameManager _gameManager;
    private ChampionshipInfo _championshipInfo;

    private const int MOVE_TIME = 5;
    public ChampionshipGameplay(GameManager gameManager): base(GameEnum.GameplayType.TwoPlayers)
    {
        _gameManager = gameManager;

        _championshipInfo = new ChampionshipInfo()
        {
            RoundInfos = new Dictionary<int, ChampionshipInfo.RoundInfo>()
            {
                { 1, new ChampionshipInfo.RoundInfo { PlayerOneName = "Player one", PlayerTwoName = "Player two" } },
                { 2, new ChampionshipInfo.RoundInfo { PlayerOneName = "Player three", PlayerTwoName = "Player four" } },
            }
        };

        GameplayInfo = _championshipInfo;
    }
    
    
    
    public override void StartRound()
    {
        if(_roundResult != GameEnum.RoundResult.Draw) _roundNum++;
        
        if (_roundNum == 1 || _roundNum == 2)
        { 
            FirstPlayer = new Player(_championshipInfo.RoundInfos[_roundNum].PlayerOneName, GameEnum.PlayersNumber.PlayerOne);
            SecondPlayer = new Player(_championshipInfo.RoundInfos[_roundNum].PlayerTwoName, GameEnum.PlayersNumber.PlayerTwo);
        }
        else if (_roundNum == 3)
        {
            FirstPlayer = new Player(_championshipInfo.RoundInfos[1].WinnerName, GameEnum.PlayersNumber.PlayerOne);
            SecondPlayer = new Player(_championshipInfo.RoundInfos[2].WinnerName, GameEnum.PlayersNumber.PlayerTwo);
        }

        base.StartRound();
        
        StartMoveTimer(MOVE_TIME, () => _gameManager.SelectedItem());
    }
    
    public override void OnSelectedItem(APlayer playersSelected, GameEnum.GameItem gameItem)
    {
        base.OnSelectedItem(playersSelected, gameItem);
        
        ChangeQueue(SecondPlayer);

        if(PlayersTurn == playersSelected) CheckPlayerWin();
        else
        {
            StopMoveTimer();
            StartMoveTimer(MOVE_TIME, () => _gameManager.SelectedItem());
        }
    }

    protected override void EndRound()
    {
        if(_championshipInfo.RoundInfos.Count >= _roundNum) _championshipInfo.RoundInfos[_roundNum].WinnerName = PlayersWin?.Name;
        base.EndRound();
    }

    protected override void StopMoveTimer()
    {
        TimerManager.RemoveWaiter("RoundTimer");
        base.StopMoveTimer();
    }

    public override void EndGame()
    {
        StopMoveTimer();
        base.EndGame();
    }
}
