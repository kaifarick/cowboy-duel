using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BotPlayer: APlayer
{
    private string[] _names = new[] { "BotJon", "BotBob", "BotAlex", "BotTony" };
    
    public BotPlayer(GameEnum.PlayersNumber playersNumber)
    {
        Name = _names[Random.Range(0, _names.Length)];
        PlayersNumber = playersNumber;
    }
}
