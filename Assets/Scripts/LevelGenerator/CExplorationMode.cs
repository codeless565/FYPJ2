using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CExplorationMode : MonoBehaviour {

    public GameObject player;

	// Use this for initialization
	void Start () {
        GetComponent<CTBoardGenerator>().Init();
        player.GetComponent<CPlayer>().Init();
        player.transform.position = CTDungeon.Instance.Floors[CTDungeon.Instance.currentFloor].GetRooms()[0].CenterPoint();
	}
}
