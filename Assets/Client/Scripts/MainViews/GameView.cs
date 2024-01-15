using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameView : MonoBehaviour
{

    [SerializeField] private Button _homeButton;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TextMeshProUGUI _timer;
    [SerializeField] private List<SelectionButtonsGroup> _selectionButtonsGroup;

    [Space] 
    [Header("DebugButtons")] 
    [SerializeField] private Button _playerOneWinRound;
    [SerializeField] private Button _playerTwoWinRound;

    [Inject] private GameManager _gameManager;
    [Inject] private GamePresenter _gamePresenter;

    public event Action<GameEnum.PlayersNumber> OnSelectionItemAction;
    public event Action<GameEnum.GameplayType> OnRoundStartAction;
    public event Action OnNextRoundStepAction;
    public event Action<GameData> OnPrepareRoundAction;
    public event Action OnGameEndAction;


    private void Awake()
    {
        SetButtons();
    }

    private void Start()
    {
        _gamePresenter.OnStartMoveTimerAction += StartMoveTimer;
        _gamePresenter.OnEndMoveTimerAction += StopMoveTimer;
        _gamePresenter.OnTimerTickAction += OnTimerTick;  
        
        _gamePresenter.OnEndGameAction += OnGameEnd;
        _gamePresenter.OnGameStartAction += OnStartGame;
        
        _gamePresenter.OnNextRoundStepAction += OnNextRoundStep;
        _gamePresenter.OnStartRoundAction += OnRoundStart;
        _gamePresenter.OnPrepareRoundAction += OnPrepareRound;
        
        _gamePresenter.OnSelectionItemAction += OnSelectionItem;

    }

    private void OnPrepareRound(GameData gameData)
    {
        OnPrepareRoundAction?.Invoke(gameData);
    }


    private void SetButtons()
    {
        _homeButton.onClick.AddListener(() => _gameManager.GameEnd());
        _playerOneWinRound.onClick.AddListener(() => _gamePresenter.DebugWinRound(GameEnum.PlayersNumber.PlayerOne));
        _playerTwoWinRound.onClick.AddListener(() => _gamePresenter.DebugWinRound(GameEnum.PlayersNumber.PlayerTwo));
        
        foreach (var buttonsGroup in _selectionButtonsGroup)
        {
            buttonsGroup.Initialize(this,  SelectedItem);
        }
    }

    private void SelectedItem(GameEnum.PlayersNumber playersNumber, GameEnum.GameItem gameItem)
    {
        _gamePresenter.SelectedItemClick(playersNumber, gameItem);
    }
    

    private void OnStartGame()
    {
        _canvas.enabled = true;
    }
    
    private void OnNextRoundStep()
    {
        OnNextRoundStepAction?.Invoke();
    }
    

    private void OnSelectionItem(GameEnum.PlayersNumber playersNumber, GameEnum.GameItem gameItem)
    {
        OnSelectionItemAction?.Invoke(playersNumber);
    }

    private void OnRoundStart(GameEnum.GameplayType gameplayType)
    {
        OnRoundStartAction?.Invoke(gameplayType);
    }
    
    private void StartMoveTimer(int time)
    {
        _timer.gameObject.SetActive(true);
        _timer.text = time.ToString();
    }

    private void StopMoveTimer()
    {
        _timer.gameObject.SetActive(false);
    }

    private void OnTimerTick(int time)
    {
        _timer.text = time.ToString();
    }
    
    private void OnGameEnd()
    {
        _canvas.enabled = false;
        _timer.gameObject.SetActive(false);
        
        OnGameEndAction?.Invoke();
    }
}
