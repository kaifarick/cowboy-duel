

public class PlayerVsComputerGameplay : AGameplay
{
    
    
    public PlayerVsComputerGameplay(GameManager gameManager) : base(GameEnum.GameplayType.OnePlayer)
    {
        
    }
   
    public override void StartRound()
    {
        base.StartRound();
        
        FirstPlayer = new Player("You", GameEnum.PlayersNumber.PlayerOne);
        SecondPlayer = new BotPlayer(GameEnum.PlayersNumber.PlayerTwo);

        GameData.RoundInfos[_roundNum].FirstPlayerName = FirstPlayer.Name;
        GameData.RoundInfos[_roundNum].SecondPlayerName = SecondPlayer.Name;

        GameData.RoundInfos[_roundNum].SecondPlayerItem = SecondPlayer.GameItem;

    }

}
