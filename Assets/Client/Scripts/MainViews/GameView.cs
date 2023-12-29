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

    [Inject] private GameManager _gameManager;
    [Inject] private GamePresenter _gamePresenter;

    public event Action<GameEnum.PlayersNumber> OnSelectionItemAction;
    public event Action<GameEnum.GameplayType> OnRoundStartAction;
    public event Action OnGameEndAction;


    private void Awake()
    {
        SetButtons();
    }

    private void Start()
    {
        _gameManager.OnGameEndAction += OnGameEnd;
        _gameManager.OnGameStartAction += OnStartGame;
        _gameManager.OnRoundStartAction += OnRoundStart;
        
        _gamePresenter.OnSelectionItemAction += OnSelectionItem;
        _gamePresenter.OnStartMoveTimerAction += StartMoveTimer;
        _gamePresenter.OnEndMoveTimerAction += StopMoveTimer;
        _gamePresenter.OnTimerTickAction += OnTimerTick;  

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
    

    private void OnStartGame()
    {
        _canvas.enabled = true;
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
