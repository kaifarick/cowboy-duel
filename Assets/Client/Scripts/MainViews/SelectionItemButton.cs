
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SelectionItemButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private GameEnum.GameItem _gameItem;
    [SerializeField] private List<GameObject> _stars;

    private Color32 _defaultDeactivateColor = new Color32(200, 200, 200, 128);
    private Color32 _selectedDeactivateColor = new Color32(155, 253, 153, 255);

    private GameEnum.PlayersNumber _playersNumber;

    private Transform _centre;
    private Vector3 _direction;
    private Quaternion _rotation;

    private float _moveSpeed;



    public void Initialize(GameView gameView, GameEnum.PlayersNumber playersNumber, Transform centre, float moveSpeed, Action<GameEnum.GameItem> onButtonClick)
    {
        _centre = centre;
        _moveSpeed = moveSpeed;
        _playersNumber = playersNumber;
        
        gameView.OnGameEndAction += ResetData;
        gameView.OnPrepareRoundAction += OnPrepareRound;
        
        _button.onClick.AddListener((() => onButtonClick?.Invoke(_gameItem)));
    }
    

    private void Update()
    {
        _direction = transform.position - _centre.position;
        _rotation = Quaternion.AngleAxis(_moveSpeed, Vector3.forward);
        transform.position = _centre.transform.position + _rotation * _direction ;

    }
    
    private void OnPrepareRound(GameData gameData)
    {
        var starCount = _playersNumber == GameEnum.PlayersNumber.PlayerOne
            ? gameData.RoundInfos[gameData.CurrentRound].FirstPlayer.SelectionItemsСharacteristic.DamageStar[_gameItem]
            : gameData.RoundInfos[gameData.CurrentRound].SecondPlayer.SelectionItemsСharacteristic
                .DamageStar[_gameItem];

        for (int i = 0; i < starCount; i++)
        {
            _stars[i].gameObject.SetActive(true);
        }
    }

    private void ResetData()
    {
        _button.interactable = true;
        
        var color = _button.colors;
        color.disabledColor = _defaultDeactivateColor;
        _button.colors = color;
        
        _stars.ForEach((o => o.SetActive(false)));
    }
}
