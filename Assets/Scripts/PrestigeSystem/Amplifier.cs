using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amplifer : PrestigeBase
{
    public Amplifer()
    { }


    public void AddPrestigeStats(ref CStats _playerStats)
    {
        _playerStats.Attack = _playerStats.Attack * 110 / 100;
    }

    public void RemovePrestigeStats(ref CStats _playerStats)
    {
        _playerStats.Attack = _playerStats.Attack * 100 / 110;

    }
}
