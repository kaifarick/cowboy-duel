using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class WinWindowPlayerVsComputer : AWinWindow
{

    public override void Initialize(APlayer winPlayer, APlayer firstPlayer, APlayer secondPlayer, int roundNum, GameEnum.RoundResult gameResult,object gameplayInfo)
    {
        
        base.Initialize(winPlayer, firstPlayer, secondPlayer,roundNum,gameResult, gameplayInfo);
        
        _resumeButton.onClick.AddListener((() =>  _gameManager.GameEnd()));
    }
}
