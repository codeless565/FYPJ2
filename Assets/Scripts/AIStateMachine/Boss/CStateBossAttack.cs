using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CStateBossAttack : IStateBase
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
    GameObject Bullet = (GameObject)Resources.Load("Projectiles/EnemyNote");

    public CStateBossAttack(GameObject _go)
    {
        m_GO = _go;
        m_Owner = m_GO.GetComponent<IEnemy>();
    }
    
    public void EnterState()
    {
        if (m_Owner.Target == null)
        {
            m_Owner.StateMachine.SetNextState("StateIdle");
        }
    }

    public void UpdateState()
    {
        Vector2 moveDir = m_Owner.Target.transform.position - m_GO.transform.position;
        m_GO.transform.Translate(moveDir.normalized * m_Owner.GetStats().MoveSpeed * Time.deltaTime * 0.5f);    //Move slower

        if (m_Owner.CanAttack)
        {
            GameObject newBullet = Object.Instantiate(Bullet, m_GO.transform.position, Quaternion.identity);
            newBullet.GetComponent<IProjectile>().Init(
                m_GO.GetComponent<IEnemy>().GetStats().Attack, 8, 20,
                ((Vector2)m_GO.GetComponent<IEnemy>().Target.transform.position - (Vector2)m_GO.transform.position).normalized,
                ProjectileType.Normal, AttackType.Basic);

            m_GO.GetComponent<IEnemy>().ResetAtkTimer();
        }

        if (moveDir.magnitude > 8)
        {
            m_Owner.StateMachine.SetNextState("StateChase");
        }
    }

    public void ExitState()
    {
    }
}