using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CStateIdle : IStateBase
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

    float m_timer;

    public CStateIdle(GameObject _go)
    {
        m_GO = _go;
        EnterState();
    }

    public void EnterState()
    {
        m_timer = Random.Range(1.0f, 3.0f);
    }

    public void UpdateState()
    {
        m_timer -= Time.deltaTime;

        if (m_timer <= 0.0f)
        {
            if (Random.Range(0.0f, 1.0f) < 0.5f)
                m_GO.GetComponent<IEnemy>().StateMachine.SetNextState("StatePatrol");
            else
                m_GO.GetComponent<IEnemy>().StateMachine.SetNextState("StateChangeRoom");
        }

        if ((m_GO.transform.position - GameObject.FindGameObjectWithTag("Player").transform.position).magnitude <= 5)
        {
            m_GO.GetComponent<IEnemy>().Target = GameObject.FindGameObjectWithTag("Player");
            m_GO.GetComponent<IEnemy>().StateMachine.SetNextState("StateChase");
        }
    }

    public void ExitState()
    {
        m_timer = 0.0f;
    }
}
