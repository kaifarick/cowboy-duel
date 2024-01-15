

public class StandartPlayerDamager : IPlayerDamager
{
    public void DamagePlayer(APlayer hittingPlayer, APlayer strikingPlayer)
    {
        hittingPlayer.GetDamage(strikingPlayer.SelectionItemsСharacteristic.DamageValue[strikingPlayer.GameItem]);
    }
}
