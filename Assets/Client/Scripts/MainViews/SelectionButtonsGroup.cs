using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class SelectionButtonsGroup : MonoBehaviour
{
    [SerializeField] private List<SelectionItemButton> _selectionItemButton;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Transform _centre;
    
    [SerializeField] private GroupType _groupType;


    public void Initialize(GameView gameView)
    {
        foreach (var button in _selectionItemButton)
        {
            button.Initialize(gameView, _centre, _moveSpeed);
        }

        gameView.OnGameStartAction += OnGameStart;
        gameView.OnGameEndAction += OnGameEnd;

    }

    private void OnGameStart(AGameplay aGameplay)
    {
        if (_groupType == GroupType.MainGroup || aGameplay.GameplayType == GameEnum.GameplayType.TwoPlayers)
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
    
    private enum GroupType
    {
        MainGroup,
        AdditionGroup
    }
}
