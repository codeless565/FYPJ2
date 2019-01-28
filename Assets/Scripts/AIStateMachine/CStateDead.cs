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

    IEnemy m_Owner;

    public CStateDead(GameObject _go)
    {
        m_GO = _go;
        m_Owner = m_GO.GetComponent<IEnemy>();
    }

    public void EnterState()
    {
        //spawn item? play animation? Call delete();
        m_Owner.Delete();
    }

    public void UpdateState()
    {
        m_Owner.Delete();
    }

    public void ExitState()
    {
    }
}
