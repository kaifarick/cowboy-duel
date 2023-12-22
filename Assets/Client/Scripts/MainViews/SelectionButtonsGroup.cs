using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class SelectionButtonsGroup : MonoBehaviour
{
    [SerializeField] private List<SelectionItemButton> _selectionItemButton;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Transform _centre;
    
    [SerializeField] private GameEnum.PlayersNumber _playersNumber;

    private Action<GameEnum.PlayersNumber, GameEnum.GameItem> _onSelectionButtonClickAction;


    public void Initialize(GameView gameView, Action<GameEnum.PlayersNumber,GameEnum.GameItem> onSelectionButtonClickAction)
    {
        _onSelectionButtonClickAction = onSelectionButtonClickAction;
        
        foreach (var button in _selectionItemButton)
        {
            button.Initialize(gameView, _centre, _moveSpeed, SelectionButtonClick);
        }

        gameView.OnRoundStartAction += OnRoundStart;
        gameView.OnGameEndAction += OnGameEnd;
        gameView.OnSelectionItemAction += OnSelectionItem;

    }

    private void SelectionButtonClick(GameEnum.GameItem gameItem)
    {
        _onSelectionButtonClickAction?.Invoke(_playersNumber, gameItem);
    }

    private void OnSelectionItem(GameEnum.PlayersNumber playersNumber)
    {
        if (_playersNumber == playersNumber) gameObject.SetActive(false);
    }

    private void OnRoundStart(GameEnum.GameplayType gameplayType)
    {
        if (_playersNumber == GameEnum.PlayersNumber.PlayerOne || gameplayType == GameEnum.GameplayType.TwoPlayers)
        {
            gameObject.SetActive(true);
        }
    }

    private void OnGameEnd()
    {
        gameObject.SetActive(false);
    }

    private void Reset()
    {
        _selectionItemButton = GetComponentsInChildren<SelectionItemButton>().ToList();
    }
}
