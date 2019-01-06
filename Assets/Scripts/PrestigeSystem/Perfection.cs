using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perfection : PrestigeBase
{
    private CPlayer playerInfo;
    public float timer;
    public static string prestigename;
    public string PrestigeName
    {
        get
        {
            return prestigename;
        }
    }

    public Perfection(CPlayer _playerinfo)
    {
        playerInfo = _playerinfo;
        prestigename = "Perfection";
        timer = 0.0f;
    }

    public void AddPrestigeStats()
    {
    }

    public void RemovePrestigeStats()
    {
    }

    public void Update()
    {
        // reset if player is damaged
        timer += Time.deltaTime ;
        Debug.Log("timer: " + timer);

    }
}
