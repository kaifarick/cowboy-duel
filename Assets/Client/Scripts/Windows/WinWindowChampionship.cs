

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WinWindowChampionship : AWinWindow
{

    [SerializeField] private List<BracketElement> _bracketElements;
    
    private ChampionshipInfo _championshipInfo;
    
    private void Awake()
    {
        _gameManager.OnGameEndAction += OnGameEnd;
    }
    
    public override void Initialize(APlayer winPlayer, APlayer firstPlayer, APlayer secondPlayer, int roundNum, GameEnum.RoundResult gameResult, object gameplayInfo)
    {
        base.Initialize(winPlayer, firstPlayer, secondPlayer,roundNum,gameResult, gameplayInfo);
        
        _championshipInfo = gameplayInfo as ChampionshipInfo;
        
        _resumeButton.onClick.AddListener(() =>
        {
            if(roundNum <= _championshipInfo.RoundInfos.Count || gameResult == GameEnum.RoundResult.Draw) _gameManager.StartGame(_gameManager.AGameplay);
            else _gameManager.GameEnd();;
        });
        
        BracketElement winElement = new BracketElement();
        if (roundNum == 1)
        {
            List<BracketElement> semifinalLeft = _bracketElements.FindAll((element =>  element.Stage == BracketElement.TournamentStage.Semifinal && element.Side == BracketElement.TournamentSide.Left));
            List<BracketElement> semifinalRight = _bracketElements.FindAll((element =>  element.Stage == BracketElement.TournamentStage.Semifinal && element.Side == BracketElement.TournamentSide.Right));

            semifinalLeft[0].Initialize(_championshipInfo.RoundInfos[1].PlayerOneName);
            semifinalLeft[1].Initialize(_championshipInfo.RoundInfos[1].PlayerTwoName);
            
            semifinalRight[0].Initialize(_championshipInfo.RoundInfos[2].PlayerOneName);
            semifinalRight[1].Initialize(_championshipInfo.RoundInfos[2].PlayerTwoName);
            
            winElement = _bracketElements.FirstOrDefault((element => element.Side == BracketElement.TournamentSide.Left && element.Stage == BracketElement.TournamentStage.Final));
        }
        else if (roundNum == 2)
        {
            winElement = _bracketElements.FirstOrDefault((element => element.Side == BracketElement.TournamentSide.Right && element.Stage == BracketElement.TournamentStage.Final));
        }
        else if (roundNum == 3)
        {
            winElement = _bracketElements.FirstOrDefault((element => element.Stage == BracketElement.TournamentStage.Winner));
        }
        
        if(gameResult!= GameEnum.RoundResult.Draw) winElement.Initialize(winPlayer.Name);
    }

    private void OnGameEnd()
    {
        _bracketElements.ForEach((element => element.Clear()));
    }
}
