using System;
using System.Collections.Generic;

public class WinWindowPlayerVsPlayer : AWinWindow
{
    
    
    public override void Initialize(APlayer winPlayer, APlayer firstPlayer, APlayer secondPlayer, int roundNum, GameEnum.RoundResult gameResult, object gameplayInfo)
    {
        base.Initialize(winPlayer, firstPlayer, secondPlayer, roundNum,gameResult, gameplayInfo);
        
        _resumeButton.onClick.AddListener((() =>  _gameManager.GameEnd()));
    }
}
