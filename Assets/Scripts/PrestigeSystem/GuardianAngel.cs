using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianAngel : PrestigeBase
{
    public static string prestigename;
    private CPlayer playerInfo;
    public bool isInvulnerable;
    float timer;

    float InvulnerableTimer;

    public GuardianAngel(CPlayer _playerinfo)
    {
        playerInfo = _playerinfo;
        prestigename = "GuardianAngel";
        isInvulnerable = false;
        timer = 0f;
        InvulnerableTimer = 0f;
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
        if (timer >= 600f && playerInfo.GetStats().HP <= 1)
            isInvulnerable = true;

        if (isInvulnerable)
        {
            InvulnerableTimer += Time.deltaTime;
            if (InvulnerableTimer >= 5f)
                isInvulnerable = false;
        }

    }
}
