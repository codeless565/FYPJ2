using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CStateExplode : IStateBase
{
    public string StateID
    {
        get { return "StateExplode"; }
    }

    GameObject m_GO;
    public GameObject GO
    {
        get { return m_GO; }
    }

    public CStateExplode(GameObject _go)
    {
        m_GO = _go;
    }

    public void EnterState()
    {
    }

    public void UpdateState()
    {
        foreach (GameObject Obj in m_GO.GetComponent<IAreaOfEffect>().TargetList)
        {
            CDamageCalculator.Instance.SendDamage(Obj, m_GO.GetComponent<IEnemy>().GetStats().HP * 100);
            Debug.Log("Hit " + Obj.name + " with explosion!");
        }

        Object.Destroy(m_GO);
    }

    public void ExitState()
    {
    }
}