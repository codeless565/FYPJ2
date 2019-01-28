using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIScript : MonoBehaviour {
    public Image HPSlider;
    public Image SPSlider;
    public Image EXPSlider;
    public Image ChargingCircle;

    private Text LevelDisplay;
    private Text HealthPercentageDisplay;

    public float m_FromHealth;
    public float m_TargetedHealth;

    public float m_FromEXP;
    public float m_TargetedEXP;

    public float m_FromSP;
    public float m_TargetedSP;

    public Canvas m_playerQuestBoardUI;
    public Button btnPrefab;
    public List<Button> btnlist;
    float startPos = -200;

    CPlayer m_Player;

    // Use this for initialization
    public void Init () {
        m_Player = GetComponent<CPlayer>();
        LevelDisplay = EXPSlider.GetComponentInChildren<Text>();
        HealthPercentageDisplay = HPSlider.GetComponentInChildren<Text>();
        HPSlider = HPSlider.transform.GetChild(0).GetComponent<Image>();
        SPSlider = SPSlider.transform.GetChild(0).GetComponent<Image>();
        EXPSlider = EXPSlider.transform.GetChild(0).GetComponent<Image>();
        ChargingCircle = ChargingCircle.transform.GetChild(0).GetComponent<Image>();

        HPSlider.fillAmount = m_Player.GetStats().MaxHP;
        SPSlider.fillAmount = m_Player.GetStats().MaxSP;
        EXPSlider.fillAmount = m_Player.GetStats().MaxEXP;

        m_TargetedHealth = m_Player.GetStats().HP;
        m_FromHealth = m_Player.GetStats().HP;

        m_TargetedEXP = m_Player.GetStats().EXP;
        m_FromEXP = m_Player.GetStats().EXP;

        m_TargetedSP = m_Player.GetStats().SP;
        m_FromSP = m_Player.GetStats().SP;

        btnlist = new List<Button>();
        for (int i = 0; i < 3; ++i)
        {
            Button newbtn = Instantiate(btnPrefab, m_playerQuestBoardUI.transform);
            newbtn.GetComponent<RectTransform>().anchoredPosition = new Vector3(startPos, 0, 0);

            btnlist.Add(newbtn);

            //btnlist[i].onClick.RemoveAllListeners();
            //btnlist[i].onClick.AddListener(delegate { AddQuestToPlayer(newquest); });

            startPos += 200;
        }
        m_playerQuestBoardUI.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        if (m_Player.GetStats().HP > m_TargetedHealth)
        {
            m_Player.GetStats().HP -= Mathf.Abs(m_FromHealth - m_TargetedHealth) * Time.deltaTime;
            if (m_Player.GetStats().HP < m_TargetedHealth)
                m_Player.GetStats().HP = m_TargetedHealth;
        }
        else if (m_Player.GetStats().HP < m_TargetedHealth)
        {
            m_Player.GetStats().HP += Mathf.Abs(m_FromHealth - m_TargetedHealth) * Time.deltaTime;
            if (m_Player.GetStats().HP > m_TargetedHealth)
                m_Player.GetStats().HP = m_TargetedHealth;
        }

        if (m_Player.GetStats().EXP > m_TargetedEXP)
        {
            m_Player.GetStats().EXP -= Mathf.Abs(m_FromEXP - m_TargetedEXP) * Time.deltaTime;
            if (m_Player.GetStats().EXP < m_TargetedEXP)
                m_Player.GetStats().EXP = m_TargetedEXP;
        }
        else if (m_Player.GetStats().EXP < m_TargetedEXP)
        {
            m_Player.GetStats().EXP += Mathf.Abs(m_FromEXP - m_TargetedEXP) * Time.deltaTime;
            if (m_Player.GetStats().EXP > m_TargetedEXP)
                m_Player.GetStats().EXP = m_TargetedEXP;
        }

        if (m_Player.GetStats().SP > m_TargetedSP)
        {
            m_Player.GetStats().SP -= Mathf.Abs(m_FromSP - m_TargetedSP) * Time.deltaTime;
            if (m_Player.GetStats().SP < m_TargetedSP)
                m_Player.GetStats().SP = m_TargetedSP;
        }
        else if (m_Player.GetStats().SP < m_TargetedSP)
        {
            m_Player.GetStats().SP += Mathf.Abs(m_FromSP - m_TargetedSP) * Time.deltaTime;
            if (m_Player.GetStats().SP > m_TargetedSP)
                m_Player.GetStats().SP = m_TargetedSP;
        }


        HPSlider.fillAmount = m_Player.GetStats().HP / m_Player.GetStats().MaxHP;
        SPSlider.fillAmount = m_Player.GetStats().SP / m_Player.GetStats().MaxSP;
        EXPSlider.fillAmount = m_Player.GetStats().EXP / m_Player.GetStats().MaxEXP;
        ChargingCircle.fillAmount = m_Player.EquippedWeapon.ChargingState;
        HealthPercentageDisplay.text = ((int)(m_Player.GetStats().HP / m_Player.GetStats().MaxHP * 100)).ToString() + "%";
        LevelDisplay.text = m_Player.GetStats().Level.ToString();
    }

    public void OpenQuestUI()
    {
            UpdateQuestUI();
            m_playerQuestBoardUI.gameObject.SetActive(!m_playerQuestBoardUI.gameObject.activeSelf);
    }


    public void ResetQuestDisplay()
    {
        foreach(Button bt in btnlist)
        {
            bt.GetComponent<Image>().color = Color.white;
            bt.GetComponent<Button>().onClick.RemoveAllListeners();
            bt.GetComponentInChildren<Text>().text = "";
        }
    }
    public void UpdateQuestUI()
    {
        ResetQuestDisplay();

        for(int i =0;i<m_Player.QuestList.Count;++i)
        {
            QuestBase quest = m_Player.QuestList[i];


            
            btnlist[i].transform.GetChild(0).GetComponent<Text>().text = quest.QuestString;
            btnlist[i].transform.GetChild(1).GetComponent<Text>().text = quest.QuestReward + " " + quest.QuestRewardType;


            btnlist[i].GetComponent<Button>().onClick.RemoveAllListeners();
            btnlist[i].GetComponent<Button>().onClick.AddListener(delegate { CompleteQuest(quest); });

            if (m_Player.QuestList[i].QuestComplete)
                btnlist[i].GetComponent<Image>().color = Color.green;
            else
                btnlist[i].GetComponent<Image>().color = Color.red;
        }
    }

    public void CompleteQuest(QuestBase _quest)
    {
        //Debug.Log("Quest Complete. TODO --- Quest Reward");

        foreach(QuestBase qb in m_Player.QuestList)
        {
            if(qb == _quest)
            {
                if (qb.QuestRewardType == RewardType.NOTES)
                    m_Player.m_InventorySystem.AddNotes((int)qb.QuestReward);
                else if (qb.QuestRewardType == RewardType.GEMS)
                    m_Player.m_InventorySystem.AddGems((int)qb.QuestReward);

                m_Player.QuestList.Remove(qb);
                UpdateQuestUI();
                return;
            }
        }
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
        if ((m_TargetedHealth + _amount) >= m_Player.GetStats().MaxHP)
            m_TargetedHealth = m_Player.GetStats().MaxHP;
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
        if ((m_TargetedHealth + _amount) >= m_Player.GetStats().MaxSP)
            m_TargetedHealth = m_Player.GetStats().MaxSP;
        else
            m_TargetedSP += _amount;

        m_FromSP = _SP;

    }
}
