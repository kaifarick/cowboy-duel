using System.Collections.Generic;

public class DataManager
{
    public GameData GameData { get; private set; }

    public void InitializeGameData(int maxRound = 0)
    {
        GameData = new GameData {RoundInfos = new Dictionary<int, GameData.RoundInfo>(), MaxRoundCount = maxRound};
    }

    public void CreateRoundData(int roundNum)
    {
        GameData.CurrentRound = roundNum;

        if (!GameData.RoundInfos.ContainsKey(roundNum))
            GameData.RoundInfos.Add(roundNum, new GameData.RoundInfo()
            {
                FirstPlayer = new GameData.PlayerInfo(),
                SecondPlayer = new GameData.PlayerInfo(),
                WinnerPlayer = new GameData.PlayerInfo()
            });
    }

    public void AddCustomRoundData(int round, string firstName, string secondName)
    {
        GameData.RoundInfos.Add(round, new GameData.RoundInfo { FirstPlayer = new GameData.PlayerInfo(){Name = firstName},
            SecondPlayer = new GameData.PlayerInfo(){Name = secondName }, WinnerPlayer = new GameData.PlayerInfo()});
    }
    

    public void PrepareRoundData(APlayer firstPlayer, APlayer secondPlayer, int roundNum)
    {
        GameData.RoundInfos[roundNum].FirstPlayer.Name = firstPlayer.Name;
        GameData.RoundInfos[roundNum].SecondPlayer.Name = secondPlayer.Name;

        GameData.RoundInfos[roundNum].FirstPlayer.PlayersNumber = firstPlayer.PlayersNumber;
        GameData.RoundInfos[roundNum].SecondPlayer.PlayersNumber = secondPlayer.PlayersNumber;

        GameData.RoundInfos[roundNum].FirstPlayer.IsBot = firstPlayer.IsBot;
        GameData.RoundInfos[roundNum].SecondPlayer.IsBot = secondPlayer.IsBot;

        GameData.RoundInfos[roundNum].FirstPlayer.SelectionItemsСharacteristic = firstPlayer.SelectionItemsСharacteristic;
        GameData.RoundInfos[roundNum].SecondPlayer.SelectionItemsСharacteristic = secondPlayer.SelectionItemsСharacteristic;
    }

    public void EndRoundData(APlayer winnerPlayer)
    {
        GameData.RoundInfos[GameData.CurrentRound].WinnerPlayer.Name = winnerPlayer.Name;
        GameData.RoundInfos[GameData.CurrentRound].WinnerPlayer.PlayersNumber = winnerPlayer.PlayersNumber;
        GameData.RoundInfos[GameData.CurrentRound].WinnerPlayer.IsBot = winnerPlayer.IsBot;
    }

    public void DamageData(GameEnum.PlayersNumber playersNumber, int damage)
    {
        if(playersNumber == GameEnum.PlayersNumber.PlayerOne) GameData.RoundInfos[GameData.CurrentRound].FirstPlayer.DamageDone += damage;
        else GameData.RoundInfos[GameData.CurrentRound].SecondPlayer.DamageDone += damage;
    }
}
