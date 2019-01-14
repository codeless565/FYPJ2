using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementSystem {
    Dictionary<string, AchievementBase> AchievementList;
    CStats m_playerstats;

    private static AchievementSystem instance;
    private AchievementSystem() { }
    public static AchievementSystem Instance
    {
        get
        {
            if (instance == null)
                instance = new AchievementSystem();
            return instance;
        }
    }

	// Use this for initialization
	public void Init(CStats _playerstats) {
        AchievementList = new Dictionary<string, AchievementBase>();
        m_playerstats = _playerstats;

        AddAchievement(new NoiseSlayer());
        AddAchievementProperty(NoiseSlayer.m_AchievementName,new KillNoiseProp(true,3.0f));
        AddAchievement(new TestAchievement());

        //SetAchievementActive(NoiseSlayer.m_AchievementName, true);
    }

    //public void SetAchievementActive(string _Achievementname, bool _active)
    //{
    //    if (!AchievementList.ContainsKey(_Achievementname))
    //        return;

    //    AchievementList[_Achievementname].AchievementActive = _active;
    //}

    public AchievementBase GetAchievementInfo(string _Achievementname)
    {
        foreach(KeyValuePair<string,AchievementBase>qb in AchievementList)
        {
            if (qb.Key == _Achievementname)
                return qb.Value;
        }
        return null;
    }

    public void AddAchievement(AchievementBase _Achievement)
    {
        if (_Achievement == null)
            return;
        if (AchievementList.ContainsKey(_Achievement.AchievementName))
            return;

        _Achievement.Init(m_playerstats);
        AchievementList.Add(_Achievement.AchievementName, _Achievement);
    }
    public void AddAchievementProperty(string _Achievementname, QPropertiesBase _property)
    {
        if (!AchievementList.ContainsKey(_Achievementname))
            return;

        AchievementList[_Achievementname].AddProperty(_property);
    }

    public void UpdateAchievementProperty(string _Achievementname, string _propname, float _amount)
    {
        if (!AchievementList.ContainsKey(_Achievementname))
            return;
        if (!AchievementList[_Achievementname].AchievementActive)
            return;
        if (!AchievementList[_Achievementname].PropertiesList.ContainsKey(_propname))
            return;

        AchievementList[_Achievementname].PropertiesList[_propname].AddCurrentValue(_amount);
        UpdateAchievement(AchievementList[_Achievementname]);
    }

    public void UpdateAchievement(AchievementBase _Achievement)
    {
        foreach (KeyValuePair<string, AchievementBase> qb in AchievementList)
        {
            if (qb.Key != _Achievement.AchievementName)
                continue;
            qb.Value.UpdateAchievement();
            Debug.Log(qb.Key + " is now " + qb.Value.AchievementCompleted + "(AchievementCompleted)");

            if (qb.Value.AchievementCompleted)
            {
                qb.Value.AchievementReward();
                qb.Value.AchievementActive = false;
            }
        }
    }
}
