using System.Collections;
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

    public CStateRunAway(GameObject _go)
    {
        m_GO = _go;
    }

    public void EnterState()
    {
        m_GO.GetComponent<IEnemy>().GetStats().MoveSpeed *= 2;

        if (m_GO.GetComponent<IEnemy>().Target == null)
            m_GO.GetComponent<IEnemy>().StateMachine.SetNextState("StateIdle");
    }

    public void UpdateState()
    {
        if (m_GO.GetComponent<IEnemy>().Target == null)
            m_GO.GetComponent<IEnemy>().StateMachine.SetNextState("StateIdle");
        else
        {
            Vector2 forwardVec = (Vector2)m_GO.transform.position - (Vector2)m_GO.GetComponent<IEnemy>().Target.transform.position;

            //is able to reach next point in this frame
            if (m_GO.GetComponent<IEnemy>().CanAttack)
            {
                m_GO.GetComponent<IEnemy>().StateMachine.SetNextState("StateChase");
            }
       
            if (forwardVec.magnitude <= 10)
                m_GO.transform.Translate(forwardVec.normalized * m_GO.GetComponent<IEnemy>().GetStats().MoveSpeed * Time.deltaTime);
        }
    }

    public void ExitState()
    {
        m_GO.GetComponent<IEnemy>().GetStats().MoveSpeed *= 0.5f;
    }
}