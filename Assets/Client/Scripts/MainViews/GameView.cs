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
    [SerializeField] private CowboyView _playerOneCowboy;
    [SerializeField] private CowboyView _playerTwoCowboy;


    [Inject] private GameManager _gameManager;

    public event Action<GameEnum.PlayersNumber> OnSelectionItemAction;
    public event Action<GameEnum.GameplayType> OnRoundStartAction;
    public event Action OnGameEndAction;
    

    private GamePresenter _gamePresenter;
    

    private void Awake()
    {
        SetButtons();
       
    }

    private void Start()
    {
        _gameManager.OnGameEndAction += OnGameEnd;
        _gameManager.OnGameStartAction += OnStartGame;
        _gameManager.OnRoundStartAction += OnRoundStart;

    }
    
    private void SetButtons()
    {
        _homeButton.onClick.AddListener(() => _gameManager.GameEnd());
        
        foreach (var buttonsGroup in _selectionButtonsGroup)
        {
            buttonsGroup.Initialize(this,  SelectedItem);
        }
    }

    private void SelectedItem(GameEnum.PlayersNumber playersNumber, GameEnum.GameItem gameItem)
    {
        _gamePresenter.SelectedItemClick(playersNumber, gameItem);
    }
    

    private void OnStartGame(GamePresenter gamePresenter)
    {
        gamePresenter.OnSelectionItemAction += OnSelectionItem;
        gamePresenter.OnStartMoveTimerAction += StartMoveTimer;
        gamePresenter.OnEndMoveTimerAction += StopMoveTimer;
        gamePresenter.OnTimerTickAction += OnTimerTick;   
        
        
        _canvas.enabled = true;
        _gamePresenter = gamePresenter;
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
        OnGameEndAction?.Invoke();
        _canvas.enabled = false;
        _timer.gameObject.SetActive(false);
    }
    
    
}
