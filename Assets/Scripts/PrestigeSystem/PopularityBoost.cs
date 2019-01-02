using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopularityBoost : PrestigeBase
{
    private CPlayer playerInfo;
    public static string prestigename;
    public PopularityBoost(CPlayer _playerinfo)
    {
        playerInfo = _playerinfo;
        prestigename = "PopularityBoost";
    }

    public string PrestigeName
    {
        get
        {
            return prestigename;
        }
    }

    public void AddPrestigeStats()
    {
        playerInfo.GetStats().EXPBoost *= 2;
    }

    public void RemovePrestigeStats()
    {
        playerInfo.GetStats().EXPBoost /= 2;
    }

    public void Update()
    {
        
    }
}
