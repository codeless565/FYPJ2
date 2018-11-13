using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTDungeon
{
    public int currentFloor;

    private static CTDungeon instance;
    private Dictionary<int, CTFloor> floors;

    public static CTDungeon Instance
    {
        get
        {
            if (instance == null)
                instance = new CTDungeon();
            return instance;
        }
    }

    private CTDungeon()
    {
        floors = new Dictionary<int, CTFloor>();
        currentFloor = -1;
    }

    public void AddNewFloor(int _floorNum, CTFloor _newFloor)
    {
        floors.Add(_floorNum, _newFloor);
    }

    public Dictionary<int, CTFloor> Floors
    {
        get
        {
            return floors;
        }
    }
}