using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CStatePathTest : IStateBase
{
    public GameObject m_GO;

    //unique var
    List<CPathNode> m_Pathing;

    public string StateID
    {
        get { return "StatePathTest"; }
    }

    public GameObject GO
    {
        get { return m_GO; }
    }

    public CStatePathTest(GameObject _gameObj)
    {
        m_GO = _gameObj;
        m_Pathing = new List<CPathNode>();
    }

    public void EnterState()
    {
        Debug.Log("CStatePath::EnterState() called");

        List<CTRoom> rmList =  CTDungeon.Instance.Floors[CTDungeon.Instance.currentFloor].GetRooms();
        int randomDestIndex = Random.Range(0, rmList.Count - 1);


    }

    public void UpdateState()
    {
        Debug.Log("CStatePath::UpdateState() called");
    }

    public void ExitState()
    {
        Debug.Log("CStatePath::ExitState() called");
    }
}
