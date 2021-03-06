﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StairsForward : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.tag == "Player")
        {
            CProgression.Instance.UpdateDungeonProgression();
            CProgression.Instance.UpdatePlayerSave(_other.GetComponent<CPlayer>());

            CTDungeon.Instance.currentFloor += 1;

            int CurrFloor = CTDungeon.Instance.currentFloor;

            //All here are boss or change in floor type
            if (CurrFloor == CTDungeon.Instance.BossFloorRed)
            {
                SceneManager.LoadScene("BossLevel_Red");
                return;
            }

            if (CurrFloor == CTDungeon.Instance.BossFloorRed + 1)
            {
                SceneManager.LoadScene("TownScene");
                return;
            }

            if (CurrFloor == CTDungeon.Instance.BossFloorPink)
            {
                SceneManager.LoadScene("TownScene");
                return;
            }

            if (CurrFloor == CTDungeon.Instance.BossFloorPink + 1)
            {
                //Victory stage? 
                SceneManager.LoadScene("TownScene");
                Debug.Log("VictoryStage Not Implemented uwu");
                return;
            }
            else
                SceneManager.LoadScene("DungeonLevel_Red");
        }
    }
}
