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
    [SerializeField] private TextMeshProUGUI _damageText;

    [Inject] private GamePresenter _gamePresenter;
    [Inject] private DataManager _dataManager;

    private Sequence _prepareSequence;
    private Sequence _hitSequence;

    private void Start()
    {
        _gamePresenter.OnHitPlayerAction += OnHitPlayer;
        _gamePresenter.OnPrepareRoundAction += OnPrepareRound;
        _gamePresenter.OnEndGameAction += OnEndGame;
        
        _damageText.alpha = 0;
    }

    private void OnEndGame()
    {
        _prepareSequence.Kill();
        _hitSequence.Kill();
    }


    private void OnPrepareRound()
    {
        _name.text = _playersNumber == GameEnum.PlayersNumber.PlayerOne
            ? _dataManager.GameData.RoundInfos[_dataManager.GameData.CurrentRound].FirstPlayer.Name
            : _dataManager.GameData.RoundInfos[_dataManager.GameData.CurrentRound].SecondPlayer.Name;
        
        
        _slider.value = _slider.minValue;
        _prepareSequence = DOTween.Sequence();
        _prepareSequence.Append(_slider.DOValue(_slider.maxValue, 2));
    }


    private void OnHitPlayer(GameEnum.PlayersNumber playersNumber, int currentHealth, int damage)
    {
        if (playersNumber == _playersNumber)
        {
            _damageText.text = $"-{damage}";
            
            _hitSequence = DOTween.Sequence();
            _hitSequence.PrependInterval(0.4f);
            _hitSequence.Append(_damageText.DOFade(1f, 0.1f));
            _hitSequence.Append(_slider.DOValue(currentHealth, 2));
            _hitSequence.AppendInterval(1.5f);
            _hitSequence.AppendCallback((() => _damageText.DOFade(0f, 0.1f)));
        }
    }
}
