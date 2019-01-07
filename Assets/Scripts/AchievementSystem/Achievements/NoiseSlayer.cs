using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseSlayer : AchievementBase {
    public static string m_AchievementName = "NoiseSlayer";
    private bool m_AchievementCompleted;
    private bool m_AchievementActive;
    private Dictionary<string, QPropertiesBase> m_propList;
    private int m_completedProps;
    private CStats m_playerstats;

    public NoiseSlayer()
    {
        
    }

    public void Init(CStats _playerstats)
    {
        m_AchievementCompleted = false;
        m_AchievementActive = false;
        m_propList = new Dictionary<string, QPropertiesBase>();
        m_completedProps = 0;
        m_playerstats = _playerstats;
    }

    public string AchievementName
    {
        get
        {
            return m_AchievementName;
        }
    }

    public bool AchievementCompleted { get { return m_AchievementCompleted; } set { m_AchievementCompleted = value; } }
    public bool AchievementActive { get { return m_AchievementActive; } set { m_AchievementActive = value; } }
    public Dictionary<string, QPropertiesBase> PropertiesList { get { return m_propList; } set { m_propList = value; } }
    public int CompletedProperties { get { return m_completedProps; } set { m_completedProps = value; } }

    public void AddProperty(QPropertiesBase _property)
    {
        if (_property == null)
            return;
        if (m_propList.ContainsKey(_property.PropertyName))
            return;

        m_propList.Add(_property.PropertyName,_property);
    }

    public void AchievementReward()
    {
        PostOffice.Instance.Send("Player", new Message(MESSAGE_TYPE.ADDEXP, 100f));
    }

    public void UpdateAchievement()
    {
        m_completedProps = 0;
        foreach (KeyValuePair<string,QPropertiesBase >pb in m_propList)
        {
            if (!pb.Value.IsActive)
                continue;
            pb.Value.Update();
            if (pb.Value.IsCompleted)
            {
                m_completedProps++;
                pb.Value.IsActive = false;
            }
        }
        if (m_completedProps == m_propList.Count)
            m_AchievementCompleted = true;
    }

    public void CheckRequirement()
    {
        if (m_playerstats.Level == 2)
            AchievementActive = true;
    }
}
