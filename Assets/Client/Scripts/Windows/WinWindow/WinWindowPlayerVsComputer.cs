using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class WinWindowPlayerVsComputer : AWinWindow
{

    public override void Initialize(GameData gameData, GameEnum.RoundResult roundResult, int roundNum)
    {

        base.Initialize(gameData, roundResult, roundNum);
        
        _resumeButton.onClick.AddListener((() =>  _gameManager.GameEnd()));
    }
}
