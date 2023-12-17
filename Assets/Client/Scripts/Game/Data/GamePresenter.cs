
using System;

public class GamePresenter
{
    public event Action<GameEnum.PlayersNumber,GameEnum.GameItem> OnSelectionItemAction;
    public event Action<int> OnStartMoveTimerAction;
    public event Action<int> OnTimerTickAction;
    public event Action  OnEndMoveTimerAction;
    
    private AGameplay _gameplay;
    private GameManager _gameManager;
    
    
    public GamePresenter(AGameplay aGameplay, GameManager gameManager)
    {
        _gameplay = aGameplay;
        _gameManager = gameManager;
        

        gameManager.OnGameStartAction += OnStartGame;
        gameManager.OnGameEndAction += OnGameEnd;
    }

    public void SelectedItemClick(GameEnum.PlayersNumber playersNumber, GameEnum.GameItem gameItem)
    {
        _gameplay.SelectItem(playersNumber, gameItem);
    }
    

    private void OnStartGame(GamePresenter gamePresenter)
    {
        _gameplay.OnStartMoveTimerAction += OnStartMoveTimer;
        _gameplay.OnStopMoveTimeAction += OnStopMoveTimer;
        _gameplay.OnTimerTickAction += OnTimerTick;
        _gameplay.OnSelectedItemAction += OnSelectedItem;
    }

    private void OnGameEnd()
    {
        _gameplay.OnStartMoveTimerAction -= OnStartMoveTimer;
        _gameplay.OnStopMoveTimeAction -= OnStopMoveTimer;
        _gameplay.OnTimerTickAction -= OnTimerTick;
        _gameplay.OnSelectedItemAction -= OnSelectedItem;
    }
    
    private void OnSelectedItem(GameEnum.PlayersNumber playersNumber, GameEnum.GameItem gameItem = GameEnum.GameItem.None)
    {
        OnSelectionItemAction?.Invoke(playersNumber,gameItem);
    }
    
    
    private void OnStartMoveTimer(int time)
    {
        OnStartMoveTimerAction?.Invoke(time);
    }

    private void OnStopMoveTimer()
    {
        OnEndMoveTimerAction?.Invoke();
    }

    private void OnTimerTick(int timeLeft)
    {
        OnTimerTickAction?.Invoke(timeLeft);
    }
}
