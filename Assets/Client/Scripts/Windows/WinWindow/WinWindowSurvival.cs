using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

public class WinWindowSurvival : AWinWindow
{
    [SerializeField] private GridLayoutGroup _gridLayoutGroup;
    [SerializeField] private ResultsElement _resultsElementPrefab;
    [SerializeField] private TextMeshProUGUI _roundText;

    private List<ResultsElement> _resultsElements = new List<ResultsElement>();

    private void Start()
    {
        _gameManager.OnGameEndAction += OnGameEnd;
    }

    public override void Initialize(GameData gameData, GameEnum.RoundResult roundResult, int roundNum)
    {
        base.Initialize(gameData, roundResult, roundNum);
        
        _resumeButton.onClick.AddListener(() =>
        {
            if(!gameData.RoundInfos[roundNum].IsBotWin || roundResult == GameEnum.RoundResult.Draw) _gameManager.StartRound();
            else  _gameManager.GameEnd();;
        });

        _roundText.text = $"Round: {roundNum}";
        
        AddOpponentResult(gameData.RoundInfos[roundNum]);
        
    }

    private void AddOpponentResult(GameData.RoundInfo roundInfo)
    {
        if(_resultsElements.Count >= 15 && _resultsElements.TrueForAll((element => element.isActiveAndEnabled)))
            _resultsElements.ForEach((element => element.gameObject.SetActive(false)));
        
        
        ResultsElement currentElement;
        if (_resultsElements.Exists((element => !element.isActiveAndEnabled)))
            currentElement = _resultsElements.FirstOrDefault((element => !element.isActiveAndEnabled));
        else
        {
            currentElement = Instantiate(_resultsElementPrefab,_gridLayoutGroup.transform);
            _resultsElements.Add(currentElement);
        } 
        
        
        currentElement.Initialize($"{roundInfo.SecondPlayerName} {roundInfo.SecondPlayerItem}");
    }

    private void OnGameEnd()
    {
        foreach (var element in _resultsElements)
        {
            element.gameObject.SetActive(false);
        }
    }
}
