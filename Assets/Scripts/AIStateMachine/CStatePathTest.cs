using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CStatePathTest : IStateBase
{
    public GameObject m_GO;

    //unique var
    Queue<CPathNode> m_Pathing;
    CPathNode nextDest;

    public string StateID
    {
        get { return "StatePathTest"; }
    }

    public GameObject GO
    {
        get { return m_GO; }
    }

    IEnemy m_Owner;

    public CStatePathTest(GameObject _gameObj)
    {
        m_GO = _gameObj;
        m_Owner = m_GO.GetComponent<IEnemy>();
        m_Pathing = new Queue<CPathNode>();

        EnterState();
    }

    public void EnterState()
    {
        Debug.Log("CStatePath::EnterState() called");

        List<CTRoom> rmList = CTDungeon.Instance.Floors[CTDungeon.Instance.currentFloor].Rooms;
        int randomDestIndex = Random.Range(0, rmList.Count - 1);

        m_Pathing = CTDungeon.Instance.BFS_ToRoom(m_Owner.RoomCoordinate, rmList[randomDestIndex].coordinate);
        if (m_Pathing != null)
            nextDest = m_Pathing.Dequeue();
    }

    public void UpdateState()
    {
        Debug.Log("CStatePath::UpdateState() called");
        if (nextDest == null)
        {
            newPathing();
            nextDest = m_Pathing.Dequeue();
        }

        if (nextDest == null)
            return;

        m_GO.transform.Translate((nextDest.position - (Vector2)m_GO.transform.position).normalized * m_Owner.GetStats().MoveSpeed * Time.deltaTime);

        if ((nextDest.position - (Vector2)m_GO.transform.position).magnitude <= m_Owner.GetStats().MoveSpeed * Time.deltaTime)
        {
            if (m_Pathing.Count <= 0)
            {
                newPathing();
            }
            nextDest = m_Pathing.Dequeue();
        }
    }

    public void ExitState()
    {
        Debug.Log("CStatePath::ExitState() called");
    }

    private void newPathing()
    {
        List<CTRoom> rmList = CTDungeon.Instance.Floors[CTDungeon.Instance.currentFloor].Rooms;
        int randomDestIndex = Random.Range(0, rmList.Count - 1);

        m_Pathing = CTDungeon.Instance.BFS_ToRoom(m_Owner.RoomCoordinate, rmList[randomDestIndex].coordinate);
        if (m_Pathing == null)
            newPathing();
    }
}
