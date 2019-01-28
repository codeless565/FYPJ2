using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPopup : MonoBehaviour
{
    [SerializeField]
    GameObject Player;

    [SerializeField]
    GameObject GameOverPanelPrefab;

    CPlayer m_Player;

    bool m_Gameover;
    float m_BounceTime;

	// Use this for initialization
	void Start ()
    {
        if (Player == null)
            Player = GameObject.FindGameObjectWithTag("Player");

        m_Player = Player.GetComponent<CPlayer>();
        m_Gameover = false;
        m_BounceTime = 5.0f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (m_Player == null)
            return;

        if (!m_Gameover)
        {
            if (m_Player.GetStats().HP <= 0)
            {
                Instantiate(GameOverPanelPrefab);
                m_Gameover = true;
            }
        }
        else
            m_BounceTime -= Time.deltaTime;

        if (m_BounceTime < 0)
            SceneManager.LoadScene("MainMenu");
    }
}
