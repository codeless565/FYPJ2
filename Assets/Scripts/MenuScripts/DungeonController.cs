using System.Collections;
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
            SceneManager.LoadScene("BossLevel_Red");
            return;
        }

        if (currentFloorSelection == CTDungeon.Instance.BossFloorPink)
        {
            Debug.Log("BossPinkStage Not Implemented owo");
            return;
        }

        if (currentFloorSelection >= 1 && currentFloorSelection < CTDungeon.Instance.BossFloorRed)
        {
            SceneManager.LoadScene("DungeonLevel_Red");
            return;
        }

        if (currentFloorSelection > CTDungeon.Instance.BossFloorRed && currentFloorSelection < CTDungeon.Instance.BossFloorPink)
        {
            Debug.Log("DungeonLevel_Pink Not Implemented owo");
            //SceneManager.LoadScene("DungeonLevel_Pink");
            return;
        }

    }
}
