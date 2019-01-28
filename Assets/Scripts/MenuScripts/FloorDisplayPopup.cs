using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorDisplayPopup : MonoBehaviour
{
    [SerializeField]
    GameObject FloorDisplayPanelPrefab;

    GameObject m_Panel;

    float m_BounceTime;
    float m_DeltaTime;

	// Use this for initialization
	void Start () {
        m_DeltaTime = Time.deltaTime;
        m_BounceTime = 3.0f;

        Time.timeScale = 0;
        m_Panel = Instantiate(FloorDisplayPanelPrefab);
        m_Panel.GetComponentInChildren<Text>().text = "Floor " + CTDungeon.Instance.currentFloor;
	}
	
	// Update is called once per frame
	void Update () {
        m_BounceTime -= m_DeltaTime;

        if (m_BounceTime <= 0)
        {
            Time.timeScale = 1;
            Destroy(m_Panel);
            Destroy(this);
        }
	}
}
