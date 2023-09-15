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

    private void Awake()
    {
        _gameManager.OnGameEndAction += OnGameEnd;
    }

    public override void Initialize(APlayer winPlayer, APlayer firstPlayer, APlayer secondPlayer, int roundNum, GameEnum.RoundResult gameResult, object gameplayInfo)
    {
        base.Initialize(winPlayer, firstPlayer, secondPlayer,roundNum,gameResult, gameplayInfo);
        
        _resumeButton.onClick.AddListener(() =>
        {
            if(winPlayer is Player || gameResult == GameEnum.RoundResult.Draw) _gameManager.StartGame(_gameManager.AGameplay);
            else  _gameManager.GameEnd();;
        });

        _roundText.text = $"Round: {roundNum}";
        
        AddResult(secondPlayer);
        
    }

    private void AddResult(APlayer opponent)
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
        
        currentElement.Initialize($"{opponent.Name} {opponent.GameItem}");
    }

    private void OnGameEnd()
    {
        foreach (var element in _resultsElements)
        {
            element.gameObject.SetActive(false);
        }
    }
}
