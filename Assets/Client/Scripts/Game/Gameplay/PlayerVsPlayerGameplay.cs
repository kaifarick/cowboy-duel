

public class PlayerVsPlayerGameplay : AGameplay
{
    private GameManager _gameManager;

    private const int MOVE_TIME = 5;
    
    public PlayerVsPlayerGameplay(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public override void StartGame()
    {
        FirstPlayer = new Player("Player One", GameEnum.PlayersNumber.PlayerOne);
        SecondPlayer = new Player("Player Two", GameEnum.PlayersNumber.PlayerTwo);

        base.StartGame();
        
        RoundNum++;
        
        StartMoveTimer(MOVE_TIME, () => _gameManager.SelectedItem());
    }
    
    public override void OnSelectedItem(APlayer playersSelected, GameEnum.GameItem gameItem)
    {
        base.OnSelectedItem(playersSelected, gameItem);
        
        ChangeQueue(SecondPlayer);

        if(PlayersTurn == playersSelected) CheckPlayerWin();
        else
        {
            StopMoveTimer();
            StartMoveTimer(MOVE_TIME, () => _gameManager.SelectedItem());
        }
    }

    protected override void StopMoveTimer()
    {
        TimerManager.RemoveWaiter("RoundTimer");
        base.StopMoveTimer();
    }

    protected override void EndGame()
    {
        StopMoveTimer();
        base.EndGame();
    }
}
