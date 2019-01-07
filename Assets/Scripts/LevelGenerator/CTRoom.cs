using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTRoom
{
    public bool generated;
    public bool firstRoom;

    public int roomDepth;
    public int xPos;                    // The x coordinate of the lower left tile of the room.
    public int yPos;                    // The y coordinate of the lower left tile of the room.
    public int roomWidth;               // How many tiles wide the room is.
    public int roomHeight;              // How many tiles high the room is.

    public Direction prevCorridor;         // The direction of the corridor that is entering this room.
    public Dictionary<Direction, CTCorridor> nextCorridors;   // The dir of the other corridors
    public CTRoomCoordinate coordinate;
    public Dictionary<Direction, CPathNode> pathnodes;

    public Vector2 CenterPoint
    {
        get
        {
            return new Vector2(xPos + (roomWidth - 1) * 0.5f, yPos + (roomHeight - 1) * 0.5f);
        }
    }

    public Vector2 RandomPoint
    {
        get
        {
            return new Vector2(Random.Range(xPos, xPos + roomWidth - 1), Random.Range(yPos, yPos + roomHeight - 1));
        }
    }

    public CPathNode GetPathnode(Direction _direction)
    {
        if (pathnodes.ContainsKey(_direction))
            return pathnodes[_direction];

        return null;
    }

    public CTRoom()
    {
        generated = false;
        firstRoom = false;
        roomDepth = 0;
        xPos = yPos = 0;
        roomWidth = roomHeight = 0;
        prevCorridor = Direction.Size;
        coordinate = new CTRoomCoordinate(0, 0);
        nextCorridors = new Dictionary<Direction, CTCorridor>();
        pathnodes = new Dictionary<Direction, CPathNode>();
    }

    public int SetupAllRoom(int _boardWidth, int _boardHeight, int _roomWidth, int _roomHeight, int _corridorLength, CTRoomCoordinate _startingCoord,
        int _maxRooms, ref bool[][] _gameBoard, ref List<CTRoom> _rooms, ref List<CTCorridor> _corridors)
    {
        /*********************
         * GENERATE 1st ROOM
         *********************/
        firstRoom = true;

        // Set a random width and height.
        roomDepth = 0;
        roomWidth = _roomWidth;
        roomHeight = _roomHeight;

        xPos = (int)(_boardWidth * 0.5f - roomWidth * 0.5f);
        yPos = (int)(_boardHeight * 0.5f - roomHeight * 0.5f);

        coordinate = new CTRoomCoordinate(_startingCoord);
        _gameBoard[coordinate.x][coordinate.y] = true;

        //set up pathnodes
        //pathnodes.Add(new CPathNode(CenterPoint.x, yPos + roomHeight, Direction.NORTH, this));
        //pathnodes.Add(new CPathNode(xPos, CenterPoint.y, Direction.WEST, this));
        //pathnodes.Add(new CPathNode(CenterPoint.x, yPos, Direction.SOUTH, this));
        //pathnodes.Add(new CPathNode(xPos + roomWidth, CenterPoint.y + roomWidth, Direction.EAST, this));

        generated = true;
        int currNumRooms = 1;   // 1st room
        //Debug.Log("CR_Count " + _rooms.Count + "  _numRooms: " + currNumRooms);
        //Debug.Log("Coord: " + coordinate.x + ", " + coordinate.y );

        // Create Next Corridors and pathnodes
        for (int i = 0; i < (int)Direction.Size; ++i)
        {
            //Create Corridors
            // Safety Check  if not, create a corridor for the room
            switch ((Direction)i)
            {
                case Direction.NORTH:
                    //if the next room will be out of board
                    if (coordinate.y + 1 < _gameBoard[0].Length)
                        if (!_gameBoard[coordinate.x][coordinate.y + 1])
                            break;
                    continue;
                case Direction.SOUTH:
                    if (coordinate.y - 1 >= 0)
                        if (!_gameBoard[coordinate.x][coordinate.y - 1])
                            break;
                    continue;
                case Direction.EAST:
                    if (coordinate.x + 1 < _gameBoard.Length)
                        if (!_gameBoard[coordinate.x + 1][coordinate.y])
                            break;
                    continue;
                case Direction.WEST:
                    if (coordinate.x - 1 >= 0)
                        if (!_gameBoard[coordinate.x - 1][coordinate.y])
                            break;
                    continue;
            }

            CTCorridor newCor = new CTCorridor();
            newCor.SetupCorridor(this, _corridorLength, (Direction)i);
            _corridors.Add(newCor);
            nextCorridors.Add((Direction)i, newCor);
        }

        int availableRooms = nextCorridors.Count;

        // Create Next Room
        CTRoomCoordinate nextRoomCoord = new CTRoomCoordinate(0, 0);

        foreach (Direction nextdir in nextCorridors.Keys)
        {
            //if direction have a room alrdy base on the _gameboard, skip
            //if direction is same as previous corridor / corridors in list, skip
            //random a corridor except for north side and check if they got slot on gameboard
            switch (nextdir)
            {
                case Direction.NORTH:
                    if (!_gameBoard[coordinate.x][coordinate.y + 1])
                    {
                        //Create next room
                        CTRoom newRoom = new CTRoom();
                        nextRoomCoord.setCoordinate(coordinate.x, coordinate.y + 1);
                        _rooms.Add(newRoom);
                        newRoom.SetupRoom(_roomWidth, _roomHeight, nextRoomCoord, nextCorridors[nextdir], ref currNumRooms, ref availableRooms, ref _maxRooms, ref _gameBoard, ref _rooms, ref _corridors, 0);
                    }
                    break;

                case Direction.EAST:
                    if (!_gameBoard[coordinate.x + 1][coordinate.y])
                    {
                        //Create next room
                        CTRoom newRoom = new CTRoom();
                        nextRoomCoord.setCoordinate(coordinate.x + 1, coordinate.y);
                        _rooms.Add(newRoom);
                        newRoom.SetupRoom(_roomWidth, _roomHeight, nextRoomCoord, nextCorridors[nextdir], ref currNumRooms, ref availableRooms, ref _maxRooms, ref _gameBoard, ref _rooms, ref _corridors, 0);
                    }
                    break;

                case Direction.WEST:
                    if (!_gameBoard[coordinate.x - 1][coordinate.y])
                    {
                        //Create next room
                        CTRoom newRoom = new CTRoom();
                        nextRoomCoord.setCoordinate(coordinate.x - 1, coordinate.y);
                        _rooms.Add(newRoom);
                        newRoom.SetupRoom(_roomWidth, _roomHeight, nextRoomCoord, nextCorridors[nextdir], ref currNumRooms, ref availableRooms, ref _maxRooms, ref _gameBoard, ref _rooms, ref _corridors, 0);
                    }
                    break;

                case Direction.SOUTH:
                    if (!_gameBoard[coordinate.x][coordinate.y - 1])
                    {
                        //Create next room
                        CTRoom newRoom = new CTRoom();
                        nextRoomCoord.setCoordinate(coordinate.x, coordinate.y - 1);
                        _rooms.Add(newRoom);
                        newRoom.SetupRoom(_roomWidth, _roomHeight, nextRoomCoord, nextCorridors[nextdir], ref currNumRooms, ref availableRooms, ref _maxRooms, ref _gameBoard, ref _rooms, ref _corridors, 0);
                    }
                    break;
            }
        }

        //Check if its enough rooms
        while (currNumRooms < _maxRooms)
        {
            CreateEndRooms(_roomWidth, _roomHeight, _corridorLength - 2, GetEndRooms(_rooms, _maxRooms / 4), ref currNumRooms, ref availableRooms, ref _maxRooms, ref _gameBoard, ref _rooms, ref _corridors);
        }

        return currNumRooms;
    }

    private void SetupRoom(int _width, int _height, CTRoomCoordinate _roomCoordinate, CTCorridor _prevCorridor,
        ref int _numRooms, ref int _availableRooms, ref int _maxRooms, ref bool[][] _gameBoard, ref List<CTRoom> _rooms, ref List<CTCorridor> _corridors,
        int _depth, bool _ignoreDepth = false)
    {
        //Return Mechanic / Safety
        if (_roomCoordinate.x >= _gameBoard.Length)
            return;

        if (_roomCoordinate.y >= _gameBoard[0].Length)
            return;

        if (_gameBoard[_roomCoordinate.x][_roomCoordinate.y])
            return;

        // Init Values
        // Set the entering corridor direction.
        nextCorridors = new Dictionary<Direction, CTCorridor>();
        prevCorridor = _prevCorridor.direction;

        roomWidth = _width;
        roomHeight = _height;
        roomDepth = _depth + 1;

        coordinate = new CTRoomCoordinate(_roomCoordinate);

        //set up pathnodes
        pathnodes = new Dictionary<Direction, CPathNode>();

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
        _prevCorridor.connectedTo = true;
        generated = true;
        _gameBoard[_roomCoordinate.x][_roomCoordinate.y] = true;

        //pathnodes.Add(new CPathNode(CenterPoint.x, yPos + roomHeight, Direction.NORTH, this));
        //pathnodes.Add(new CPathNode(xPos, CenterPoint.y, Direction.WEST, this));
        //pathnodes.Add(new CPathNode(CenterPoint.x, yPos, Direction.SOUTH, this));
        //pathnodes.Add(new CPathNode(xPos + roomWidth, CenterPoint.y + roomWidth, Direction.EAST, this));

        _availableRooms--;
        _numRooms++;
        //Debug.Log("CR_Count " + _rooms.Count + "  _numRooms: " + _numRooms);
        //Debug.Log("Coord: " + coordinate.x + ", " + coordinate.y);

        if (_numRooms >= _maxRooms)
            return;

        int limit = _maxRooms / 4;

        if (limit < _depth && !_ignoreDepth)
        {
            return;
        }

        // Create Next Corridors and Pathnode
        int startDir = Random.Range(0, (int)Direction.Size);
        for (int i = 0; i < (int)Direction.Size; ++i, ++startDir)
        {
            Direction nextDir = (Direction)(startDir % (int)Direction.Size);
            //Create Corridors
            if (Random.Range(0.0f, 1.0f) <= 1.0f - (_numRooms + _availableRooms) / _maxRooms - i * 0.1f)
            {
                // Safety Check  if not, create a corridor for the room
                switch (nextDir)
                {
                    case Direction.NORTH:
                        //if the next room will be out of board
                        if (coordinate.y + 1 < _gameBoard[0].Length)
                            if (!_gameBoard[coordinate.x][coordinate.y + 1])
                                break;
                        continue;
                    case Direction.SOUTH:
                        if (coordinate.y - 1 >= 0)
                            if (!_gameBoard[coordinate.x][coordinate.y - 1])
                                break;
                        continue;
                    case Direction.EAST:
                        if (coordinate.x + 1 < _gameBoard.Length)
                            if (!_gameBoard[coordinate.x + 1][coordinate.y])
                                break;
                        continue;
                    case Direction.WEST:
                        if (coordinate.x - 1 >= 0)
                            if (!_gameBoard[coordinate.x - 1][coordinate.y])
                                break;
                        continue;
                }

                CTCorridor newCor = new CTCorridor();
                newCor.SetupCorridor(this, _prevCorridor.corridorLength, nextDir);
                _corridors.Add(newCor);
                nextCorridors.Add(nextDir, newCor);
            }
        }

        // Create 1 End Room
        if (nextCorridors.Count <= 0)
            return;

        _availableRooms += nextCorridors.Count;

        // Create Next Room
        CTRoomCoordinate nextRoomCoord = new CTRoomCoordinate(0, 0);

        foreach (Direction nextdir in nextCorridors.Keys)
        {
            //if direction have a room alrdy base on the _gameboard, skip
            switch (nextdir)
            {
                case Direction.NORTH:
                    if (!_gameBoard[coordinate.x][coordinate.y + 1])
                    {
                        //Create next room
                        CTRoom newRoom = new CTRoom();
                        nextRoomCoord.setCoordinate(coordinate.x, coordinate.y + 1);
                        _rooms.Add(newRoom);
                        newRoom.SetupRoom(_width, _height, nextRoomCoord, nextCorridors[nextdir], ref _numRooms, ref _availableRooms, ref _maxRooms, ref _gameBoard, ref _rooms, ref _corridors, roomDepth);
                    }
                    break;

                case Direction.EAST:
                    if (!_gameBoard[coordinate.x + 1][coordinate.y])
                    {
                        //Create next room
                        CTRoom newRoom = new CTRoom();
                        nextRoomCoord.setCoordinate(coordinate.x + 1, coordinate.y);
                        _rooms.Add(newRoom);
                        newRoom.SetupRoom(_width, _height, nextRoomCoord, nextCorridors[nextdir], ref _numRooms, ref _availableRooms, ref _maxRooms, ref _gameBoard, ref _rooms, ref _corridors, roomDepth);
                    }
                    break;

                case Direction.WEST:
                    if (!_gameBoard[coordinate.x - 1][coordinate.y])
                    {
                        //Create next room
                        CTRoom newRoom = new CTRoom();
                        nextRoomCoord.setCoordinate(coordinate.x - 1, coordinate.y);
                        _rooms.Add(newRoom);
                        newRoom.SetupRoom(_width, _height, nextRoomCoord, nextCorridors[nextdir], ref _numRooms, ref _availableRooms, ref _maxRooms, ref _gameBoard, ref _rooms, ref _corridors, roomDepth);
                    }
                    break;

                case Direction.SOUTH:
                    if (!_gameBoard[coordinate.x][coordinate.y - 1])
                    {
                        //Create next room
                        CTRoom newRoom = new CTRoom();
                        nextRoomCoord.setCoordinate(coordinate.x, coordinate.y - 1);
                        _rooms.Add(newRoom);
                        newRoom.SetupRoom(_width, _height, nextRoomCoord, nextCorridors[nextdir], ref _numRooms, ref _availableRooms, ref _maxRooms, ref _gameBoard, ref _rooms, ref _corridors, roomDepth);
                    }
                    break;
            }
        }

    }

    /*****************************
     * FIRST ROOM FUNCTIONS ONLY
     *****************************/
    private List<CTRoom> GetEndRooms(List<CTRoom> _rooms, int _depth)
    {
        //Get all room with the same finalDepth
        List<CTRoom> endRooms = new List<CTRoom>();
        for (int i = 0; i < _rooms.Count; ++i)
        {
            if (_rooms[i].roomDepth >= _depth)
            {
                if (_rooms[i].nextCorridors.Count <= 0)
                    endRooms.Add(_rooms[i]);
            }
        }

        return endRooms;

    }
    //Overlapping issue somewhere when forcing creation, mayb _gameboard is not registerig new rooms
    private void CreateEndRooms(int _width, int _height, int _corridorLength, List<CTRoom> _endRooms,
        ref int _numRooms, ref int _availableRooms, ref int _maxRooms, ref bool[][] _gameBoard, ref List<CTRoom> _rooms, ref List<CTCorridor> _corridors)
    {
        for (int i = 0; i < _endRooms.Count; ++i)
        {
            if (_endRooms[i].nextCorridors.Count > 0)
                continue;

            _endRooms[i].SetupEndRoom(_width, _height, _corridorLength, ref _numRooms, ref _availableRooms, ref _maxRooms, ref _gameBoard, ref _rooms, ref _corridors);

            if (_numRooms >= _maxRooms)
                break;
        }
    }

    private void SetupEndRoom(int _width, int _height, int _corridorLength,
        ref int _numRooms, ref int _availableRooms, ref int _maxRooms, ref bool[][] _gameBoard, ref List<CTRoom> _rooms, ref List<CTCorridor> _corridors)
    {
        //Safety measures
        if (nextCorridors.Count > 0)
            return;

        //Create Corridors
        GameObject node;
        int startDir = Random.Range(0, (int)Direction.Size);
        for (int i = 0; i < (int)Direction.Size; ++i, ++startDir)
        {
            Direction nextDir = (Direction)(startDir % (int)Direction.Size);
            //if ((startDir % (int)Direction.Size) != (int)prevCorridor)
            {
                //Create Corridors
                if (Random.Range(0.0f, 1.0f) <= 0.5f - i * 0.1f)
                {
                    // Safety Check  if not, create a corridor for the room
                    switch (nextDir)
                    {
                        case Direction.NORTH:
                            //if the next room will be out of board
                            if (coordinate.y + 1 < _gameBoard[0].Length)
                                if (!_gameBoard[coordinate.x][coordinate.y + 1])
                                {
                                    node = Object.Instantiate(Resources.Load("Pathnode"), new Vector3(CenterPoint.x, yPos + roomHeight - 1, 0), Quaternion.identity) as GameObject;
                                    node.GetComponent<CPathNode>().Init(CenterPoint.x, yPos + roomHeight, nextDir, this);
                                    pathnodes.Add(nextDir, node.GetComponent<CPathNode>());
                                    break;
                                }
                            continue;
                        case Direction.SOUTH:
                            if (coordinate.y - 1 >= 0)
                                if (!_gameBoard[coordinate.x][coordinate.y - 1])
                                {
                                    node = Object.Instantiate(Resources.Load("Pathnode"), new Vector3(CenterPoint.x, yPos, 0), Quaternion.identity) as GameObject;
                                    node.GetComponent<CPathNode>().Init(CenterPoint.x, yPos, nextDir, this);
                                    pathnodes.Add(nextDir, node.GetComponent<CPathNode>());
                                    break;
                                }
                            continue;
                        case Direction.EAST:
                            if (coordinate.x + 1 < _gameBoard.Length)
                                if (!_gameBoard[coordinate.x + 1][coordinate.y])
                                {
                                    node = Object.Instantiate(Resources.Load("Pathnode"), new Vector3(xPos + roomWidth - 1, CenterPoint.y, 0), Quaternion.identity) as GameObject;
                                    node.GetComponent<CPathNode>().Init(xPos + roomWidth, CenterPoint.y, nextDir, this);
                                    pathnodes.Add(nextDir, node.GetComponent<CPathNode>());
                                    break;
                                }
                            continue;
                        case Direction.WEST:
                            if (coordinate.x - 1 >= 0)
                                if (!_gameBoard[coordinate.x - 1][coordinate.y])
                                {
                                    node = Object.Instantiate(Resources.Load("Pathnode"), new Vector3(xPos, CenterPoint.y, 0), Quaternion.identity) as GameObject;
                                    node.GetComponent<CPathNode>().Init(xPos, CenterPoint.y, nextDir, this);
                                    pathnodes.Add(nextDir, node.GetComponent<CPathNode>());
                                    break;
                                }
                            continue;
                    }

                    CTCorridor newCor = new CTCorridor();
                    newCor.SetupCorridor(this, _corridorLength, nextDir);
                    _corridors.Add(newCor);
                    nextCorridors.Add(nextDir, newCor);
                }
            }
        }

        // Create 1 End Room
        if (nextCorridors.Count <= 0)
            return;

        _availableRooms += nextCorridors.Count;

        // Create Next Room
        CTRoomCoordinate nextRoomCoord = new CTRoomCoordinate(0, 0);

        foreach (Direction nextdir in nextCorridors.Keys)
        {
            //if direction have a room alrdy base on the _gameboard, skip
            switch (nextdir)
            {
                case Direction.NORTH:
                    if (!_gameBoard[coordinate.x][coordinate.y + 1])
                    {
                        //Create next room
                        CTRoom newRoom = new CTRoom();
                        nextRoomCoord.setCoordinate(coordinate.x, coordinate.y + 1);
                        _rooms.Add(newRoom);
                        newRoom.SetupRoom(_width, _height, nextRoomCoord, nextCorridors[nextdir], ref _numRooms, ref _availableRooms, ref _maxRooms, ref _gameBoard, ref _rooms, ref _corridors, roomDepth, true);
                    }
                    break;

                case Direction.EAST:
                    if (!_gameBoard[coordinate.x + 1][coordinate.y])
                    {
                        //Create next room
                        CTRoom newRoom = new CTRoom();
                        nextRoomCoord.setCoordinate(coordinate.x + 1, coordinate.y);
                        _rooms.Add(newRoom);
                        newRoom.SetupRoom(_width, _height, nextRoomCoord, nextCorridors[nextdir], ref _numRooms, ref _availableRooms, ref _maxRooms, ref _gameBoard, ref _rooms, ref _corridors, roomDepth, true);
                    }
                    break;

                case Direction.WEST:
                    if (!_gameBoard[coordinate.x - 1][coordinate.y])
                    {
                        //Create next room
                        CTRoom newRoom = new CTRoom();
                        nextRoomCoord.setCoordinate(coordinate.x - 1, coordinate.y);
                        _rooms.Add(newRoom);
                        newRoom.SetupRoom(_width, _height, nextRoomCoord, nextCorridors[nextdir], ref _numRooms, ref _availableRooms, ref _maxRooms, ref _gameBoard, ref _rooms, ref _corridors, roomDepth, true);
                    }
                    break;

                case Direction.SOUTH:
                    if (!_gameBoard[coordinate.x][coordinate.y - 1])
                    {
                        //Create next room
                        CTRoom newRoom = new CTRoom();
                        nextRoomCoord.setCoordinate(coordinate.x, coordinate.y - 1);
                        _rooms.Add(newRoom);
                        newRoom.SetupRoom(_width, _height, nextRoomCoord, nextCorridors[nextdir], ref _numRooms, ref _availableRooms, ref _maxRooms, ref _gameBoard, ref _rooms, ref _corridors, roomDepth, true);
                    }
                    break;
            }
        }
    }


    /******************
     * BOSS FLOOR ONLY
     ******************/
    public void SetupRoom(int _column, int _row, int _width, int _height, CTRoomCoordinate _coordinate, CTCorridor _prevCorridor = null)
    {
        //For 1st room custom creation
        roomWidth = _width;
        roomHeight = _height;
        coordinate = new CTRoomCoordinate(_coordinate);

        if (_prevCorridor == null)
        {
            firstRoom = true;
            xPos = (int)(_column * 0.5f);
            yPos = yPos = (int)(_height * 0.5f);

            prevCorridor = Direction.Size;
        }
        else
        {
            firstRoom = false;

            switch (_prevCorridor.direction)
            {
                case Direction.NORTH:
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
            _prevCorridor.connectedTo = true;
        }

        // room created successfully!
        generated = true;
    }

    public void SetupRoom(int _width, int _height, CTRoomCoordinate _coordinate, CTCorridor _prevCorridor = null)
    {
        roomWidth = _width;
        roomHeight = _height;
        coordinate = new CTRoomCoordinate(_coordinate);

        if (_prevCorridor == null)
        {
            firstRoom = true;
            xPos = (int)(_width * 0.5f);
            yPos = (int)(_height * 0.5f);
            prevCorridor = Direction.Size;
        }
        else
        {
            firstRoom = false;

            switch (_prevCorridor.direction)
            {
                case Direction.NORTH:
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
            _prevCorridor.connectedTo = true;
        }

        // room created successfully!
        generated = true;
    }
}