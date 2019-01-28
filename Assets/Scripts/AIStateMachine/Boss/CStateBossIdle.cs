using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CStateBossIdle : IStateBase
{
    public string StateID
    {
        get { return "StateIdle"; }
    }

    GameObject m_GO;
    public GameObject GO
    {
        get { return m_GO; }
    }

    Vector2 m_RestingPt;
    IEnemy m_Owner;

    public CStateBossIdle(GameObject _go, Vector2 _startPos)
    {
        m_RestingPt = _startPos;
        m_GO = _go;
        m_Owner = m_GO.GetComponent<IEnemy>();
        EnterState();
    }

    public void EnterState()
    {
    }

    public void UpdateState()
    {
        if (((Vector2)m_GO.transform.position - m_RestingPt).magnitude > 1)
        {
            Vector2 movedir = (m_RestingPt - (Vector2)m_GO.transform.position).normalized;
            m_GO.transform.Translate(movedir * m_Owner.GetStats().MoveSpeed * Time.deltaTime);
        }

        if ((m_GO.transform.position - GameObject.FindGameObjectWithTag("Player").transform.position).magnitude <= 5)
        {
            m_Owner.Target = GameObject.FindGameObjectWithTag("Player");
            m_Owner.StateMachine.SetNextState("StateChase");
        }
    }

    public void ExitState()
    {
    }
}
