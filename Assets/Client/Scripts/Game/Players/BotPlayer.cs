using Random = UnityEngine.Random;

public class BotPlayer: APlayer
{
    private string[] _names = new[] { "Bot Jon", "Bot Bob", "Bot Alex", "Bot Tony", "Bot Max" };
    
    public BotPlayer(GameEnum.PlayersNumber playersNumber)
    {
        Name = _names[Random.Range(0, _names.Length)];
        PlayersNumber = playersNumber;
        
        SelectItem(GameEnum.GameItem.None);
    }
}
