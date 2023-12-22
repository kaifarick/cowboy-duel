

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WinWindowChampionship : AWinWindow
{

    [SerializeField] private List<BracketElement> _bracketElements;
    
    
    private void Start()
    {
        _gameManager.OnGameEndAction += OnGameEnd;
    }
    
    public override void Initialize(GameData gameData, GameEnum.RoundResult roundResult, int roundNum)
    {
        base.Initialize(gameData, roundResult,roundNum);

        _resumeButton.onClick.AddListener(() =>
        {
            if (roundNum < gameData.MaxRoundCount || roundResult == GameEnum.RoundResult.Draw) _gameManager.PrepareGameRound();
            else _gameManager.GameEnd();;
        });

        BracketElement winElement = null;
        if (roundNum == 1)
        {
            List<BracketElement> semifinalLeft = _bracketElements.FindAll((element =>  element.Stage == BracketElement.TournamentStage.Semifinal && element.Side == BracketElement.TournamentSide.Left));
            List<BracketElement> semifinalRight = _bracketElements.FindAll((element =>  element.Stage == BracketElement.TournamentStage.Semifinal && element.Side == BracketElement.TournamentSide.Right));

            semifinalLeft[0].Initialize(gameData.RoundInfos[1].FirstPlayer.Name);
            semifinalLeft[1].Initialize(gameData.RoundInfos[1].SecondPlayer.Name);
            
            semifinalRight[0].Initialize(gameData.RoundInfos[2].FirstPlayer.Name);
            semifinalRight[1].Initialize(gameData.RoundInfos[2].SecondPlayer.Name);
            
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
        
        if(roundResult!= GameEnum.RoundResult.Draw) winElement.Initialize(gameData.RoundInfos[roundNum].WinnerPlayer.Name);
    }

    private void OnGameEnd()
    {
        _bracketElements.ForEach(element => element.Clear());
    }
}
