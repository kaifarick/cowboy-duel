
public class ChampionshipGameplay : AGameplay
{

    private const int MOVE_TIME = 10;
    public ChampionshipGameplay(DataManager dataManager): base(dataManager, GameEnum.GameplayType.TwoPlayers)
    {
        _dataManager.InitializeGameData(3);
        
        _dataManager.AddCustomRoundData(1,"Player one", "Player two");
        _dataManager.AddCustomRoundData(2,"Player three", "Player four");
    }
    
    
    public override void PrepareGameRound()
    {
        base.PrepareGameRound();

        if (_roundNum == 1 || _roundNum == 2)
        { 
            FirstPlayer = new Player(_dataManager.GameData.RoundInfos[_roundNum].FirstPlayer.Name, GameEnum.PlayersNumber.PlayerOne);
            SecondPlayer = new Player(_dataManager.GameData.RoundInfos[_roundNum].SecondPlayer.Name, GameEnum.PlayersNumber.PlayerTwo);
        }
        else if (_roundNum == _dataManager.GameData.MaxRoundCount)
        {
            FirstPlayer = new Player(_dataManager.GameData.RoundInfos[1].WinnerPlayer.Name, GameEnum.PlayersNumber.PlayerOne);
            SecondPlayer = new Player(_dataManager.GameData.RoundInfos[2].WinnerPlayer.Name, GameEnum.PlayersNumber.PlayerTwo);
        }

        CallPrepareRoundAction();
        
    }
    
    public override void SelectItem(GameEnum.PlayersNumber playersNumber, GameEnum.GameItem gameItem = GameEnum.GameItem.None)
    {
        StopMoveTimer();
        base.SelectItem(playersNumber, gameItem);
    }
    
    private void EndRoundTime()
    {
        StopMoveTimer();

        if (PlayerTurn == FirstPlayer)
        {
            SelectItem(GameEnum.PlayersNumber.PlayerOne);
            StartMoveTimer(MOVE_TIME, EndRoundTime);
            return;
        }

        if (PlayerTurn == SecondPlayer)
        {
            SelectItem(GameEnum.PlayersNumber.PlayerTwo);
            return;
        }
    }
    
    public override void NextRoundStep()
    {
        base.NextRoundStep();
        
        StartMoveTimer(MOVE_TIME, EndRoundTime);
    }

    protected override void EndRoundStep()
    {
        base.EndRoundStep();
        
        StopMoveTimer();
    }

    public override void StartRound()
    {
        base.StartRound();
        
        StartMoveTimer(MOVE_TIME, EndRoundTime);
    }

    public override void EndGame()
    {
        base.EndGame();
        
        StopMoveTimer();
    }

}
