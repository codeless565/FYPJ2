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

    public void InitNewLevel(int _columns, int _rows, IntRange _numRooms, int _roomWidth, int _roomHeight, int _corridorLength)
    {
        columns = _columns;
        rows = _rows;

        // Set up Gameboard and Starting Room Coordinates
        StartingRoom = new CTRoomCoordinate(0,0);
        int gameboardColum;
        if (columns % (int)Mathf.Sqrt(columns) == 0)
        {
            gameboardColum = (int)Mathf.Sqrt(columns) + 1;
            StartingRoom.x = gameboardColum / 2 + 1;
        }
        else
        {
            gameboardColum = (int)Mathf.Sqrt(columns);
            StartingRoom.x = gameboardColum / 2;
        }

        int gameboardRow;
        if (rows % (int)Mathf.Sqrt(rows) == 0)
        {
            gameboardRow = (int)Mathf.Sqrt(rows) + 1;
            StartingRoom.y = gameboardRow / 2 + 1;
        }
        else
        {
            gameboardRow = (int)Mathf.Sqrt(rows);
            StartingRoom.y = gameboardRow / 2;
        }
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

    void CreateRoomsAndCorridors(IntRange _numRooms, int _roomWidth, int _roomHeight, int _corridorLength)
    {
        // Create the rooms array with a random size.
        rooms = new List<CTRoom>();

        // There should be one less corridor than there is rooms.
        corridors = new List<CTCorridor>();
        Debug.Log("Creating Room and Corridors" + corridors.Count);

        // Create the first room and corridor.
        CTRoom firstRoom = new CTRoom();
        rooms.Add(firstRoom);
        // Setup the first room, there is no previous corridor so we do not use one.
        firstRoom.SetupAllRoom(columns, rows, _roomWidth, _roomHeight, _corridorLength, StartingRoom,
            0, 10, ref gameBoard, ref rooms, ref corridors);
        Debug.Log("Total Rooms: " + rooms.Count);
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
            for (int j = 0; j < currentRoom.roomWidth; ++j)
            {
                int xCoord = currentRoom.xPos + j;

                // For each horizontal tile, go up vertically through the room's height.
                for (int k = 0; k < currentRoom.roomHeight; k++)
                {
                    int yCoord = currentRoom.yPos + k;

                    if (yCoord < 0)
                        yCoord = 0;

                    // The coordinates in the jagged array are based on the room's position and it's width and height.
                    //                    Debug.Log("x " + xCoord + " : " + "  ||  " + " y " + yCoord + " : ");
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
                switch (currentCorridor.direction)
                {
                    case Direction.NORTH:
                        yCoord += j;
                        break;
                    case Direction.EAST:
                        xCoord += j;
                        break;
                    case Direction.SOUTH:
                        yCoord -= j;
                        break;
                    case Direction.WEST:
                        xCoord -= j;
                        break;
                }

                // Set the tile at these coordinates to Floor.
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

// The type of tile that will be laid in a specific position.
//public enum TileType
//{
//    Wall, Floor
//}


