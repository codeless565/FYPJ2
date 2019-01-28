using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CStateBossTeleport : IStateBase
{
    public string StateID
    {
        get { return "StateTeleport"; }
    }

    GameObject m_GO;
    public GameObject GO
    {
        get { return m_GO; }
    }

    IEnemy m_Owner;
    Vector2 m_Destination;
    CTRoom m_RoomInfo;

    float m_BounceTime;
    Color m_originalColor;

    public CStateBossTeleport(GameObject _go, CTRoom _roomInfo)
    {
        m_GO = _go;
        m_Owner = m_GO.GetComponent<IEnemy>();
        m_RoomInfo = _roomInfo;
        m_Destination = m_RoomInfo.CenterPoint;
    }

    public void EnterState()
    {
        m_originalColor = m_GO.GetComponent<SpriteRenderer>().color;

        Vector2 targetPos = m_Owner.Target.transform.position;
        m_Destination = new Vector2(targetPos.x + Random.Range(-3.0f, 3.0f), targetPos.y + Random.Range(-3.0f, 3.0f));
        if (m_Destination.x < m_RoomInfo.xPos)
            m_Destination.x = m_RoomInfo.xPos;
        if (m_Destination.x > m_RoomInfo.xPos + m_RoomInfo.roomWidth - 1)
            m_Destination.x = m_RoomInfo.xPos + m_RoomInfo.roomWidth - 1;
        if (m_Destination.y < m_RoomInfo.yPos)
            m_Destination.y = m_RoomInfo.yPos;
        if (m_Destination.y > m_RoomInfo.yPos + m_RoomInfo.roomHeight - 1)
            m_Destination.y = m_RoomInfo.yPos + m_RoomInfo.roomHeight - 1;

        Debug.Log("Teleportation Dest: " + m_Destination);

        m_GO.transform.position = m_Destination;
        m_GO.GetComponent<SpriteRenderer>().color = Color.gray;
        m_BounceTime = 0.0f;
    }

    public void UpdateState()
    {
        m_BounceTime += Time.deltaTime;

        if (m_BounceTime >= 0.5f)
        {
            m_GO.GetComponent<SpriteRenderer>().color = m_originalColor;
            m_Owner.StateMachine.SetNextState("StateChase");
        }
    }

    public void ExitState()
    {
    }
}