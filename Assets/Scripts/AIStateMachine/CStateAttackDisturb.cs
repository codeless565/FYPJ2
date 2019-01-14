using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CStateAttackDisturb : IStateBase
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

    public CStateAttackDisturb(GameObject _go)
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
            foreach (GameObject Obj in m_GO.GetComponent<IAreaOfEffect>().TargetList)
            {
                if (Obj.tag == "Player")
                    CDamageCalculator.Instance.SendDamage(Obj, m_GO);
                else if (Obj.tag == "Monster")
                    CDamageCalculator.Instance.SendHealing(Obj, m_GO);
            }

            m_GO.GetComponent<IEnemy>().ResetAtkTimer();
        }

        if (((Vector2)m_GO.GetComponent<IEnemy>().Target.transform.position - (Vector2)m_GO.transform.position).magnitude > 2)
        {
            m_GO.GetComponent<IEnemy>().StateMachine.SetNextState("StateChase");
        }
    }

    public void ExitState()
    {
    }
}
