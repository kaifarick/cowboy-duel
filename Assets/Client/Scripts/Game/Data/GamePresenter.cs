using System;
using DG.Tweening;

public class GamePresenter
{
    public event Action<GameEnum.PlayersNumber,GameEnum.GameItem> OnSelectionItemAction;
    public event Action<GameData> OnEndRoundAction;
    public event Action<Action<GameEnum.PrepareGameplayPoint>> OnPrepareRoundAction;
    
    
    public event Action<int> OnStartMoveTimerAction;
    public event Action<int> OnTimerTickAction;
    public event Action  OnEndMoveTimerAction;
    
    private AGameplay _gameplay;
    private GameManager _gameManager;
    private WindowsManager _windowsManager;
    
    
    public GamePresenter(AGameplay aGameplay, GameManager gameManager, WindowsManager windowsManager)
    {
        _gameplay = aGameplay;
        _gameManager = gameManager;
        _windowsManager = windowsManager;
        

        gameManager.OnGameStartAction += OnStartGame;
        gameManager.OnGameEndAction += OnGameEnd;
        gameManager.OnPrepareRoundAction += OnPrepareRound;
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
        _gameplay.OnEndRoundAction += OnEndRound;
    }

    private void OnGameEnd()
    {
        _gameplay.OnStartMoveTimerAction -= OnStartMoveTimer;
        _gameplay.OnStopMoveTimeAction -= OnStopMoveTimer;
        _gameplay.OnTimerTickAction -= OnTimerTick;
        _gameplay.OnSelectedItemAction -= OnSelectedItem;
        _gameplay.OnEndRoundAction -= OnEndRound;
    }
    
    private void OnSelectedItem(GameEnum.PlayersNumber playersNumber, GameEnum.GameItem gameItem = GameEnum.GameItem.None)
    {
        OnSelectionItemAction?.Invoke(playersNumber,gameItem);
    }

    private void OnEndRound(GameData gameData, GameEnum.RoundResult roundResult, int roundNum)
    {
        OnEndRoundAction?.Invoke(gameData);

        int winWindowDelay = 4;
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(winWindowDelay).AppendCallback((() => ShowWinWindow(gameData,roundResult,roundNum)));

    }
    
    private void OnPrepareRound(Action<GameEnum.PrepareGameplayPoint> onComplete)
    {
        OnPrepareRoundAction?.Invoke((point => onComplete?.Invoke(point)));
    }

    private void ShowWinWindow(GameData gameData, GameEnum.RoundResult roundResult, int roundNum)
    {
        AWinWindow winWindow = null;
        if (_gameplay is PlayerVsComputerGameplay) winWindow = _windowsManager.OpenWindow<WinWindowPlayerVsComputer>();
        if (_gameplay is PlayerVsPlayerGameplay) winWindow = _windowsManager.OpenWindow<WinWindowPlayerVsPlayer>();
        if (_gameplay is SurvivalGameplay) winWindow = _windowsManager.OpenWindow<WinWindowSurvival>();
        if (_gameplay is ChampionshipGameplay) winWindow = _windowsManager.OpenWindow<WinWindowChampionship>();
        
        winWindow.Initialize(gameData, roundResult, roundNum);
    }



    #region Timer

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
    
    #endregion

}
