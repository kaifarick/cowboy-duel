
public class DebugPlayerDamager : IPlayerDamager
{
    public void DamagePlayer(APlayer hittingPlayer, APlayer strikingPlayer)
    {
        hittingPlayer.GetDamage(100);
    }
}
