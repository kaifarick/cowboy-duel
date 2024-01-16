using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionItemsСharacteristic
{
    
    public Dictionary<GameEnum.GameItem, int> DamageValue;
    public Dictionary<GameEnum.GameItem, int> DamageStar;
    
    public SelectionItemsСharacteristic()
    {
        var presetNum = Random.Range(0, DamagePreset.Count);

        DamageValue = new Dictionary<GameEnum.GameItem, int>();
        DamageStar = new Dictionary<GameEnum.GameItem, int>();
        
        foreach (var item in DamagePreset[presetNum])
        {
            DamageStar.Add(item.Key, item.Value);
            DamageValue.Add(item.Key, DamageConvert(item.Value));
        }
    }

    
    private List<Dictionary<GameEnum.GameItem, int>> DamagePreset = new List<Dictionary<GameEnum.GameItem, int>>()
    {
        new Dictionary<GameEnum.GameItem, int>()
        {
            {GameEnum.GameItem.Rock, 1},
            {GameEnum.GameItem.Paper, 3},
            {GameEnum.GameItem.Scissors, 3},
        },
        new Dictionary<GameEnum.GameItem, int>()
        {
            {GameEnum.GameItem.Rock, 2},
            {GameEnum.GameItem.Paper, 2},
            {GameEnum.GameItem.Scissors, 2},
        },
        new Dictionary<GameEnum.GameItem, int>()
        {
            {GameEnum.GameItem.Rock, 3},
            {GameEnum.GameItem.Paper, 2},
            {GameEnum.GameItem.Scissors, 2},
        },
    };



    private int DamageConvert(int star)
    {
        switch (star)
        {
            case 1:
                return Random.Range(100, 200);
            case 2:
                return Random.Range(200, 300);
            case 3:
                return Random.Range(300, 400);
            
            default: return 0;
        }
    }
}
