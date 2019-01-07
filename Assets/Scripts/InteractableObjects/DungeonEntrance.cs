using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonEntrance : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.tag == "Player")
        {
            CTDungeon.Instance.currentFloor = 1;
            SceneManager.LoadScene("DungeonLevel_Red");
        }
    }
}
