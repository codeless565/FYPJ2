using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CStateAttackRanged : IStateBase
{
    GameObject Bullet = (GameObject)Resources.Load("Projectiles/EnemyNote");

    public string StateID
    {
        get { return "StateAttack"; }
    }

    GameObject m_GO;
    public GameObject GO
    {
        get { return m_GO; }
    }

    public CStateAttackRanged(GameObject _go)
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
            GameObject newBullet = Object.Instantiate(Bullet, m_GO.transform.position, Quaternion.identity);
            newBullet.GetComponent<IProjectile>().Init(
                m_GO.GetComponent<IEnemy>().GetStats().Attack, 10, 10, 
                ((Vector2)m_GO.GetComponent<IEnemy>().Target.transform.position - (Vector2)m_GO.transform.position).normalized, 
                ProjectileType.Normal, AttackType.Basic);

            m_GO.GetComponent<IEnemy>().ResetAtkTimer();
        }

        if (((Vector2)m_GO.GetComponent<IEnemy>().Target.transform.position - (Vector2)m_GO.transform.position).magnitude > 3)
        {
            m_GO.GetComponent<IEnemy>().StateMachine.SetNextState("StateChase");
        }
    }

    public void ExitState()
    {
    }
}
