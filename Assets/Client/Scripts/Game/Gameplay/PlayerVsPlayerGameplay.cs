

public class PlayerVsPlayerGameplay : AGameplay
{

    private const int MOVE_TIME = 10;
    
    public PlayerVsPlayerGameplay(GameManager gameManager):base(GameEnum.GameplayType.TwoPlayers)
    {
    }


    public override void PrepareGameRound()
    {
        base.PrepareGameRound();
        
        FirstPlayer = new Player("Player One", GameEnum.PlayersNumber.PlayerOne);
        SecondPlayer = new Player("Player Two", GameEnum.PlayersNumber.PlayerTwo);

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
    

    private void EndRoundTime()
    {
        StopMoveTimer();
        
        if(FirstPlayer.GameItem == GameEnum.GameItem.None) SelectItem(GameEnum.PlayersNumber.PlayerOne);
        if(SecondPlayer.GameItem == GameEnum.GameItem.None) SelectItem(GameEnum.PlayersNumber.PlayerTwo);
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
