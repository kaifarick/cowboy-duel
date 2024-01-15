

public class SurvivalGameplay : AGameplay
{
    public SurvivalGameplay(GameManager gameManager):base(GameEnum.GameplayType.OnePlayer)
    {

    }
    


    public override void PrepareGameRound()
    {
        base.PrepareGameRound();
        
        FirstPlayer = new Player("You", GameEnum.PlayersNumber.PlayerOne);
        SecondPlayer = new BotPlayer(GameEnum.PlayersNumber.PlayerTwo);
        SecondPlayer.SelectItem(GameEnum.GameItem.None);

        GameData.RoundInfos[_roundNum].FirstPlayer.Name = FirstPlayer.Name;
        GameData.RoundInfos[_roundNum].SecondPlayer.Name = SecondPlayer.Name;

        GameData.RoundInfos[_roundNum].FirstPlayer.PlayersNumber = FirstPlayer.PlayersNumber;
        GameData.RoundInfos[_roundNum].SecondPlayer.PlayersNumber = SecondPlayer.PlayersNumber;

        GameData.RoundInfos[_roundNum].FirstPlayer.IsBot = FirstPlayer.IsBot;
        GameData.RoundInfos[_roundNum].SecondPlayer.IsBot = SecondPlayer.IsBot;
        
        GameData.RoundInfos[_roundNum].FirstPlayer.SelectionItemsСharacteristic = FirstPlayer.SelectionItemsСharacteristic;
        GameData.RoundInfos[_roundNum].SecondPlayer.SelectionItemsСharacteristic = SecondPlayer.SelectionItemsСharacteristic;
        
        CallPrepareRoundAction(GameData);
    }
}
