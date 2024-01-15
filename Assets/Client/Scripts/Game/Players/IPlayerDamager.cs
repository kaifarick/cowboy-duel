

public interface IPlayerDamager
{

    public void DamagePlayer(APlayer hittingPlayer, APlayer strikingPlayer);
    
    public APlayer GetHittingPlayer(APlayer firstPlayer, APlayer secondPlayer)
    {
        if (firstPlayer.GameItem == GameEnum.GameItem.Rock)
        {
            switch (secondPlayer.GameItem)
            {
                case GameEnum.GameItem.Rock:
                    return null;
                case GameEnum.GameItem.Paper:
                    return firstPlayer;
                case GameEnum.GameItem.Scissors:
                    return secondPlayer;
            }
        }

        if (firstPlayer?.GameItem == GameEnum.GameItem.Paper)
        {
            switch (secondPlayer.GameItem)
            {
                case GameEnum.GameItem.Rock:
                    return secondPlayer;
                case GameEnum.GameItem.Paper:
                    return null;
                case GameEnum.GameItem.Scissors:
                    return firstPlayer;
            }
        }

        if (firstPlayer.GameItem == GameEnum.GameItem.Scissors)
        {
            switch (secondPlayer?.GameItem)
            {
                case GameEnum.GameItem.Rock:
                    return firstPlayer;
                case GameEnum.GameItem.Paper:
                    return secondPlayer;
                case GameEnum.GameItem.Scissors:
                    return null;
            }
        }

        return null;
    }
    
    
    public APlayer GetStrikingPlayer(APlayer firstPlayer, APlayer secondPlayer)
    {
        if (firstPlayer.GameItem == GameEnum.GameItem.Rock)
        {
            switch (secondPlayer.GameItem)
            {
                case GameEnum.GameItem.Rock:
                    return null;
                case GameEnum.GameItem.Paper:
                    return secondPlayer;
                case GameEnum.GameItem.Scissors:
                    return firstPlayer;
            }
        }

        if (firstPlayer?.GameItem == GameEnum.GameItem.Paper)
        {
            switch (secondPlayer.GameItem)
            {
                case GameEnum.GameItem.Rock:
                    return firstPlayer;
                case GameEnum.GameItem.Paper:
                    return null;
                case GameEnum.GameItem.Scissors:
                    return secondPlayer;
            }
        }

        if (firstPlayer.GameItem == GameEnum.GameItem.Scissors)
        {
            switch (secondPlayer?.GameItem)
            {
                case GameEnum.GameItem.Rock:
                    return secondPlayer;
                case GameEnum.GameItem.Paper:
                    return firstPlayer;
                case GameEnum.GameItem.Scissors:
                    return null;
            }
        }

        return null;
    }
}
