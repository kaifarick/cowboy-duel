
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SelectionItemButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private GameEnum.GameItem _gameItem;

    private Color32 _defaultDeactivateColor = new Color32(200, 200, 200, 128);
    private Color32 _selectedDeactivateColor = new Color32(155, 253, 153, 255);

    [Inject] private GameManager _gameManager;


    private void Awake()
    {
        _button.onClick.AddListener((() => _gameManager.SelectedItem(_gameItem)));
    }

    public void Initialize(GameView gameView)
    {
        gameView.OnSelectionItemButtonClickAction += OnSelectionItemButtonClickAction;
        gameView.OnGameEndAction += ResetData;
    }
    
    private void OnSelectionItemButtonClickAction(GameEnum.GameItem gameItem)
    {
        if (_gameManager.AGameplay is PlayerVsComputerGameplay)
        {
            _button.interactable = false;
            var color = _button.colors;
            if (gameItem == _gameItem)
            {
                color.disabledColor = _selectedDeactivateColor;
                _button.colors = color;
            }
        }
        
        else if (_gameManager.AGameplay is PlayerVsPlayerGameplay)
        {
            
        }
    }

    private void ResetData()
    {
        _button.interactable = true;
        
        var color = _button.colors;
        color.disabledColor = _defaultDeactivateColor;
        _button.colors = color;
    }
}
