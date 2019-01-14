using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CStateExplodeArmed : IStateBase
{
    public string StateID
    {
        get { return "StateAttack"; }
    }

    GameObject m_GO;
    public GameObject GO
    {
        get { return m_GO; }
    }

    float m_CountdownTimer;
    public CStateExplodeArmed(GameObject _go)
    {
        m_GO = _go;
        m_CountdownTimer = 3.0f;
    }

    public void EnterState()
    {
        m_GO.GetComponent<IEnemy>().ResetAtkTimer();
        m_CountdownTimer = 3.0f;
    }

    public void UpdateState()
    {
        m_CountdownTimer -= Time.deltaTime;
        if (m_CountdownTimer <= 0)
        {
            m_GO.GetComponent<IEnemy>().StateMachine.SetNextState("StateExplode");
        }

        if (((Vector2)m_GO.GetComponent<IEnemy>().Target.transform.position - (Vector2)m_GO.transform.position).magnitude > 2)
        {
            m_GO.GetComponent<IEnemy>().StateMachine.SetNextState("StateChase");
        }
    }

    public void ExitState()
    {
        m_CountdownTimer = 3.0f;
    }
}
