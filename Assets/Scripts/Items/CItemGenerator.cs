using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CItemGenerator : MonoBehaviour
{
    [SerializeField]
    int NumberOfItems = 1;

    public void Init()
    {
        List<CTRoom> roomList = CTDungeon.Instance.Floors[CTDungeon.Instance.currentFloor].Rooms;

        int generatedItem = 0;

        foreach (var room in roomList)
        {
            if (generatedItem >= NumberOfItems)
                break;

            int randomAmtInThisRoom = Random.Range(0, 3);
            for (int i = 0; i < randomAmtInThisRoom; ++i)
            {
                GenerateIteminRoom(room.RandomPoint);
            }
        }
    }

    private void GenerateIteminRoom(Vector3 _pos)
    {
        //get a random item from itemdatabase and generate in room
        if (CTDungeon.Instance.currentFloor < 10)
        {
            Object.Instantiate(CItemDatabase.Instance.RandomLowGradeItem, _pos, Quaternion.identity);
        }
        else
        {
            Object.Instantiate(CItemDatabase.Instance.RandomItem, _pos, Quaternion.identity);
        }
    }
}
