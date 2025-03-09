using System;
using Random = UnityEngine.Random;

public abstract class APlayer
{
    public string Name { get; protected set; }
    public bool IsBot   { get; protected set; }
    public GameEnum.GameItem GameItem { get; protected set; } = GameEnum.GameItem.None;
    public GameEnum.PlayersNumber PlayersNumber { get; protected set; }
    public int Health { get; private set; } = 1000;
    public SelectionItemsСharacteristic SelectionItemsСharacteristic { get; private set; }
    public bool IsPlayerMadeNove => GameItem != GameEnum.GameItem.None;

    public APlayer()
    {
        SelectionItemsСharacteristic = new SelectionItemsСharacteristic();
    }

    public virtual void SelectItem(GameEnum.GameItem gameItem)
    {
        if (gameItem != GameEnum.GameItem.None) GameItem = gameItem;
        
        else
        {
            Type type = typeof(GameEnum.GameItem);
            Array values = type.GetEnumValues();
            //1 - None enum
            int index = Random.Range(1,values.Length);
            GameItem = (GameEnum.GameItem)values.GetValue(index);
            
            //debug
            //GameItem = GameEnum.GameItem.Scissors;
        }
    }

    public void ResetData()
    {
        GameItem = GameEnum.GameItem.None;
        if(IsBot) SelectItem(GameEnum.GameItem.None);
    }

    public void GetDamage(int damage)
    {
        Health -= damage;
    }
}
