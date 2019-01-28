using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CStateBossChase : IStateBase
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

    public CStateBossChase(GameObject _go)
    {
        m_GO = _go;
        m_Owner = m_GO.GetComponent<IEnemy>();
    }

    public void EnterState()
    {
        if (m_Owner.Target == null)
        {
            m_Owner.StateMachine.SetNextState("StateIdle");
        }
    }

    public void UpdateState()
    {
        if (m_Owner.Target == null)
        {
            m_Owner.StateMachine.SetNextState("StateIdle");
            return;
        }

        Vector2 moveDir = m_Owner.Target.transform.position - m_GO.transform.position;
        m_GO.transform.Translate(moveDir.normalized * m_Owner.GetStats().MoveSpeed * Time.deltaTime);

        if (moveDir.magnitude <= 5)
        {
            m_Owner.StateMachine.SetNextState("StateAttack");
        }
        else if (moveDir.magnitude > 8)
        {
            m_Owner.StateMachine.SetNextState("StateTeleport");
        }
    }

    public void ExitState()
    {
    }
}