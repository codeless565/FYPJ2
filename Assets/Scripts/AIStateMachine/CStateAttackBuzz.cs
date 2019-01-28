using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CStateAttackBuzz : IStateBase
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

    public CStateAttackBuzz(GameObject _go)
    {
        m_GO = _go;
        m_Owner = m_GO.GetComponent<IEnemy>();
    }

    public void EnterState()
    {
        m_Target = m_Owner.Target.GetComponent<IEntity>();
    }

    public void UpdateState()
    {
        if (m_GO.GetComponent<IEnemy>().CanAttack)
        {
            m_Target.IsDamaged(m_Owner.GetStats().Attack);
            m_GO.GetComponent<IEnemy>().ResetAtkTimer();
            m_GO.GetComponent<IEnemy>().StateMachine.SetNextState("StateRunAway");
        }
    }

    public void ExitState()
    {
    }
}
