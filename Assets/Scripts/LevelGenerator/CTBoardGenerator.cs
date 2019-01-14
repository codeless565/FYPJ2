using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTBoardGenerator : MonoBehaviour
{
    public bool isBossLevel = false;
    private int gridSize;
    private int columns;        // The number of columns on the board (how wide it will be).
    private int rows;           // The number of rows on the board (how tall it will be).

    private int rooms;
    public int roomWidth = 11;
    public int roomHeight = 11;
    public int corridorLength = 8;

    public int currFloor;

    public GameObject[] floorTiles;             // An array of floor tile prefabs.
    public GameObject[] wallTiles;              // An array of wall tile prefabs.
    public GameObject[] wallInnerCornerTiles;
    public GameObject[] wallOuterCornerTiles;
    public GameObject stairsDown;
    public GameObject stairsUp;
    public GameObject checkpoint;
    public GameObject exitPortal;

    public GameObject boardHolder;              // GameObject that acts as a container for all other tiles.

    public void Init()
    {
        currFloor = CTDungeon.Instance.currentFloor;

        if (currFloor < 0)
        {
            currFloor = 1;
            CTDungeon.Instance.currentFloor = currFloor;
        }

        rooms = Random.Range(6 + currFloor / 3, 6 + currFloor / 2);

        if (!isBossLevel)
        {
            gridSize = rooms / 2;
            if (gridSize * gridSize <= rooms)
                gridSize += gridSize;
            columns = gridSize * (roomWidth + corridorLength) + 2 * (roomWidth + corridorLength);
            rows = gridSize * (roomHeight + corridorLength) + 2 * (roomHeight + corridorLength);
        }
        else
        {
            gridSize = rooms;
            if (gridSize < 5)
                gridSize = 5;
            columns = gridSize * (roomWidth + corridorLength) + 2 * (roomWidth + corridorLength);
            rows = gridSize * (roomHeight + corridorLength) + 2 * (roomHeight + corridorLength);
        }

        CreateBoard(currFloor);
    }

    public void CreateBoard(int _currentFloor)
    {
        if (boardHolder == null)
        {
            boardHolder = new GameObject("GameFloor");
        }

        if (_currentFloor <= 0) // Floor must be > 0
            return;

        IFloor temp = CTDungeon.Instance.GetFloorData(_currentFloor, isBossLevel);

        if (!temp.Generated)
        {
            temp.Name = "Floor_" + currFloor;
            temp.InitNewLevel(currFloor, columns, rows, rooms, gridSize, roomWidth, roomHeight, corridorLength);
        }

        Debug.Log("CurrLevel is " + _currentFloor + " act " + CTDungeon.Instance.currentFloor);

        InstantiateTiles(temp.Tiles);
        InstantiateStairs(temp.StairsForward, temp.StairsBack);
        InstantiateCPExit(temp.Checkpoint, temp.ExitPortal);

        Debug.Log("Board Created");
    }
    
    void InstantiateStairs(Vector2 _StairsFoward, Vector2 _StairsBack)
    {
        if (_StairsFoward != Vector2.zero)
        {
            Instantiate(stairsDown, _StairsFoward, Quaternion.identity, boardHolder.transform);
        }

        if (_StairsBack != Vector2.zero)
        {
            Instantiate(stairsUp, _StairsBack, Quaternion.identity, boardHolder.transform);
        }
    }

    void InstantiateCPExit(Vector2 _checkpoint, Vector2 _exitPortal)
    {
        if (_checkpoint != Vector2.zero)
        {
            Instantiate(checkpoint, _checkpoint, Quaternion.identity, boardHolder.transform);
        }

        if (_exitPortal != Vector2.zero)
        {
            Instantiate(exitPortal, _exitPortal, Quaternion.identity, boardHolder.transform);
        }
    }

    void InstantiateTiles(TileType[][] _tiles)
    {
        // Go through all the tiles in the jagged array...
        for (int i = 0; i < _tiles.Length; i++)
        {
            for (int j = 0; j < _tiles[i].Length; j++)
            {
                /*
                 * NEEDS TESTING
                 */
                // Instantiate tile depending on the tileType
                switch(_tiles[i][j])
                {
                    case TileType.Wall_Up:
                        InstantiateFromArray(wallTiles[0], i, j);
                        break;
                    case TileType.Wall_Left:
                        InstantiateFromArray(wallTiles[1], i, j);
                        break;
                    case TileType.Wall_Down:
                        InstantiateFromArray(wallTiles[2], i, j);
                        break;
                    case TileType.Wall_Right:
                        InstantiateFromArray(wallTiles[3], i, j);
                        break;

                    case TileType.WallInnerCorner_Q1:
                        InstantiateFromArray(wallInnerCornerTiles[0], i, j);
                        break;
                    case TileType.WallInnerCorner_Q2:
                        InstantiateFromArray(wallInnerCornerTiles[1], i, j);
                        break;
                    case TileType.WallInnerCorner_Q3:
                        InstantiateFromArray(wallInnerCornerTiles[2], i, j);
                        break;
                    case TileType.WallInnerCorner_Q4:
                        InstantiateFromArray(wallInnerCornerTiles[3], i, j);
                        break;

                    case TileType.WallOuterCorner_Q1:
                        InstantiateFromArray(wallOuterCornerTiles[0], i, j);
                        break;
                    case TileType.WallOuterCorner_Q2:
                        InstantiateFromArray(wallOuterCornerTiles[1], i, j);
                        break;
                    case TileType.WallOuterCorner_Q3:
                        InstantiateFromArray(wallOuterCornerTiles[2], i, j);
                        break;
                    case TileType.WallOuterCorner_Q4:
                        InstantiateFromArray(wallOuterCornerTiles[3], i, j);
                        break;

                    case TileType.Floor:
                        InstantiateFromArray(floorTiles, i, j);
                        break;
                }
            }
        }
    }

    void InstantiateFromArray(GameObject[] _prefabs, float xCoord, float yCoord, float zRot = 0)
    {
        // Create a random index for the array.
        int randomIndex = Random.Range(0, _prefabs.Length);

        // The position to be instantiated at is based on the coordinates.
        Vector3 position = new Vector3(xCoord, yCoord, 0f);

        // Create an instance of the prefab from the random index of the array.
        GameObject tileInstance = Instantiate(_prefabs[randomIndex], position, Quaternion.Euler(0, 0, zRot), boardHolder.transform) as GameObject;
    }

    //find out how to rotate with Quaterion

    void InstantiateFromArray(GameObject _prefab, float xCoord, float yCoord, float zRot = 0)
    {
        // The position to be instantiated at is based on the coordinates.
        Vector3 position = new Vector3(xCoord, yCoord, 0f);

        // Create an instance of the prefab from the random index of the array.
        GameObject tileInstance = Instantiate(_prefab, position, Quaternion.Euler(0, 0, zRot), boardHolder.transform) as GameObject;
    }


}
