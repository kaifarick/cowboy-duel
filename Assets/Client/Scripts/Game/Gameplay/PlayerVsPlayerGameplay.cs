

public class PlayerVsPlayerGameplay : AGameplay
{

    private const int MOVE_TIME = 10;
    
    public PlayerVsPlayerGameplay(GameManager gameManager):base(GameEnum.GameplayType.TwoPlayers)
    {
    }


    public override void StartRound()
    {
        base.StartRound();
        
        FirstPlayer = new Player("Player One", GameEnum.PlayersNumber.PlayerOne);
        SecondPlayer = new Player("Player Two", GameEnum.PlayersNumber.PlayerTwo);

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
