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
    
    
    public event Action<Action<GameEnum.PrepareGameplayPoint>> OnCheckPreparePointsAction;
    


    private Dictionary<GameEnum.PrepareGameplayPoint, bool> _prepareGameplayPoints =
        new Dictionary<GameEnum.PrepareGameplayPoint, bool>()
        {
            {GameEnum.PrepareGameplayPoint.Animations, false}
        };
    
    [Inject] private GamePresenter _gamePresenter;


    #region public
    
    public void StartGame(AGameplay gameplay)
    {
        _gameplay = gameplay;
        _gamePresenter.InitializeGameplay(gameplay);
        
        gameplay.OnGameStartAction += OnGameStart;
        gameplay.OnGameEndAction += OnGameEnd;
        
        gameplay.OnPrepareRoundAction += OnPrepareRound;

        _gameplay.StartGame();
        
    }
    

    public void PrepareGameRound()
    {
        _gameplay.PrepareGameRound();
    }

    public void GameEnd()
    {
        _gameplay.EndGame();
    }
    
    #endregion
    
    
    #region private
    


    private void CheckPreparePoints(GameEnum.PrepareGameplayPoint prepareGameplayPoint)
    {
        //if already ready
        if(_prepareGameplayPoints[prepareGameplayPoint]) return;
        
        _prepareGameplayPoints[prepareGameplayPoint] = true;
        if (!_prepareGameplayPoints.ContainsValue(false))
        {
            _gameplay.StartRound();
        }
    }
    
    private void ResetPrepareData()
    {
        foreach (var point in _prepareGameplayPoints.Keys.ToList())
        {
            _prepareGameplayPoints[point] = false;
        }
    }
    
    private void OnGameStart()
    {
        OnGameStartAction?.Invoke();
        
        PrepareGameRound();
    }


    private void OnGameEnd()
    {
        ResetPrepareData();
        
        _gameplay.OnGameEndAction -= OnGameEnd;
        _gameplay.OnPrepareRoundAction -= OnPrepareRound;
        _gameplay.OnGameStartAction -= OnGameStart;
        
        OnGameEndAction?.Invoke();
    }
    

    private void OnPrepareRound()
    {
        ResetPrepareData();
        OnCheckPreparePointsAction?.Invoke(CheckPreparePoints);
    }
    
    #endregion
}
