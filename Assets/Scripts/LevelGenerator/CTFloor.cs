using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTFloor
{
    public int columns;                                 // The number of columns on the board (how wide it will be).
    public int rows;                                    // The number of rows on the board (how tall it will be).
    private int m_FloorNum;
    string levelName;

    public bool[][] gameBoard;

    private TileType[][] m_Tiles;                               // A jagged array of tile types representing the board, like a grid.
    private List<CTRoom> m_Rooms;                                     // All the rooms that are created for this board.
    private List<CTCorridor> m_Corridors;                             // All the corridors that connect the rooms.
    private CTRoomCoordinate m_StartingRoom;
    private Vector2 m_StairsForward;
    private Vector2 m_StairsBack;

    public void InitNewLevel(int _floorNum, int _columns, int _rows, int _numRooms, int _gridSize, int _roomWidth, int _roomHeight, int _corridorLength)
    {
        columns = _columns;
        rows = _rows;
        m_FloorNum = _floorNum;

        // Set up Gameboard and Starting Room Coordinates
        m_StartingRoom = new CTRoomCoordinate(0, 0);

        int gameboardColum;
        gameboardColum = _gridSize;
        m_StartingRoom.x = gameboardColum / 2;

        int gameboardRow;
        gameboardRow = _gridSize;
        m_StartingRoom.y = gameboardRow / 2;

        //Debug.Log("ColumsSqrt: " + gameboardColum);
        //Debug.Log("RowsSqrt: " + gameboardRow);
        //Debug.Log("StartingRm: " + m_StartingRoom.x + ", " + m_StartingRoom.y);


        // Initialize GameBoard
        gameBoard = new bool[gameboardColum][];
        for (int i = 0; i < gameBoard.Length; ++i)
        {
            gameBoard[i] = new bool[gameboardRow];
        }


        SetupTilesArray();

        CreateRoomsAndCorridors(_numRooms, _roomWidth, _roomHeight, _corridorLength);

        SetTilesValuesForRooms();
        SetTilesValuesForCorridors();

        SetUpPathNodes();
        SetUpRoomDetector();
        SetUpStairs();
    }

    //public void InitLevelFromSave()
    //{

    //}

    void SetupTilesArray()
    {
        // Set the tiles jagged array to the correct width.
        m_Tiles = new TileType[columns][];

        // Go through all the tile arrays...
        for (int i = 0; i < m_Tiles.Length; i++)
        {
            // ... and set each tile array is the correct height.
            m_Tiles[i] = new TileType[rows];
        }
    }

    void CreateRoomsAndCorridors(int _numRooms, int _roomWidth, int _roomHeight, int _corridorLength)
    {
        // Create the rooms array with a random size.
        m_Rooms = new List<CTRoom>();

        // There should be one less corridor than there is rooms.
        m_Corridors = new List<CTCorridor>();
        Debug.Log("Creating Room and Corridors" + m_Corridors.Count);

        // Create the first room and corridor.
        CTRoom firstRoom = new CTRoom();
        m_Rooms.Add(firstRoom);
        // Setup the first room, RMCount will start from 0
        int totalRooms = firstRoom.SetupAllRoom(columns, rows, _roomWidth, _roomHeight, _corridorLength, m_StartingRoom,
            _numRooms, ref gameBoard, ref m_Rooms, ref m_Corridors);

        Debug.Log("Total Rooms: " + m_Rooms.Count + " GeneratedRooms: " + totalRooms);
    }

    void SetUpPathNodes()
    {
        if (m_Rooms.Count <= 0)
            return;

        GameObject node;

        foreach (CTRoom currRoom in m_Rooms)
        {
            foreach (Direction dir in currRoom.nextCorridors.Keys)
            {
                switch (dir)
                {
                    case Direction.NORTH:
                        if (!currRoom.pathnodes.ContainsKey(dir))
                        {
                            node = Object.Instantiate(Resources.Load("Pathnode"), new Vector3(currRoom.CenterPoint.x, currRoom.yPos + currRoom.roomHeight - 1, 0), Quaternion.identity) as GameObject;
                            node.GetComponent<CPathNode>().Init(currRoom.CenterPoint.x, currRoom.yPos + currRoom.roomHeight, dir, currRoom);
                            currRoom.pathnodes.Add(dir, node.GetComponent<CPathNode>());
                        }
                        break;
                    case Direction.SOUTH:
                        if (!currRoom.pathnodes.ContainsKey(dir))
                        {
                            node = Object.Instantiate(Resources.Load("Pathnode"), new Vector3(currRoom.CenterPoint.x, currRoom.yPos, 0), Quaternion.identity) as GameObject;
                            node.GetComponent<CPathNode>().Init(currRoom.CenterPoint.x, currRoom.yPos, dir, currRoom);
                            currRoom.pathnodes.Add(dir, node.GetComponent<CPathNode>());
                        }
                        break;
                    case Direction.EAST:
                        if (!currRoom.pathnodes.ContainsKey(dir))
                        {
                            node = Object.Instantiate(Resources.Load("Pathnode"), new Vector3(currRoom.xPos + currRoom.roomWidth - 1, currRoom.CenterPoint.y, 0), Quaternion.identity) as GameObject;
                            node.GetComponent<CPathNode>().Init(currRoom.xPos + currRoom.roomWidth, currRoom.CenterPoint.y, dir, currRoom);
                            currRoom.pathnodes.Add(dir, node.GetComponent<CPathNode>());
                        }
                        break;
                    case Direction.WEST:
                        if (!currRoom.pathnodes.ContainsKey(dir))
                        {
                            node = Object.Instantiate(Resources.Load("Pathnode"), new Vector3(currRoom.xPos, currRoom.CenterPoint.y, 0), Quaternion.identity) as GameObject;
                            node.GetComponent<CPathNode>().Init(currRoom.xPos, currRoom.CenterPoint.y, dir, currRoom);
                            currRoom.pathnodes.Add(dir, node.GetComponent<CPathNode>());
                        }
                        break;
                }
            }

            // Check surrounding rooms to add pathnode for
            for (int i = 0; i < (int)Direction.Size; ++i)
            {
                Direction nextDir = (Direction)(i % (int)Direction.Size);
                // Safety Check  if not, create a corridor for the room
                switch (nextDir)
                {
                    case Direction.NORTH:
                        //if the next room will be out of board
                        if (currRoom.coordinate.y + 1 < gameBoard[0].Length)
                            if (gameBoard[currRoom.coordinate.x][currRoom.coordinate.y + 1])
                            {
                                CTRoom otherRm = GetRoomFromCoord(new CTRoomCoordinate(currRoom.coordinate.x, currRoom.coordinate.y + 1));
                                if (otherRm == null)
                                {
                                    Debug.Log("Room " + currRoom.coordinate.x + " " + (currRoom.coordinate.y + 1) + " is null");
                                    continue;
                                }
                                if (otherRm.nextCorridors.ContainsKey(Direction.SOUTH))
                                    if (!currRoom.pathnodes.ContainsKey(nextDir))
                                    {
                                        node = Object.Instantiate(Resources.Load("Pathnode"), new Vector3(currRoom.CenterPoint.x, currRoom.yPos + currRoom.roomHeight - 1, 0), Quaternion.identity) as GameObject;
                                        node.GetComponent<CPathNode>().Init(currRoom.CenterPoint.x, currRoom.yPos + currRoom.roomHeight, nextDir, currRoom);
                                        currRoom.pathnodes.Add(nextDir, node.GetComponent<CPathNode>());
                                        break;
                                    }
                            }
                        continue;
                    case Direction.SOUTH:
                        if (currRoom.coordinate.y - 1 >= 0)
                            if (gameBoard[currRoom.coordinate.x][currRoom.coordinate.y - 1])
                            {
                                CTRoom otherRm = GetRoomFromCoord(new CTRoomCoordinate(currRoom.coordinate.x, currRoom.coordinate.y - 1));
                                if (otherRm == null)
                                {
                                    Debug.Log("Room " + currRoom.coordinate.x + " " + (currRoom.coordinate.y - 1) + " is null");
                                    continue;
                                }
                                if (otherRm.nextCorridors.ContainsKey(Direction.NORTH))
                                    if (!currRoom.pathnodes.ContainsKey(nextDir))
                                    {
                                        node = Object.Instantiate(Resources.Load("Pathnode"), new Vector3(currRoom.CenterPoint.x, currRoom.yPos, 0), Quaternion.identity) as GameObject;
                                        node.GetComponent<CPathNode>().Init(currRoom.CenterPoint.x, currRoom.yPos, nextDir, currRoom);
                                        currRoom.pathnodes.Add(nextDir, node.GetComponent<CPathNode>());
                                        break;
                                    }
                            }
                        continue;
                    case Direction.EAST:
                        if (currRoom.coordinate.x + 1 < gameBoard.Length)
                            if (gameBoard[currRoom.coordinate.x + 1][currRoom.coordinate.y])
                            {
                                CTRoom otherRm = GetRoomFromCoord(new CTRoomCoordinate(currRoom.coordinate.x + 1, currRoom.coordinate.y));
                                if (otherRm == null)
                                {
                                    Debug.Log("Room " + (currRoom.coordinate.x + 1) + " " + currRoom.coordinate.y + " is null");
                                    continue;
                                }
                                if (otherRm.nextCorridors.ContainsKey(Direction.WEST))
                                    if (!currRoom.pathnodes.ContainsKey(nextDir))
                                    {
                                        node = Object.Instantiate(Resources.Load("Pathnode"), new Vector3(currRoom.xPos + currRoom.roomWidth - 1, currRoom.CenterPoint.y, 0), Quaternion.identity) as GameObject;
                                        node.GetComponent<CPathNode>().Init(currRoom.xPos + currRoom.roomWidth, currRoom.CenterPoint.y, nextDir, currRoom);
                                        currRoom.pathnodes.Add(nextDir, node.GetComponent<CPathNode>());
                                        break;
                                    }
                            }
                        continue;
                    case Direction.WEST:
                        if (currRoom.coordinate.x - 1 >= 0)
                            if (gameBoard[currRoom.coordinate.x - 1][currRoom.coordinate.y])
                            {
                                CTRoom otherRm = GetRoomFromCoord(new CTRoomCoordinate(currRoom.coordinate.x - 1, currRoom.coordinate.y));
                                if (otherRm == null)
                                {
                                    Debug.Log("Room " + (currRoom.coordinate.x - 1) + " " + currRoom.coordinate.y + " is null");
                                    continue;
                                }
                                if (otherRm.nextCorridors.ContainsKey(Direction.EAST))
                                    if (!currRoom.pathnodes.ContainsKey(nextDir))
                                    {
                                        node = Object.Instantiate(Resources.Load("Pathnode"), new Vector3(currRoom.xPos, currRoom.CenterPoint.y, 0), Quaternion.identity) as GameObject;
                                        node.GetComponent<CPathNode>().Init(currRoom.xPos, currRoom.CenterPoint.y, nextDir, currRoom);
                                        currRoom.pathnodes.Add(nextDir, node.GetComponent<CPathNode>());
                                        break;
                                    }
                            }
                        continue;
                }

            }
        }
    }

    void SetUpRoomDetector()
    {
        GameObject detector;

        foreach (var currRoom in m_Rooms)
        {
            detector = Object.Instantiate(Resources.Load("RoomDetector"), new Vector3(currRoom.CenterPoint.x, currRoom.CenterPoint.y, 0), Quaternion.identity) as GameObject;
            detector.transform.localScale = new Vector3(currRoom.roomWidth, currRoom.roomHeight, 1);
            if (detector.GetComponent<RoomCoordinateSetter>())
                detector.GetComponent<RoomCoordinateSetter>().Init(currRoom.coordinate);
        }
    }

    void SetUpStairs(bool _goForward = true, bool _goBack = false)
    {
        if (_goBack)
            m_StairsBack = m_Rooms[0].RandomPoint;
        else
            m_StairsBack = new Vector2(0, 0);

        if (_goForward)
            m_StairsForward = m_Rooms[m_Rooms.Count - 1].RandomPoint;
        else
            m_StairsForward = new Vector2(0, 0);
    }

    void SetTilesValuesForRooms()
    {
        // Go through all the rooms...
        for (int i = 0; i < m_Rooms.Count; ++i)
        {
            CTRoom currentRoom = m_Rooms[i];
            if (!currentRoom.generated)
                continue;

            // ... and for each room go through it's width.
            for (int j = -1; j <= currentRoom.roomWidth; ++j)
            {
                int xCoord = currentRoom.xPos + j;

                // For each horizontal tile, go up vertically through the room's height.
                for (int k = -1; k <= currentRoom.roomHeight; k++)
                {
                    int yCoord = currentRoom.yPos + k;

                    if (yCoord < 0)
                        yCoord = 0;

                    //Set wall
                    if (k < 0)
                    {
                        if (j < 0)
                        {
                            m_Tiles[xCoord][yCoord] = TileType.WallInnerCorner_Q3;
                            continue;
                        }
                        else if (j == currentRoom.roomWidth)
                        {
                            m_Tiles[xCoord][yCoord] = TileType.WallInnerCorner_Q4;
                            continue;
                        }

                        m_Tiles[xCoord][yCoord] = TileType.Wall_Down;
                        continue;
                    }
                    if (k >= currentRoom.roomHeight)
                    {
                        if (j < 0)
                        {
                            m_Tiles[xCoord][yCoord] = TileType.WallInnerCorner_Q2;
                            continue;
                        }
                        else if (j == currentRoom.roomWidth)
                        {
                            m_Tiles[xCoord][yCoord] = TileType.WallInnerCorner_Q1;
                            continue;
                        }

                        m_Tiles[xCoord][yCoord] = TileType.Wall_Up;
                        continue;
                    }
                    if (j < 0)
                    {
                        if (k < 0)
                        {
                            m_Tiles[xCoord][yCoord] = TileType.WallInnerCorner_Q3;
                            continue;
                        }
                        else if (k == currentRoom.roomHeight)
                        {
                            m_Tiles[xCoord][yCoord] = TileType.WallInnerCorner_Q2;
                            continue;
                        }

                        m_Tiles[xCoord][yCoord] = TileType.Wall_Left;
                        continue;
                    }
                    if (j >= currentRoom.roomWidth)
                    {
                        if (k < 0)
                        {
                            m_Tiles[xCoord][yCoord] = TileType.WallInnerCorner_Q4;
                            continue;
                        }
                        else if (k == currentRoom.roomHeight)
                        {
                            m_Tiles[xCoord][yCoord] = TileType.WallInnerCorner_Q1;
                            continue;
                        }

                        m_Tiles[xCoord][yCoord] = TileType.Wall_Right;
                        continue;
                    }

                    m_Tiles[xCoord][yCoord] = TileType.Floor;
                }
            }
        }
    }

    void SetTilesValuesForCorridors()
    {
        // Go through every corridor...
        for (int i = 0; i < m_Corridors.Count; ++i)
        {
            CTCorridor currentCorridor = m_Corridors[i];

            // and go through it's length.
            for (int j = 0; j < currentCorridor.corridorLength; ++j)
            {
                // Start the coordinates at the start of the corridor.
                int xCoord = currentCorridor.startXPos;
                int yCoord = currentCorridor.startYPos;

                // Depending on the direction, add or subtract from the appropriate
                // coordinate based on how far through the length the loop is.

                //Starting Corners
                if (j == 0)
                    switch (currentCorridor.direction)
                    {
                        case Direction.NORTH:
                            yCoord += j;
                            m_Tiles[xCoord - 1][yCoord] = TileType.WallOuterCorner_Q2;
                            m_Tiles[xCoord + 1][yCoord] = TileType.WallOuterCorner_Q1;
                            break;
                        case Direction.EAST:
                            xCoord += j;
                            m_Tiles[xCoord][yCoord + 1] = TileType.WallOuterCorner_Q1;
                            m_Tiles[xCoord][yCoord - 1] = TileType.WallOuterCorner_Q4;
                            break;
                        case Direction.SOUTH:
                            yCoord -= j;
                            m_Tiles[xCoord - 1][yCoord] = TileType.WallOuterCorner_Q3;
                            m_Tiles[xCoord + 1][yCoord] = TileType.WallOuterCorner_Q4;
                            break;
                        case Direction.WEST:
                            xCoord -= j;
                            m_Tiles[xCoord][yCoord + 1] = TileType.WallOuterCorner_Q2;
                            m_Tiles[xCoord][yCoord - 1] = TileType.WallOuterCorner_Q3;
                            break;
                    }
                //Ending Corner
                else if (j == currentCorridor.corridorLength - 1)
                    switch (currentCorridor.direction)
                    {
                        case Direction.NORTH:
                            yCoord += j;
                            m_Tiles[xCoord - 1][yCoord] = TileType.WallOuterCorner_Q3;
                            m_Tiles[xCoord + 1][yCoord] = TileType.WallOuterCorner_Q4;
                            break;
                        case Direction.EAST:
                            xCoord += j;
                            m_Tiles[xCoord][yCoord + 1] = TileType.WallOuterCorner_Q2;
                            m_Tiles[xCoord][yCoord - 1] = TileType.WallOuterCorner_Q3;
                            break;
                        case Direction.SOUTH:
                            yCoord -= j;
                            m_Tiles[xCoord - 1][yCoord] = TileType.WallOuterCorner_Q2;
                            m_Tiles[xCoord + 1][yCoord] = TileType.WallOuterCorner_Q1;
                            break;
                        case Direction.WEST:
                            xCoord -= j;
                            m_Tiles[xCoord][yCoord + 1] = TileType.WallOuterCorner_Q1;
                            m_Tiles[xCoord][yCoord - 1] = TileType.WallOuterCorner_Q4;
                            break;
                    }
                //Side walls
                else
                    switch (currentCorridor.direction)
                    {
                        case Direction.NORTH:
                            yCoord += j;
                            m_Tiles[xCoord - 1][yCoord] = TileType.Wall_Left;
                            m_Tiles[xCoord + 1][yCoord] = TileType.Wall_Right;
                            break;
                        case Direction.EAST:
                            xCoord += j;
                            m_Tiles[xCoord][yCoord + 1] = TileType.Wall_Up;
                            m_Tiles[xCoord][yCoord - 1] = TileType.Wall_Down;
                            break;
                        case Direction.SOUTH:
                            yCoord -= j;
                            m_Tiles[xCoord - 1][yCoord] = TileType.Wall_Left;
                            m_Tiles[xCoord + 1][yCoord] = TileType.Wall_Right;
                            break;
                        case Direction.WEST:
                            xCoord -= j;
                            m_Tiles[xCoord][yCoord + 1] = TileType.Wall_Up;
                            m_Tiles[xCoord][yCoord - 1] = TileType.Wall_Down;
                            break;
                    }
                //Floor
                m_Tiles[xCoord][yCoord] = TileType.Floor;
            }
        }
    }

    public CTRoom GetRoomFromCoord(CTRoomCoordinate _coord)
    {
        foreach (CTRoom currRm in m_Rooms)
        {
            if (currRm.coordinate.sameAs(_coord))
                return currRm;
        }
        return null;
    }

    public List<CTRoom> GetRooms()
    { return m_Rooms; }

    public TileType[][] GetTiles()
    { return m_Tiles; }

    public Vector2 StairsForward
    {
        get { return m_StairsForward; }
    }

    public Vector2 StairsBack
    {
        get { return m_StairsBack; }
    }

    public string Name
    {
        get
        { return levelName; }

        set
        { levelName = value; }
    }

    //public void SaveLevel()
    //{
    //    string temp = "";
    //    for (int i = 0; i < tiles.Length; i++)
    //    {
    //        for (int j = 0; j <tiles[i].Length; j++)
    //        {
    //            temp = temp + tiles[i][j].ToString();
    //        }
    //        temp = temp + "/";
    //    }
    //    //Create a line of 0 and 1 for each  of the level
    //    PlayerPrefs.SetString(levelName + "_data", temp);
    //    PlayerPrefs.SetString(levelName + "_row", rows.ToString());
    //    PlayerPrefs.SetString(levelName + "_column", columns.ToString());
    //}
}

