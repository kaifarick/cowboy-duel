using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;


public abstract class AWinWindow : BaseWindow
{
    [SerializeField] protected TextMeshProUGUI _winPlayerTxt;
    [SerializeField] protected TextMeshProUGUI _playerOneItemTxt;
    [SerializeField] protected TextMeshProUGUI _playerTwoItemTxt;
    [SerializeField] protected Button _resumeButton;

    [Inject] protected GameManager _gameManager;
    

    public virtual void Initialize(APlayer winPlayer, APlayer firstPlayer, APlayer secondPlayer, int roundNum, GameEnum.RoundResult gameResult, object gameplayInfo)
    {
        string winTxt = gameResult == GameEnum.RoundResult.Draw ? gameResult.ToString() : $"{winPlayer.Name} win!";
        _winPlayerTxt.text = winTxt;
        
        _playerOneItemTxt.text = $"{firstPlayer.Name} {firstPlayer.GameItem}";
        _playerTwoItemTxt.text = $"{secondPlayer.Name} {secondPlayer.GameItem}";
        
        _resumeButton.onClick.AddListener(Hide);
        
    }
    
    
    public override void Hide()
    {
        _resumeButton.onClick.RemoveAllListeners();
        base.Hide();
    }
}
