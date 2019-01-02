using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : PrestigeBase
{
    public Metronome()
    { }


    public void AddPrestigeStats(ref CStats _playerStats)
    {
        _playerStats.PlayRate = _playerStats.PlayRate * 110 / 100;
    }

    public void RemovePrestigeStats(ref CStats _playerStats)
    {
        _playerStats.PlayRate = _playerStats.PlayRate * 100 / 110;

    }
}
