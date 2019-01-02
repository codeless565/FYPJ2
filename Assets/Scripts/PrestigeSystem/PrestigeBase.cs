using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PrestigeBase {
    //string GetName();

    void AddPrestigeStats(ref CStats _playerStats);
    void RemovePrestigeStats(ref CStats _playerStats);
}
