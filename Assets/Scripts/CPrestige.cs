using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CPrestige {
    PRESTIGELEVEL GetPretige();
    void SetPrestige(PRESTIGELEVEL _prestige);
    void IncreasePrestige();
}
public enum PRESTIGELEVEL
{
    MAINTENANCE,
    METRONOME,
    AMPLIFIER,
    NOICECANCELLER,
    POPULARITY,
    PERFECTION,
    ENCORE,
    GUARDIANANGEL,
    EUPHORIA
}
