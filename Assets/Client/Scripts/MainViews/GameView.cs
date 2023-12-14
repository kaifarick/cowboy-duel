using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameView : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI _timer;
    [SerializeField] private TextMeshProUGUI _playerQueueTxt;
    
    [Space]
    [SerializeField] private Button _homeButton;
    [SerializeField] private List<SelectionButtonsGroup> _selectionButtonsGroup;
    [SerializeField] private Canvas _canvas;
    

    [Inject] private GameManager _gameManager;

    public event Action<GameEnum.GameItem> OnSelectionItemButtonClickAction;
    public event Action OnGameEndAction;
    public event Action<AGameplay> OnGameStartAction; 
    

    private void Awake()
    {
        SetButtons();
    }

    private void Start()
    {
        _gameManager.OnGameEndAction += OnGameEnd;
        _gameManager.OnGameStartAction += OnStartGame;
        
        _gameManager.OnChangeQueueAction += OnChangeQueue;
        _gameManager.OnSelectionItemAction += OnSelectionItem;
        
        _gameManager.OnStartMoveTimerAction += StartMoveTimer;
        _gameManager.OnEndMoveTimerAction += StopMoveTimer;
        _gameManager.OnTimerTickAction += OnTimerTick;

    }

    private void OnStartGame(AGameplay gameplay)
    {
        _canvas.enabled = true;
        OnGameStartAction?.Invoke(gameplay);
    }

    private void SetButtons()
    {
        _homeButton.onClick.AddListener(() => _gameManager.GameEnd());
        
        foreach (var buttonsGroup in _selectionButtonsGroup)
        {
            buttonsGroup.Initialize(this);
        }
    }

    private void OnSelectionItem(APlayer player, GameEnum.GameItem gameItem)
    {
        OnSelectionItemButtonClickAction?.Invoke(gameItem);
    }

    private void OnChangeQueue(APlayer aPlayer)
    {
        _playerQueueTxt.text = $"{aPlayer.Name} queue";
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
