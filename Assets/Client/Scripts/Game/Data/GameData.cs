using System.Collections;
using System.Collections.Generic;

public class GameData
{
    public Dictionary<int, RoundInfo> RoundInfos;
    public int MaxRoundCount;

    public class RoundInfo
    {
        public string FirstPlayerName;
        public string SecondPlayerName;
        public GameEnum.GameItem FirstPlayerItem;
        public GameEnum.GameItem SecondPlayerItem;
        
        public string WinnerName;
        public bool IsBotWin;
    }
    
}
