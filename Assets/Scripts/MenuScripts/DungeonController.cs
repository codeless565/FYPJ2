﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DungeonController : MonoBehaviour
{
    int currentFloorSelection = 1;

    private void Start()
    {
        GetComponentInChildren<Text>().text = "1";
    }

    public void IncreaseFloorSelection()
    {
        if (currentFloorSelection >= CProgression.Instance.GetMaxDungeonProgression())
            return;
        currentFloorSelection += 1;

        GetComponentInChildren<Text>().text = currentFloorSelection.ToString();
    }

    public void DecreaseFloorSelection()
    {
        if (currentFloorSelection <= 1)
            return;
        currentFloorSelection -= 1;
        GetComponentInChildren<Text>().text = currentFloorSelection.ToString();
    }

    public void EnterDungeon()
    {
        CTDungeon.Instance.currentFloor = currentFloorSelection;

        //All here are boss or change in floor type
        if (currentFloorSelection == CTDungeon.Instance.BossFloorRed)
        {
            CProgression.Instance.UpdatePlayerSave(GameObject.FindGameObjectWithTag("Player").GetComponent<CPlayer>());
            SceneManager.LoadScene("BossLevel_Red");
            return;
        }

        if (currentFloorSelection == CTDungeon.Instance.BossFloorPink)
        {
            CProgression.Instance.UpdatePlayerSave(GameObject.FindGameObjectWithTag("Player").GetComponent<CPlayer>());
            SceneManager.LoadScene("DungeonLevel_Red");
            Debug.Log("BossPinkStage Not Implemented owo");
            return;
        }

        if (currentFloorSelection >= 1 && currentFloorSelection < CTDungeon.Instance.BossFloorRed)
        {
            CProgression.Instance.UpdatePlayerSave(GameObject.FindGameObjectWithTag("Player").GetComponent<CPlayer>());
            SceneManager.LoadScene("DungeonLevel_Red");
            MAudio.Instance.PlayBGM(AudioDatabase.Instance.getBGMAudio("gamescene"));

            return;
        }

        if (currentFloorSelection > CTDungeon.Instance.BossFloorRed && currentFloorSelection < CTDungeon.Instance.BossFloorPink)
        {
            CProgression.Instance.UpdatePlayerSave(GameObject.FindGameObjectWithTag("Player").GetComponent<CPlayer>());
            SceneManager.LoadScene("DungeonLevel_Red");
            //SceneManager.LoadScene("DungeonLevel_Pink");
            return;
        }


    }

    public void CloseSelectorWindow()
    {
        gameObject.SetActive(false);
    }
}
