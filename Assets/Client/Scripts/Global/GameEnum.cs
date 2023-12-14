using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnum : MonoBehaviour
{
    public enum GameItem
    {
        Rock,
        Paper,
        Scissors,
        None
    }

    public enum RoundResult
    {
        None,
        PlayerOneWin,
        PlayerTwoWin,
        Draw
    }
    
    public enum PlayersNumber
    {
        PlayerOne,
        PlayerTwo
    }

    public enum GameplayType
    {
        OnePlayer,
        TwoPlayers
    }
    
}
