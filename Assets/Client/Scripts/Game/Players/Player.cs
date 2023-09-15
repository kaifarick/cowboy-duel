

public class Player : APlayer
{

    public Player(string name, GameEnum.PlayersNumber playersNumber)
    {
        Name = name;
        PlayersNumber = playersNumber;
    }

    public override void SelectItem(GameEnum.GameItem gameItem)
    {
        base.SelectItem(gameItem);
    }
}
