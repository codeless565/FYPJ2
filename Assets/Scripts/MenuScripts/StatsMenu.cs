using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsMenu : MonoBehaviour
{
    [SerializeField]
    GameObject Player;
    [SerializeField]
    GameObject LevelText;
    [SerializeField]
    GameObject EXPText;
    [SerializeField]
    GameObject HPText;
    [SerializeField]
    GameObject SPText;
    [SerializeField]
    GameObject AttackText;
    [SerializeField]
    GameObject DefenseText;
    [SerializeField]
    GameObject MoveSpeedText;
    [SerializeField]
    GameObject AttackSpeedText;

    CPlayer m_Player;
    Text m_LevelText;
    Text m_EXPText;
    Text m_HPText;
    Text m_SPText;
    Text m_AttackText;
    Text m_DefenseText;
    Text m_MoveSpeedText;
    Text m_AttackSpeedText;

    // Use this for initialization
    void Start () {
        if (Player == null)
            Player = GameObject.FindGameObjectWithTag("Player");

        m_Player = Player.GetComponent<CPlayer>();

        m_LevelText       = LevelText.GetComponentInChildren<Text>();
        m_EXPText         = EXPText.GetComponentInChildren<Text>();
        m_HPText          = HPText.GetComponentInChildren<Text>();
        m_SPText          = SPText.GetComponentInChildren<Text>();
        m_AttackText      = AttackText.GetComponentInChildren<Text>();
        m_DefenseText     = DefenseText.GetComponentInChildren<Text>();
        m_MoveSpeedText   = MoveSpeedText.GetComponentInChildren<Text>();
        m_AttackSpeedText = AttackSpeedText.GetComponentInChildren<Text>();

        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            m_LevelText.text = m_Player.GetStats().Level.ToString();
            m_EXPText.text = (int)m_Player.GetStats().EXP + " / " + (int)m_Player.GetStats().MaxEXP;
            m_HPText.text = (int)m_Player.GetStats().HP + " / " + (int)m_Player.GetStats().MaxHP;
            m_SPText.text = (int)m_Player.GetStats().SP + " / " + (int)m_Player.GetStats().MaxSP;
            m_AttackText.text = m_Player.GetStats().Attack.ToString();
            m_DefenseText.text = m_Player.GetStats().Defense.ToString();
            m_MoveSpeedText.text = m_Player.GetStats().MoveSpeed.ToString("N2");
            m_AttackSpeedText.text = m_Player.GetStats().PlayRate.ToString("N2");
        }
    }

    public void OpenStats()
    {
        m_LevelText.text        = m_Player.GetStats().Level.ToString();
        m_EXPText.text          = (int)m_Player.GetStats().EXP + " / " + (int)m_Player.GetStats().MaxEXP;
        m_HPText.text           = (int)m_Player.GetStats().HP + " / " + (int)m_Player.GetStats().MaxHP;
        m_SPText.text           = (int)m_Player.GetStats().SP + " / " + (int)m_Player.GetStats().MaxSP;
        m_AttackText.text       = m_Player.GetStats().Attack.ToString();
        m_DefenseText.text      = m_Player.GetStats().Defense.ToString();
        m_MoveSpeedText.text    = m_Player.GetStats().MoveSpeed.ToString("N2");
        m_AttackSpeedText.text  = m_Player.GetStats().PlayRate.ToString("N2");
        gameObject.SetActive(true);
    }

    public void CloseStats()
    {
        gameObject.SetActive(false);
    }

    public void ToggleStatsMenu()
    {
        m_LevelText.text = m_Player.GetStats().Level.ToString();
        m_EXPText.text = (int)m_Player.GetStats().EXP + " / " + (int)m_Player.GetStats().MaxEXP;
        m_HPText.text = (int)m_Player.GetStats().HP + " / " + (int)m_Player.GetStats().MaxHP;
        m_SPText.text = (int)m_Player.GetStats().SP + " / " + (int)m_Player.GetStats().MaxSP;
        m_AttackText.text = m_Player.GetStats().Attack.ToString();
        m_DefenseText.text = m_Player.GetStats().Defense.ToString();
        m_MoveSpeedText.text = m_Player.GetStats().MoveSpeed.ToString("N2");
        m_AttackSpeedText.text = m_Player.GetStats().PlayRate.ToString("N2");
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
