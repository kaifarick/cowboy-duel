using System.Collections.Generic;

public class GameData
{
    public Dictionary<int, RoundInfo> RoundInfos;

    public int CurrentRound;
    public int MaxRoundCount;

    public class RoundInfo
    {
        public PlayerInfo FirstPlayer;
        public PlayerInfo SecondPlayer;
        
        public PlayerInfo WinnerPlayer;
    }
    
    public class PlayerInfo
    {
        public string Name;
        public bool IsBot;
        
        public GameEnum.GameItem GameItem;
        public GameEnum.PlayersNumber PlayersNumber;
    }
    
}
