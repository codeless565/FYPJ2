using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : PrestigeBase
{
    public static string prestigename;
    private CPlayer playerInfo;

    public Metronome(CPlayer _playerinfo)
    {
        playerInfo = _playerinfo;
        prestigename = "Metronome";
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
        playerInfo.GetStats().PlayRate = playerInfo.GetStats().PlayRate * 110 / 100;
    }

    public void RemovePrestigeStats()
    {
        playerInfo.GetStats().PlayRate = playerInfo.GetStats().PlayRate * 100 / 110;
    }

    public void Update()
    {
    }
}
