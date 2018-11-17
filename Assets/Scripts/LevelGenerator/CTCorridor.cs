using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTCorridor
{
    public bool connectedTo;
    public int startXPos;         // The x coordinate for the start of the corridor.
    public int startYPos;         // The y coordinate for the start of the corridor.
    public int corridorLength;    // How many units long the corridor is.
    public Direction direction;   // Which direction the corridor is heading from it's room.

    public CTRoomCoordinate endRoomCoord;
    public CTRoom prevRoom;

    public int EndPositionX
    {
        get
        {
            if (direction == Direction.NORTH || direction == Direction.SOUTH)
                return startXPos;
            if (direction == Direction.EAST)
                return startXPos + corridorLength - 1;
            return startXPos - corridorLength + 1;
        }
    }

    public int EndPositionY
    {
        get
        {
            if (direction == Direction.EAST || direction == Direction.WEST)
                return startYPos;
            if (direction == Direction.NORTH)
                return startYPos + corridorLength - 1;
            return startYPos - corridorLength + 1;
        }
    }

    public void SetupCorridor(CTRoom _prevRoom, int length, Direction _direction)
    {
        direction = _direction;
        prevRoom = _prevRoom;

        // Set a random length.
        corridorLength = length;

        switch (direction)
        {
            // If the choosen direction is NORTH (up)...
            case Direction.NORTH:
                // ... the starting position in the x axis can be random but within the width of the room.
                startXPos = _prevRoom.xPos + (_prevRoom.roomWidth / 2);
                startYPos = _prevRoom.yPos + _prevRoom.roomHeight;
                endRoomCoord = new CTRoomCoordinate(prevRoom.coordinate.x, prevRoom.coordinate.y + 1);
                break;
            case Direction.EAST:
                startXPos = _prevRoom.xPos + _prevRoom.roomWidth;
                startYPos = _prevRoom.yPos + _prevRoom.roomHeight / 2;
                endRoomCoord = new CTRoomCoordinate(prevRoom.coordinate.x + 1, prevRoom.coordinate.y);
                break;
            case Direction.SOUTH:
                startXPos = _prevRoom.xPos + _prevRoom.roomWidth / 2;
                startYPos = _prevRoom.yPos - 1;
                endRoomCoord = new CTRoomCoordinate(prevRoom.coordinate.x, prevRoom.coordinate.y - 1);
                break;
            case Direction.WEST:
                startXPos = _prevRoom.xPos - 1;
                startYPos = _prevRoom.yPos + _prevRoom.roomHeight / 2;
                endRoomCoord = new CTRoomCoordinate(prevRoom.coordinate.x - 1, prevRoom.coordinate.y);
                break;
        }
    }
}