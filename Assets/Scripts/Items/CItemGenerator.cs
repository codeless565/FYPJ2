using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CItemGenerator
{
    public CItemGenerator()
    {
    }

    public void GenerateItemsOnFloor(int _numItems = 1)
    {
        List<CTRoom> roomList = CTDungeon.Instance.Floors[CTDungeon.Instance.currentFloor].Rooms;

        int amtOfItems = _numItems;

        int generatedItem = 0;

        foreach (var room in roomList)
        {
            if (generatedItem >= amtOfItems)
                break;

            int randomAmtInThisRoom = Random.Range(0, 3);
            for (int i = 0; i < randomAmtInThisRoom; ++i)
            {
                GenerateIteminRoom(room);
            }
        }
    }

    private void GenerateIteminRoom(CTRoom _room)
    {
        float xPos = Random.Range(_room.xPos, _room.xPos + _room.roomWidth);
        float yPos = Random.Range(_room.yPos, _room.yPos + _room.roomHeight);

        //get a random item from itemdatabase and generate in room
        if (CTDungeon.Instance.currentFloor < 10)
        {
            Object.Instantiate(CItemDatabase.Instance.RandomLowGradeItem, new Vector3(xPos, yPos, 0f), Quaternion.identity);
        }
        else
        {
            Object.Instantiate(CItemDatabase.Instance.RandomItem, new Vector3(xPos, yPos, 0f), Quaternion.identity);
        }
    }
}
