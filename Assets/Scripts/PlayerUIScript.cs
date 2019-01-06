using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIScript : MonoBehaviour {
    public Slider HPSlider;
    public Slider SPSlider;
    public Slider EXPSlider;

    public float m_FromHealth;
    public float m_TargetedHealth;

    public float m_FromEXP;
    public float m_TargetedEXP;

    public float m_FromSP;
    public float m_TargetedSP;

    // Use this for initialization
    public void Init () { 
        HPSlider.maxValue = GetComponent<CPlayer>().GetStats().MaxHP;
        SPSlider.maxValue = GetComponent<CPlayer>().GetStats().MaxSP;
        EXPSlider.maxValue = GetComponent<CPlayer>().GetStats().MaxEXP;

        m_TargetedHealth = GetComponent<CPlayer>().GetStats().HP;
        m_FromHealth = GetComponent<CPlayer>().GetStats().HP;

        m_TargetedEXP = GetComponent<CPlayer>().GetStats().EXP;
        m_FromEXP = GetComponent<CPlayer>().GetStats().EXP;

        m_TargetedSP = GetComponent<CPlayer>().GetStats().SP;
        m_FromSP = GetComponent<CPlayer>().GetStats().SP;
    }

    // Update is called once per frame
    void Update () {
        if (GetComponent<CPlayer>().GetStats().HP > m_TargetedHealth)
        {
            GetComponent<CPlayer>().GetStats().HP -= Mathf.Abs(m_FromHealth - m_TargetedHealth) * Time.deltaTime;
            if (GetComponent<CPlayer>().GetStats().HP < m_TargetedHealth)
                GetComponent<CPlayer>().GetStats().HP = m_TargetedHealth;
        }
        else if (GetComponent<CPlayer>().GetStats().HP < m_TargetedHealth)
        {
            GetComponent<CPlayer>().GetStats().HP += Mathf.Abs(m_FromHealth - m_TargetedHealth) * Time.deltaTime;
            if (GetComponent<CPlayer>().GetStats().HP > m_TargetedHealth)
                GetComponent<CPlayer>().GetStats().HP = m_TargetedHealth;
        }

        if (GetComponent<CPlayer>().GetStats().EXP > m_TargetedEXP)
        {
            GetComponent<CPlayer>().GetStats().EXP -= Mathf.Abs(m_FromEXP - m_TargetedEXP) * Time.deltaTime;
            if (GetComponent<CPlayer>().GetStats().EXP < m_TargetedEXP)
                GetComponent<CPlayer>().GetStats().EXP = m_TargetedEXP;
        }
        else if (GetComponent<CPlayer>().GetStats().EXP < m_TargetedEXP)
        {
            GetComponent<CPlayer>().GetStats().EXP += Mathf.Abs(m_FromEXP - m_TargetedEXP) * Time.deltaTime;
            if (GetComponent<CPlayer>().GetStats().EXP > m_TargetedEXP)
                GetComponent<CPlayer>().GetStats().EXP = m_TargetedEXP;
        }

        if (GetComponent<CPlayer>().GetStats().SP > m_TargetedHealth)
        {
            GetComponent<CPlayer>().GetStats().SP -= Mathf.Abs(m_FromHealth - m_TargetedHealth) * Time.deltaTime;
            if (GetComponent<CPlayer>().GetStats().SP < m_TargetedHealth)
                GetComponent<CPlayer>().GetStats().SP = m_TargetedHealth;
        }
        else if (GetComponent<CPlayer>().GetStats().SP < m_TargetedHealth)
        {
            GetComponent<CPlayer>().GetStats().SP += Mathf.Abs(m_FromHealth - m_TargetedHealth) * Time.deltaTime;
            if (GetComponent<CPlayer>().GetStats().SP > m_TargetedHealth)
                GetComponent<CPlayer>().GetStats().SP = m_TargetedHealth;
        }


        HPSlider.value = GetComponent<CPlayer>().GetStats().HP;
        SPSlider.value = GetComponent<CPlayer>().GetStats().SP;
        EXPSlider.value = GetComponent<CPlayer>().GetStats().EXP;
    }

    public void RemoveHealth(float _health, float _amount)
    {
        if ((m_TargetedHealth - _amount) <= 0)
            m_TargetedHealth = 0;
        else
            m_TargetedHealth -= _amount;

        m_FromHealth = _health;
    }

    public void AddHealth(float _health, float _amount)
    {
        if ((m_TargetedHealth + _amount) >= GetComponent<CPlayer>().GetStats().MaxHP)
            m_TargetedHealth = GetComponent<CPlayer>().GetStats().MaxHP;
        else
            m_TargetedHealth += _amount;

        m_FromHealth = _health;
        
    }

    public void AddEXP(float _exp, float _amount)
    {
        m_FromEXP = _exp;
        m_TargetedEXP += _amount;
    }

    public void RemoveSP(float _SP, float _amount)
    {
        if ((m_TargetedSP - _amount) <= 0)
            m_TargetedSP = 0;
        else
            m_TargetedSP -= _amount;

        m_FromSP = _SP;
    }

    public void AddSP(float _SP, float _amount)
    {
        if ((m_TargetedSP + _amount) >= GetComponent<CPlayer>().GetStats().MaxHP)
            m_TargetedSP = GetComponent<CPlayer>().GetStats().MaxHP;
        else
            m_TargetedSP += _amount;

        m_FromSP = _SP;

    }
}
