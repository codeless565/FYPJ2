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

    public CStateChaseRanged(GameObject _go)
    {
        m_GO = _go;
    }

    public void EnterState()
    {
        if (m_GO.GetComponent<IEnemy>().Target == null)
            m_GO.GetComponent<IEnemy>().StateMachine.SetNextState("StateIdle");
    }

    public void UpdateState()
    {
        if (m_GO.GetComponent<IEnemy>().Target == null)
            m_GO.GetComponent<IEnemy>().StateMachine.SetNextState("StateIdle");
        else
        {
            Vector2 forwardVec = (Vector2)m_GO.GetComponent<IEnemy>().Target.transform.position - (Vector2)m_GO.transform.position;

            //is able to reach next point in this frame
            if (forwardVec.magnitude <= 3)
            {
                m_GO.transform.Translate(forwardVec.normalized * m_GO.GetComponent<IEnemy>().GetStats().MoveSpeed * Time.deltaTime);
                m_GO.GetComponent<IEnemy>().StateMachine.SetNextState("StateAttack");
            }
            else
                m_GO.transform.Translate(forwardVec.normalized * m_GO.GetComponent<IEnemy>().GetStats().MoveSpeed * Time.deltaTime);

            if (forwardVec.magnitude >= 10)
                m_GO.GetComponent<IEnemy>().StateMachine.SetNextState("StatePatrol");
        }
    }

    public void ExitState()
    {
    }
}