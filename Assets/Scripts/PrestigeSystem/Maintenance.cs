using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maintenance : PrestigeBase
{
    public static string prestigename;
    private CPlayer playerInfo;
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

    public Maintenance(CPlayer _player)
    {
        prestigename = "Maintenance";
        playerInfo = _player;
    }    

    public void AddPrestigeStats()
    {
        playerInfo.GetStats().HP = playerInfo.GetStats().HP * 110 / 100;
        playerInfo.GetStats().MaxHP = playerInfo.GetStats().MaxHP * 110 / 100;
    }

    public void RemovePrestigeStats()
    {
        playerInfo.GetStats().HP = playerInfo.GetStats().HP * 100 / 110;
        playerInfo.GetStats().MaxHP = playerInfo.GetStats().MaxHP * 100 / 110;

    }

    public void Update()
    {
    }
}
