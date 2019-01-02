﻿using System.Collections;
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

    public Queue<CPathNode> BFS_ToRoom(CTRoomCoordinate _startRoom, CTRoomCoordinate _destRoom)
    {
        if (_startRoom.sameAs(_destRoom))
            return null;

        CTFloor currFloor = Floors[currentFloor];
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
                    nextnode1 = mShortestPath[i - 1].GetPathnode(Direction.NORTH);
                    if (nextnode1 == null)
                        return pathList;
                    pathList.Enqueue(nextnode1);

                    nextnode2 = mShortestPath[i].GetPathnode(Direction.SOUTH);
                    if (nextnode2 == null)
                        return pathList;
                    pathList.Enqueue(nextnode2);
                    break;
                case Direction.SOUTH:
                    nextnode1 = mShortestPath[i - 1].GetPathnode(Direction.SOUTH);
                    if (nextnode1 == null)
                        return pathList;
                    pathList.Enqueue(nextnode1);

                    nextnode2 = mShortestPath[i].GetPathnode(Direction.NORTH);
                    if (nextnode2 == null)
                        return pathList;
                    pathList.Enqueue(nextnode2);
                    break;
                case Direction.EAST:
                    nextnode1 = mShortestPath[i - 1].GetPathnode(Direction.EAST);
                    if (nextnode1 == null)
                        return pathList;
                    pathList.Enqueue(nextnode1);

                    nextnode2 = mShortestPath[i].GetPathnode(Direction.WEST);
                    if (nextnode2 == null)
                        return pathList;
                    pathList.Enqueue(nextnode2);
                    break;
                case Direction.WEST:
                    nextnode1 = mShortestPath[i - 1].GetPathnode(Direction.WEST);
                    if (nextnode1 == null)
                        return pathList;
                    pathList.Enqueue(nextnode1);

                    nextnode2 = mShortestPath[i].GetPathnode(Direction.EAST);
                    if (nextnode2 == null)
                        return pathList;
                    pathList.Enqueue(nextnode2);
                    break;
            }
        }

        return pathList;
    }
}