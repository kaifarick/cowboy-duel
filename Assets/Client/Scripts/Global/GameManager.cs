using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    private AGameplay _gameplay;

    public event Action OnGameEndAction;
    public event Action OnGameStartAction;
    public event Action<GameEnum.GameplayType> OnRoundStartAction;
    public event Action<Action<GameEnum.PrepareGameplayPoint>> OnPrepareRoundAction;


    private Dictionary<GameEnum.PrepareGameplayPoint, bool> _prepareGameplayPoints =
        new Dictionary<GameEnum.PrepareGameplayPoint, bool>()
        {
            {GameEnum.PrepareGameplayPoint.Animations, false}
        };
    
    [Inject] private GamePresenter _gamePresenter;


    public void StartGame(AGameplay gameplay)
    {
        _gameplay = gameplay;
        _gamePresenter.InitializeGameplay(gameplay);
        
        gameplay.OnRoundStartAction += OnRoundStart;
        gameplay.OnGameEndAction += OnGameEnd;
        gameplay.OnEndRoundAction += OnRoundEnd;

        OnGameStartAction?.Invoke();
        
        PrepareGameRound();
    }
    
    public void PrepareGameRound()
    {
        OnPrepareRoundAction?.Invoke(OnPrepareRound);
    }


    public void GameEnd()
    {
        _gameplay.EndGame();
    }
    
    private void StartRound()
    {
        _gameplay.StartRound();
    }
    
    private void ResetData()
    {
        foreach (var point in _prepareGameplayPoints.Keys.ToList())
        {
            _prepareGameplayPoints[point] = false;
        }
    }

    private void OnPrepareRound(GameEnum.PrepareGameplayPoint prepareGameplayPoint)
    {
        if(_prepareGameplayPoints[prepareGameplayPoint]) return;
        
        _prepareGameplayPoints[prepareGameplayPoint] = true;
        if (!_prepareGameplayPoints.ContainsValue(false))
        {
            StartRound();
        }
    }


    private void OnGameEnd()
    {
        ResetData();
        
        _gameplay.OnRoundStartAction -= OnRoundStart;
        _gameplay.OnGameEndAction -= OnGameEnd;
        
        OnGameEndAction?.Invoke();
    }

    private void OnRoundEnd(GameData gameData, GameEnum.RoundResult roundResult, int roundNum)
    {
        ResetData();
    }

    private void OnRoundStart(GameEnum.GameplayType gameplayType)
    {
        OnRoundStartAction?.Invoke(gameplayType);
    }
}
