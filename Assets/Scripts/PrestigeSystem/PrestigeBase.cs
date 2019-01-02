using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PrestigeBase {
    //string GetName();
    string PrestigeName
    { get; }
    //CPlayer PlayerInfo
    //{ get; set; }

    void AddPrestigeStats();
    void RemovePrestigeStats();

    void Update();
}
