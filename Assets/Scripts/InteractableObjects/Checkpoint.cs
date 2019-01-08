using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.tag == "Player")
        {
            if (CTDungeon.Instance.currentFloor == CTDungeon.Instance.BossFloorRed)
                CTDungeon.Instance.CheckpointRed = true;
            else if (CTDungeon.Instance.currentFloor == CTDungeon.Instance.BossFloorPink)
                CTDungeon.Instance.CheckpointPink = true;
        }
    }
}
