using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrestigeSystem
{
    Dictionary<string, PrestigeBase> PrestigeList;
	// Use this for initialization
	public PrestigeSystem () {
        PrestigeList = new Dictionary<string, PrestigeBase>();
	}
    public Dictionary<string, PrestigeBase> GetList()
    {
        return PrestigeList;
    }

    public void Update()
    {

        foreach (KeyValuePair<string, PrestigeBase> pb in PrestigeList)
            pb.Value.Update();
    }

    public void AddPrestige(PrestigeBase _prestige)
    {
        if (_prestige == null)
            return;
        if (PrestigeList.ContainsKey(_prestige.PrestigeName))
            return;

        PrestigeList.Add(_prestige.PrestigeName, _prestige);
    }
    public PrestigeBase GetPrestige(string _prestigename)
    {
        if (!PrestigeList.ContainsKey(_prestigename))
            return null;

        return PrestigeList[_prestigename];
    }

}
