using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTDungeon
{
    public bool CheckpointRed;
    public bool CheckpointPink;

    public int currentFloor;

    public int BossFloorRed
    {
        get { return 25; }
    }

    public int BossFloorPink
    {
        get { return 50; }
    }

    private static CTDungeon instance;
    private Dictionary<int, IFloor> floors;

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
        floors = new Dictionary<int, IFloor>();
        currentFloor = -1;
        CheckpointRed = false;
        CheckpointPink = false;
    }

    public IFloor GetFloorData(int _floorNumber, bool _isBossLevel = false)
    {
        if (floors.ContainsKey(_floorNumber))
            return floors[_floorNumber];

        IFloor temp;

        //No such floor for this level exists
        if (_isBossLevel)
        {
            temp = new CBossFloor();
            floors.Add(_floorNumber, temp);
        }
        else
        {
            temp = new CTFloor();
            floors.Add(_floorNumber, temp);
        }

        return temp;
    }

    public Dictionary<int, IFloor> Floors
    {
        get
        {
            return floors;
        }
    }

    public Queue<CPathNode> BFS_ToRoom(CTRoomCoordinate _startRoom, CTRoomCoordinate _destRoom)
    {
        if (_startRoom.sameAs(_destRoom))
            return null;

        CTFloor currFloor;

        if (floors[currentFloor] is CTFloor)
        {
            currFloor = floors[currentFloor] as CTFloor;
        }
        else
            return null;

        bool[][] m_gameBoard = currFloor.gameBoard;
        int boardColumn = m_gameBoard.Length;
        int boardRow = m_gameBoard[0].Length;

        bool[] mVisited = new bool[boardColumn * boardRow];
        CTRoomCoordinate[] mPrevious = new CTRoomCoordinate[boardColumn * boardRow];

        List<CTRoom> mShortestPath = new List<CTRoom>();
        Queue<CTRoomCoordinate> mQueue = new Queue<CTRoomCoordinate>();
        mQueue.Enqueue(_startRoom);

        while (mQueue.Count > 0)
        {
            CTRoomCoordinate curr = mQueue.Dequeue();
            if (curr.sameAs(_destRoom))
            {
                while (!curr.sameAs(_startRoom))
                {
                    mShortestPath.Insert(0, currFloor.GetRoomFromCoord(curr));
                    curr = mPrevious[curr.y * boardRow + curr.x];
                }
                mShortestPath.Insert(0, currFloor.GetRoomFromCoord(curr));
                break;
            }
            //Up
            if (curr.y < boardRow - 1)
            {
                CTRoomCoordinate next = new CTRoomCoordinate(curr.x, curr.y + 1);
                if (m_gameBoard[next.x][next.y])
                    if (!mVisited[next.y * boardRow + next.x])
                    {
                        mPrevious[next.y * boardRow + next.x] = curr;
                        mQueue.Enqueue(next);
                        mVisited[next.y * boardRow + next.x] = true;
                    }
            }
            //Down
            if (curr.y > 0)
            {
                CTRoomCoordinate next = new CTRoomCoordinate(curr.x, curr.y - 1);
                if (m_gameBoard[next.x][next.y])
                    if (!mVisited[next.y * boardRow + next.x])
                    {
                        mPrevious[next.y * boardRow + next.x] = curr;
                        mQueue.Enqueue(next);
                        mVisited[next.y * boardRow + next.x] = true;
                    }
            }
            //Right
            if (curr.x < boardColumn - 1)
            {
                CTRoomCoordinate next = new CTRoomCoordinate(curr.x + 1, curr.y);
                if (m_gameBoard[next.x][next.y])
                    if (!mVisited[next.y * boardRow + next.x])
                    {
                        mPrevious[next.y * boardRow + next.x] = curr;
                        mQueue.Enqueue(next);
                        mVisited[next.y * boardRow + next.x] = true;
                    }
            }
            //Up
            if (curr.x > 0)
            {
                CTRoomCoordinate next = new CTRoomCoordinate(curr.x - 1, curr.y);
                if (m_gameBoard[next.x][next.y])
                    if (!mVisited[next.y * boardRow + next.x])
                    {
                        mPrevious[next.y * boardRow + next.x] = curr;
                        mQueue.Enqueue(next);
                        mVisited[next.y * boardRow + next.x] = true;
                    }
            }
        }

        // No path found
        if (mShortestPath.Count <= 0)
            return null;

        Queue<CPathNode> pathList = new Queue<CPathNode>();

        for (int i = 1; i < mShortestPath.Count; ++i)
        {
            CPathNode nextnode1;
            CPathNode nextnode2;

            switch (mShortestPath[i - 1].coordinate.directionTo(mShortestPath[i].coordinate))
            {
                case Direction.NORTH:
                    nextnode1 = mShortestPath[i - 1].GetPathnode(PathNodeDir.NorthL);
                    if (nextnode1 == null)
                        return pathList;
                    pathList.Enqueue(nextnode1);

                    nextnode2 = mShortestPath[i].GetPathnode(PathNodeDir.SouthL);
                    if (nextnode2 == null)
                        return pathList;
                    pathList.Enqueue(nextnode2);
                    break;
                case Direction.SOUTH:
                    nextnode1 = mShortestPath[i - 1].GetPathnode(PathNodeDir.SouthR);
                    if (nextnode1 == null)
                        return pathList;
                    pathList.Enqueue(nextnode1);

                    nextnode2 = mShortestPath[i].GetPathnode(PathNodeDir.NorthR);
                    if (nextnode2 == null)
                        return pathList;
                    pathList.Enqueue(nextnode2);
                    break;
                case Direction.EAST:
                    nextnode1 = mShortestPath[i - 1].GetPathnode(PathNodeDir.EastU);
                    if (nextnode1 == null)
                        return pathList;
                    pathList.Enqueue(nextnode1);

                    nextnode2 = mShortestPath[i].GetPathnode(PathNodeDir.WestU);
                    if (nextnode2 == null)
                        return pathList;
                    pathList.Enqueue(nextnode2);
                    break;
                case Direction.WEST:
                    nextnode1 = mShortestPath[i - 1].GetPathnode(PathNodeDir.WestD);
                    if (nextnode1 == null)
                        return pathList;
                    pathList.Enqueue(nextnode1);

                    nextnode2 = mShortestPath[i].GetPathnode(PathNodeDir.EastD);
                    if (nextnode2 == null)
                        return pathList;
                    pathList.Enqueue(nextnode2);
                    break;
            }
        }

        return pathList;
    }
}