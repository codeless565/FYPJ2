﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CStateRunAway : IStateBase
{
    public string StateID
    {
        get { return "StateRunAway"; }
    }

    GameObject m_GO;
    public GameObject GO
    {
        get { return m_GO; }
    }

    IEnemy m_Owner;

    public CStateRunAway(GameObject _go)
    {
        m_GO = _go;
        m_Owner = m_GO.GetComponent<IEnemy>();
    }

    public void EnterState()
    {
        m_Owner.GetStats().MoveSpeed *= 2;

        if (m_Owner.Target == null)
            m_Owner.StateMachine.SetNextState("StateIdle");
    }

    public void UpdateState()
    {
        if (m_Owner.Target == null)
            m_Owner.StateMachine.SetNextState("StateIdle");
        else
        {
            Vector2 forwardVec = (Vector2)m_GO.transform.position - (Vector2)m_Owner.Target.transform.position;

            //is able to reach next point in this frame
            if (m_Owner.CanAttack)
            {
                m_Owner.StateMachine.SetNextState("StateChase");
            }
       
            if (forwardVec.magnitude <= 10)
                m_GO.transform.Translate(forwardVec.normalized * m_Owner.GetStats().MoveSpeed * Time.deltaTime);
        }
    }

    public void ExitState()
    {
        m_Owner.GetStats().MoveSpeed *= 0.5f;
    }
}