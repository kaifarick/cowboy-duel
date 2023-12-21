using UnityEngine;

public class GameEnum : MonoBehaviour
{
    public enum GameItem
    {
        None,
        Rock,
        Paper,
        Scissors
    }

    public enum RoundResult
    {
        None,
        PlayerWin,
        BotWin,
        Draw
    }
    
    public enum PlayersNumber
    {
        None,
        PlayerOne,
        PlayerTwo
    }

    public enum GameplayType
    {
        OnePlayer,
        TwoPlayers
    }
    
}
