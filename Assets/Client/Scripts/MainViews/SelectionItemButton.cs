
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

    private Transform _centre;
    private float _moveSpeed;
    private Vector3 _direction;
    private Quaternion _rotation;



    public void Initialize(GameView gameView, Transform centre, float moveSpeed, Action<GameEnum.GameItem> onButtonClick)
    {
        _centre = centre;
        _moveSpeed = moveSpeed;
        
        gameView.OnGameEndAction += ResetData;
        
        _button.onClick.AddListener((() => onButtonClick?.Invoke(_gameItem)));
    }

    private void Update()
    {
        _direction = transform.position - _centre.position;
        _rotation = Quaternion.AngleAxis(_moveSpeed, Vector3.forward);
        transform.position = _centre.transform.position + _rotation * _direction ;

    }

    private void ResetData()
    {
        _button.interactable = true;
        
        var color = _button.colors;
        color.disabledColor = _defaultDeactivateColor;
        _button.colors = color;
    }
}
