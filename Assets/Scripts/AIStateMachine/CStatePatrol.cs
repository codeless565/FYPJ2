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

    IEnemy m_Owner;

    Vector2 m_destination;

    public CStatePatrol(GameObject _go)
    {
        m_GO = _go;
        m_Owner = m_GO.GetComponent<IEnemy>();
        EnterState();
    }

    public void EnterState()
    {
        if (!m_Owner.IsInRoom)
        {
            m_Owner.StateMachine.SetNextState("StateChangeRoom");
            return;
        }

        CTRoom temp = CTDungeon.Instance.Floors[CTDungeon.Instance.currentFloor].GetRoomFromCoord(m_Owner.RoomCoordinate);

        if (temp != null)
            m_destination = temp.RandomPoint;
        else
        {
            m_Owner.StateMachine.SetNextState("StateChangeRoom");
            Debug.Log(m_GO.name + "::EnterState() ERROR. Dungeon.Floor[currentFloor].GetRoomFromCoord() return null");
        }
    }

    public void UpdateState()
    {
        if (m_destination == Vector2.zero)
            m_Owner.StateMachine.SetNextState("StateIdle");
        else
        {
            Vector2 forwardVec = m_destination - (Vector2)m_GO.transform.position;

            //is able to reach next point in this frame
            if (forwardVec.magnitude <= m_Owner.GetStats().MoveSpeed * Time.deltaTime)
            {
                m_GO.transform.Translate(forwardVec.normalized * m_Owner.GetStats().MoveSpeed * Time.deltaTime);
                m_Owner.StateMachine.SetNextState("StateIdle");
            }
            else
                m_GO.transform.Translate(forwardVec.normalized * m_Owner.GetStats().MoveSpeed * Time.deltaTime);

            if ((m_GO.transform.position - GameObject.FindGameObjectWithTag("Player").transform.position).magnitude <= 5)
            {
                m_Owner.Target = GameObject.FindGameObjectWithTag("Player");
                m_Owner.StateMachine.SetNextState("StateChase");
            }
        }
    }

    public void ExitState()
    {
        m_destination = Vector2.zero;
    }
}
