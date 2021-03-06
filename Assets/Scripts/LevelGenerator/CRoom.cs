﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRoom
{
    public int xPos;                    // The x coordinate of the lower left tile of the room.
    public int yPos;                    // The y coordinate of the lower left tile of the room.
    public int roomWidth;               // How many tiles wide the room is.
    public int roomHeight;              // How many tiles high the room is.
    public Direction Corridors;         // The direction of the corridor that is entering this room.
    public bool generated;

    public void SetupRoom(IntRange widthRange, IntRange heightRange, int columns, int rows)
    {
        generated = true;
        // Set a random width and height.
        roomWidth = widthRange.Random;
        roomHeight = heightRange.Random;

        // Set the x and y coordinates so the room is roughly in the middle of the board.
        float xStart = Random.Range(0.2f, 0.8f);
        float yStart = Random.Range(0.2f, 0.8f);

        xPos = Mathf.RoundToInt(columns * xStart - roomWidth * 0.5f);
        yPos = Mathf.RoundToInt(rows * yStart - roomHeight * 0.5f);
    }

    public void SetupRoom(IntRange widthRange, IntRange heightRange, int columns, int rows, CCorridor corridor)
    {
        // Set the entering corridor direction.
        Corridors = corridor.direction;
        corridor.connectedTo = true;
        generated = true;

        // Set random values for width and height.
        roomWidth = widthRange.Random;
        roomHeight = heightRange.Random;

        switch (corridor.direction)
        {
            // If the corridor entering this room is going north...
            case Direction.NORTH:
                // ... the height of the room mustn't go beyond the board so it must be clamped based
                // on the height of the board (rows) and the end of corridor that leads to the room.
                roomHeight = Mathf.Clamp(roomHeight, 1, rows - corridor.EndPositionY);

                // The y coordinate of the room must be at the end of the corridor (since the corridor leads to the bottom of the room).
                yPos = corridor.EndPositionY;

                // The x coordinate can be random but the left-most possibility is no further than the width
                // and the right-most possibility is that the end of the corridor is at the position of the room.
                xPos = Random.Range(corridor.EndPositionX - roomWidth + 1, corridor.EndPositionX);

                // This must be clamped to ensure that the room doesn't go off the board.
                xPos = Mathf.Clamp(xPos, 0, columns - roomWidth);
                break;
            case Direction.EAST:
                roomWidth = Mathf.Clamp(roomWidth, 1, columns - corridor.EndPositionX);
                xPos = corridor.EndPositionX;

                yPos = Random.Range(corridor.EndPositionY - roomHeight + 1, corridor.EndPositionY);
                yPos = Mathf.Clamp(yPos, 0, rows - roomHeight);
                break;
            case Direction.SOUTH:
                roomHeight = Mathf.Clamp(roomHeight, 1, corridor.EndPositionY);
                yPos = corridor.EndPositionY - roomHeight + 2;

                xPos = Random.Range(corridor.EndPositionX - roomWidth + 1, corridor.EndPositionX);
                xPos = Mathf.Clamp(xPos, 0, columns - roomWidth);
                break;
            case Direction.WEST:
                roomWidth = Mathf.Clamp(roomWidth, 1, corridor.EndPositionX);
                xPos = corridor.EndPositionX - roomWidth + 1;

                yPos = Random.Range(corridor.EndPositionY - roomHeight + 1, corridor.EndPositionY);
                yPos = Mathf.Clamp(yPos, 0, rows - roomHeight);
                break;
        }
    }
}