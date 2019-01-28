using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CExplorationMode : MonoBehaviour {

    public GameObject player;

    // Use this for initialization
    void Start()
    {
        if (GetComponent<CTBoardGenerator>())
            GetComponent<CTBoardGenerator>().Init();

        player.GetComponent<CPlayer>().Init();

        if (SceneManager.GetActiveScene().name == "TownScene")
        {
            player.transform.position = new Vector3(0, -13, 0);
            return;
        }

        if (CTDungeon.Instance.currentFloor > 0)
        {
            player.transform.position = CTDungeon.Instance.Floors[CTDungeon.Instance.currentFloor].Rooms[0].CenterPoint;

            if (GetComponent<CItemGenerator>())
                GetComponent<CItemGenerator>().Init();
            if (GetComponent<CMonsterGenerator>())
                GetComponent<CMonsterGenerator>().Init();
            if (GetComponent<CBossGenerator>())
                GetComponent<CBossGenerator>().Init();
        }
    }
}
