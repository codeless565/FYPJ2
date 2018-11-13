using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCorridor
{
    public int startXPos;         // The x coordinate for the start of the corridor.
    public int startYPos;         // The y coordinate for the start of the corridor.
    public int corridorLength;    // How many units long the corridor is.
    public Direction direction;   // Which direction the corridor is heading from it's room.
    public bool connectedTo;

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
    
    public int SetupCorridor(CRoom room, IntRange length, IntRange roomWidth, IntRange roomHeight, int columns, int rows, bool firstCorridor)
    {
        // Set a random direction (a random index from 0 to 3, cast to Direction).
        direction = (Direction)Random.Range(0, 4);

        // Find the direction opposite to the one entering the room this corridor is leaving from.
        // Cast the previous corridor's direction to an int between 0 and 3 and add 2 (a number between 2 and 5).
        // Find the remainder when dividing by 4 (if 2 then 2, if 3 then 3, if 4 then 0, if 5 then 1).
        // Cast this number back to a direction.
        // Overall effect is if the direction was SOUTH then that is 2, becomes 4, remainder is 0, which is NORTH.
        Direction oppositeDirection = (Direction)(((int)room.Corridors + 2) % 4);

        // If this is not the first corridor and the randomly selected direction is opposite to the previous corridor's direction...
        if (!firstCorridor && direction == oppositeDirection)
        {
            // Rotate the direction 90 degrees clockwise (NORTH becomes EAST, EAST becomes SOUTH, etc).
            // This is a more broken down version of the opposite direction operation above but instead of adding 2 we're adding 1.
            // This means instead of rotating 180 (the opposite direction) we're rotating 90.
            int directionInt = (int)direction;
            directionInt++;
            directionInt = directionInt % 4;
            direction = (Direction)directionInt;
        }

        // Set a random length.
        corridorLength = length.Random;

        // Create a cap for how long the length can be (this will be changed based on the direction and position).
        int maxLength = length.m_Max;

        switch (direction)
        {
            // If the choosen direction is NORTH (up)...
            case Direction.NORTH:
                // ... the starting position in the x axis can be random but within the width of the room.
                startXPos = Random.Range(room.xPos, room.xPos + room.roomWidth - 1);

                // The starting position in the y axis must be the top of the room.
                startYPos = room.yPos + room.roomHeight;

                // The maximum length the corridor can be is the height of the board (rows) but from the top of the room (y pos + height).
                maxLength = rows - startYPos - roomHeight.m_Min;
                break;
            case Direction.EAST:
                startXPos = room.xPos + room.roomWidth;
                startYPos = Random.Range(room.yPos, room.yPos + room.roomHeight - 1);
                maxLength = columns - startXPos - roomWidth.m_Min;
                break;
            case Direction.SOUTH:
                startXPos = Random.Range(room.xPos, room.xPos + room.roomWidth);
                startYPos = room.yPos;
                maxLength = startYPos - roomHeight.m_Min;
                break;
            case Direction.WEST:
                startXPos = room.xPos;
                startYPos = Random.Range(room.yPos, room.yPos + room.roomHeight);
                maxLength = startXPos - roomWidth.m_Min;
                break;
        }

        // We clamp the length of the corridor to make sure it doesn't go off the board.
        corridorLength = Mathf.Clamp(corridorLength, 1, maxLength);
        return (int)direction;
    }

    public void SetupCorridor(CRoom room, IntRange length, IntRange roomWidth, IntRange roomHeight, int columns, int rows, int _direction)
    {
        // Set a random direction (a random index from 0 to 3, cast to Direction).
        if (_direction >= 4)
            _direction = _direction % 4;

        direction = (Direction)_direction;

        // Set a random length.
        corridorLength = length.Random;

        // Create a cap for how long the length can be (this will be changed based on the direction and position).
        int maxLength = length.m_Max;

        switch (direction)
        {
            // If the choosen direction is NORTH (up)...
            case Direction.NORTH:
                // ... the starting position in the x axis can be random but within the width of the room.
                startXPos = Random.Range(room.xPos, room.xPos + room.roomWidth - 1);

                // The starting position in the y axis must be the top of the room.
                startYPos = room.yPos + room.roomHeight;

                // The maximum length the corridor can be is the height of the board (rows) but from the top of the room (y pos + height).
                maxLength = rows - startYPos - roomHeight.m_Min;
                break;
            case Direction.EAST:
                startXPos = room.xPos + room.roomWidth;
                startYPos = Random.Range(room.yPos, room.yPos + room.roomHeight - 1);
                maxLength = columns - startXPos - roomWidth.m_Min;
                break;
            case Direction.SOUTH:
                startXPos = Random.Range(room.xPos, room.xPos + room.roomWidth);
                startYPos = room.yPos;
                maxLength = startYPos - roomHeight.m_Min;
                break;
            case Direction.WEST:
                startXPos = room.xPos;
                startYPos = Random.Range(room.yPos, room.yPos + room.roomHeight);
                maxLength = startXPos - roomWidth.m_Min;
                break;
        }

        // We clamp the length of the corridor to make sure it doesn't go off the board.
        corridorLength = Mathf.Clamp(corridorLength, 1, maxLength);
    }
}


/******************
 * General Codes
 ******************/

public enum Direction
{
    NORTH, SOUTH, EAST, WEST, Size
}

[System.Serializable]
public class IntRange
{
    public int m_Min;       // The minimum value in this range.
    public int m_Max;       // The maximum value in this range.

    // Constructor to set the values.
    public IntRange(int min, int max)
    {
        m_Min = min;
        m_Max = max;
    }
    
    // Get a random value from the range.
    public int Random
    {
        get { return UnityEngine.Random.Range(m_Min, m_Max); }
    }
}

