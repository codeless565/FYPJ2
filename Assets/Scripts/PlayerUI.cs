using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

    public Slider HPSlider;
    public Slider SPSlider;
    public Slider EXPSlider;

    public float m_FromHealth;
    public float m_TargetedHealth;

    // Use this for initialization
    public void Init () {
        HPSlider.maxValue = GetComponent<CPlayer>().GetStats().MaxHP;
        SPSlider.maxValue = GetComponent<CPlayer>().GetStats().MaxSP;
        EXPSlider.maxValue = GetComponent<CPlayer>().GetStats().MaxEXP;

        m_TargetedHealth = GetComponent<CPlayer>().GetStats().HP;
        m_FromHealth = GetComponent<CPlayer>().GetStats().HP;
    }

    // Update is called once per frame
    void Update () {
        if (GetComponent<CPlayer>().GetStats().HP > m_TargetedHealth)
            GetComponent<CPlayer>().GetStats().HP -= Mathf.Abs(m_FromHealth - m_TargetedHealth) * Time.deltaTime;
        else if (GetComponent<CPlayer>().GetStats().HP < m_TargetedHealth)
            GetComponent<CPlayer>().GetStats().HP = m_TargetedHealth;

        HPSlider.value = GetComponent<CPlayer>().GetStats().HP;
        SPSlider.value = GetComponent<CPlayer>().GetStats().SP;
        EXPSlider.value = GetComponent<CPlayer>().GetStats().EXP;
    }
}
