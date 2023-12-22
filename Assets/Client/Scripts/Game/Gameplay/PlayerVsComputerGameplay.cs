

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

        GameData.RoundInfos[_roundNum].FirstPlayer.Name = FirstPlayer.Name;
        GameData.RoundInfos[_roundNum].SecondPlayer.Name = SecondPlayer.Name;

        GameData.RoundInfos[_roundNum].FirstPlayer.GameItem = FirstPlayer.GameItem;
        GameData.RoundInfos[_roundNum].SecondPlayer.GameItem = SecondPlayer.GameItem;

        GameData.RoundInfos[_roundNum].FirstPlayer.PlayersNumber = FirstPlayer.PlayersNumber;
        GameData.RoundInfos[_roundNum].SecondPlayer.PlayersNumber = SecondPlayer.PlayersNumber;

        GameData.RoundInfos[_roundNum].FirstPlayer.IsBot = FirstPlayer.IsBot;
        GameData.RoundInfos[_roundNum].SecondPlayer.IsBot = SecondPlayer.IsBot;

    }

}
