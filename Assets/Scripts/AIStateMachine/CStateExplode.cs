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

    IEnemy m_Owner;

    public CStateExplode(GameObject _go)
    {
        m_GO = _go;
        m_Owner = m_GO.GetComponent<IEnemy>();
    }

    public void EnterState()
    {
    }

    public void UpdateState()
    {
        foreach (GameObject Obj in m_GO.GetComponent<IAreaOfEffect>().TargetList)
        {
            Obj.GetComponent<IEntity>().IsDamaged(m_Owner.GetStats().HP * m_Owner.GetStats().Attack);
        }

        Object.Destroy(m_GO);
    }

    public void ExitState()
    {
    }
}