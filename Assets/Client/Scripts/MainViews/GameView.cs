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
    [SerializeField] private Button _playerOneWinRoundButton;
    [SerializeField] private Button _playerTwoWinRoundButton;
    [SerializeField] private GameObject _debugButtonBox;

    [Inject] private GameManager _gameManager;
    [Inject] private GamePresenter _gamePresenter;
    [Inject] private DataManager _dataManager;

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

    private void OnPrepareRound()
    {
        OnPrepareRoundAction?.Invoke(_dataManager.GameData);
    }


    private void SetButtons()
    {
        _homeButton.onClick.AddListener(() => _gameManager.GameEnd());

        foreach (var buttonsGroup in _selectionButtonsGroup)
        {
            buttonsGroup.Initialize(this,  SelectedItem);
        }
        
        DebugButtonState();
    }

    private void DebugButtonState()
    {
#if DEBUG_LOGIC
        _debugButtonBox.gameObject.SetActive(true);
        
        _playerOneWinRoundButton.onClick.AddListener(() => _gamePresenter.DebugWinRound(GameEnum.PlayersNumber.PlayerOne));
        _playerTwoWinRoundButton.onClick.AddListener(() => _gamePresenter.DebugWinRound(GameEnum.PlayersNumber.PlayerTwo));
#endif
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
