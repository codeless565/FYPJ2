using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseSlayer : QuestBase {
    public static string m_questName;
    private bool m_questCompleted;
    private bool m_questActive;
    private Dictionary<string, QPropertiesBase> m_propList;
    private int m_completedProps;

    public NoiseSlayer()
    {
        m_questName = "NoiseSlayer";
        m_questCompleted = false;
        m_questActive = false;
        m_propList = new Dictionary<string, QPropertiesBase>();
        m_completedProps = 0;
    }

    public string QuestName
    {
        get
        {
            return m_questName;
        }
    }

    public bool QuestCompleted { get { return m_questCompleted; } set { m_questCompleted = value; } }
    public bool QuestActive { get { return m_questActive; } set { m_questActive = value; } }
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

    public void QuestReward()
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {
        m_completedProps = 0;
        foreach (KeyValuePair<string,QPropertiesBase >pb in m_propList)
        {
            if (!pb.Value.IsActive)
                continue;
            if (pb.Value.IsCompleted)
                m_completedProps++;
            pb.Value.Update();
        }
        if (m_completedProps == m_propList.Count)
        {
            m_questCompleted = true;
            // Reward
        }
    }
}
