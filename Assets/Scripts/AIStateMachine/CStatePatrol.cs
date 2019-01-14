using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CStatePatrol : IStateBase
{
    public string StateID
    {
        get { return "StatePatrol"; }
    }

    GameObject m_GO;
    public GameObject GO
    {
        get { return m_GO; }
    }

    Vector2 m_destination;

    public CStatePatrol(GameObject _go)
    {
        m_GO = _go;
        EnterState();
    }

    public void EnterState()
    {
        if (!m_GO.GetComponent<IEnemy>().IsInRoom)
        {
            m_GO.GetComponent<IEnemy>().StateMachine.SetNextState("StateChangeRoom");
            return;
        }

        CTRoom temp = CTDungeon.Instance.Floors[CTDungeon.Instance.currentFloor].GetRoomFromCoord(m_GO.GetComponent<IEnemy>().RoomCoordinate);

        if (temp != null)
            m_destination = temp.RandomPoint;
        else
        {
            m_GO.GetComponent<IEnemy>().StateMachine.SetNextState("StateChangeRoom");
            Debug.Log(m_GO.name + "::EnterState() ERROR. Dungeon.Floor[currentFloor].GetRoomFromCoord() return null");
        }
    }

    public void UpdateState()
    {
        if (m_destination == Vector2.zero)
            m_GO.GetComponent<IEnemy>().StateMachine.SetNextState("StateIdle");
        else
        {
            Vector2 forwardVec = m_destination - (Vector2)m_GO.transform.position;

            //is able to reach next point in this frame
            if (forwardVec.magnitude <= m_GO.GetComponent<IEnemy>().GetStats().MoveSpeed * Time.deltaTime)
            {
                m_GO.transform.Translate(forwardVec.normalized * m_GO.GetComponent<IEnemy>().GetStats().MoveSpeed * Time.deltaTime);
                m_GO.GetComponent<IEnemy>().StateMachine.SetNextState("StateIdle");
            }
            else
                m_GO.transform.Translate(forwardVec.normalized * m_GO.GetComponent<IEnemy>().GetStats().MoveSpeed * Time.deltaTime);

            if ((m_GO.transform.position - GameObject.FindGameObjectWithTag("Player").transform.position).magnitude <= 5)
            {
                m_GO.GetComponent<IEnemy>().Target = GameObject.FindGameObjectWithTag("Player");
                m_GO.GetComponent<IEnemy>().StateMachine.SetNextState("StateChase");
            }
        }
    }

    public void ExitState()
    {
        m_destination = Vector2.zero;
    }
}
