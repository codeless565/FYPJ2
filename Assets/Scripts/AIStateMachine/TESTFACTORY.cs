using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTFACTORY : MonoBehaviour {
    public GameObject NoisePrefab;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.R))
        {
            List<CTRoom> rooms = CTDungeon.Instance.Floors[CTDungeon.Instance.currentFloor].Rooms;
            CTRoom selectedRoom = rooms[Random.Range(0, rooms.Count)];
            GameObject newNoise = Instantiate(NoisePrefab, selectedRoom.CenterPoint, Quaternion.identity);
            newNoise.GetComponent<IEnemy>().Init(selectedRoom.coordinate);
        }

    }
}
