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

    public CStateAttackBuzz(GameObject _go)
    {
        m_GO = _go;
    }

    public void EnterState()
    {
    }

    public void UpdateState()
    {
        if (m_GO.GetComponent<IEnemy>().CanAttack)
        {
            CDamageCalculator.Instance.SendDamage(m_GO.GetComponent<IEnemy>().Target, m_GO);
            m_GO.GetComponent<IEnemy>().ResetAtkTimer();
            m_GO.GetComponent<IEnemy>().StateMachine.SetNextState("StateRunAway");
        }
    }

    public void ExitState()
    {
    }
}
