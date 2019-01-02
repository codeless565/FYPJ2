using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSystem {
    Dictionary<string, QuestBase> QuestList;
	// Use this for initialization
	public QuestSystem() {
        QuestList = new Dictionary<string, QuestBase>();
        AddQuest(new NoiseSlayer());
        AddQuestProperty(NoiseSlayer.m_questName,new KillNoiseProp(true,3.0f));
        SetQuestActive(NoiseSlayer.m_questName, true);
	}

    public void SetQuestActive(string _questname, bool _active)
    {
        if (!QuestList.ContainsKey(_questname))
            return;

        QuestList[_questname].QuestActive = _active;
    }

    public void AddQuest(QuestBase _quest)
    {
        if (_quest == null)
            return;
        if (QuestList.ContainsKey(_quest.QuestName))
            return;

        QuestList.Add(_quest.QuestName, _quest);
    }
	
	// Update is called once per frame
	public void Update () {
		foreach(KeyValuePair<string,QuestBase> qb in QuestList)
        {
            if (!qb.Value.QuestActive)
                continue;
            qb.Value.Update();
            Debug.Log(qb.Value.QuestName + ": " + qb.Value.QuestCompleted);

            if (qb.Value.QuestCompleted)
            {
                qb.Value.QuestActive = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
            PostOffice.Instance.Send("Player", new Message(MESSAGE_TYPE.ADDPROP));
	}

    public void AddQuestProperty(string _questname, QPropertiesBase _property)
    {
        if (!QuestList.ContainsKey(_questname))
            return;

        QuestList[_questname].AddProperty(_property);
    }

    public void UpdateQuestProperty(string _questname, string _propname, float _amount)
    {
        if (!QuestList.ContainsKey(_questname))
            return;
        if (!QuestList[_questname].PropertiesList.ContainsKey(_propname))
            return;

        QuestList[_questname].PropertiesList[_propname].AddCurrentValue(_amount);
    }
}
