using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CExplorationMode : MonoBehaviour {

    public GameObject player;

	// Use this for initialization
	void Start () {
        if (GetComponent<CTBoardGenerator>())
            GetComponent<CTBoardGenerator>().Init();

        player.GetComponent<CPlayer>().Init();

        if (CTDungeon.Instance.currentFloor > 0)
            player.transform.position = CTDungeon.Instance.Floors[CTDungeon.Instance.currentFloor].Rooms[0].CenterPoint;
	}
}
