﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBoardGenerator : MonoBehaviour
{
    public int numFloors = 100;
    public int columns = 100;                                 // The number of columns on the board (how wide it will be).
    public int rows = 100;                                    // The number of rows on the board (how tall it will be).

    private List<CLevel> DungeonLevels;

    public GameObject[] floorTiles;                           // An array of floor tile prefabs.
    public GameObject[] wallTiles;                            // An array of wall tile prefabs.
    public GameObject[] outerWallTiles;                       // An array of outer wall tile prefabs.

    public GameObject boardHolder;                           // GameObject that acts as a container for all other tiles.

    public void Init()
    {
        if (DungeonLevels.Count == 0)
        {
            CreateLevels(numFloors);
        }
        CreateBoard(/*get current floor*/ - 1);
    }

    public void CreateLevels(int _floors)
    {
        DungeonLevels = new List<CLevel>();

        for (int i = 0; i < _floors; ++i)
        {
            CLevel temp = new CLevel();
            //Init the level;
        }
    }

    public void CreateBoard(int _currentFloor)
    {
        if (boardHolder == null)
        {
            boardHolder = new GameObject("Level");
        }

        if (_currentFloor < 0) // Floor must be >= 0
            return;

        InstantiateTiles(DungeonLevels[_currentFloor].GetTiles());
        InstantiateOuterWalls(DungeonLevels[_currentFloor].columns, DungeonLevels[_currentFloor].rows);
    }

    void InstantiateTiles(TileType[][] _tiles)
    {
        // Go through all the tiles in the jagged array...
        for (int i = 0; i < _tiles.Length; i++)
        {
            for (int j = 0; j < _tiles[i].Length; j++)
            {
                // If the tile type is Wall...
                if (_tiles[i][j] == TileType.Wall)
                {
                    // ... instantiate a wall.
                    InstantiateFromArray(wallTiles, i, j);
                    continue;
                }
                // If not, Instantiate a floor
                InstantiateFromArray(floorTiles, i, j);
            }
        }
    }

    void InstantiateOuterWalls(int _levelColumns, int _levelRows)
    {
        // The outer walls are one unit left, right, up and down from the board.
        float leftEdgeX = -1f;
        float rightEdgeX = columns + 0f;
        float bottomEdgeY = -1f;
        float topEdgeY = rows + 0f;

        // Instantiate both vertical walls (one on each side).
        InstantiateVerticalOuterWall(leftEdgeX, bottomEdgeY, topEdgeY);
        InstantiateVerticalOuterWall(rightEdgeX, bottomEdgeY, topEdgeY);

        // Instantiate both horizontal walls, these are one in left and right from the outer walls.
        InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, bottomEdgeY);
        InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, topEdgeY);
    }

    void InstantiateVerticalOuterWall(float xCoord, float startingY, float endingY)
    {
        // Start the loop at the starting value for Y.
        float currentY = startingY;

        // While the value for Y is less than the end value...
        while (currentY <= endingY)
        {
            // ... instantiate an outer wall tile at the x coordinate and the current y coordinate.
            InstantiateFromArray(outerWallTiles, xCoord, currentY);

            currentY++;
        }
    }

    void InstantiateHorizontalOuterWall(float startingX, float endingX, float yCoord)
    {
        // Start the loop at the starting value for X.
        float currentX = startingX;

        // While the value for X is less than the end value...
        while (currentX <= endingX)
        {
            // ... instantiate an outer wall tile at the y coordinate and the current x coordinate.
            InstantiateFromArray(outerWallTiles, currentX, yCoord);

            currentX++;
        }
    }

    void InstantiateFromArray(GameObject[] prefabs, float xCoord, float yCoord)
    {
        // Create a random index for the array.
        int randomIndex = Random.Range(0, prefabs.Length);

        // The position to be instantiated at is based on the coordinates.
        Vector3 position = new Vector3(xCoord, yCoord, 0f);

        // Create an instance of the prefab from the random index of the array.
        GameObject tileInstance = Instantiate(prefabs[randomIndex], position, Quaternion.identity, boardHolder.transform) as GameObject;
    }

}
