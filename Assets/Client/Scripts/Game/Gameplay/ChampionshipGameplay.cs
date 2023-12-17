
public class ChampionshipGameplay : AGameplay
{

    private const int MOVE_TIME = 10;
    public ChampionshipGameplay(GameManager gameManager): base(GameEnum.GameplayType.TwoPlayers)
    {
        GameData.RoundInfos.Add(1, new GameData.RoundInfo { FirstPlayerName = "Player one", SecondPlayerName = "Player two" });
        GameData.RoundInfos.Add(2, new GameData.RoundInfo { FirstPlayerName = "Player three", SecondPlayerName = "Player four" });

        GameData.MaxRoundCount = 3;
    }
    
    
    public override void StartRound()
    {
        base.StartRound();

        if (_roundNum == 1 || _roundNum == 2)
        { 
            FirstPlayer = new Player(GameData.RoundInfos[_roundNum].FirstPlayerName, GameEnum.PlayersNumber.PlayerOne);
            SecondPlayer = new Player(GameData.RoundInfos[_roundNum].SecondPlayerName, GameEnum.PlayersNumber.PlayerTwo);
        }
        else if (_roundNum == GameData.MaxRoundCount)
        {
            FirstPlayer = new Player(GameData.RoundInfos[1].WinnerName, GameEnum.PlayersNumber.PlayerOne);
            SecondPlayer = new Player(GameData.RoundInfos[2].WinnerName, GameEnum.PlayersNumber.PlayerTwo);
        }
        
        GameData.RoundInfos[_roundNum].FirstPlayerName = FirstPlayer.Name;
        GameData.RoundInfos[_roundNum].SecondPlayerName = SecondPlayer.Name;
        
        
        StartMoveTimer(MOVE_TIME, EndRoundTime);
    }
    
    private void EndRoundTime()
    {
        StopMoveTimer();
        
        if(FirstPlayer.GameItem == GameEnum.GameItem.None) SelectItem(GameEnum.PlayersNumber.PlayerOne);
        if(SecondPlayer.GameItem == GameEnum.GameItem.None) SelectItem(GameEnum.PlayersNumber.PlayerTwo);
    }

}
