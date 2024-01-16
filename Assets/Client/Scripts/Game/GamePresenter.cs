using System;
using DG.Tweening;
using Zenject;

public class GamePresenter :  IInitializable
{
    public event Action<GameEnum.PlayersNumber,GameEnum.GameItem> OnSelectionItemAction;
    public event Action<Action<GameEnum.PrepareGameplayPoint>> OnCheckPreparePointsAction;
    public event Action <GameEnum.PlayersNumber,int, int> OnHitPlayerAction;
    
    
    public event Action OnEndGameAction;
    public event Action OnGameStartAction;
    
    
    public event Action OnNextRoundStepAction;
    public event Action OnEndRoundStepAction;


    public event Action<GameEnum.GameplayType> OnStartRoundAction;
    public event Action<GameData> OnEndRoundAction;
    public event Action<GameData> OnPrepareRoundAction;


    public event Action<int> OnStartMoveTimerAction;
    public event Action<int> OnTimerTickAction;
    public event Action  OnEndMoveTimerAction;
    
    
    private AGameplay _gameplay;
    
    private Sequence _endRoundSequence;
    private Sequence _hitPlayerSequence;

    [Inject] private GameManager _gameManager;
    [Inject] private WindowsManager _windowsManager;

    
    public void Initialize()
    {
        _gameManager.OnCheckPreparePointsAction += OnCheckPreparePoints;
    }


    public void InitializeGameplay(AGameplay aGameplay)
    {
        _gameplay = aGameplay;
        _gameplay.OnGameStartAction += OnStartGame;
    }

    public void SelectedItemClick(GameEnum.PlayersNumber playersNumber, GameEnum.GameItem gameItem)
    {
        _gameplay.SelectItem(playersNumber, gameItem);
    }

    public void DebugWinRound(GameEnum.PlayersNumber playersNumber)
    {
        _gameplay.DebugWinRound(playersNumber);
    }

    private void NextRoundStep()
    {
        _gameplay.NextRoundStep();
    }

    private void OnStartGame()
    {
        _gameplay.OnStartMoveTimerAction += OnStartMoveTimer;
        _gameplay.OnStopMoveTimeAction += OnStopMoveTimer;
        _gameplay.OnTimerTickAction += OnTimerTick;
        
        _gameplay.OnNextRoundStepAction += OnNextRoundStep;
        _gameplay.OnEndRoundStepAction += OnEndRoundStep;
        
        _gameplay.OnEndRoundAction += OnEndRound;
        _gameplay.OnRoundStartAction += OnRoundStart;
        _gameplay.OnPrepareRoundAction += OnPrepareRound;
        
        _gameplay.OnSelectedItemAction += OnSelectedItem;
        _gameplay.OnGetHitAction += OnHitPlayer;
        _gameplay.OnGameEndAction += OnGameEnd;

        OnGameStartAction?.Invoke();

    }
    


    private void OnGameEnd()
    {
        _endRoundSequence.Kill();
        OnEndGameAction?.Invoke();
        
        _gameplay.OnStartMoveTimerAction -= OnStartMoveTimer;
        _gameplay.OnStopMoveTimeAction -= OnStopMoveTimer;
        _gameplay.OnTimerTickAction -= OnTimerTick;
        
        _gameplay.OnNextRoundStepAction -= OnNextRoundStep;
        _gameplay.OnEndRoundStepAction -= OnEndRoundStep;
        
        _gameplay.OnGameEndAction -= OnGameEnd;
        _gameplay.OnGameStartAction -= OnStartGame;
        
        _gameplay.OnSelectedItemAction -= OnSelectedItem;
        _gameplay.OnGetHitAction -= OnHitPlayer;
        
        _gameplay.OnEndRoundAction -= OnEndRound;
        _gameplay.OnRoundStartAction -= OnRoundStart;
        _gameplay.OnPrepareRoundAction -= OnPrepareRound;

    }
    
    private void OnEndRoundStep()
    {
        OnEndRoundStepAction?.Invoke();
    }
    
    private void OnRoundStart(GameEnum.GameplayType gameplayType)
    {
        OnStartRoundAction?.Invoke(gameplayType);
    }
    
    private void OnPrepareRound(GameData gameData)
    {
        OnPrepareRoundAction?.Invoke(gameData);
    }
    
    private void OnSelectedItem(GameEnum.PlayersNumber playersNumber, GameEnum.GameItem gameItem = GameEnum.GameItem.None)
    {
        OnSelectionItemAction?.Invoke(playersNumber,gameItem);
    }
    
    private void OnNextRoundStep()
    {
        OnNextRoundStepAction?.Invoke();
    }
    
    
    private void OnCheckPreparePoints(Action<GameEnum.PrepareGameplayPoint> onComplete)
    {
        OnCheckPreparePointsAction?.Invoke((point => onComplete?.Invoke(point)));
    }
    
    private void OnEndRound(GameData gameData, GameEnum.RoundResult roundResult, int roundNum)
    {
        OnEndRoundAction?.Invoke(gameData);

        int winWindowDelay = 3;
        _endRoundSequence = DOTween.Sequence();
        _endRoundSequence.AppendInterval(winWindowDelay).AppendCallback(() => ShowWinWindow(gameData,roundResult,roundNum));

    }

    private void OnHitPlayer(GameEnum.PlayersNumber playersNumber, int currentHealth, int damage)
    {
        OnHitPlayerAction?.Invoke(playersNumber,currentHealth, damage);
        
        if(playersNumber != GameEnum.PlayersNumber.None && currentHealth <= 0) return;
        
        float waitTime = 4;
        _hitPlayerSequence = DOTween.Sequence();
        _hitPlayerSequence.AppendInterval(waitTime).AppendCallback(NextRoundStep);
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
