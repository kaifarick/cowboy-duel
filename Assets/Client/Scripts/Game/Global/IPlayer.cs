using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer
{
    
    public void SetWinState(bool win);
    public void SelectItem(GameEnum.GameItem gameItem);
}
