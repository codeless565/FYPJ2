using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseCanceller : PrestigeBase
{
    public NoiseCanceller()
    { }


    public void AddPrestigeStats(ref CStats _playerStats)
    {
        _playerStats.Defense = _playerStats.Defense * 110 / 100;
    }

    public void RemovePrestigeStats(ref CStats _playerStats)
    {
        _playerStats.Defense = _playerStats.Defense * 100 / 110;

    }
}
