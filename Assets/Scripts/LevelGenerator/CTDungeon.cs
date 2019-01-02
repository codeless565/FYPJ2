using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTDungeon
{
    public int currentFloor;

    private static CTDungeon instance;
    private Dictionary<int, CTFloor> floors;

    public static CTDungeon Instance
    {
        get
        {
            if (instance == null)
                instance = new CTDungeon();
            return instance;
        }
    }

    private CTDungeon()
    {
        floors = new Dictionary<int, CTFloor>();
        currentFloor = -1;
    }

    public void AddNewFloor(int _floorNum, CTFloor _newFloor)
    {
        floors.Add(_floorNum, _newFloor);
    }

    public Dictionary<int, CTFloor> Floors
    {
        get
        {
            return floors;
        }
    }

    //public List<CPathNode> BFS_ToRoom(CTRoomCoordinate _startRoom, CTRoomCoordinate _destRoom)
    //{
    //    if (_startRoom.sameAs(_destRoom))
    //        return null;

    //    bool[][] m_gameBoard = Floors[currentFloor].gameBoard;
    //    int      boardColumn = m_gameBoard.Length;
    //    int      boardRow    = m_gameBoard.Length;

    //    List<bool>             mVisited  = new List<bool>(boardColumn * boardRow);
    //    List<CTRoomCoordinate> mPrevious = new List<CTRoomCoordinate>(boardColumn * boardRow);

    //    List<CTRoom> mShortestPath = new List<CTRoom>();
    //    Queue<CTRoomCoordinate> mQueue = new Queue<CTRoomCoordinate>();
    //    mQueue.Enqueue(_startRoom);
        
    //    while (mQueue.Count > 0)
    //    {
    //        CTRoomCoordinate curr = mQueue.Dequeue();
    //        if (curr.sameAs(_destRoom))
    //        {
    //            while (!curr.sameAs(_startRoom))
    //            {
    //                foreach(CTRoom currRoom in floors[currentFloor].GetRooms())
    //                {
    //                    if (currRoom.coordinate.sameAs(curr))
    //                    {
    //                        mShortestPath.Insert(0, currRoom);
    //                        curr = mPrevious[curr.y * boardRow + curr.x];
    //                    }
    //                }
    //            }
    //            mShortestPath.Insert(0, currRoom);
    //            break;
    //        }
    //        //Up
    //        if (curr.y < boardRow - 1)
    //        {
    //            CTRoomCoordinate next = new CTRoomCoordinate(curr.x, curr.y + 1);
    //            if (m_gameBoard[next.x][next.y])
    //                if (!mVisited[next.y *boardRow + next.x])
    //                {
    //                    mPrevious[next.y * boardRow + next.x] = curr;
    //                    mQueue.Enqueue(next);
    //                    mVisited[next.y * boardRow + next.x] = true;
    //                }
    //        }
    //        //Down
    //        if (curr.y > 0)
    //        {
    //            CTRoomCoordinate next = new CTRoomCoordinate(curr.x, curr.y - 1);
    //            if (m_gameBoard[next.x][next.y])
    //                if (!mVisited[next.y * boardRow + next.x])
    //                {
    //                    mPrevious[next.y * boardRow + next.x] = curr;
    //                    mQueue.Enqueue(next);
    //                    mVisited[next.y * boardRow + next.x] = true;
    //                }
    //        }
    //        //Right
    //        if (curr.x < boardColumn - 1)
    //        {
    //            CTRoomCoordinate next = new CTRoomCoordinate(curr.x + 1, curr.y);
    //            if (m_gameBoard[next.x][next.y])
    //                if (!mVisited[next.y * boardRow + next.x])
    //                {
    //                    mPrevious[next.y * boardRow + next.x] = curr;
    //                    mQueue.Enqueue(next);
    //                    mVisited[next.y * boardRow + next.x] = true;
    //                }
    //        }
    //        //Up
    //        if (curr.x > 0)
    //        {
    //            CTRoomCoordinate next = new CTRoomCoordinate(curr.x - 1, curr.y);
    //            if (m_gameBoard[next.x][next.y])
    //                if (!mVisited[next.y * boardRow + next.x])
    //                {
    //                    mPrevious[next.y * boardRow + next.x] = curr;
    //                    mQueue.Enqueue(next);
    //                    mVisited[next.y * boardRow + next.x] = true;
    //                }
    //        }
    //    }

    //    // No path found
    //    if (mShortestPath.Count <= 0)
    //        return null;

    //    List<CPathNode> pathList = new List<CPathNode>();

    //    for (int i = 0;  i < mShortestPath.Count; ++i)
    //    {
    //        if (i + 1 < mShortestPath.Count)
    //        {
    //            switch (mShortestPath[i].directionTo(mShortestPath[i+1]))
    //            {
    //                case Direction.NORTH:

    //                    break;
    //                case Direction.SOUTH:
    //                    break;
    //                case Direction.EAST:
    //                    break;
    //                case Direction.WEST:
    //                    break;
    //            }
    //        }

    //    }

    //    return pathList;
    //}
}