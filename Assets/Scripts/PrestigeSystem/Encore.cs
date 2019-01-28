using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encore : PrestigeBase
{
    public static string prestigename;
    private CPlayer playerInfo;
    float timer;
    float activetimer;
    public bool isActive;


    public Encore(CPlayer _playerinfo)
    {
        playerInfo = _playerinfo;
        prestigename = "Encore";
        timer = 0f;
        activetimer = 0f;
        isActive = false;
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
    }

    public void RemovePrestigeStats()
    {
    }

    public void Update()
    {
        timer += Time.deltaTime;

        if (playerInfo.GetStats().HP <= (0.05 * playerInfo.GetStats().MaxHP) && timer >= 300f)
                isActive = true;

        if (isActive)
        {
            activetimer += Time.deltaTime;
            if(activetimer >= 10f)
            {
                timer = 0f;
                activetimer = 0f;
                isActive = false;
            }
        }
    }
}
