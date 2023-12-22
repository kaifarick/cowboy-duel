
public class ChampionshipGameplay : AGameplay
{

    private const int MOVE_TIME = 10;
    public ChampionshipGameplay(GameManager gameManager): base(GameEnum.GameplayType.TwoPlayers)
    {
        GameData.RoundInfos.Add(1, new GameData.RoundInfo { FirstPlayer = new GameData.PlayerInfo(){Name = "Player one"}, SecondPlayer = new GameData.PlayerInfo(){Name = "Player two" }, WinnerPlayer = new GameData.PlayerInfo()});
        GameData.RoundInfos.Add(2, new GameData.RoundInfo { FirstPlayer = new GameData.PlayerInfo(){Name = "Player three"}, SecondPlayer = new GameData.PlayerInfo(){Name = "Player four" }, WinnerPlayer = new GameData.PlayerInfo()});

        GameData.MaxRoundCount = 3;
    }
    
    
    public override void StartRound()
    {
        base.StartRound();

        if (_roundNum == 1 || _roundNum == 2)
        { 
            FirstPlayer = new Player(GameData.RoundInfos[_roundNum].FirstPlayer.Name, GameEnum.PlayersNumber.PlayerOne);
            SecondPlayer = new Player(GameData.RoundInfos[_roundNum].SecondPlayer.Name, GameEnum.PlayersNumber.PlayerTwo);
        }
        else if (_roundNum == GameData.MaxRoundCount)
        {
            FirstPlayer = new Player(GameData.RoundInfos[1].WinnerPlayer.Name, GameEnum.PlayersNumber.PlayerOne);
            SecondPlayer = new Player(GameData.RoundInfos[2].WinnerPlayer.Name, GameEnum.PlayersNumber.PlayerTwo);
        }
        
        GameData.RoundInfos[_roundNum].FirstPlayer.Name = FirstPlayer.Name;
        GameData.RoundInfos[_roundNum].SecondPlayer.Name = SecondPlayer.Name;

        GameData.RoundInfos[_roundNum].FirstPlayer.GameItem = FirstPlayer.GameItem;
        GameData.RoundInfos[_roundNum].SecondPlayer.GameItem = SecondPlayer.GameItem;

        GameData.RoundInfos[_roundNum].FirstPlayer.PlayersNumber = FirstPlayer.PlayersNumber;
        GameData.RoundInfos[_roundNum].SecondPlayer.PlayersNumber = SecondPlayer.PlayersNumber;

        GameData.RoundInfos[_roundNum].FirstPlayer.IsBot = FirstPlayer.IsBot;
        GameData.RoundInfos[_roundNum].SecondPlayer.IsBot = SecondPlayer.IsBot;
        
        
        StartMoveTimer(MOVE_TIME, EndRoundTime);
    }
    
    private void EndRoundTime()
    {
        StopMoveTimer();
        
        if(FirstPlayer.GameItem == GameEnum.GameItem.None) SelectItem(GameEnum.PlayersNumber.PlayerOne);
        if(SecondPlayer.GameItem == GameEnum.GameItem.None) SelectItem(GameEnum.PlayersNumber.PlayerTwo);
    }
    
    protected override void EndRound()
    {
        base.EndRound();
        
        StopMoveTimer();
    }

    public override void EndGame()
    {
        base.EndGame();
        
        StopMoveTimer();
    }

}
