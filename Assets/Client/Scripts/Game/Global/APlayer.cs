using System;
using Random = UnityEngine.Random;

public abstract class APlayer
{
    public string Name { get; protected set; }
    public GameEnum.GameItem GameItem { get; protected set; }
    public GameEnum.PlayersNumber PlayersNumber { get; protected set; }
    
    public bool IsWin { get; protected set; }

    public virtual void SetWinState(bool isWin) => IsWin = isWin;
    public virtual void SelectItem(GameEnum.GameItem gameItem)
    {
        if (gameItem != GameEnum.GameItem.None) GameItem = gameItem;
        else
        {
            Type type = typeof(GameEnum.GameItem);
            Array values = type.GetEnumValues();
            int index = Random.Range(0,values.Length - 1);
            GameItem = (GameEnum.GameItem)values.GetValue(index);
            
            //debug
            //GameItem = GameEnum.GameItem.Scissors;
        }
    }
}
