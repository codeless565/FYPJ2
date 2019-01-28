using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBossGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject BossRed;
    [SerializeField]
    GameObject BossPink;

    public void Init()
    {
        CTRoom BossRoom = CTDungeon.Instance.Floors[CTDungeon.Instance.currentFloor].Rooms[1];

        GenerateinRoom(BossRoom);
    }

    private void GenerateinRoom(CTRoom _Room)
    {
        GameObject Monster;

        if (CTDungeon.Instance.currentFloor == 25)
        {
            Monster = Instantiate(BossRed, _Room.CenterPoint, Quaternion.identity);
            Monster.GetComponent<IEnemy>().Init(_Room.coordinate);
        }
        else if (CTDungeon.Instance.currentFloor == 50)
        {
            Monster = Instantiate(BossPink, _Room.CenterPoint, Quaternion.identity);
            Monster.GetComponent<IEnemy>().Init(_Room.coordinate);
        }
    }
}
