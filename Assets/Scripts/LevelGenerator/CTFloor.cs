using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTFloor
{
    public int columns;                                 // The number of columns on the board (how wide it will be).
    public int rows;                                    // The number of rows on the board (how tall it will be).
    string levelName;

    public bool[][] gameBoard;

    private TileType[][] tiles;                               // A jagged array of tile types representing the board, like a grid.
    private List<CTRoom> rooms;                                     // All the rooms that are created for this board.
    private List<CTCorridor> corridors;                             // All the corridors that connect the rooms.
    private CTRoomCoordinate StartingRoom;

    public void InitNewLevel(int _columns, int _rows, int _numRooms, int _gridSize, int _roomWidth, int _roomHeight, int _corridorLength)
    {
        columns = _columns;
        rows = _rows;

        // Set up Gameboard and Starting Room Coordinates
        StartingRoom = new CTRoomCoordinate(0,0);

        int gameboardColum;
        gameboardColum = _gridSize;
        StartingRoom.x = gameboardColum / 2;

        int gameboardRow;
        gameboardRow = _gridSize;
        StartingRoom.y = gameboardRow / 2;

        Debug.Log("ColumsSqrt: " + gameboardColum);
        Debug.Log("RowsSqrt: " + gameboardRow);
        Debug.Log("StartingRm: " + StartingRoom.x + ", " + StartingRoom.y);


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
    }

    //public void InitLevelFromSave()
    //{

    //}

    void SetupTilesArray()
    {
        // Set the tiles jagged array to the correct width.
        tiles = new TileType[columns][];

        // Go through all the tile arrays...
        for (int i = 0; i < tiles.Length; i++)
        {
            // ... and set each tile array is the correct height.
            tiles[i] = new TileType[rows];
        }
    }

    void CreateRoomsAndCorridors(int _numRooms, int _roomWidth, int _roomHeight, int _corridorLength)
    {
        // Create the rooms array with a random size.
        rooms = new List<CTRoom>();

        // There should be one less corridor than there is rooms.
        corridors = new List<CTCorridor>();
        Debug.Log("Creating Room and Corridors" + corridors.Count);

        // Create the first room and corridor.
        CTRoom firstRoom = new CTRoom();
        rooms.Add(firstRoom);
        // Setup the first room, RMCount will start from 0
        int totalRooms = firstRoom.SetupAllRoom(columns, rows, _roomWidth, _roomHeight, _corridorLength, StartingRoom,
            _numRooms, ref gameBoard, ref rooms, ref corridors);
        Debug.Log("Total Rooms: " + rooms.Count + " GeneratedRooms: " + totalRooms);
    }

    void SetTilesValuesForRooms()
    {
        // Go through all the rooms...
        for (int i = 0; i < rooms.Count; ++i)
        {
            CTRoom currentRoom = rooms[i];
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
                            tiles[xCoord][yCoord] = TileType.WallInnerCorner_Q3;
                            continue;
                        }
                        else if (j == currentRoom.roomWidth)
                        {
                            tiles[xCoord][yCoord] = TileType.WallInnerCorner_Q4;
                            continue;
                        }

                        tiles[xCoord][yCoord] = TileType.Wall_Down;
                        continue;
                    }
                    if (k >= currentRoom.roomHeight)
                    {
                        if (j < 0)
                        {
                            tiles[xCoord][yCoord] = TileType.WallInnerCorner_Q2;
                            continue;
                        }
                        else if (j == currentRoom.roomWidth)
                        {
                            tiles[xCoord][yCoord] = TileType.WallInnerCorner_Q1;
                            continue;
                        }

                        tiles[xCoord][yCoord] = TileType.Wall_Up;
                        continue;
                    }
                    if (j < 0)
                    {
                        if (k < 0)
                        {
                            tiles[xCoord][yCoord] = TileType.WallInnerCorner_Q3;
                            continue;
                        }
                        else if (k == currentRoom.roomHeight)
                        {
                            tiles[xCoord][yCoord] = TileType.WallInnerCorner_Q2;
                            continue;
                        }

                        tiles[xCoord][yCoord] = TileType.Wall_Left;
                        continue;
                    }
                    if (j >= currentRoom.roomWidth)
                    {
                        if (k < 0)
                        {
                            tiles[xCoord][yCoord] = TileType.WallInnerCorner_Q4;
                            continue;
                        }
                        else if (k == currentRoom.roomHeight)
                        {
                            tiles[xCoord][yCoord] = TileType.WallInnerCorner_Q1;
                            continue;
                        }

                        tiles[xCoord][yCoord] = TileType.Wall_Right;
                        continue;
                    }

                    tiles[xCoord][yCoord] = TileType.Floor;
                }
            }
        }
    }

    void SetTilesValuesForCorridors()
    {
        // Go through every corridor...
        for (int i = 0; i < corridors.Count; ++i)
        {
            CTCorridor currentCorridor = corridors[i];

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
                            tiles[xCoord - 1][yCoord] = TileType.WallOuterCorner_Q4;
                            tiles[xCoord + 1][yCoord] = TileType.WallOuterCorner_Q3;
                            break;                                  
                        case Direction.EAST:                        
                            xCoord += j;                            
                            tiles[xCoord][yCoord + 1] = TileType.WallOuterCorner_Q3;
                            tiles[xCoord][yCoord - 1] = TileType.WallOuterCorner_Q2;
                            break;                                   
                        case Direction.SOUTH:                        
                            yCoord -= j;                             
                            tiles[xCoord - 1][yCoord] = TileType.WallOuterCorner_Q1;
                            tiles[xCoord + 1][yCoord] = TileType.WallOuterCorner_Q2;
                            break;                                  
                        case Direction.WEST:                        
                            xCoord -= j;                            
                            tiles[xCoord][yCoord + 1] = TileType.WallOuterCorner_Q4;
                            tiles[xCoord][yCoord - 1] = TileType.WallOuterCorner_Q1;
                            break;
                    }
                //Ending Corner
                else if (j == currentCorridor.corridorLength - 1)
                    switch (currentCorridor.direction)
                    {
                        case Direction.NORTH:
                            yCoord += j;
                            tiles[xCoord - 1][yCoord] = TileType.WallOuterCorner_Q1;
                            tiles[xCoord + 1][yCoord] = TileType.WallOuterCorner_Q2;
                            break;                                  
                        case Direction.EAST:                        
                            xCoord += j;
                            tiles[xCoord][yCoord + 1] = TileType.WallOuterCorner_Q4;
                            tiles[xCoord][yCoord - 1] = TileType.WallOuterCorner_Q1;
                            break;                                   
                        case Direction.SOUTH:                        
                            yCoord -= j;                             
                            tiles[xCoord - 1][yCoord] = TileType.WallOuterCorner_Q4;
                            tiles[xCoord + 1][yCoord] = TileType.WallOuterCorner_Q3;
                            break;                                  
                        case Direction.WEST:                        
                            xCoord -= j;
                            tiles[xCoord][yCoord + 1] = TileType.WallOuterCorner_Q3;
                            tiles[xCoord][yCoord - 1] = TileType.WallOuterCorner_Q2;
                            break;
                    }
                //Side walls
                else
                    switch (currentCorridor.direction)
                    {
                        case Direction.NORTH:
                            yCoord += j;
                            tiles[xCoord - 1][yCoord] = TileType.Wall_Left;
                            tiles[xCoord + 1][yCoord] = TileType.Wall_Right;
                            break;
                        case Direction.EAST:
                            xCoord += j;
                            tiles[xCoord][yCoord + 1] = TileType.Wall_Up;
                            tiles[xCoord][yCoord - 1] = TileType.Wall_Down;
                            break;
                        case Direction.SOUTH:
                            yCoord -= j;
                            tiles[xCoord - 1][yCoord] = TileType.Wall_Left;
                            tiles[xCoord + 1][yCoord] = TileType.Wall_Right;
                            break;
                        case Direction.WEST:
                            xCoord -= j;
                            tiles[xCoord][yCoord + 1] = TileType.Wall_Up;
                            tiles[xCoord][yCoord - 1] = TileType.Wall_Down;
                            break;
                    }
                //Floor
                tiles[xCoord][yCoord] = TileType.Floor;
            }
        }
    }    

    public List<CTRoom> GetRooms()
    { return rooms; }

    public TileType[][] GetTiles()
    { return tiles; }

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

