using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDungeon
{
    public int currentFloor;

    private static CDungeon instance;
    private Dictionary<int, CFloor> floors;

    public static CDungeon Instance
    {
        get
        {
            if (instance == null)
                instance = new CDungeon();
            return instance;
        }
    }

    private CDungeon()
    {
        floors = new Dictionary<int, CFloor>();
        currentFloor = -1;
    }

    public void AddNewFloor(int _floorNum, CFloor _newFloor)
    {
        floors.Add(_floorNum, _newFloor);
    }

    public Dictionary<int, CFloor> Floors
    {
        get
        {
            return floors;
        }
    }
}
