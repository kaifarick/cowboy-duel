using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class CowboySounds : MonoBehaviour
{
    [SerializeField] private AudioClip _shotSound;

    [Inject] private GamePresenter _gamePresenter;
    [Inject] private AudioManager _audioManager;

    private Sequence _shotSequence;

    private void Start()
    {
        _gamePresenter.OnHitPlayerAction += OnHitPlayer;
        _gamePresenter.OnEndGameAction += OnEndGame;
    }

    private void OnEndGame()
    {
        _shotSequence.Kill();
    }

    private void OnHitPlayer(GameEnum.PlayersNumber playersNumber, int currentHealth, int damage)
    {
        float shootWaitTime = 0.4f;
        _shotSequence = DOTween.Sequence();
        _shotSequence.AppendInterval(shootWaitTime).AppendCallback(() => _audioManager.PlaySound(_shotSound));
    }
}
