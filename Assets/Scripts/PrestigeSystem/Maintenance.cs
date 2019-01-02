using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maintenance : PrestigeBase
{
    public Maintenance()
    { }


    public void AddPrestigeStats(ref CStats _playerStats)
    {
        _playerStats.HP = _playerStats.HP * 110 / 100;
        _playerStats.MaxHP = _playerStats.MaxHP * 110 / 100;
    }

    public void RemovePrestigeStats(ref CStats _playerStats)
    {
        _playerStats.HP = _playerStats.HP * 100 / 110;
        _playerStats.MaxHP = _playerStats.MaxHP * 100 / 110;

    }
}
