using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrestigeSystem
{
    List<PrestigeBase> PrestigeList;
	// Use this for initialization
	public PrestigeSystem () {
        PrestigeList = new List<PrestigeBase>();
	}
    public List<PrestigeBase> GetList()
    {
        return PrestigeList;
    }

}
