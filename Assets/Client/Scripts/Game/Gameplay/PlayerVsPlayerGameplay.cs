

public class PlayerVsPlayerGameplay : AGameplay
{

    private const int MOVE_TIME = 10;
    
    public PlayerVsPlayerGameplay(GameManager gameManager):base(GameEnum.GameplayType.TwoPlayers)
    {
        GameData.RoundInfos.Add(1, new GameData.RoundInfo {FirstPlayerName = "Player One", SecondPlayerName = "Player Two" });
    }


    public override void StartRound()
    {
        base.StartRound();
        
        FirstPlayer = new Player(GameData.RoundInfos[_roundNum].FirstPlayerName, GameEnum.PlayersNumber.PlayerOne);
        SecondPlayer = new Player(GameData.RoundInfos[_roundNum].SecondPlayerName, GameEnum.PlayersNumber.PlayerTwo);

        StartMoveTimer(MOVE_TIME, EndRoundTime);
    }
    

    private void EndRoundTime()
    {
        StopMoveTimer();
        
        if(FirstPlayer.GameItem == GameEnum.GameItem.None) SelectItem(GameEnum.PlayersNumber.PlayerOne);
        if(SecondPlayer.GameItem == GameEnum.GameItem.None) SelectItem(GameEnum.PlayersNumber.PlayerTwo);
    }
    
}
