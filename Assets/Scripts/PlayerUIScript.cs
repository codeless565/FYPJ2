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

    public Canvas m_playerQuestBoardUI;
    public Button btnPrefab;
    public List<Button> btnlist;
    float startPos = -175;


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

        btnlist = new List<Button>();
        for(int i =0;i<3;++i)
        {
            Button newbtn = Instantiate(btnPrefab, m_playerQuestBoardUI.transform);
            newbtn.GetComponent<RectTransform>().anchoredPosition = new Vector3(startPos, 0, 0);
            
            btnlist.Add(newbtn);
            
            //btnlist[i].onClick.RemoveAllListeners();
            //btnlist[i].onClick.AddListener(delegate { AddQuestToPlayer(newquest); });

            startPos += 175;
        }
        m_playerQuestBoardUI.gameObject.SetActive(false);
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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            UpdateQuestUI();
            m_playerQuestBoardUI.gameObject.SetActive(!m_playerQuestBoardUI.gameObject.activeSelf);
        }

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

        for(int i =0;i<GetComponent<CPlayer>().m_playerQuestList.Count;++i)
        {
            // todo ui
            QuestBase quest = GetComponent<CPlayer>().m_playerQuestList[i];


            btnlist[i].GetComponentInChildren<Text>().text = quest.QuestString;
            btnlist[i].GetComponent<Button>().onClick.RemoveAllListeners();
            btnlist[i].GetComponent<Button>().onClick.AddListener(delegate { CompleteQuest(quest); });

            if (GetComponent<CPlayer>().m_playerQuestList[i].QuestComplete)
                btnlist[i].GetComponent<Image>().color = Color.green;
            else
                btnlist[i].GetComponent<Image>().color = Color.red;
        }
    }

    public void CompleteQuest(QuestBase _quest)
    {
        Debug.Log("Quest Complete. TODO --- Quest Reward");

        foreach(QuestBase qb in GetComponent<CPlayer>().m_playerQuestList)
        {
            if(qb == _quest)
            {
                GetComponent<CPlayer>().m_playerQuestList.Remove(qb);
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
