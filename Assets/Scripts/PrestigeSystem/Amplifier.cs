using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amplifier : PrestigeBase
{
    public static string prestigename;
    private CPlayer playerInfo;
    public Amplifier(CPlayer _playerinfo)
    {
        playerInfo = _playerinfo;
        prestigename = "Amplfier";
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
        playerInfo.GetStats().Attack = playerInfo.GetStats().Attack * 110 / 100;
    }

    public void RemovePrestigeStats()
    {
        playerInfo.GetStats().Attack = playerInfo.GetStats().Attack * 100 / 110;

    }

    public void Update()
    {
    }
}
