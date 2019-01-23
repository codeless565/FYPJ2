using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CStateChangeRoom : IStateBase
{
    public string StateID
    {
        get { return "StateChangeRoom"; }
    }

    GameObject m_GO;
    public GameObject GO
    {
        get { return m_GO; }
    }

    Queue<CPathNode> m_Pathing;
    CPathNode nextDest;

    Vector3 m_lastknowposition;
    float m_blockedTime;

    public CStateChangeRoom(GameObject _go)
    {
        m_GO = _go;
        EnterState();
    }

    public void EnterState()
    {
        List<CTRoom> rmList = CTDungeon.Instance.Floors[CTDungeon.Instance.currentFloor].Rooms;
        int randomDestIndex = Random.Range(0, rmList.Count - 1);

        m_Pathing = CTDungeon.Instance.BFS_ToRoom(m_GO.GetComponent<IEntity>().RoomCoordinate, rmList[randomDestIndex].coordinate);
        if (m_Pathing != null)
        {
            if (m_Pathing.Count > 0)
                nextDest = m_Pathing.Dequeue();
        }
        else
            m_GO.GetComponent<IEnemy>().StateMachine.SetNextState("StateIdle");

        m_lastknowposition = m_GO.gameObject.transform.position;
    }

    public void UpdateState()
    {
        if (nextDest == null || m_Pathing == null)
        {
            m_GO.GetComponent<IEnemy>().StateMachine.SetNextState("StateIdle");
            return;
        }

        if ((m_GO.transform.position - GameObject.FindGameObjectWithTag("Player").transform.position).magnitude <= 5)
        {
            Debug.Log("Dist to plyer = " + (m_GO.transform.position - GameObject.FindGameObjectWithTag("Player").transform.position).magnitude);
            m_GO.GetComponent<IEnemy>().Target = GameObject.FindGameObjectWithTag("Player");
            m_GO.GetComponent<IEnemy>().StateMachine.SetNextState("StateChase");
        }

        //Move to next point
        Vector2 forwardVec = nextDest.position - (Vector2)m_GO.transform.position;

        //Reaching next point in the this frame
        if (forwardVec.magnitude <= m_GO.GetComponent<IEntity>().GetStats().MoveSpeed * Time.deltaTime)
        {
            m_GO.transform.Translate(forwardVec.normalized * m_GO.GetComponent<IEntity>().GetStats().MoveSpeed * Time.deltaTime);
            if (m_Pathing.Count > 0) //still have more point till final point
                nextDest = m_Pathing.Dequeue();
            else //Reached
                m_GO.GetComponent<IEnemy>().StateMachine.SetNextState("StatePatrol");
        }
        else
            m_GO.transform.Translate(forwardVec.normalized * m_GO.GetComponent<IEntity>().GetStats().MoveSpeed * Time.deltaTime);

        //Make way if it is blocked
        if ((m_lastknowposition - m_GO.gameObject.transform.position).magnitude < 1)
        {
            m_blockedTime += Time.deltaTime;
            if (m_blockedTime >= 1)
            {
                Vector2 makeleft = -Vector3.Cross(forwardVec, Vector3.forward).normalized;
                m_GO.transform.Translate(makeleft * m_GO.GetComponent<IEntity>().GetStats().MoveSpeed * Time.deltaTime);
                Debug.Log(m_GO.name +" making way: " + makeleft);
            }
        }
        else
        {
            m_lastknowposition = m_GO.gameObject.transform.position;
            m_blockedTime = 0;
        }
    }

    public void ExitState()
    {
    }
}
