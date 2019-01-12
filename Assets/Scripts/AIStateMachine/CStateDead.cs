using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CStateDead : IStateBase
{
    public string StateID
    {
        get { return "StateDead"; }
    }

    GameObject m_GO;
    public GameObject GO
    {
        get { return m_GO; }
    }

    public CStateDead(GameObject _go)
    {
        m_GO = _go;
    }

    public void EnterState()
    {
        Debug.Log("Entered StateDead");
        //spawn item? play animation? Call delete();
        m_GO.GetComponent<IEnemy>().Delete();
    }

    public void UpdateState()
    {
        m_GO.GetComponent<IEnemy>().Delete();
    }

    public void ExitState()
    {
    }
}
