using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private GameEnum.PlayersNumber _playersNumber;
    [SerializeField] private TextMeshProUGUI _name;

    [Inject] private GamePresenter _gamePresenter;

    private Sequence _prepareSequence;

    private void Start()
    {
        _gamePresenter.OnHitPlayerAction += OnHitPlayer;
        _gamePresenter.OnPrepareRoundAction += OnPrepareRound;
        _gamePresenter.OnEndGameAction += OnEndGame;
    }

    private void OnEndGame()
    {
        _prepareSequence.Kill();
    }


    private void OnPrepareRound(GameData gameData)
    {
        _name.text = _playersNumber == GameEnum.PlayersNumber.PlayerOne
            ? gameData.RoundInfos[gameData.CurrentRound].FirstPlayer.Name
            : gameData.RoundInfos[gameData.CurrentRound].SecondPlayer.Name;
        
        
        _slider.value = _slider.minValue;
        _prepareSequence = DOTween.Sequence();
        _prepareSequence.Append(_slider.DOValue(_slider.maxValue, 2));
    }


    private void OnHitPlayer(GameEnum.PlayersNumber playersNumber, int health)
    {
        if (playersNumber == _playersNumber) _slider.value = health;
    }
}
