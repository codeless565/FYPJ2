using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMonsterGenerator : MonoBehaviour
{
    [SerializeField]
    int NumberOfMonsters = 0;

    public void Init()
    {
        List<CTRoom> roomList = CTDungeon.Instance.Floors[CTDungeon.Instance.currentFloor].Rooms;

        if (NumberOfMonsters < 0)
        {
            int currFloor = CTDungeon.Instance.currentFloor;
            NumberOfMonsters = Random.Range(roomList.Count , currFloor + roomList.Count);
        }

        int generatedMobs = 0;

        foreach (var room in roomList)
        {
            if (generatedMobs >= NumberOfMonsters)
                break;

            int randomAmtInThisRoom = Random.Range(0, 3);
            for (int i = 0; i < randomAmtInThisRoom; ++i)
            {
                GenerateinRoom(room);
            }
        }
    }

    private void GenerateinRoom(CTRoom _Room)
    {
        GameObject Monster;

        //get a random item from itemdatabase and generate in room
        if (CTDungeon.Instance.currentFloor < 5)
        {
            Monster = Object.Instantiate(CMonsterDatabase.Instance.EnemyNoise, _Room.RandomPoint, Quaternion.identity);
        }
        else if (CTDungeon.Instance.currentFloor < 10)
        {
            if (Random.Range(0.0f, 1.0f) >= 0.5f)
                Monster = Object.Instantiate(CMonsterDatabase.Instance.EnemyNoise, _Room.RandomPoint, Quaternion.identity);
            else
                Monster = Object.Instantiate(CMonsterDatabase.Instance.EnemyStatic, _Room.RandomPoint, Quaternion.identity);
        }
        else if (CTDungeon.Instance.currentFloor < 15)
        {
            Monster = Object.Instantiate(CMonsterDatabase.Instance.RandomLowLevelMonster, _Room.RandomPoint, Quaternion.identity);
        }
        else
        {
            Monster = Object.Instantiate(CMonsterDatabase.Instance.RandomMonster, _Room.RandomPoint, Quaternion.identity);
        }

        Monster.GetComponent<IEnemy>().Init(_Room.coordinate);
    }
}
