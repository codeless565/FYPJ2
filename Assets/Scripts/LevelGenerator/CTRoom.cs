using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTRoom
{
    public int xPos;                    // The x coordinate of the lower left tile of the room.
    public int yPos;                    // The y coordinate of the lower left tile of the room.
    public int roomWidth;               // How many tiles wide the room is.
    public int roomHeight;              // How many tiles high the room is.

    public Direction prevCorridor;         // The direction of the corridor that is entering this room.
    public List<CTCorridor> nextCorridors;   // The dir of the other corridors
    public CTRoomCoordinate coordinate;

    public bool generated;

    public bool SetupAllRoom(int _boardWidth, int _boardHeight, int _roomWidth, int _roomHeight, int _corridorLength, CTRoomCoordinate _startingCoord,
        int _numRooms, int _maxRooms, ref bool[][] _gameBoard, ref List<CTRoom> _rooms, ref List<CTCorridor> _corridors)
    {
        // Set a random width and height.
        roomWidth = _roomWidth;
        roomHeight = _roomHeight;

        xPos = (int)(_boardWidth * 0.5f - roomWidth * 0.5f);
        yPos = (int)(_boardHeight *0.5f - roomHeight * 0.5f);

        coordinate = _startingCoord;
        _gameBoard[coordinate.x][coordinate.y] = true;

        generated = true;
        _numRooms++;
        Debug.Log("CR_Count " + _rooms.Count + "  _numRooms: " + _numRooms);
        Debug.Log("Coord: " + coordinate.x + ", " + coordinate.y );

        // Create Next Corridors
        nextCorridors = new List<CTCorridor>();
        for (int i = 0; i < (int)Direction.Size; ++i)
        {
            //Create Corridors
            //// Safety Check  if not, create a corridor for the room
            //switch ((Direction)i)
            //{
            //    case Direction.NORTH:
            //        //if the next room will be out of board
            //        if (coordinate.y + 1 >= _gameBoard[0].Length)
            //            continue;
            //        //if corridor has a room already?
            //        if (_gameBoard[coordinate.x][coordinate.y + 1])
            //            continue;
            //        break;
            //    case Direction.SOUTH:
            //        if (coordinate.y - 1 <= 0)
            //            continue;
            //        if (_gameBoard[coordinate.x][coordinate.y - 1])
            //            continue;
            //        break;
            //    case Direction.EAST:
            //        if (coordinate.x + 1 >= _gameBoard.Length)
            //            continue;
            //        if (_gameBoard[coordinate.x + 1][coordinate.y])
            //            continue;
            //        break;
            //    case Direction.WEST:
            //        if (coordinate.x - 1 <= 0)
            //            continue;
            //        if (_gameBoard[coordinate.x - 1][coordinate.y])
            //            continue;
            //        break;
            //}

            CTCorridor newCor = new CTCorridor();
            newCor.SetupCorridor(this, _corridorLength, (Direction)i);
            _corridors.Add(newCor);
            nextCorridors.Add(newCor);
        }

        // Create Next Room
        CTRoomCoordinate nextRoomCoord = new CTRoomCoordinate(coordinate.x, coordinate.y);

        for (int i = 0; i < nextCorridors.Count; ++i)
        {
            //if direction have a room alrdy base on the _gameboard, skip
            //if direction is same as previous corridor / corridors in list, skip
            //random a corridor except for north side and check if they got slot on gameboard
            switch (nextCorridors[i].direction)
            {
                case Direction.NORTH:
                    if (!_gameBoard[coordinate.x][coordinate.y + 1])
                    {
                        //Create next room
                        CTRoom newRoom = new CTRoom();
                        nextRoomCoord.setCoordinate(coordinate.x, coordinate.y + 1);
                        _rooms.Add(newRoom);
                        newRoom.SetupRoom(_roomWidth, _roomHeight, nextRoomCoord, nextCorridors[i], ref _numRooms, ref _maxRooms, ref _gameBoard, ref _rooms, ref _corridors);
                    }
                    break;

                case Direction.SOUTH:
                    if (!_gameBoard[coordinate.x][coordinate.y - 1])
                    {
                        //Create next room
                        CTRoom newRoom = new CTRoom();
                        nextRoomCoord.setCoordinate(coordinate.x, coordinate.y - 1);
                        _rooms.Add(newRoom);
                        newRoom.SetupRoom(_roomWidth, _roomHeight, nextRoomCoord, nextCorridors[i], ref _numRooms, ref _maxRooms, ref _gameBoard, ref _rooms, ref _corridors);
                    }
                    break;

                case Direction.EAST:
                    if (!_gameBoard[coordinate.x + 1][coordinate.y])
                    {
                        //Create next room
                        CTRoom newRoom = new CTRoom();
                        nextRoomCoord.setCoordinate(coordinate.x + 1, coordinate.y);
                        _rooms.Add(newRoom);
                        newRoom.SetupRoom(_roomWidth, _roomHeight, nextRoomCoord, nextCorridors[i], ref _numRooms, ref _maxRooms, ref _gameBoard, ref _rooms, ref _corridors);
                    }
                    break;

                case Direction.WEST:
                    if (!_gameBoard[coordinate.x - 1][coordinate.y])
                    {
                        //Create next room
                        CTRoom newRoom = new CTRoom();
                        nextRoomCoord.setCoordinate(coordinate.x - 1, coordinate.y);
                        _rooms.Add(newRoom);
                        newRoom.SetupRoom(_roomWidth, _roomHeight, nextRoomCoord, nextCorridors[i], ref _numRooms, ref _maxRooms, ref _gameBoard, ref _rooms, ref _corridors);
                    }
                    break;
            }
        }

        return true;
    }

    private void SetupRoom(int _width, int _height, CTRoomCoordinate _roomCoordinate, CTCorridor _prevCorridor, ref int _numRooms, ref int _maxRooms, ref bool[][] _gameBoard, ref List<CTRoom> _rooms, ref List<CTCorridor> _corridors)
    {
        //Return Mechanic / Safety
        if (_numRooms >= _maxRooms)
            return;

        if (_roomCoordinate.x >= _gameBoard.Length)
            return;

        if (_roomCoordinate.y >= _gameBoard[0].Length)
            return;

        // Set the entering corridor direction.
        prevCorridor = _prevCorridor.direction;
        _prevCorridor.connectedTo = true;
        generated = true;

        // Set values for width and height.
        roomWidth = _width;
        roomHeight = _height;
        coordinate = _roomCoordinate;

        //Create Room
        switch (_prevCorridor.direction)
        {
            // If the corridor entering this room is going north...
            case Direction.NORTH:
                // The y coordinate of the room must be at the end of the corridor (since the corridor leads to the bottom of the room).
                xPos = _prevCorridor.EndPositionX - (roomWidth / 2);
                yPos = _prevCorridor.EndPositionY + 1;
                break;

            case Direction.EAST:
                xPos = _prevCorridor.EndPositionX + 1;
                yPos = _prevCorridor.EndPositionY - (roomHeight / 2);
                break;

            case Direction.SOUTH:
                xPos = _prevCorridor.EndPositionX - (roomWidth / 2);
                yPos = _prevCorridor.EndPositionY - roomHeight;
                break;

            case Direction.WEST:
                xPos = _prevCorridor.EndPositionX - roomWidth;
                yPos = _prevCorridor.EndPositionY - (roomHeight / 2);
                break;
        }

        // room created successfully!
        _numRooms++;
        _gameBoard[coordinate.x][coordinate.y] = true;
        Debug.Log("CR_Count " + _rooms.Count + "  _numRooms: " + _numRooms);
        Debug.Log("Coord: " + coordinate.x + ", " + coordinate.y);

        //random set corridors
        //if that corridor direction can put 1 more room, do it, if not, destroy the corridor
        //once room created, continue and return once no more corridor to cont

        if (_numRooms >= _maxRooms)
            return;

        // Create Next Corridors
        nextCorridors = new List<CTCorridor>();
        for (int i = 0; i < (int)Direction.Size; ++i)
        {
            if (i != (int)prevCorridor)
            {
                //Create Corridors
                if (Random.Range(0.0f, 1.0f) >= 0.5f)
                {
                    // Safety Check  if not, create a corridor for the room
                    switch ((Direction)i)
                    {
                        case Direction.NORTH:
                            //if the next room will be out of board
                            if (coordinate.y + 1 >= _gameBoard[0].Length)
                                continue;
                            //if corridor has a room already?
                            if (_gameBoard[coordinate.x][coordinate.y + 1])
                                continue;
                                break;
                        case Direction.SOUTH:
                            if (coordinate.y - 1 <= 0)
                                continue;
                            if (_gameBoard[coordinate.x][coordinate.y - 1])
                                continue;
                            break;
                        case Direction.EAST:
                            if (coordinate.x + 1 >= _gameBoard.Length)
                                continue;
                            if (_gameBoard[coordinate.x + 1][coordinate.y])
                                continue;
                            break;
                        case Direction.WEST:
                            if (coordinate.x - 1 <= 0)
                                continue;
                            if (_gameBoard[coordinate.x - 1][coordinate.y])
                                continue;
                            break;
                    }

                    CTCorridor newCor = new CTCorridor();
                    newCor.SetupCorridor(this, _prevCorridor.corridorLength, (Direction)i);
                    _corridors.Add(newCor);
                    nextCorridors.Add(newCor);
                }
            }
        }

        if (nextCorridors.Count <= 0)
            return;

        // Create Next Room
        CTRoomCoordinate nextRoomCoord = new CTRoomCoordinate(coordinate.x, coordinate.y);

        for (int i = 0; i < nextCorridors.Count; ++i)
        {
            //if direction have a room alrdy base on the _gameboard, skip
            //if direction is same as previous corridor / corridors in list, skip
            //random a corridor except for north side and check if they got slot on gameboard
            switch (nextCorridors[i].direction)
            {
                case Direction.NORTH:
                    if (!_gameBoard[coordinate.x][coordinate.y + 1])
                    {
                        //Create next room
                        CTRoom newRoom = new CTRoom();
                        nextRoomCoord.setCoordinate(coordinate.x, coordinate.y + 1);
                        _rooms.Add(newRoom);
                        newRoom.SetupRoom(_width, _height, nextRoomCoord, nextCorridors[i], ref _numRooms, ref _maxRooms, ref _gameBoard, ref _rooms, ref _corridors);
                    }
                    break;

                case Direction.SOUTH:
                    if (!_gameBoard[coordinate.x][coordinate.y - 1])
                    {
                        //Create next room
                        CTRoom newRoom = new CTRoom();
                        nextRoomCoord.setCoordinate(coordinate.x, coordinate.y - 1);
                        _rooms.Add(newRoom);
                        newRoom.SetupRoom(_width, _height, nextRoomCoord, nextCorridors[i], ref _numRooms, ref _maxRooms, ref _gameBoard, ref _rooms, ref _corridors);
                    }
                    break;

                case Direction.EAST:
                    if (!_gameBoard[coordinate.x + 1][coordinate.y])
                    {
                        //Create next room
                        CTRoom newRoom = new CTRoom();
                        nextRoomCoord.setCoordinate(coordinate.x + 1, coordinate.y);
                        _rooms.Add(newRoom);
                        newRoom.SetupRoom(_width, _height, nextRoomCoord, nextCorridors[i], ref _numRooms, ref _maxRooms, ref _gameBoard, ref _rooms, ref _corridors);
                    }
                    break;

                case Direction.WEST:
                    if (!_gameBoard[coordinate.x - 1][coordinate.y])
                    {
                        //Create next room
                        CTRoom newRoom = new CTRoom();
                        nextRoomCoord.setCoordinate(coordinate.x - 1, coordinate.y);
                        _rooms.Add(newRoom);
                        newRoom.SetupRoom(_width, _height, nextRoomCoord, nextCorridors[i], ref _numRooms, ref _maxRooms, ref _gameBoard, ref _rooms, ref _corridors);
                    }
                    break;
            }
        }
    }
}


public class CTRoomCoordinate
{
    public int x;
    public int y;

    public CTRoomCoordinate(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

    public void setCoordinate(int _x, int _y)
    {
        x = _x;
        y = _y;
    }
}