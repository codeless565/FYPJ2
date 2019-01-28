using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelector : MonoBehaviour
{
    [SerializeField]
    GameObject Player;
    CPlayer m_Player;

    [SerializeField]
    GameObject ConfirmationText;
    Text m_CfmText;

	// Use this for initialization
	void Start ()
    {
        if (Player == null)
            Player = GameObject.FindGameObjectWithTag("Player");
        m_Player = Player.GetComponent<CPlayer>();

        m_CfmText = ConfirmationText.GetComponent<Text>();

        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        m_CfmText.text = "Current Instrument: " + m_Player.EquippedWeapon.Name;
	}

    public void OpenMenu()
    {
        gameObject.SetActive(true);
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }

    public void ChangeWeaponToMelodica()
    {
        m_Player.EquippedWeapon = new Melodica();
    }

    public void ChangeWeaponToGuitar()
    {
        m_Player.EquippedWeapon = new Guitar();
    }

    public void ChangeWeaponToRecorder()
    {
        m_Player.EquippedWeapon = new Recorder();
    }
}
