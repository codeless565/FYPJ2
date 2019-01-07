using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBossFloor : IFloor
{
    private bool m_isGenerated;
    public int columns;                                 // The number of columns on the board (how wide it will be).
    public int rows;                                    // The number of rows on the board (how tall it will be).
    private int m_FloorNum;
    string levelName;

    private TileType[][] m_Tiles;                               // A jagged array of tile types representing the board, like a grid.
    private List<CTRoom> m_Rooms;                                     // All the rooms that are created for this board.
    private List<CTCorridor> m_Corridors;                             // All the corridors that connect the rooms.
    private CTRoomCoordinate m_StartingRoom;
    private Vector2 m_StairsForward;
    private Vector2 m_StairsBack;

    public CBossFloor()
    {
        m_isGenerated = false;
        m_FloorNum = 0;
        levelName = "NotGenerated";
    }

    public void InitNewLevel(int _floorNum, int _columns, int _rows, int _numRooms, int _gridSize, int _roomWidth, int _roomHeight, int _corridorLength)
    {
        columns = _columns;
        rows = _rows;
        m_FloorNum = _floorNum;

        // Set up Gameboard and Starting Room Coordinates
        m_StartingRoom = new CTRoomCoordinate(0, 0);

        SetupTilesArray();

        CreateRoomsAndCorridors(_numRooms, _roomWidth, _roomHeight, _corridorLength);

        SetTilesValuesForRooms();
        SetTilesValuesForCorridors();

        SetUpRoomDetector();
        SetUpStairs();

        m_isGenerated = true;
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

        //0
        CTRoom startRoom = new CTRoom();
        m_Rooms.Add(startRoom);
        startRoom.SetupRoom(columns, rows, _roomWidth, _roomHeight, new CTRoomCoordinate(0, 0));
        CTCorridor start2mainCor = new CTCorridor();
        start2mainCor.SetupCorridor(startRoom, _corridorLength, Direction.NORTH);
        m_Corridors.Add(start2mainCor);
        startRoom.nextCorridors.Add(start2mainCor.direction, start2mainCor);

        //1
        CTRoom mainRoom = new CTRoom();
        m_Rooms.Add(mainRoom);
        mainRoom.SetupRoom(_roomWidth * 3, _roomHeight * 3, new CTRoomCoordinate(0, 1), start2mainCor);
        CTCorridor main2endCor = new CTCorridor();
        main2endCor.SetupCorridor(mainRoom, _corridorLength, Direction.NORTH);
        m_Corridors.Add(main2endCor);
        mainRoom.nextCorridors.Add(main2endCor.direction, main2endCor);

        //2
        CTRoom endRoom = new CTRoom();
        m_Rooms.Add(endRoom);
        endRoom.SetupRoom(_roomWidth, _roomHeight, new CTRoomCoordinate(0, 2), main2endCor);
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
            m_StairsBack = m_Rooms[0].CenterPoint;
        else
            m_StairsBack = new Vector2(0, 0);

        if (_goForward)
            m_StairsForward = m_Rooms[m_Rooms.Count - 1].CenterPoint;
        else
            m_StairsForward = new Vector2(0, 0);
    }

    void SetUpCheckpoint()
    {
        //spawn a checkpoint 
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

    public bool Generated
    {
        get { return m_isGenerated; }
    }

    public string Name
    {
        get
        { return levelName; }

        set
        { levelName = value; }
    }

    public Vector2 StairsForward
    {
        get { return m_StairsForward; }
    }

    public Vector2 StairsBack
    {
        get { return m_StairsBack; }
    }

    public List<CTRoom> Rooms
    {
        get { return m_Rooms; }
    }

    public TileType[][] Tiles
    {
        get { return m_Tiles; }
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
