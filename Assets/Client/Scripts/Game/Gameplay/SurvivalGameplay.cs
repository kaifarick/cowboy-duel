

public class SurvivalGameplay : AGameplay
{
    private GameManager _gameManager;
    public SurvivalGameplay(GameManager gameManager)
    {
        _gameManager = gameManager;
    }
    
    public override void StartRound()
    {
        FirstPlayer = new Player("You", GameEnum.PlayersNumber.PlayerOne);
        SecondPlayer = new BotPlayer(GameEnum.PlayersNumber.PlayerTwo);
        
        if(_roundResult != GameEnum.RoundResult.Draw) _roundNum++;
        
        base.StartRound();
    }
    
    public override void OnSelectedItem(APlayer playersSelected, GameEnum.GameItem gameItem)
    {
        base.OnSelectedItem(playersSelected, gameItem);
        
        ChangeQueue(SecondPlayer);
        
        //bot move
        if(PlayersTurn != playersSelected) _gameManager.SelectedItem();
        else CheckPlayerWin();
    }
}
