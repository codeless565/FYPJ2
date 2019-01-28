using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CStateChaseRanged : IStateBase
{
    public string StateID
    {
        get { return "StateChase"; }
    }

    GameObject m_GO;
    public GameObject GO
    {
        get { return m_GO; }
    }

    IEnemy m_Owner;

    public CStateChaseRanged(GameObject _go)
    {
        m_GO = _go;
        m_Owner = m_GO.GetComponent<IEnemy>();
    }

    public void EnterState()
    {
        if (m_Owner.Target == null)
            m_Owner.StateMachine.SetNextState("StateIdle");
    }

    public void UpdateState()
    {
        if (m_Owner.Target == null)
            m_Owner.StateMachine.SetNextState("StateIdle");
        else
        {
            Vector2 forwardVec = (Vector2)m_Owner.Target.transform.position - (Vector2)m_GO.transform.position;

            //is able to reach next point in this frame
            if (forwardVec.magnitude <= 3)
            {
                m_GO.transform.Translate(forwardVec.normalized * m_Owner.GetStats().MoveSpeed * Time.deltaTime);
                m_Owner.StateMachine.SetNextState("StateAttack");
            }
            else
                m_GO.transform.Translate(forwardVec.normalized * m_Owner.GetStats().MoveSpeed * Time.deltaTime);

            if (forwardVec.magnitude >= 10)
                m_Owner.StateMachine.SetNextState("StatePatrol");
        }
    }

    public void ExitState()
    {
    }
}