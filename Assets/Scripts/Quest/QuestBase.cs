using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RewardType
{
    NONE,
    GEMS,
    NOTES
}


public class QuestBase {


    QuestType m_QuestType;
    QuestTarget m_QuestTarget;
    float m_Amount;
    float m_CompleteAmount;
    bool m_QuestComplete;
    string m_questString;
    float m_QuestReward;
    RewardType m_QuestRewardType;

        public RewardType QuestRewardType
    {
        get
        {
            return m_QuestRewardType;
        }
        set
        {
            m_QuestRewardType = value;
        }
    }


    public float QuestReward
    {
        get
        {
            return m_QuestReward;

        }
        set
        {
            m_QuestReward = value;
        }
    }


    public string QuestString
    {
        get
        {
            return m_questString;
        }
        set
        {
            m_questString = value;
        }
    }

    public QuestType QuestType
    {
        get
        {
            return m_QuestType;
        }
        set
        {
            m_QuestType = value;
        }
    }

    public QuestTarget QuestTarget
    {
        get
        {
            return m_QuestTarget;
        }
        set
        {
            m_QuestTarget = value;
        }
    }

    public float QuestAmount
    {
        get
        {
            return m_Amount;
        }
        set
        {
            m_Amount = value;
        }
    }

    public float QuestCompleteAmount
    {
        get
        {
            return m_CompleteAmount;
        }
        set
        {
            m_CompleteAmount = value;
        }
    }

    public bool QuestComplete
    {
        get
        {
            return m_QuestComplete;
        }
        set
        {
            m_QuestComplete = value;
        }
    }

    

    public QuestBase()
    {
        m_QuestType = QuestType.NONE;
        m_QuestTarget = QuestTarget.NONE;
        m_Amount = 0;
        m_CompleteAmount = 0;
        m_QuestComplete = false;
        m_questString = "";
        m_QuestReward = 0f;
        m_QuestRewardType = RewardType.NONE;
    }

    // for slay quests
    public QuestBase(QuestType _questtype, QuestTarget _questtarget, float _completeamount, float _rewardamount, RewardType _rewardtype)
    {
        m_QuestType = _questtype;
        m_QuestTarget = _questtarget;
        m_Amount = 0;
        m_CompleteAmount = _completeamount;
        m_QuestComplete = false;
        m_questString = _questtype + " " + _questtarget + " " + _completeamount + " times";
        m_QuestReward = _rewardamount;
        m_QuestRewardType = _rewardtype;
    }

    // for reach quests
    public QuestBase(QuestType _questtype, float _completeamount, float _rewardamount, RewardType _rewardtype)
    {
        m_QuestType = _questtype;
        m_QuestTarget = QuestTarget.NONE;
        m_Amount = 0;
        m_CompleteAmount = _completeamount;
        m_QuestComplete = false;
        m_questString = _questtype + " level " + _completeamount;
        m_QuestReward = _rewardamount;
        m_QuestRewardType = _rewardtype;
    }
}


public enum QuestType
{
    NONE,
    SLAY,
    REACH,
    TOTAL,
}

public enum QuestTarget
{
    NONE,
    NOISE,
    TOTAL
}