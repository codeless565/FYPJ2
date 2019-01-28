using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CStateAttack : IStateBase
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

    IEnemy m_Owner;
    IEntity m_Target;

    public CStateAttack(GameObject _go)
    {
        m_GO = _go;
        m_Owner = m_GO.GetComponent<IEnemy>();
        m_Target = m_Owner.Target.GetComponent<IEntity>();
    }

    public void EnterState()
    {
    }

    public void UpdateState()
    {
        if (m_GO.GetComponent<IEnemy>().CanAttack)
        {
            m_Target.IsDamaged(m_Owner.GetStats().Attack);
            m_GO.GetComponent<IEnemy>().ResetAtkTimer();
        }

        if (((Vector2)m_GO.GetComponent<IEnemy>().Target.transform.position - (Vector2)m_GO.transform.position).magnitude > 1)
        {
            m_GO.GetComponent<IEnemy>().StateMachine.SetNextState("StateChase");
        }
    }

    public void ExitState()
    {
    }
}
