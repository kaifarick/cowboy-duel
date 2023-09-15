using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampionshipInfo
{
    public Dictionary<int, RoundInfo> RoundInfos;

    public class RoundInfo
    {
        public string PlayerOneName;
        public string PlayerTwoName;
        public string WinnerName;
    }
}
