using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonEntrance : MonoBehaviour {

    [SerializeField]
    GameObject FloorSelectorPanel;

    private void Start()
    {
        if (FloorSelectorPanel == null)
            Destroy(this);
        FloorSelectorPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.tag == "Player")
        {
            FloorSelectorPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D _other)
    {
        if (_other.tag == "Player")
        {
            FloorSelectorPanel.SetActive(false);
        }

    }

    public void CloseFloorSelectorPanel()
    {
        FloorSelectorPanel.SetActive(false);
    }
}
