

public class SurvivalGameplay : AGameplay
{
    public SurvivalGameplay(DataManager dataManager):base(dataManager, GameEnum.GameplayType.OnePlayer)
    {
        _dataManager.InitializeGameData();
    }
    


    public override void PrepareGameRound()
    {
        base.PrepareGameRound();
        
        FirstPlayer = new Player("You", GameEnum.PlayersNumber.PlayerOne);
        SecondPlayer = new BotPlayer(GameEnum.PlayersNumber.PlayerTwo);
        SecondPlayer.SelectItem(GameEnum.GameItem.None);

        CallPrepareRoundAction();
    }
}
