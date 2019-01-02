using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseCanceller : PrestigeBase
{
    private CPlayer playerInfo;
    public static string prestigename;
    public NoiseCanceller(CPlayer _playerinfo)
    {
        playerInfo = _playerinfo;
        prestigename = "NoiseCanceller";
    }

    public string PrestigeName
    {
        get
        {
            return prestigename;
        }
    }

    //public CPlayer PlayerInfo
    //{
    //    get
    //    {
    //        return playerInfo;
    //    }

    //    set
    //    {
    //        playerInfo = value;
    //    }
    //}

    public void AddPrestigeStats()
    {
        playerInfo.GetStats().Defense = playerInfo.GetStats().Defense * 110 / 100;
    }

    public void RemovePrestigeStats()
    {
        playerInfo.GetStats().Defense = playerInfo.GetStats().Defense * 100 / 110;

    }

    public void Update()
    {
    }
}
