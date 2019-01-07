using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StairsForward : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.tag == "Player")
        {
            CTDungeon.Instance.currentFloor += 1;

            switch (CTDungeon.Instance.currentFloor)
            {
                //All here are boss or change in floor type
                case 25:
                    break;
                case 26:
                    break;
                case 50:
                    break;
                case 51:
                    break;
                default:
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    break;
            }        
        }
    }
}
