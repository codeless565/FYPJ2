using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLevel
{
    public int columns;                                 // The number of columns on the board (how wide it will be).
    public int rows;                                    // The number of rows on the board (how tall it will be).

    private TileType[][] tiles;                               // A jagged array of tile types representing the board, like a grid.
    private CRoom[] rooms;                                     // All the rooms that are created for this board.
    private List<CCorridor> corridors;                             // All the corridors that connect the rooms.

    public void Init(int _columns, int _rows, IntRange _numRooms, IntRange _numCorridors, IntRange _roomWidth, IntRange _roomHeight, IntRange _corridorLength)
    {
        columns = _columns;
        rows = _rows;

        // Safety Check for numCorridor - WARNING Might overwrite existing and create complications
        if (_numCorridors.m_Min < _numRooms.m_Max)
        {
            _numCorridors.m_Min = _numRooms.m_Max + 5;
            _numCorridors.m_Max = _numCorridors.m_Min + 5;
        }

        SetupTilesArray();

        CreateRoomsAndCorridors(_numRooms, _numCorridors, _roomWidth, _roomHeight, _corridorLength);

        SetTilesValuesForRooms();
        SetTilesValuesForCorridors();
    }

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

    void CreateRoomsAndCorridors(IntRange _numRooms, IntRange _numCorridors, IntRange _roomWidth, IntRange _roomHeight, IntRange _corridorLength)
    {
        // Create the rooms array with a random size.
        rooms = new CRoom[_numRooms.Random];

        // There should be one less corridor than there is rooms.
        int rand = _numCorridors.Random;
        corridors = new List<CCorridor>(rand);
        //Debug.Log(rand +  "   " + corridors.Count);

        // Create the first room and corridor.
        rooms[0] = new CRoom();
        // Setup the first room, there is no previous corridor so we do not use one.
        rooms[0].SetupRoom(_roomWidth, _roomHeight, columns, rows);

        for (int startingCorr = 0; startingCorr < 4; ++startingCorr)
        {         // ... create a corridor.
            CCorridor firstCorridor = new CCorridor();
            // Setup the first corridor using the first room.
            firstCorridor.SetupCorridor(rooms[0], _corridorLength, _roomWidth, _roomHeight, columns, rows, startingCorr);

            corridors.Add(firstCorridor);
        }

        for (int i = 1; i < rooms.Length; i++)
        {
            // Create a room.
            rooms[i] = new CRoom();

            List<int> notConnectedCorrsIndex = new List<int>();

            // Setup the room base on a unConnectedTo corridor
            for (int corIndex = 0; corIndex < corridors.Count; ++corIndex)
            {
                if (corridors[corIndex].connectedTo == false)
                {
                    notConnectedCorrsIndex.Add(corIndex);
                }
            }

            //choose randomly from the list of unconnected corridors
            if (notConnectedCorrsIndex.Count > 0)
            {
                int randomChoice = Random.Range(0, notConnectedCorrsIndex.Count);
                rooms[i].SetupRoom(_roomWidth, _roomHeight, columns, rows, corridors[notConnectedCorrsIndex[randomChoice]]);
            }
            else
            {
                rooms[i].SetupRoom(_roomWidth, _roomHeight, columns, rows, corridors[i - 1]);
            }

            notConnectedCorrsIndex.Clear();

            // If we haven't reached the end of the corridors array...
            if (corridors.Count < corridors.Capacity)
            {
                CCorridor tempCorridor = new CCorridor();
                int firstDir = tempCorridor.SetupCorridor(rooms[i], _corridorLength, _roomWidth, _roomHeight, columns, rows, false);
                corridors.Add(tempCorridor);
            }
        }
    }

    void SetTilesValuesForRooms()
    {
        // Go through all the rooms...
        for (int i = 0; i < rooms.Length; ++i)
        {
            CRoom currentRoom = rooms[i];

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
            CCorridor currentCorridor = corridors[i];

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

    public CRoom[] GetRooms()
    { return rooms; }

    public TileType[][] GetTiles()
    { return tiles; }
}

// The type of tile that will be laid in a specific position.
public enum TileType
{
    Wall, Floor
}


