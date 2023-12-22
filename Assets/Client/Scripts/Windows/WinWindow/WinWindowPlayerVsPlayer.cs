using System;
using System.Collections.Generic;

public class WinWindowPlayerVsPlayer : AWinWindow
{
    
    
    public override void Initialize(GameData gameData, GameEnum.RoundResult roundResult, int roundNum)
    {
        base.Initialize(gameData, roundResult, roundNum);
        
        _resumeButton.onClick.AddListener((() =>  _gameManager.GameEnd()));
    }
}
